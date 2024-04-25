using System;
using System.Linq;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace Map
{
    public class MapPlayerTracker : MonoBehaviour
    {
        public bool lockAfterSelecting = false;
        public float enterNodeDelay = 1f;
        public MapManager mapManager;
        public MapView view;

        public static MapPlayerTracker Instance;

        public bool Locked { get; set; }

        private void Awake()
        {
            Instance = this;
        }

        public void SelectNode(MapNode mapNode)
        {
            if (Locked) return;

            // Debug.Log("Selected node: " + mapNode.Node.point);

            if (mapManager.CurrentMap.path.Count == 0)
            {
                // player has not selected the node yet, he can select any of the nodes with y = 0
                if (mapNode.Node.point.y == 0)
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
            else
            {
                var currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
                var currentNode = mapManager.CurrentMap.GetNode(currentPoint);

                if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
        }
        /// <summary>
        /// This method is called when the player selects a node.
        /// </summary>
        /// <param name="mapNode"></param>
        private void SendPlayerToNode(MapNode mapNode)
        {
            //mapManager.SaveMap();没啥用
            MapManager.Instance.OnExitMap();//提前保存数据
            Locked = lockAfterSelecting;
            mapManager.CurrentMap.path.Add(mapNode.Node.point);
            GameManager.Instance.currentMap = mapManager.CurrentMap.ToJson();
            view.SetAttainableNodes();
            view.SetLineColors();
            mapNode.ShowSwirlAnimation();
            
            DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(mapNode));
        }
        /// <summary>
        /// This method is called when the player enters a node.
        /// 这边是玩家进入节点的时候调用的方法
        /// 可以在这边加载对应的场景，或者显示对应的GUI
        /// 进行扩展
        /// </summary>
        /// <param name="mapNode"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private static void EnterNode(MapNode mapNode)
        {
            // we have access to blueprint name here as well
            Debug.Log("Entering node: " + mapNode.Node.blueprintName + " of type: " + mapNode.Node.nodeType);
            // load appropriate scene with context based on nodeType:
            // or show appropriate GUI over the map: 
            // if you choose to show GUI in some of these cases, do not forget to set "Locked" in MapPlayerTracker back to false
            switch (mapNode.Node.nodeType)
            {
                case NodeType.EasyEnemy:
                    EnterBattleNode(mapNode.transform, EnemyType.Easy);
                    break;
                case NodeType.NormalEnemy:
                    EnterBattleNode(mapNode.transform, EnemyType.Normal);
                    break;
                case NodeType.HardEnemy:
                    EnterBattleNode(mapNode.transform, EnemyType.Hard);
                    break;
                case NodeType.Store:
                    MapManager.Instance.OnEnterShop();
                    StoreManager.Instance.OnEnterStore?.Invoke();
                    break;
                case NodeType.Boss:
                    EnterBattleNode(mapNode.transform, EnemyType.Boss);
                    break;
                case NodeType.Mystery:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        /// <summary>
        /// 战斗界面的封装
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="enemyType"></param>
        public static void EnterBattleNode(Transform transform,EnemyType enemyType)
        {
            Vector2 cameraPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 viewPointView = Camera.main.ScreenToViewportPoint(cameraPosition);
            MapManager.Instance.playerChaState.RefreshRerollTimes();
            GameManager.Instance.enemyDataSO = EnemyManager.GetEnemyDataSOviaCondition(enemyType, MapManager.Instance.playerDataSO.playerRoomData.enemyIDs);
            Debug.Log("Enter Battle Node: " + GameManager.Instance.enemyDataSO.enemyType.ToString());
            MapManager.Instance.playerDataSO.ifUseSaveData = true;
            

            SceneLoader.Instance.LoadSceneAsync(GameScene.BattleScene, viewPointView);
        }
        private void PlayWarningThatNodeCannotBeAccessed()
        {
            Debug.Log("Selected node cannot be accessed");
        }
    }
}
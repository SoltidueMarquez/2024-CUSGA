using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using UI;

namespace Map
{

    public class MapManager : MonoBehaviour
    {
        [Header("地图配置")]
        public MapConfig config;
        public MapView view;
        [Header("玩家信息")]
        public PlayerDataSO playerDataSO;
        public ChaState playerChaState;
        public Map CurrentMap { get; private set; }

        private void Start()
        {
            //玩家信息初始化
            InitializePlayer();
            if (PlayerPrefs.HasKey("Map"))
            {
                var mapJson = PlayerPrefs.GetString("Map");
                var map = JsonConvert.DeserializeObject<Map>(mapJson);
                // using this instead of .Contains()
                if (map.path.Any(p => p.Equals(map.GetBossNode().point)))
                {
                    // payer has already reached the boss, generate a new map
                    GenerateNewMap();
                }
                else
                {
                    CurrentMap = map;
                    // player has not reached the boss yet, load the current map
                    view.ShowMap(map);
                }
            }
            else
            {
                GenerateNewMap();
            }
        }

        public void GenerateNewMap()
        {
            var map = MapGenerator.GetMap(config);
            CurrentMap = map;
            Debug.Log(map.ToJson());
            view.ShowMap(map);
        }

        public void SaveMap()
        {
            if (CurrentMap == null) return;

            var json = JsonConvert.SerializeObject(CurrentMap, Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            PlayerPrefs.SetString("Map", json);
            PlayerPrefs.Save();
        }

        private void OnApplicationQuit()
        {
            SaveMap();
        }
        #region Player相关
        /// <summary>
        /// 玩家数据的初始化
        /// </summary>
        public void InitializePlayer()
        {

            //初始化玩家的基础数值
            this.playerChaState.SetBaseprop(playerDataSO.baseProp);
            //对圣物设置玩家的基础数值
            HalidomManager.Instance.baseProp = playerDataSO.baseProp;
            //初始化圣物给的数值
            HalidomManager.Instance.RefreshAllHalidoms();
            this.playerChaState.Initialize();

            //初始化玩家的骰面
            if (!playerDataSO.ifUseSaveData)
            {
                //初始化玩家的骰子
                var playerDiceSOItems = playerDataSO.playerDiceSOItems;
                //设置玩家骰子的数量
                int battleDiceCount = playerDiceSOItems.Count;
                this.playerChaState.GetBattleDiceHandler().battleDiceCount = battleDiceCount;
                this.playerChaState.GetBattleDiceHandler().maxDiceInBag = playerDataSO.maxBagDiceCount;
                this.playerChaState.GetBattleDiceHandler().InitDice(playerDiceSOItems);
                this.playerChaState.GetBattleDiceHandler().InitBagDiceWithoutData(playerDataSO.bagDiceSOList);
            }
            else
            {
                //获取玩家存档信息中所有的保存的信息
                var battleDiceSODatas = playerDataSO.battleDiceList;
                //设置玩家骰子的数量
                int battleDiceCount = battleDiceSODatas.Count;
                this.playerChaState.GetBattleDiceHandler().battleDiceCount = battleDiceCount;
                this.playerChaState.GetBattleDiceHandler().maxDiceInBag = playerDataSO.maxBagDiceCount;
                //根据存档进行骰子的数值初始化
                this.playerChaState.GetBattleDiceHandler().InitDiceWithData(battleDiceSODatas);
                this.playerChaState.resource = playerDataSO.chaResource;
                this.playerChaState.GetBattleDiceHandler().InitBagDiceWithData(playerDataSO.bagDiceList);
            }
            //this.playerChaState.GetBattleDiceHandler().InitBattleDiceUI();
            //初始化玩家的背包骰面
            //this.playerChaState.GetBattleDiceHandler().InitBagDiceUI(SellSingleDice);
            
        }
        #endregion
        #region 骰子交互相关
        public void SellSingleDice(SingleDiceObj singleDiceObj)
        {
            this.playerChaState.GetBattleDiceHandler().RemoveSingleBattleDiceFromBag(singleDiceObj);
            var resource = new ChaResource(0, singleDiceObj.model.value, 0, 0);
            this.playerChaState.ModResources(resource);
            //进行一些其他判定
        }
        #endregion
    }
}
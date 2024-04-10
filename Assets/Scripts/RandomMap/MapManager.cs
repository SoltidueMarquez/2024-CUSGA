using System.Linq;
using UnityEngine;
using Newtonsoft.Json;

namespace Map
{
    public class MapManager : MonoBehaviour
    {
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
                new JsonSerializerSettings {ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
            PlayerPrefs.SetString("Map", json);
            PlayerPrefs.Save();
        }

        private void OnApplicationQuit()
        {
            SaveMap();
        }
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

            }
        }
    }
}

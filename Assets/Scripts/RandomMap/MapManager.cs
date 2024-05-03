using System.Linq;
using UnityEngine;
using Newtonsoft.Json;
using UI;
using System.Collections.Generic;
using System;
using DesignerScripts;
using UI.Tutorial;

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
        public static MapManager Instance;
        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            this.playerDataSO = GameManager.Instance.playerDataSO;
            //地图场景玩家信息初始化
            InitializePlayer();
            //地图场景圣物初始化
            InitializeHalidom();
            //地图场景地图初始化
            InitializeMap();
            if (GameManager.Instance.ifTutorial)
            {
                if (GameManager.Instance.battleTurtorial)
                {
                    if (!GameManager.Instance.mapTurtorial)
                    {
                        TutorialManager.Instance.EnterUI(TutorPage.Map);
                        GameManager.Instance.mapTurtorial = true;
                    }
                }
                //一些其他东西的初始化
                
            }
        }

            public void GenerateNewMap()
            {
                var map = MapGenerator.GetMap(config);
                CurrentMap = map;
                Debug.Log(map.ToJson());
                view.ShowMap(map);
                this.playerDataSO.ifHasMap = true;
                this.playerDataSO.ifHasData = true;
                //和地图相关的数据
                this.playerDataSO.playerRoomData = new();
                string mapString = map.ToJson();
                this.playerDataSO.UpdataPlayerDataSoMap(mapString);
                this.playerDataSO.UpdatePlayerDataSO(this.playerChaState);
                this.playerDataSO.SaveData();
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
            #region 初始化相关
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
                    ChaProperty chaProperty = playerDataSO.baseProp;
                    ChaResource resource = playerDataSO.chaResource - new ChaResource(chaProperty.health, chaProperty.money, chaProperty.maxRollTimes, 0);
                    this.playerChaState.ModResources(resource);
                    this.playerChaState.GetBattleDiceHandler().InitBagDiceWithData(playerDataSO.bagDiceList);
                }


                List<LogicDice> logicDicelist = new List<LogicDice>();
                //获取logicDiceList
                for (int i = 0; i < this.playerChaState.GetBattleDiceHandler().battleDiceCount; i++)
                {
                    var actionList = new List<Action<SingleDiceObj>>();
                    var singleDiceObjs = this.playerChaState.GetBattleDiceHandler().battleDices[i].GetBattleDiceSingleDices();
                    var logicDice = new LogicDice();
                    logicDice.singleDiceList = singleDiceObjs;
                    logicDice.index = i;
                    for (int j = 0; j < singleDiceObjs.Count; j++)
                    {
                        var singleDiceObj = singleDiceObjs[j];
                        Action<SingleDiceObj> action = SellSingleDice;
                        actionList.Add(action);
                    }
                    logicDice.removeList = actionList;
                    logicDicelist.Add(logicDice);
                }
                //获取背包的logicDice
                var currentBagDiceList = this.playerChaState.GetBattleDiceHandler().bagDiceCards;
                Debug.Log("<color=red>MapManager:" + currentBagDiceList.Count);
                var bagLogicDice = new LogicDice();
                bagLogicDice.singleDiceList = currentBagDiceList;
                bagLogicDice.index = 0;
                bagLogicDice.removeList = new List<Action<SingleDiceObj>>();
                for (int i = 0; i < currentBagDiceList.Count; i++)
                {
                    var singleDiceObj = currentBagDiceList[i];
                    Action<SingleDiceObj> action = SellSingleDice;
                    bagLogicDice.removeList.Add(action);
                }
                EditableDiceUIManager.Instance.Init(logicDicelist, bagLogicDice);
                UpdatePlayerUI();



            }
            /// <summary>
            /// 初始化圣物
            /// </summary>
            public void InitializeHalidom()
            {
                //如果1ifUseSaveData为true，那么就是使用存档数据，对宇圣物来说，就是存在圣物manager中的list,因为圣物manager DontDestroyOnLoad
                if (playerDataSO.ifUseSaveData)
                {
                    HalidomManager.Instance.InitHalidomUI(GameScene.MapScene);
                    HalidomManager.Instance.InitHalidomInMapScene();
                }
                else
                {
                    //如果ifUseSaveData为false，那么就是使用初始数据，对于圣物来说，就是存在playerDataSO中的list
                    //遍历halidomSO列表
                    foreach (var halidom in playerDataSO.halidomSOs)
                    {
                        //找halidom字典里是否有这个键
                        if (DataInitManager.Instance.halidomDataTable.halidomDictionary.ContainsKey(halidom.halidomName.ToString()))
                        {
                            HalidomManager.Instance.AddHalidomInMap(DataInitManager.Instance.halidomDataTable.halidomDictionary[halidom.halidomName.ToString()]);
                        }
                    }
                    GameManager.Instance.ifLoadedHalidom = true;
                }
            }
            /// <summary>
            /// 根据不同的情况初始化地图
            /// </summary>
            public void InitializeMap()
            {
                if (this.playerDataSO.ifUseSaveData)//使用保存的数据
                {
                    if (this.playerDataSO.ifHasMap)//playerDataSO中有地图数据
                    {
                        this.CurrentMap = JsonConvert.DeserializeObject<Map>(this.playerDataSO.currentMap);
                        this.view.ShowMap(CurrentMap);
                    }
                    else
                    {
                        GenerateNewMap();//创建新地图并且存入把所有的数据存入PlayerData

                    }
                }
                else//使用初始数据
                {
                    GenerateNewMap();
                }

            }
            #endregion
            #region 骰子交互相关
            public void SellSingleDice(SingleDiceObj singleDiceObj)
            {
                Debug.Log("<color=green>MapManager</color>:" + singleDiceObj.idInDice);
                this.playerChaState.GetBattleDiceHandler().RemoveSingleBattleDiceFromBag(singleDiceObj);
                var resource = new ChaResource(0, singleDiceObj.SaleValue, 0, 0);
                this.playerChaState.ModResources(resource);
                //进行一些其他判定
            }
            #endregion
            #region 跨场景
            public void OnExitMap()
            {
                this.playerDataSO.UpdatePlayerDataSO(this.playerChaState);
                this.playerDataSO.UpdataPlayerDataSoMap(CurrentMap.ToJson());
                this.playerDataSO.SaveData();
            }
            public void OnEnterShop()
            {
                if (GameManager.Instance.ifTutorial)
                {
                    if (!GameManager.Instance.storeTurtorial)
                    {
                        TutorialManager.Instance.EnterUI(TutorPage.Store);
                        GameManager.Instance.storeTurtorial = true;
                    }
                }
            StoreManager.Instance.SetPlayer(this.playerChaState);
                playerDataSO.playerRoomData.roomNums++;
                playerChaState.RefreshRerollTimes();
                UpdatePlayerUI();
            }
            public void OnExitShop()
            {
                Debug.Log("OnExitShop");
                playerChaState.RefreshRerollTimes();
                this.playerDataSO.UpdatePlayerDataSO(this.playerChaState);
                this.playerDataSO.UpdataPlayerDataSoMap(CurrentMap.ToJson());
                UpdatePlayerUI();
                this.playerDataSO.SaveData();
                MapPlayerTracker.Instance.Locked = false;
                view.SetAttainableNodes();
            }
            #endregion
            #region 更新玩家UI信息
            public void UpdatePlayerUI()
            {
                DataUIManager.Instance.UpdateRerollText(playerChaState.resource.currentRollTimes, playerChaState.prop.maxRollTimes);
                DataUIManager.Instance.UpdateHealthText(playerChaState.resource.currentHp, playerChaState.prop.health);
            }
            #endregion
        }
    }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    //玩家当前的资源
    public ChaResource chaResource = new();
    //玩家当前的战斗骰子
    public List<BattleDiceSOData> battleDiceList = new();
    //玩家当前的圣物
    public List<HalidomDataForSave> halidomDataForSaves =new();
    //玩家当前在背包中的骰面
    public List<SingleDiceObjSOData> bagDiceList = new();
    public bool ifHasSaveData;
    //玩家的Map信息,因为unity的json无法序列化map，所以这里用string代替
    public string map;
    public PlayerRoomData playerRoomData = new();
    public PlayerData(PlayerDataSO playerDataSO)
    {
        chaResource = playerDataSO.chaResource;
        battleDiceList = playerDataSO.battleDiceList;
        halidomDataForSaves = playerDataSO.halidomDataForSaves;
        bagDiceList = playerDataSO.bagDiceList;
        map = playerDataSO.currentMap;
        ifHasSaveData = playerDataSO.ifHasData;
        playerRoomData = playerDataSO.playerRoomData;
    }
    public PlayerData()
    {
    }
}

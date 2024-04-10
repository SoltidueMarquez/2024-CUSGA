using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    //玩家当前的资源
    public ChaResource chaResource;
    //玩家当前的战斗骰子
    public List<BattleDiceSOData> battleDiceList;
    //玩家当前的圣物
    public List<HalidomDataForSave> halidomDataForSaves;
    //玩家当前在背包中的骰面
    public List<SingleDiceObjSOData> bagDiceList;

    public PlayerData(PlayerDataSO playerDataSO)
    {
        chaResource = playerDataSO.chaResource;
        battleDiceList = playerDataSO.battleDiceList;
        halidomDataForSaves = playerDataSO.halidomDataForSaves;
        bagDiceList = playerDataSO.bagDiceList;
    }
}

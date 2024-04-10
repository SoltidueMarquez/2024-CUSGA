
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using DesignerScripts;
/// <summary>
/// 在一开始初始化的时候，玩家的骰子类型和骰子的模型是固定的，这个类用于存储玩家的骰子类型和骰子的模型
/// </summary>

[Serializable]
public class DiceSOItem//这个类是初始化的时候用的，用于存储玩家的骰子类型和骰子的模型
{
    [Header("骰子类型")]
    public DiceType diceType;
    [Header("骰子的模型列表")]
    public List<SingleDiceModelSO> singleDiceModelSOs;

}
[Serializable]
public class BattleDiceSOData//这个类是保存的数据
{
    public DiceType diceType;
    [Header("一个战斗骰子拥有的骰面数据")]
    public List<SingleDiceObjSOData> singleDiceObjSODatas;
}
[Serializable]
public class SingleDiceObjSOData
{
    [Header("骰子model的id")]
    public string id;
    [Header("骰子的点数,可以无限叠加")]
    public int idInDice;
    [Header("骰子的等级")]
    public int level;
    [Header("骰子的售价")]
    public int value;
}
[Serializable]
public class HalidomDataForSave
{
    //圣物在字典中的key
    public string halidomName;
    public int halidomIndex;
}

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Data/PlayerDataSO", order = 1)]
/// <summary>
/// 在游戏过程中，玩家的数据会发生变化，这个类用于存储玩家的数据，是playerData上层的数据
/// </summary>
public class PlayerDataSO : ScriptableObject
{
    [Header("是否存档读取")]
    public bool ifUseSaveData;
    [Header("玩家初始的数值")]
    public ChaProperty baseProp;
    [Header("玩家初始的圣物")]
    public List<HalidomDataSO> halidomSOData;
    [Header("玩家初始时候的骰子类型列表")]
    public List<DiceSOItem> playerDiceSOItems;
    [Header("玩家初始时候的背包骰面上限")]
    public int maxBagDiceCount;
    [Header("玩家身上的圣物")]
    public List<HalidomDataForSave> halidomDataForSaves;
    [Header("玩家当前的骰子(保存的数据)")]
    //玩家身上的战斗骰子列表
    public List<BattleDiceSOData> battleDiceList;
    [Header("玩家当前身上背包的骰面")]
    public List<SingleDiceObjSOData> bagDiceList;
    [Header("玩家当前的资源(保存的数据)")]
    public ChaResource chaResource;
    /// <summary>
    /// 从json文件中读取数据
    /// </summary>
    public void LoadData()
    {
        string playerDataJson = SImpleJsonUtil.ReadData("PlayerData.json");
        PlayerData playerData = JsonConvert.DeserializeObject<PlayerData>(playerDataJson);
        this.chaResource = playerData.chaResource;
        this.battleDiceList = playerData.battleDiceList;
        this.halidomDataForSaves = playerData.halidomDataForSaves;
    }
    public void SaveData()
    {
        if (HalidomManager.Instance != null)
        {
            halidomDataForSaves = HalidomManager.Instance.GetHalidomDataForSaves();
        }
        PlayerData playerData = new PlayerData(this);
        string playerDataJson = JsonConvert.SerializeObject(playerData);
        SImpleJsonUtil.WriteData("PlayerData.json", playerDataJson);
    }

    public void UpdatePlayerDataSO(ChaState chaState)
    {
        chaResource = chaState.resource;
        this.battleDiceList = chaState.GetBattleDiceHandler().GetBattleDiceSOData();
        this.bagDiceList = chaState.GetBattleDiceHandler().GetSingleDiceObjSODatas();

    }
}

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 在一开始初始化的时候，玩家的骰子类型和骰子的模型是固定的，这个类用于存储玩家的骰子类型和骰子的模型
/// </summary>
[Serializable]
public class DiceSOItem
{
    [Header("骰子类型")]
    public DiceType diceType;
    [Header("骰子的模型列表")]
    public List<SingleDiceModelSO> singleDiceModelSOs;

}
[Serializable]
public class BattleDiceSOData
{
    public DiceType diceType;
    [Header("一个战斗骰子拥有的骰面数据")]
    public List<SingleDiceObjSOData> singleDiceObjSOData;
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
    [Header("骰子在骰子中的位置")]
    public int positionInDice;
}


[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Data/PlayerDataSO", order = 1)]
/// <summary>
/// 在游戏过程中，玩家的数据会发生变化，这个类用于存储玩家的数据，是playerData上层的数据
/// </summary>
public class PlayerDataSO : ScriptableObject
{
    [Header("玩家初始的数值")]
    public ChaProperty baseProp;
    [Header("玩家初始时候的骰子类型列表")]
    public List<DiceSOItem> playerDiceSOItems;
    [Header("玩家当前的骰子(保存的数据)")]
    //玩家身上的战斗骰子列表
    public List<BattleDiceSOData> BattleDiceList;
    [Header("玩家当前的资源(保存的数据)")]
    public ChaResource chaResource;
    

}

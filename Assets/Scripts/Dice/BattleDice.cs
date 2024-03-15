using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单个战斗骰子的runtime数据
/// </summary>
public class BattleDice
{
    /// <summary>
    /// BattleDice的骰面
    /// </summary>
    List<SingleDiceObj> diceObjs = new List<SingleDiceObj>();
    /// <summary>
    /// 该战斗骰子的种类，用于区分不同的战斗骰子
    /// </summary>
    public DiceType diceType;

    public BattleDice(DiceType diceType)
    {
        this.diceType = diceType;
    }
    /// <summary>
    /// 初始化单个骰子的时候，需要传入单个骰子的数据
    /// </summary>
    /// <param name="singleDiceModel"></param>
    public void AddDice(SingleDiceModel singleDiceModel, int idInDice)
    {
        SingleDiceObj singleDiceObj = new SingleDiceObj(singleDiceModel, idInDice);
        diceObjs.Add(singleDiceObj);
    }


    
}
/// <summary>
/// 战斗骰子的种类
/// </summary>
public enum DiceType
{
    Attack,
    Defense,
    Support
}

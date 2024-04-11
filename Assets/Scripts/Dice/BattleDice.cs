using System;
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
    /// <summary>
    /// 在骰子列表中的序号
    /// </summary>
    public int diceIndexInList;

    public BattleDice(DiceType diceType)
    {
        this.diceType = diceType;
    }
    /// <summary>
    /// 初始化单个骰子的时候，需要传入单个骰子的数据
    /// </summary>
    /// <param name="idInDice">点数，可能会叠加</param>
    /// <param name="singleDiceModel"></param>
    public void AddDice(SingleDiceModel singleDiceModel, int idInDice,int diceIndexInList,int positionInDice)
    {
        //TODO:如果有超出上界怎么办
        SingleDiceObj singleDiceObj = new SingleDiceObj(singleDiceModel, idInDice);
        singleDiceObj.positionInDice = positionInDice;
        this.diceIndexInList = diceIndexInList;
        diceObjs.Add(singleDiceObj);
    }
    /// <summary>
    /// 获得随机的一个骰面
    /// </summary>
    /// <param name="singleDiceObj"></param>
    /// <returns>当前骰面在骰子中的位置</returns>
    public int GetRandomDice(out SingleDiceObj singleDiceObj)
    {
        //TODO:这边的随机数生成有问题,应该是根据权重来生成
        int randomIndex = UnityEngine.Random.Range(0, diceObjs.Count);
        singleDiceObj = diceObjs[randomIndex];
        return randomIndex;
    }
    public List<SingleDiceObj> GetBattleDiceSingleDices()
    {
        return diceObjs;
    }
}
/// <summary>
/// 战斗骰子的种类
/// </summary>
public enum DiceType
{
    Attack,
    Defense,
    Support,
    Special
}

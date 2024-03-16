using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家和敌人身上挂载的战斗骰子管理器
/// </summary>
public class BattleDiceHandler : MonoBehaviour
{

    /// <summary>
    /// 玩家和敌人身上的战斗骰子
    /// </summary>
    public List<BattleDice> battleDices = new List<BattleDice>();
    /// <summary>
    /// 玩家和敌人身上的战斗骰子数量
    /// </summary>
    public int battleDiceCount = 2;
    /// <summary>
    /// 玩家和敌人身上的背包骰面
    /// </summary>
    public List<SingleDiceObj> bagDiceCards = new List<SingleDiceObj>();
    /// <summary>
    /// 背包中最多能放的骰面数量
    /// </summary>
    public int maxDiceInBag = 8;
    /// <summary>
    /// 玩家和敌人身上能使用的骰面
    /// </summary>
    public List<SingleDiceObj> diceCardsInUse = new List<SingleDiceObj>();
    /// <summary>
    /// 数据上的交换两个骰面
    /// </summary>
    /// <param name="indexInBag">骰面在背包中的下标</param>
    /// <param name="indexInBattle">骰面在战斗中的下标</param>
    public void ChangeDiceInBattle(int indexInBag, int indexInBattle)
    {
        SingleDiceObj temp = bagDiceCards[indexInBattle];
        bagDiceCards[indexInBattle] = bagDiceCards[indexInBag];
        bagDiceCards[indexInBag] = temp;
    }
    /// <summary>
    /// 清楚战斗中的骰面
    /// </summary>
    public void ClearDiceInBattle()
    {
        bagDiceCards.Clear();
    }
    /// <summary>
    /// 释放单个骰子
    /// </summary>
    public void CastSingleDice(int index,ChaState chaState)//这边的函数
    {   SingleDiceObj singleDiceObj = diceCardsInUse[index];
        if(chaState.resource.Enough(singleDiceObj.model.condition) == true)
        {
            //减少资源
            chaState.ModResources(-1 * singleDiceObj.model.cost);
            //造成伤害
            Damage damage = singleDiceObj.model.damage;
            //DamageInfo damageInfo = new DamageInfo(chaState.gameObject,damage);
            //视觉逻辑
            diceCardsInUse.Remove(singleDiceObj);
        }
        else
        {
            Debug.Log("资源不足");
        }
        //先判断资源够不够使用
    }
    /// <summary>
    /// 释放所有的骰面
    /// </summary>
    public void CastDiceAll(ChaState chaState)
    {
        for(int i = 0;i<diceCardsInUse.Count;i++)
        {
            CastSingleDice(i,chaState);
        }
    }

    public void ClearBattleSingleDices()
    {
        diceCardsInUse.Clear();
    }
    /// <summary>
    /// 没有存档的情况下，默认初始化骰子
    /// </summary>
    public void InitDice()
    {

    }
    /// <summary>
    /// 有存档的情况下，初始化骰子
    /// </summary>
    public void InitDiceWithData()
    {

    }
}

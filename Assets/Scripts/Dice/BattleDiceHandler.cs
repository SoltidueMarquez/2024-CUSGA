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
    //public void ChangeDiceInBattle(int indexInBag, int indexInBattle)
    //{
    //    SingleDiceObj temp = bagDiceCards[indexInBattle];
    //    bagDiceCards[indexInBattle] = bagDiceCards[indexInBag];
    //    bagDiceCards[indexInBag] = temp;
    //}
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
    public void CastSingleDice(int index, ChaState chaState, GameObject target)//这边的函数
    {
        SingleDiceObj singleDiceObj = diceCardsInUse[index];
        if (chaState.resource.Enough(singleDiceObj.model.condition) == true)
        {
            //减少资源
            chaState.ModResources(-1 * singleDiceObj.model.cost);
            //造成伤害
            Damage damage = singleDiceObj.model.damage;
            damage.indexDamage = singleDiceObj.idInDice;//将index的值赋值给index
            DamageManager.Instance.DoDamage(chaState.gameObject, target, damage,false);
            //视觉逻辑
            foreach(var buffinfo in singleDiceObj.model.buffInfos)
            {
                Debug.Log("success");
                chaState.AddBuff(buffinfo,chaState.gameObject);
            }
            diceCardsInUse.Remove(singleDiceObj);
            //添加技能特殊效果Buff
            
        }
        else
        {
            Debug.Log("资源不足");
        }
        //先判断资源够不够使用
        Debug.Log("释放单个骰子");
    }

    /// <summary>
    /// 释放所有的骰面
    /// </summary>
    public void CastDiceAll(ChaState chaState, GameObject target)
    {
        for (int i = 0; i < diceCardsInUse.Count; i++)
        {
            CastSingleDice(i, chaState, target);
        }
    }

    public void ClearBattleSingleDices()
    {
        diceCardsInUse.Clear();
    }
    /// <summary>
    /// 没有存档的情况下，默认初始化骰子,应该是在一开始去加载？？，暂时先算战斗开始的时候加载,这边需要修改
    /// </summary>
    public void InitDice()
    {
        for (int i = 0; i < battleDiceCount; i++)
        {
            BattleDice battleDice = new BattleDice(DiceType.Attack);//这边需要修改
            battleDices.Add(battleDice);
            for (int j = 0; j < 6; j++)
            {
                SingleDiceModel singleDiceModel = RandomManager.Instance.GetSingleDiceModel(battleDices[i].diceType, 1); 
                battleDice.AddDice(singleDiceModel, j);
            }
        }
    }
    /// <summary>
    /// 有存档的情况下，初始化骰子
    /// </summary>
    public void InitDiceWithData()
    {

    }
    public void AddBattleSingleDice(List<SingleDiceObj> singleDiceObjs)
    {
        ClearBattleSingleDices();
        for (int i = 0; i < singleDiceObjs.Count; i++)
        {
            diceCardsInUse.Add(singleDiceObjs[i]);
        }
    }

    public List<SingleDiceObj> GetRandomSingleDices()
    {
        List<SingleDiceObj> singleDiceObjs = new List<SingleDiceObj>();
        for (int i = 0; i < battleDices.Count; i++)
        {
            int index = battleDices[i].GetRandomDice(out SingleDiceObj singleDiceObj);
            singleDiceObjs.Add(singleDiceObj);
        }
        return singleDiceObjs; 
    }
}
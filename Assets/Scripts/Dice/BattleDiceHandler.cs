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
    public SingleDiceObj[] diceCardsInUse;
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
            damage.indexDamageRate = singleDiceObj.idInDice + 1;//根据骰子的id来计算倍率
            DamageManager.Instance.DoDamage(chaState.gameObject, target, damage, singleDiceObj.model.type, singleDiceObj.level);
            //视觉逻辑
            Debug.Log(singleDiceObj.model.name);
            foreach (var buffinfo in singleDiceObj.model.buffInfos)
            {
                Debug.Log("success");
                chaState.AddBuff(buffinfo, chaState.gameObject);
            }
            diceCardsInUse[index] = null;
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
        for (int i = 0; i < diceCardsInUse.Length; i++)
        {
            CastSingleDice(i, chaState, target);
        }
    }
    //将战斗骰子每一个设置为空
    public void ClearBattleSingleDices()
    {
        for (int i = 0; i < diceCardsInUse.Length; i++)
        {
            diceCardsInUse[i] = null;
        }
    }
    /// <summary>
    /// 判断战斗骰面是否为空
    /// </summary>
    /// <returns></returns>
    public bool IfSingleBattleDiceEmpty()
    {
        bool result;
        for (int i = 0; i < diceCardsInUse.Length; i++)
        {
            if (diceCardsInUse[i] != null)
            {
                result = false;
                return result;
            }
        }
        return true;
    }
    /// <summary>
    /// 没有存档的情况下，默认初始化骰子,应该是在一开始去加载？？，暂时先算战斗开始的时候加载,这边需要修改
    /// </summary>
    public void InitDice(int side)
    {
        for (int i = 0; i < battleDiceCount; i++)
        {
            BattleDice battleDice = new BattleDice(DiceType.Attack);//这边需要修改
            battleDices.Add(battleDice);
            for (int j = 0; j < 6; j++)
            {
                SingleDiceModel singleDiceModel = RandomManager.Instance.GetSingleDiceModel(battleDices[i].diceType, 1, side);
                battleDice.AddDice(singleDiceModel, j);
            }
        }
        //初始化战斗时骰子的数组大小
        diceCardsInUse = new SingleDiceObj[battleDiceCount];
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
            diceCardsInUse[i] = singleDiceObjs[i];
        }
    }
    /// <summary>
    /// 获取随机的战斗骰子数量的骰面
    /// </summary>
    /// <returns>骰面的list</returns>
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

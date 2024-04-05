using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public int battleDiceCount = 3;
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
        if (singleDiceObj == null) return;//非空判断
        if (chaState.resource.Enough(singleDiceObj.model.condition) == true)
        {
            //减少资源
            chaState.ModResources(-1 * singleDiceObj.model.cost);
            //造成伤害
            Damage damage = singleDiceObj.model.damage;
            damage.indexDamageRate = singleDiceObj.idInDice + 1;//根据骰子的id来计算倍率
            //再次通过tag查找需要加入damageInfo类中的加给敌人的buffInfo
            List<BuffInfo> addToEnemyBuffs = null;
            if (singleDiceObj.model.buffInfos != null)
            {
                addToEnemyBuffs = FindBuffInfoWithTag(singleDiceObj.model.buffInfos.ToList<BuffInfo>(), "Target");
            }
            //拼装buffInfo
            var damageInfo = new DamageInfo(chaState.gameObject, target, damage, singleDiceObj.model.type, singleDiceObj.level, addToEnemyBuffs);
            DamageManager.Instance.DoDamage(damageInfo);
            //视觉逻辑
            Debug.Log(singleDiceObj.model.name);
            if (singleDiceObj.model.buffInfos != null)
            {
                //添加技能特殊效果Buff
                foreach (var buffinfo in singleDiceObj.model.buffInfos)
                {
                    BuffInfo temp = new BuffInfo(buffinfo.buffData, buffinfo.creator, buffinfo.target, buffinfo.curStack, buffinfo.isPermanent, buffinfo.buffParam);
                    //通过tag进行buff的查找，对施法者添加buff,如果包含self就添加给自己，如果包含target就添加给目标,具体添加给对方的函数走DamageManager中的DealWithDamage函数
                    if (buffinfo.buffData.tags.Contains("Self"))
                    {
                        chaState.AddBuff(temp, chaState.gameObject);
                    }
                }
            }
            //释放骰子
            diceCardsInUse[index] = null;

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
    
    #region 初始化战斗骰子，有数据的情况下和测试的情况下
    /// <summary>
    /// 没有存档的情况下，默认初始化骰子,应该是在一开始用其他数据结构去加载，暂时先算战斗开始的时候加载,这边需要修改
    /// </summary>
    public void InitDice(int side)
    {
        for (int i = 0; i < battleDiceCount; i++)
        {
            DiceType diceType = (DiceType)i;//这边需要修改
            BattleDice battleDice = new BattleDice(diceType);//这边需要修改
            battleDices.Add(battleDice);
            for (int j = 0; j < 6; j++)
            {
                SingleDiceModel singleDiceModel = RandomManager.Instance.GetSingleDiceModel(battleDices[i].diceType, 1, side);
                battleDice.AddDice(singleDiceModel, j, i);
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
    #endregion
    #region 添加骰面到各个地方，例如背包和战斗骰面
    /// <summary>
    /// 将单个战斗骰面加入当前的战斗骰面数组中
    /// </summary>
    /// <param name="singleDiceObjs"></param>
    public void AddBattleSingleDice(List<SingleDiceObj> singleDiceObjs)
    {
        ClearBattleSingleDices();
        for (int i = 0; i < singleDiceObjs.Count; i++)
        {
            diceCardsInUse[i] = singleDiceObjs[i];
        }
    }
    public void AddSingleBattleDiceToBag(SingleDiceObj singleDiceObj)
    {
        this.bagDiceCards.Add(singleDiceObj);
    }
    #endregion
    #region 随机数相关
    /// <summary>
    /// 获取随机的战斗骰子数量的骰面,主要用于生成战斗时用的骰面
    /// </summary>
    /// <returns>骰面的list</returns>
    public List<SingleDiceObj> GetRandomSingleDices()
    {
        List<SingleDiceObj> singleDiceObjs = new List<SingleDiceObj>();
        for (int i = 0; i < battleDices.Count; i++)
        {
            //TODO:这边其实可以重写一下
            int index = battleDices[i].GetRandomDice(out SingleDiceObj singleDiceObj);
            singleDiceObjs.Add(singleDiceObj);
        }
        return singleDiceObjs;
    }
    
    /// <summary>
    /// 根据index获取相应的战斗骰子，然后获取随机的骰面，用于重新投掷的时候根据现有的index投掷
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public SingleDiceObj GetRandomSingleDice(int index)
    {
        SingleDiceObj singleDiceObj;
        battleDices[index].GetRandomDice(out singleDiceObj);
        return singleDiceObj;
    }
    #endregion
    #region 一些辅助函数
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
    /// 根据tag查找buffInfo
    /// </summary>
    /// <param name="buffInfos"></param>
    /// <param name="tag"></param>
    /// <returns></returns>
    public List<BuffInfo> FindBuffInfoWithTag(List<BuffInfo> buffInfos, string tag)
    {
        if (buffInfos == null)
        {
            return null;
        }
        List<BuffInfo> temp = new List<BuffInfo>(buffInfos.Where(x => x.buffData.tags.Contains(tag)).ToList().ToArray());
        //深拷贝到新的list
        List<BuffInfo> result = new List<BuffInfo>();
        foreach (var item in temp)
        {
            BuffInfo a = new BuffInfo(item.buffData, item.creator, item.target, item.curStack, item.isPermanent, item.buffParam);
            result.Add(a);
        }
        return result;
    }
    #endregion
}

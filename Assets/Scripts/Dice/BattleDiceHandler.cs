using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using Cysharp.Threading.Tasks;
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
    public int maxDiceInBag = 1;
    /// <summary>
    /// 玩家和敌人身上能使用的骰面
    /// </summary>
    public SingleDiceObj[] diceCardsInUse;
    /// <summary>
    /// 用于查找上一个或者下一个
    /// </summary>
    private Stack<SingleDiceObj> previousSingleDices = new();

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
            //释放骰子
            if (singleDiceObj.model.buffInfos.Length > 0)
            {
                Debug.Log(singleDiceObj.model.buffInfos.Length);
                for (int i = 0; i < singleDiceObj.model.buffInfos.Length; i++)
                {
                    var item = singleDiceObj.model.buffInfos[i];
                    var result = item.buffData.OnCast?.Invoke(item, singleDiceObj);
                    singleDiceObj = result == null ? singleDiceObj : result;

                }
            }
            //添加进栈
            previousSingleDices.Push(new SingleDiceObj(singleDiceObj));
            diceCardsInUse[index] = null;

            //造成伤害
            Damage damage = singleDiceObj.model.damage;
            damage.indexDamageRate = singleDiceObj.idInDice;//根据骰子的id来计算倍率
            //再次通过tag查找需要加入damageInfo类中的加给敌人的buffInfo
            List<BuffInfo> addToEnemyBuffs = null;
            if (singleDiceObj.model.buffInfos != null)
            {
                addToEnemyBuffs = FindBuffInfoWithTag(singleDiceObj.model.buffInfos.ToList<BuffInfo>(), "Target");
            }
            //拼装buffInfo
            var damageInfo = new DamageInfo(chaState.gameObject, target, damage, singleDiceObj.model.type, singleDiceObj.level, addToEnemyBuffs);
            DamageManager.Instance.DoDamage(damageInfo);
            DamageManager.Instance.DealWithAllDamage();
            //视觉逻辑
            Debug.Log(singleDiceObj.model.name);
            if (singleDiceObj.model.buffInfos != null)
            {
                //添加技能特殊效果Buff
                foreach (var buffinfo in singleDiceObj.model.buffInfos)
                {
                    BuffInfo temp = new BuffInfo(buffinfo.buffData, buffinfo.creator, buffinfo.target, buffinfo.curStack, buffinfo.isPermanent, buffinfo.buffParam);
                    //Debug.Log("<color=red>BattlediceHandler:"+chaState.gameObject.name);
                    //通过tag进行buff的查找，对施法者添加buff,如果包含self就添加给自己，如果包含target就添加给目标,具体添加给对方的函数走DamageManager中的DealWithDamage函数
                    if (buffinfo.buffData.tags.Contains("Self"))
                    {
                        temp.target = chaState.gameObject;
                        chaState.AddBuff(temp, chaState.gameObject);
                    }
                }
            }
            //释放骰子


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
    public  void CastDiceAll(ChaState chaState, GameObject target)
    {
        //for (int i = 0; i < diceCardsInUse.Length; i++)
        //{
        //    CastSingleDice(i, chaState, target);
        //}
        CastDiceALLAsync(chaState, target);
    }
    //异步释放所有的骰面
    public async void CastDiceALLAsync(ChaState chastate,GameObject target)
    {
        for(int i = 0;i<diceCardsInUse.Length;i++)
        {
            CastSingleDice(i, chastate, target);
            //Debug.Log("释放第"+i+"个骰子");
            
            if (chastate.side == 0)
            {
                RollingResultUIManager.Instance.RemoveResultUI(i);
            }
            await UniTask.Delay(1000);
        }
    }
    
    #region 骰面交换
    public void SwapDiceInBagAndBattle(SingleDiceObj singleDiceObjInBag, SingleDiceObj singleDiceObjInBattle, int indexOfDices)
    {
        int indexInBag = bagDiceCards.IndexOf(singleDiceObjInBag);
        var singleDiceObjs = this.battleDices[indexOfDices].GetBattleDiceSingleDices();
        int indexInDice = singleDiceObjs.IndexOf(singleDiceObjInBattle);
        bagDiceCards[indexInBag] = singleDiceObjInBattle;
        singleDiceObjs[indexInDice] = singleDiceObjInBag;
    }
    /// <summary>
    /// 设置背包中的骰子的位置
    /// </summary>
    /// <param name="singleDiceObjs"></param>
    public void ResetDiceInBag(List<SingleDiceObj> singleDiceObjs)
    {
        for (int i = 0; i < singleDiceObjs.Count; i++)
        {
            bagDiceCards[i] = singleDiceObjs[i];
        }
    }
    #endregion
    #region 初始化战斗骰子，有数据的情况下和测试的情况下
    /// <summary>
    /// 没有存档的情况下，默认初始化骰子,以及敌人的骰子初始化都是这个函数
    /// </summary>
    public void InitDice(List<DiceSOItem> playerDiceSOItems)
    {
        for (int i = 0; i < battleDiceCount; i++)
        {
            var diceSOItem = playerDiceSOItems[i];
            DiceType diceType = diceSOItem.diceType;
            BattleDice battleDice = new BattleDice(diceType);
            battleDices.Add(battleDice);
            for (int j = 0; j < 6; j++)
            {
                //获取骰面在骰面字典中的key
                string dicKey = diceSOItem.singleDiceModelSOs[j].singleDiceModelName;
                SingleDiceModel singleDiceModel = SingleDiceData.diceDictionary[dicKey];
                //暂时的idinDice，也就是点数，是初始化的时候决定的
                battleDice.AddDice(singleDiceModel, j + 1, i, j);
            }
        }
        //初始化战斗时骰子的数组大小
        diceCardsInUse = new SingleDiceObj[battleDiceCount];
    }
    /// <summary>
    /// 有存档的情况下，初始化骰子
    /// </summary>
    public void InitDiceWithData(List<BattleDiceSOData> battleDiceSODatas)
    {
        for (int i = 0; i < battleDiceCount; i++)
        {
            var battleDiceSOData = battleDiceSODatas[i];
            DiceType diceType = battleDiceSOData.diceType;
            BattleDice battleDice = new BattleDice(diceType);
            battleDices.Add(battleDice);
            for (int j = 0; j < 6; j++)
            {
                var singleDiceObjSOData = battleDiceSOData.singleDiceObjSODatas[j];
                string singleDiceid = singleDiceObjSOData.id;
                var singleDiceModel = ResourcesManager.GetSingleDiceModelViaid(singleDiceid);

                battleDice.AddDice(singleDiceModel, singleDiceObjSOData.idInDice, i, j);
            }
        }
        //初始化战斗时骰子的数组大小
        diceCardsInUse = new SingleDiceObj[battleDiceCount];
    }
    /// <summary>
    /// 在有存档的情况下，初始化背包骰面
    /// </summary>
    /// <param name="singleDiceObjSODatas"></param>
    public void InitBagDiceWithData(List<SingleDiceObjSOData> singleDiceObjSODatas)
    {
        this.bagDiceCards.Clear();
        for (int i = 0; i < singleDiceObjSODatas.Count; i++)
        {
            var singleDiceObjSOData = singleDiceObjSODatas[i];
            string singleDiceid = singleDiceObjSOData.id;
            var singleDiceModel = ResourcesManager.GetSingleDiceModelViaid(singleDiceid);
            SingleDiceObj singleDiceObj = new SingleDiceObj(singleDiceModel, singleDiceObjSOData.idInDice);
            this.AddSingleBattleDiceToBag(singleDiceObj);
        }
    }
    /// <summary>
    /// 在没有存档的情况下，初始化背包骰面
    /// </summary>
    /// <param name="singleDiceModelSOs"></param>
    public void InitBagDiceWithoutData(List<SingleDiceModelSO> singleDiceModelSOs)
    {
        this.bagDiceCards.Clear();
        for (int i = 0; i < singleDiceModelSOs.Count; i++)
        {
            SingleDiceModel singleDiceModel = SingleDiceData.diceDictionary[singleDiceModelSOs[i].singleDiceModelName];
            SingleDiceObj singleDiceObj = new SingleDiceObj(singleDiceModel, i + 1);
            this.AddSingleBattleDiceToBag(singleDiceObj);
        }
    }
    #endregion
    #region 创建骰面UI
    public void InitBattleDiceUI()
    {
        for (int i = 0; i < this.battleDiceCount; i++)
        {
            //获取
            var singleDices = this.battleDices[i].GetBattleDiceSingleDices();
            string name = $"页面:{i + 1}";
            FightDicePageManager.Instance.CreatePageUI(name, singleDices);//UI创建page
        }
    }
    public void InitBagDiceUI(Action<SingleDiceObj> sellFunction)
    {
        //创建背包骰子页面
        for (int i = 0; i < this.bagDiceCards.Count; i++)
        {
            var singleDice = this.bagDiceCards[i];
            var singleDiceUIData = ResourcesManager.GetSingleDiceUIData(singleDice);
            BagDiceUIManager.Instance.CreateBagUIDice(sellFunction, singleDice);
            //初始化玩家的背包骰面
        }
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
    #region 从各个地方删除骰面
    public void RemoveSingleBattleDiceFromBag(SingleDiceObj singleDiceObj)
    {
        this.bagDiceCards.Remove(singleDiceObj);
    }
    //将战斗骰子每一个设置为空
    public void ClearBattleSingleDices()
    {
        for (int i = 0; i < diceCardsInUse.Length; i++)
        {
            diceCardsInUse[i] = null;
        }
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
    /// 获取骰面在背包中的index
    /// </summary>
    /// <param name="singleDiceObj"></param>
    /// <returns></returns>
    public int GetIndexOfSingleDiceInBag(SingleDiceObj singleDiceObj)
    {
        return bagDiceCards.IndexOf(singleDiceObj);
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

    public void ClearPreviousSingleDiceStack()
    {
        this.previousSingleDices.Clear();
    }
    #endregion
    #region 存档相关
    /// <summary>
    /// 从runtimeData中获取战斗骰子的数据
    /// </summary>
    /// <returns></returns>
    public List<BattleDiceSOData> GetBattleDiceSOData()
    {
        //创建一个骰子的数据列表
        var battleDiceSODatas = new List<BattleDiceSOData>();
        for (int i = 0; i < battleDices.Count; i++)
        {
            BattleDice battleDice = battleDices[i];
            BattleDiceSOData battleDiceSOData = new BattleDiceSOData();
            battleDiceSOData.diceType = battleDice.diceType;
            List<SingleDiceObjSOData> singleDiceObjSODatas = new List<SingleDiceObjSOData>();
            //获取每一个骰子的骰面数据
            var singleDiceObjs = battleDice.GetBattleDiceSingleDices();
            for (int j = 0; j < 6; j++)
            {
                SingleDiceObj singleDiceObj = singleDiceObjs[j];
                var singleDiceObjSOData = new SingleDiceObjSOData();
                singleDiceObjSOData.id = singleDiceObj.model.id;
                singleDiceObjSOData.idInDice = singleDiceObj.idInDice;
                singleDiceObjSOData.level = singleDiceObj.level;
                singleDiceObjSOData.value = singleDiceObj.model.value;
                singleDiceObjSODatas.Add(singleDiceObjSOData);
            }
            battleDiceSOData.singleDiceObjSODatas = singleDiceObjSODatas;
            battleDiceSODatas.Add(battleDiceSOData);
        }
        return battleDiceSODatas;
    }
    //获取玩家身上的背包骰面数据
    public List<SingleDiceObjSOData> GetSingleDiceObjSODatas()
    {
        var singleDiceObjSODatas = new List<SingleDiceObjSOData>();
        //遍历背包中的骰面
        for (int i = 0; i < bagDiceCards.Count; i++)
        {
            SingleDiceObj singleDiceObj = bagDiceCards[i];
            var singleDiceObjSOData = new SingleDiceObjSOData();
            singleDiceObjSOData.id = singleDiceObj.model.id;
            singleDiceObjSOData.idInDice = singleDiceObj.idInDice;
            singleDiceObjSOData.level = singleDiceObj.level;
            singleDiceObjSOData.value = singleDiceObj.model.value;
            singleDiceObjSODatas.Add(singleDiceObjSOData);
        }
        return singleDiceObjSODatas;
    }
    #endregion


    public Stack<SingleDiceObj> GetPreviousSingleDicesStack()
    {
        return this.previousSingleDices;
    }
}

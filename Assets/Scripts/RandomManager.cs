using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DesignerScripts;
using static Cinemachine.DocumentationSortingAttribute;
using UI;
/// <summary>
/// 获取随机数的管理器，获取随机的数据
/// </summary>
public static class RandomManager
{
    /// <summary>
    /// 获取随机的单个骰子
    /// </summary>
    /// <param name="diceType">骰子的种类</param>
    /// <param name="level">骰子的等级</param>
    /// <param name="side">骰面属于那一类</param>
    /// <returns>单个符合条件的骰面model</returns>
    public static SingleDiceModel GetSingleDiceModel(DiceType diceType, int level, int side)
    {
        Dictionary<string, SingleDiceModel> diceDictionary = SingleDiceData.diceDictionary;
        List<SingleDiceModel> singleDiceModels = new List<SingleDiceModel>();
        singleDiceModels.AddRange(diceDictionary.Values);
        List<SingleDiceModel> singleDiceModellegal = singleDiceModels.Where((SingleDiceModel singleDiceModel) =>
        {
            return singleDiceModel.type == diceType && singleDiceModel.level == level && (singleDiceModel.side == side|| singleDiceModel.side == 2) ;
        }).ToList();
        return singleDiceModellegal[Random.Range(0, singleDiceModellegal.Count)];
    }

    public static SingleDiceModel GetSingleDiceModel(int level, int side)
    {
        Dictionary<string, SingleDiceModel> diceDictionary = SingleDiceData.diceDictionary;
        List<SingleDiceModel> singleDiceModels = new List<SingleDiceModel>();
        singleDiceModels.AddRange(diceDictionary.Values);
        List<SingleDiceModel> singleDiceModellegal = singleDiceModels.Where((SingleDiceModel singleDiceModel) =>
        {
            return singleDiceModel.level == level && (singleDiceModel.side == side || singleDiceModel.side == 2);
        }).ToList();
        return singleDiceModellegal[Random.Range(0, singleDiceModellegal.Count)];
    }


    /// <summary>
    /// 根据稀有度获取随机的单个圣物
    /// </summary>
    /// <param name="rareType">圣物的稀有度</param>
    /// <returns></returns>
    public static HalidomObject GetRandomHalidomObj(RareType rareType)
    {
        Dictionary<string,HalidomObject> keyValuePairs = HalidomData.halidomDictionary;
        List<HalidomObject> halidomObjects = new List<HalidomObject>();
        halidomObjects.AddRange(keyValuePairs.Values);
        List<HalidomObject> halidomObjectLegals = halidomObjects.Where((HalidomObject halidomObject) =>
        {
            return halidomObject.rareType == rareType;
        }).ToList();
        return halidomObjectLegals[Random.Range(0, halidomObjectLegals.Count)];
    }
    #region 获得金钱
    /// <summary>
    /// 通过进入奖励界面获取的投掷结果获取金钱
    /// </summary>
    /// <param name="singleDiceObjs">用于计算点数的投掷结果</param>
    /// <returns></returns>
    public static int GetMoneyViaRollResult(List<SingleDiceObj> conditionSingleDiceObjs)
    {
        //计算用于条件的骰子的点数总和
        int sum = 0;
        
        foreach (var singleDiceObj in conditionSingleDiceObjs)
        {
            sum += singleDiceObj.idInDice;
        }
        if (sum < 15)
        {
            return Random.Range(8, 13);
        }
        else if (sum >= 15 && sum < 24)
        {
            return Random.Range(13, 18);
        }
        else
        {
            return Random.Range(18, 23);
        }

    }
    /// <summary>
    /// 通过当前玩家的状态获取买进的价格
    /// </summary>
    /// <param name="chaState"></param>
    /// <returns></returns>
    public static int GetSalePriceViaChaState(ChaState chaState)
    {
        //进入奖励界面获取的金钱
        int money = 5;
        return money;
    }
    #endregion
    /// <summary>
    /// 根据投出骰子和当前游戏的状态获取奖励的骰子
    /// </summary>
    /// <param name="playerState"></param>
    ///<param name="count">获取的骰子数量</param>
    /// <returns></returns>
    public static List<SingleDiceObj> GetRewardSingleDiceObjsViaPlayerData(List<SingleDiceObj> conditionSingleDiceObj,int count)
    {
        //计算用于条件的骰子的点数总和
        int sum = 0;
        foreach (var singleDiceObj in conditionSingleDiceObj)
        {
            sum += singleDiceObj.idInDice ;
        }
        Debug.Log("<color=#39C5BB>RandomManager</color>" + sum);
        //根据条件骰子的点数总和获取奖励的骰子
        //生成骰面的等级
        int level;
        if (sum < 15)
        {
            level = 1;
        }
        else if (sum >= 15 && sum <  24)
        {
            level = 2;
        }
        else
        {
            level = 3;
        }
        var singleDiceObjs = new List<SingleDiceObj>();
        for (int i = 0; i < count; i++)
        {
            DiceType diceType = (DiceType)Random.Range(0, 3);
            SingleDiceModel singleDiceModel = GetSingleDiceModel(diceType, level, 0);
            int idInDice = Random.Range(0, 6);
            SingleDiceObj singleDiceObj = new SingleDiceObj(singleDiceModel, idInDice);

            singleDiceObjs.Add(singleDiceObj);
        }

        return singleDiceObjs;

    }

    public static HalidomObject GetRewardHalidomViaPlayerData(List<SingleDiceObj> conditionSingleDiceObj)
    {
        int sum = 0;
        foreach (var singleDiceObj in conditionSingleDiceObj)
        {
            sum += singleDiceObj.idInDice;
        }
        //Debug.Log("<color=#39C5BB>RandomManager</color>" + sum);
        //根据条件骰子的点数总和获取奖励的骰子
        if (sum < 15)
        {
            //不生成圣物
            return GetRandomHalidomObj(RareType.Common);
        }
        else if (sum >= 15 && sum < 24)
        {
            return GetRandomHalidomObj(RareType.Rare);
        }
        else
        {
            return GetRandomHalidomObj(RareType.Legendary);
        }
    }

}

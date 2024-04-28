using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using UI;
using System;
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
        Dictionary<string, SingleDiceModel> diceDictionary = DataInitManager.Instance.singleDiceDataTable.diceDictionary;
        List<SingleDiceModel> singleDiceModels = new List<SingleDiceModel>();
        singleDiceModels.AddRange(diceDictionary.Values);
        singleDiceModels = singleDiceModels.Where((SingleDiceModel singleDiceModel) =>
        {
            return singleDiceModel.id != "0_00";
        }).ToList();
        List<SingleDiceModel> singleDiceModellegal = singleDiceModels.Where((SingleDiceModel singleDiceModel) =>
        {
            return singleDiceModel.type == diceType && singleDiceModel.level == level && (singleDiceModel.side == side|| singleDiceModel.side == 2) ;
        }).ToList();
        return singleDiceModellegal[UnityEngine.Random.Range(0, singleDiceModellegal.Count)];
    }

    public static SingleDiceModel GetSingleDiceModel(int level, int side)
    {
        Dictionary<string, SingleDiceModel> diceDictionary = DataInitManager.Instance.singleDiceDataTable.diceDictionary;
        
        List<SingleDiceModel> singleDiceModels = new List<SingleDiceModel>();
        singleDiceModels.AddRange(diceDictionary.Values);
        //去除待机,如果说这边需要去除很多，那可以改成dictionary
        singleDiceModels = singleDiceModels.Where((SingleDiceModel singleDiceModel) =>
        {
            return singleDiceModel.id != "0_00";
        }).ToList();
        List<SingleDiceModel> singleDiceModellegal = singleDiceModels.Where((SingleDiceModel singleDiceModel) =>
        {
            return singleDiceModel.level == level && (singleDiceModel.side == side || singleDiceModel.side == 2);
        }).ToList();
        return singleDiceModellegal[UnityEngine.Random.Range(0, singleDiceModellegal.Count)];
    }


    /// <summary>
    /// 根据稀有度获取随机的单个圣物
    /// </summary>
    /// <param name="rareType">圣物的稀有度</param>
    /// <returns></returns>
    public static HalidomObject GetRandomHalidomObj(RareType rareType)
    {
        Dictionary<string,HalidomObject> keyValuePairs = DataInitManager.Instance.halidomDataTable.halidomDictionary;
        List<HalidomObject> halidomObjects = new List<HalidomObject>();
        halidomObjects.AddRange(keyValuePairs.Values);
        List<HalidomObject> halidomObjectLegals = halidomObjects.Where((HalidomObject halidomObject) =>
        {
            return halidomObject.rareType == rareType;
        }).ToList();
        return halidomObjectLegals[UnityEngine.Random.Range(0, halidomObjectLegals.Count)];
    }

    public static HalidomObject GetRandomHalidomObj(RareType rareType,Dictionary<string,HalidomObject> currentHalidomList)
    {
        Dictionary<string, HalidomObject> keyValuePairs = DataInitManager.Instance.halidomDataTable.halidomDictionary;
        List<HalidomObject> halidomObjects = new List<HalidomObject>();
        halidomObjects.AddRange(keyValuePairs.Values);
        List<HalidomObject> halidomObjectLegals = halidomObjects.Where((HalidomObject halidomObject) =>
        {
            return halidomObject.rareType == rareType && !(currentHalidomList.ContainsKey(halidomObject.id));
        }).ToList();
        return halidomObjectLegals[UnityEngine.Random.Range(0, halidomObjectLegals.Count)];
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
            return UnityEngine.Random.Range(8, 13);
        }
        else if (sum >= 15 && sum < 24)
        {
            return UnityEngine.Random.Range(13, 18);
        }
        else
        {
            return UnityEngine.Random.Range(18, 23);
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
            DiceType diceType = (DiceType)UnityEngine.Random.Range(0, 3);
            SingleDiceModel singleDiceModel = GetSingleDiceModel(diceType, level, 0);
            int idInDice = UnityEngine.Random.Range(1, 6);
            SingleDiceObj singleDiceObj = new SingleDiceObj(singleDiceModel, idInDice);

            singleDiceObjs.Add(singleDiceObj);
        }

        return singleDiceObjs;

    }

    public static HalidomObject GetRewardHalidomViaPlayerData(List<SingleDiceObj> conditionSingleDiceObj,Dictionary<String, HalidomObject> currentHalidomList)
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
            return GetRandomHalidomObj(RareType.Common,currentHalidomList);
        }
        else if (sum >= 15 && sum < 24)
        {
            return GetRandomHalidomObj(RareType.Rare,currentHalidomList);
        }
        else
        {
            return GetRandomHalidomObj(RareType.Legendary, currentHalidomList);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 获取随机数的管理器，获取随机的数据
/// </summary>
public class RandomManager : MonoSingleton<RandomManager>
{
    /// <summary>
    /// 获取随机的单个骰子
    /// </summary>
    /// <param name="diceType">骰子的种类</param>
    /// <param name="level">骰子的等级</param>
    /// <param name="side">骰面属于那一类</param>
    /// <returns></returns>
    public SingleDiceModel GetSingleDiceModel(DiceType diceType, int level, int side)
    {
        Dictionary<string, SingleDiceModel> diceDictionary = SingleDiceData.diceDictionary;
        List<SingleDiceModel> singleDiceModels = new List<SingleDiceModel>();
        singleDiceModels.AddRange(diceDictionary.Values);
        //打印出所有的骰子的信息
        //for (int i = 0; i < singleDiceModels.Count; i++)
        //{
        //    Debug.Log(singleDiceModels[i].type.ToString() + " " + singleDiceModels[i].level + " " + singleDiceModels[i].side);
        //}
        //从中选取符合要求的骰子
        List<SingleDiceModel> singleDiceModellegal = singleDiceModels.Where((SingleDiceModel singleDiceModel) =>
        {
            return singleDiceModel.type == diceType && singleDiceModel.level == level && (singleDiceModel.side == side|| singleDiceModel.side == 2) ;
        }).ToList();
        Debug.Log(singleDiceModellegal.Count);
        return singleDiceModellegal[Random.Range(0, singleDiceModellegal.Count)];
    }


    public int GetMoneyViaChaState(ChaState chaState)
    {
        //进入奖励界面获取的金钱
        int money = 5;
        return money;
    }   
}

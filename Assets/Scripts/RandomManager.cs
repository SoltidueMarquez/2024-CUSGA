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
    /// <returns></returns>
    public SingleDiceModel GetSingleDiceModel(DiceType diceType,int level)
    {
        Dictionary<string, SingleDiceModel> diceDictionary = SingleDiceData.diceDictionary;
        List<SingleDiceModel> singleDiceModels = new List<SingleDiceModel>();
        foreach (var item in diceDictionary)
        {
            if (item.Value.type == diceType && item.Value.level == level)
            {
                singleDiceModels.Add(item.Value);
            }
        }
        //从中选取符合要求的骰子
        List<SingleDiceModel> singleDiceModellegal = singleDiceModels.Where((SingleDiceModel singleDiceModel) =>
        {
            return singleDiceModel.type == diceType && singleDiceModel.level == level;
        }).ToList();
        return singleDiceModellegal[Random.Range(0, singleDiceModellegal.Count)];
    }
}

using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// 统一资源读取
/// </summary>
public enum SpriteType
{
    Buff,
    SingleDice,
    Intention,
    Halidom
}

public class HalidomUIData
{
    public string name;
    public string description;
    public int value;
    public int salevalue;
    public Sprite sprite;
}
public class SingleDiceUIData
{
    public int idInDice;
    public int positionInDice;
    public int baseValue;
    public DiceType type;
    public Sprite sprite;
    public string description;
    public int value;
    public int salevalue;
    public string name;
    public int level;
}
public class BuffUIData
{
    public Sprite sprite;
    public string name;
    public string description;
}
public class IntentionUIData
{
    public Sprite sprite;
    public string name;
    public string description;
}
public static class ResourcesManager
{
    /// <summary>
    /// 通过id获取圣物的UI信息
    /// </summary>
    /// <param name="id">圣物的唯一id</param>
    /// <returns></returns>
    public static HalidomUIData GetHalidomUIData(string id)
    {
        HalidomDataSO halidomDataSO = Resources.Load<HalidomDataSO>("Data/HalidomData/HalidomData_" + id);
        if (halidomDataSO == null)
        {
            Debug.LogWarning("ResourcesManager:HalidomDataSO is null");
            return null;
        }
        HalidomUIData halidomUIData = new HalidomUIData();
        halidomUIData.name = halidomDataSO.halidomName.ToString();
        halidomUIData.description = halidomDataSO.description;
        halidomUIData.value = halidomDataSO.value;
        halidomUIData.salevalue = Mathf.FloorToInt(halidomDataSO.value / 4);
        halidomUIData.sprite = halidomDataSO.sprite;
        return halidomUIData;
    }
    /// <summary>
    /// 通过id获取相应的singleDiceModel
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static SingleDiceModel GetSingleDiceModelViaid(string id)
    {
        var singleDiceModelList = SingleDiceData.diceDictionary.Values.ToList();
        var singleDiceModel = singleDiceModelList.Where(x => x.id == id).FirstOrDefault();
        return singleDiceModel;
    }
    public static BuffUIData GetBuffUIData(string id)
    {
        BuffDataSO buffDataSO = Resources.Load<BuffDataSO>("Data/BuffData/BaseBuffData/BuffData_" + id);
        if (buffDataSO == null)
        {
            Debug.LogWarning("ResourcesManager:BuffDataSO is null");
            return null;
        }
        BuffUIData buffUIData = new BuffUIData();
        buffUIData.name = buffDataSO.dataName.ToString();
        buffUIData.description = buffDataSO.description;
        buffUIData.sprite = buffDataSO.sprite;
        return buffUIData;
    }
    /// <summary>
    /// 加载单个骰面的UI数据，需要传入单个骰面的对象，因为有些属性是运行时
    /// </summary>
    /// <param name="singleDiceObj"></param>
    /// <returns></returns>
    public static SingleDiceUIData GetSingleDiceUIData(SingleDiceObj singleDiceObj)
    {
        string id = singleDiceObj.model.id;
        SingleDiceModelSO singleDiceModelSO = Resources.Load<SingleDiceModelSO>("Data/SingleDiceData/DiceData_" + id);
        if (singleDiceModelSO == null)
        {
            Debug.LogWarning("ResourcesManager:SingleDiceModelSO is null");
            return null;
        }
        SingleDiceUIData singleDiceUIData = new SingleDiceUIData();
        //这边的idInDice是点数，positionInDice是在骰子中的位置
        singleDiceUIData.idInDice = singleDiceObj.idInDice;
        singleDiceUIData.positionInDice = singleDiceObj.positionInDice;
        //根据骰子类型和基础等级计算基础值
        singleDiceUIData.baseValue = singleDiceModelSO.baseValue;
        singleDiceUIData.type = singleDiceModelSO.type;
        singleDiceUIData.sprite = singleDiceModelSO.sprite;
        singleDiceUIData.description = singleDiceModelSO.description;
        singleDiceUIData.value = singleDiceObj.value;
        singleDiceUIData.salevalue = singleDiceObj.SaleValue;
        singleDiceUIData.name = singleDiceModelSO.singleDiceModelName;
        singleDiceUIData.level = (int)singleDiceModelSO.level + 1;
        return singleDiceUIData;
    }
    /// <summary>
    /// 通过id获取意图的UI数据，id为骰面的id,即通过骰面的类型来获取意图的UI数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static IntentionUIData GetIntentionUIData(string id)
    {
        IntentionSO[] intensionSOs = Resources.LoadAll<IntentionSO>("Data/IntentionData");
        SingleDiceModelSO singleDiceModelSO = Resources.Load<SingleDiceModelSO>("Data/SingleDiceData/DiceData_" + id);
        if (intensionSOs == null)
        {
            Debug.LogWarning("ResourcesManager:SingleDiceModelSOs is null");
            return null;
        }
        var intentionSO = intensionSOs.Where(intensionSOs => intensionSOs.diceType == singleDiceModelSO.type).FirstOrDefault();
        IntentionUIData intentionUIData = new IntentionUIData();
        intentionUIData.sprite = intentionSO.icon;
        intentionUIData.name = intentionSO.intentionName;
        intentionUIData.description = intentionSO.description;
        //TODO:根据骰子类型获取意图的UI数据
        return intentionUIData;

    }

}

using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
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
    public Sprite sprite;
}
public class SingleDiceUIData
{
    public int index;
    public int baseValue;
    public DiceType type;
    public Sprite sprite;
    public string description;
    public int value;
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
    public static HalidomUIData GetHalidomUIData(string id)
    {
        HalidomDataSO halidomDataSO = Resources.Load<HalidomDataSO>("Data/HalidomData/HalidomData" + id);
        if (halidomDataSO == null)
        {
            Debug.LogWarning("ResourcesManager:HalidomDataSO is null");
            return null;
        }
        HalidomUIData halidomUIData = new HalidomUIData();
        halidomUIData.name = halidomDataSO.halidomName.ToString();
        halidomUIData.description = halidomDataSO.description;
        halidomUIData.value = halidomDataSO.value;
        halidomUIData.sprite = halidomDataSO.sprite;
        return halidomUIData;
    }

    public static BuffUIData GetBuffUIData(string id)
    {
        BuffDataSO buffDataSO = Resources.Load<BuffDataSO>("Data/BuffData/BaseBuffData/BuffData" + id);
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
        SingleDiceModelSO singleDiceModelSO = Resources.Load<SingleDiceModelSO>("Data/SingleDiceData/SingleDiceData" + id);
        if (singleDiceModelSO == null)
        {
            Debug.LogWarning("ResourcesManager:SingleDiceModelSO is null");
            return null;
        }
        SingleDiceUIData singleDiceUIData = new SingleDiceUIData();
        singleDiceUIData.index = singleDiceModelSO.side;
        //根据骰子类型和基础等级计算基础值
        switch (singleDiceModelSO.type)
        {
            case DiceType.Attack:
                singleDiceUIData.baseValue = DesignerScripts.DamageUtil.GetlevelBasedDamage(singleDiceObj.level);
                break;
            case DiceType.Defense:
                singleDiceUIData.baseValue = DesignerScripts.DamageUtil.GetIndexLevelBasedShield(singleDiceObj.level);
                break;
            case DiceType.Support:
                singleDiceUIData.baseValue = DesignerScripts.DamageUtil.GetIndexLevelBasedHeal(singleDiceObj.level);
                break;
        }
        singleDiceUIData.type = singleDiceModelSO.type;
        singleDiceUIData.sprite = singleDiceModelSO.sprite;
        singleDiceUIData.description = singleDiceModelSO.description;
        singleDiceUIData.value = singleDiceModelSO.value;
        singleDiceUIData.name = singleDiceModelSO.singleDiceModelName;
        return singleDiceUIData;
    }
    /// <summary>
    /// 通过id获取意图的UI数据，id为骰面的id,即通过骰面的类型来获取意图的UI数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static IntentionUIData GetIntentionUIData(string id)
    {
        SingleDiceModelSO singleDiceModelSO = Resources.Load<SingleDiceModelSO>("Data/SingleDiceData/SingleDiceData" + id);
        if (singleDiceModelSO == null)
        {
            Debug.LogWarning("ResourcesManager:SingleDiceModelSO is null");
            return null;
        }
        IntentionUIData intentionUIData = new IntentionUIData();
        //TODO:根据骰子类型获取意图的UI数据
        return intentionUIData;

    }

}

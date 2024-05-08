using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Product_", menuName = "BattleStore/BattleProduct")]
public class BaseBattleProduct : ScriptableObject
{
    /// <summary>
    /// 局内物品售价
    /// </summary>
    public int value;
    /// <summary>
    /// 局内物品类型
    /// </summary>
    public BattleProductType type;
    /// <summary>
    /// 局内物品名称
    public string productName;
    /// <summary>
    /// 局内物品描述
    /// </summary>
    public string description;
    /// <summary>
    /// 使用so存储的productEffect
    /// </summary>
    public BaseProductEffect effect;
    /// <summary>
    /// 购买失败调用的事件，包含ui提示
    /// </summary>
    public UnityEvent OnBuyFail;
    /// <summary>
    /// 购买成功调用的事件，包含ui提示
    /// </summary>
    public UnityEvent OnBuySuccess;

    public void TryBuy()
    {
        int battleCurrency=BattleManager.Instance.parameter.battleCurrency;
        if(battleCurrency>=value)
        {
            OnBuySuccess?.Invoke();
            
        }
        else
        {
            OnBuyFail?.Invoke();
        }
    }

    public void InitialProduct()
    {
        OnBuySuccess.RemoveAllListeners();
        OnBuyFail.RemoveAllListeners();
        OnBuySuccess.AddListener(ProductBrought);
    }

    public void ProductBrought()
    {
        //使用道具
        effect.Use();
        //扣除货币
        BattleManager.Instance.parameter.battleCurrency -= value;
    }

}

public enum BattleProductType
{
    First,
    Second,
    Third
}


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

    public UnityEvent OnBuyFail;

    public void TryBuy()
    {
        int battleCurrency=BattleManager.Instance.parameter.battleCurrency;
        if(battleCurrency>=value)
        {
            effect.Use();
        }
        else
        {
            OnBuyFail.Invoke();
        }
    }

}

public enum BattleProductType
{
    First,
    Second,
    Third
}


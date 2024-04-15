using DesignerScripts;
using Map;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 代表商品格子
/// </summary>
/// <typeparam name="T">商品类型</typeparam>
public abstract class ProductBase<T> : MonoBehaviour where T : class
{
    public bool isEmpty = true;
    public T product;
    // Start is called before the first frame update
    protected virtual void Start()
    {

        
    }
  
    /// <summary>
    /// 试图购买，返回是否购买成功
    /// </summary>
    /// <returns></returns>
    public virtual void TryBuy()
    {      
        
    }

    /// <summary>
    /// 商品初始化
    /// </summary>
    /// <param name="product"></param>
    public virtual void InitialProduct(T _product)
    {
        isEmpty = false;
        product = _product;
        OnBuySuccess.RemoveAllListeners();
        OnBuyFail.RemoveAllListeners();
        OnBuySuccess.AddListener(ProductBrought);
    }

    /// <summary>
    /// 这个商品被买走了
    /// </summary>
    public virtual void ProductBrought()
    {
        isEmpty = true;
    }

    public virtual void ClearProduct()
    {
        isEmpty = true;
        product = null;
    }

    /// <summary>
    /// 购买成功时调用
    /// </summary>
    public UnityEvent OnBuySuccess;
    /// <summary>
    /// 购买失败是调用
    /// </summary>
    public UnityEvent<BuyFailType> OnBuyFail;

}
/// <summary>
/// 商品购买失败的类型
/// </summary>
public enum BuyFailType
{
    NoMoney,
    NoBagSpace
}
using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ProductBase : MonoBehaviour
{
    public bool isEmpty = true;
    public object productData; 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 试图购买，返回是否购买成功
    /// </summary>
    /// <returns></returns>
    public abstract bool TryBuy();


    public virtual void AddProduct<T>(T product) where T : HalidomObject
    {

    }

    /// <summary>
    /// 点击商品时调用
    /// </summary>
    public UnityEvent OnClickProduct;
    /// <summary>
    /// 购买成功时调用
    /// </summary>
    public UnityEvent OnBuySuccess;
    /// <summary>
    /// 购买失败是调用
    /// </summary>
    public UnityEvent<BuyFailType> OnBuyFail;

    public enum BuyFailType
    {
        NoMoney,
        NoBagSpace
    }
}

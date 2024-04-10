using DesignerScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ������Ʒ����
/// </summary>
/// <typeparam name="T">��Ʒ����</typeparam>
public abstract class ProductBase<T> : MonoBehaviour where T : class
{
    public bool isEmpty = true;
    public T product;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        OnBuyProduct.AddListener(TryBuy);
        OnBuySuccess.AddListener(ProductBrought);
    }

    /// <summary>
    /// ��ͼ���򣬷����Ƿ���ɹ�
    /// </summary>
    /// <returns></returns>
    protected abstract void TryBuy();

    /// <summary>
    /// ��Ʒ��ʼ��
    /// </summary>
    /// <param name="product"></param>
    protected virtual void InitialProduct(T _product)
    {
        isEmpty = false;
        product = _product;
    }

    /// <summary>
    /// �����Ʒ��������
    /// </summary>
    protected virtual void ProductBrought()
    {
        isEmpty = true;
    }

    /// <summary>
    /// ��ͼ������Ʒʱ����,�����Ƿ���ɹ�
    /// </summary>
    public UnityEvent OnBuyProduct;
    /// <summary>
    /// ����ɹ�ʱ����
    /// </summary>
    public UnityEvent OnBuySuccess;
    /// <summary>
    /// ����ʧ���ǵ���
    /// </summary>
    public UnityEvent<BuyFailType> OnBuyFail;

}
/// <summary>
/// ��Ʒ����ʧ�ܵ�����
/// </summary>
public enum BuyFailType
{
    NoMoney,
    NoBagSpace
}
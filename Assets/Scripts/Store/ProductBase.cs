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
    /// ��ͼ���򣬷����Ƿ���ɹ�
    /// </summary>
    /// <returns></returns>
    public abstract bool TryBuy();


    public virtual void AddProduct<T>(T product) where T : HalidomObject
    {

    }

    /// <summary>
    /// �����Ʒʱ����
    /// </summary>
    public UnityEvent OnClickProduct;
    /// <summary>
    /// ����ɹ�ʱ����
    /// </summary>
    public UnityEvent OnBuySuccess;
    /// <summary>
    /// ����ʧ���ǵ���
    /// </summary>
    public UnityEvent<BuyFailType> OnBuyFail;

    public enum BuyFailType
    {
        NoMoney,
        NoBagSpace
    }
}

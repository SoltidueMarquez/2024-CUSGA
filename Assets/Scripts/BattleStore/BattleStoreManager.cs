using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleStoreManager : MonoSingleton<BattleStoreManager>
{
    /// <summary>
    /// 局内商店刷新次数
    /// </summary>
    public int rerollCount;
    /// <summary>
    /// 是否是第一次进商店
    /// </summary>
    public bool isFristEnter;
    /// <summary>
    /// 商店的商品列表
    /// </summary>
    public List<BaseBattleProduct> battleProducts = new List<BaseBattleProduct>();
    /// <summary>
    /// 已经买过的商品列表（后续可能会更新）
    /// </summary>
    public List<BaseBattleProduct> broughtBattleProducts = new List<BaseBattleProduct>();

    [Header("事件")]
    /// <summary>
    /// 进入商店时调用
    /// </summary>
    public UnityEvent OnEnterStore;
    /// <summary>
    /// 离开商店时调用
    /// </summary>
    public UnityEvent OnExitStore;
    /// <summary>
    /// 刷新商品店时调用
    /// </summary>
    public UnityEvent<List<BaseBattleProduct>> OnRefreshStore;

    private void Start()
    {
        OnRefreshStore.AddListener(RerollShop);
        OnEnterStore.AddListener(OpenStore);
        //OnExitStore.AddListener(CloseStore);
        
    }

    public void InvokeOnRefreshStore()
    {
        OnRefreshStore?.Invoke(battleProducts);
    }

    


    public void OpenStore()
    {
        if(isFristEnter == true)
        {
            OnRefreshStore.Invoke(battleProducts);
        }
        
    }

    public void RerollShop(List<BaseBattleProduct> productList)
    {
        if (isFristEnter == true)
        {
            isFristEnter = false;
            battleProducts.Clear();
            AddProduct();
        }
        else if (isFristEnter == false && rerollCount > 0)
        {
            rerollCount--;
            battleProducts.Clear();
            AddProduct();
        }
    }

    public bool AlreadyHasProduct(BaseBattleProduct product)
    {

        foreach (var item in battleProducts)
        {
            if (item.productName == product.productName)
            {
                return true;
            }
        }
        return false;
    }

    public void AddProduct()
    {
        int i = 0;
        while (i < 100)
        {
            var product = RandomManager.GetBattleProduct(BattleProductType.First);
            if (!AlreadyHasProduct(product))
            {
                battleProducts.Add(product);
                battleProducts[0].InitialProduct();
                break;
            }
            i++;
        }
        i = 0;
        while (i < 100)
        {
            var product = RandomManager.GetBattleProduct(BattleProductType.First);
            if (!AlreadyHasProduct(product))
            {
                battleProducts.Add(product);
                battleProducts[1].InitialProduct();
                break;
            }
            i++;
        }
        i = 0;
        while (i < 100)
        {
            var product = RandomManager.GetBattleProduct(BattleProductType.Second);
            if (!AlreadyHasProduct(product))
            {
                battleProducts.Add(product);
                battleProducts[2].InitialProduct();
                break;
            }

            i++;
        }
        i = 0;
        while (i < 100)
        {
            var product = RandomManager.GetBattleProduct(BattleProductType.Second);
            if (!AlreadyHasProduct(product))
            {
                battleProducts.Add(product);
                battleProducts[3].InitialProduct();
                break;
            }

            i++;
        }
        i = 0;
        while (i < 100)
        {
            var product = RandomManager.GetBattleProduct(BattleProductType.Third);
            if (!AlreadyHasProduct(product))
            {
                battleProducts.Add(product);
                battleProducts[4].InitialProduct();
                break;
            }

            i++;
        }
        i = 0;
        while (i < 100)
        {
            var product = RandomManager.GetBattleProduct(BattleProductType.Third);
            if (!AlreadyHasProduct(product))
            {
                battleProducts.Add(product);
                battleProducts[5].InitialProduct();
                break;
            }

            i++;
        }


    }

    public void RefreashRerollCount()
    {
        this.rerollCount = 0;
        this.isFristEnter = true;
    }

}

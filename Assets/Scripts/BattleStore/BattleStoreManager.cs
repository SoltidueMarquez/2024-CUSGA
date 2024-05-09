using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleStoreManager : MonoSingleton<BattleStoreManager>
{
    public List<BaseBattleProduct> battleProducts = new List<BaseBattleProduct>();
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
    public UnityEvent OnRefreshStore;

    private void Start()
    {
        OnEnterStore.AddListener(OpenStore);
        OnExitStore.AddListener(CloseStore);
        OnRefreshStore.AddListener(RerollShop);
    }

    

    public void CloseStore()
    {

    }


    public void OpenStore()
    {

    }

    public void RerollShop()
    {
        battleProducts.Clear();
        AddProduct();
    }

    public bool AlreadyHasProduct(BaseBattleProduct product)
    {

        foreach (var item in battleProducts)
        {
            if(item.productName == product.productName)
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
    
}

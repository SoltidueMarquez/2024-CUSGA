using Map;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI.Store;
using Unity.Mathematics;
using UnityEngine;

public class ProductHalidom : ProductBase<HalidomObject>
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void TryBuy()
    {
        base.TryBuy();
        ChaState player = MapManager.Instance.playerChaState;
        if (player.resource.currentMoney < product.value)
        {
            OnBuyFail?.Invoke(BuyFailType.NoMoney);
            return;
        }

        int count = 0;
        foreach (var item in HalidomManager.Instance.halidomList)
        {
            if (item != null)
            {
                count++;
            }
        }
        if (count >= HalidomManager.Instance.halidomMaxCount)
        {
            OnBuyFail?.Invoke(BuyFailType.NoBagSpace);
            return;
        }

        OnBuySuccess?.Invoke();

    }

    public override void InitialProduct(HalidomObject product)
    {
        base.InitialProduct(product);
    }

    public override void ProductBrought()
    {
        base.ProductBrought();
        HalidomManager.Instance.AddHalidomInMap(product);
        ChaState player = MapManager.Instance.playerChaState;
        player.resource.currentMoney -= product.value;
        
    }


}

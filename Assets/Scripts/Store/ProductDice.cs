using Map;
using System.Collections;
using System.Collections.Generic;
using UI.Store;
using UnityEngine;

public class ProductDice : ProductBase<SingleDiceObj>
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
        else if (player.GetBattleDiceHandler().bagDiceCards.Count >= player.GetBattleDiceHandler().maxDiceInBag)
        {
            OnBuyFail?.Invoke(BuyFailType.NoBagSpace);
            return;
        }

        OnBuySuccess?.Invoke();
    }
    public void TryBuy(SingleDiceObj singleDiceObj)
    {
        TryBuy();
    }

    public override void InitialProduct(SingleDiceObj product)
    {
        base.InitialProduct(product);
    }

    public override void ProductBrought()
    {
        base.ProductBrought();
        ChaState player = MapManager.Instance.playerChaState;
        player.resource.currentMoney -= product.value;
        player.GetBattleDiceHandler().AddSingleBattleDiceToBag(product);
    }

    private void CreatreUI()
    {
        //ChaState player = MapManager.Instance.playerChaState;
        //StoreAreaUIManager.Instance.CreateDiceUI(player.GetBattleDiceHandler().bagDiceCards.Count+1)
    }
}

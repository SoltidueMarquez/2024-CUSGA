using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductDice : ProductBase<BattleDice>
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    protected override void TryBuy()
    {
        //if ()
        //{
        //    OnBuyFail?.Invoke(BuyFailType.NoMoney);
        //    
        //}
        //else if ()
        //{
        //    OnBuyFail?.Invoke(BuyFailType.NoBagSpace);
        //    
        //}
        OnBuySuccess?.Invoke();

    }



    public override void InitialProduct(BattleDice product)
    {
        base.InitialProduct(product);
    }

    public override void ProductBrought()
    {
        base.ProductBrought();
    }
}

using System.Collections;
using System.Collections.Generic;
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
    }
}

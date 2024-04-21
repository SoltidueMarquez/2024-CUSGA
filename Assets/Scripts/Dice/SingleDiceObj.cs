using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff的runtime数据
/// </summary>
public class SingleDiceObj
{
    public SingleDiceModel model;
    /// <summary>
    /// 在骰子中的id,准确的说是点数，因为是runtime，所以这边的点数也是可以变化的
    /// </summary>
    public int idInDice;
    /// <summary>
    /// 等级，用于计算伤害，因为是runtime，所以这边的等级是可以变化的
    /// </summary>
    public int level;
    /// <summary>
    /// 购入的价格,用于卖出,因为是runtime，所以这边的购入价是可以变化的，由商店的逻辑控制
    /// </summary>
    public int value;
    /// <summary>
    /// 骰面在骰子中的位置,真正的 0 - 5
    /// </summary>
    public int positionInDice;
    /// <summary>
    /// 骰面的售价，初始化为价值的1/4
    /// </summary>
    public int SaleValue ;
    public SingleDiceObj(SingleDiceModel model, int idInDice)
    {
        this.model = model;
        this.idInDice = idInDice;
        this.value = model.value;
        this.level = model.level;
        SaleValue = Mathf.FloorToInt(value / 4);
    }
}

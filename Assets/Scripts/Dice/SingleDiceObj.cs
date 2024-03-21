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
    /// 在骰子中的id
    /// </summary>
    public int idInDice;
    /// <summary>
    /// 等级，用于计算伤害，因为是runtime，所以这边的等级是可以变化的
    /// </summary>
    public int level;
    /// <summary>
    /// 售价,用于卖出,因为是runtime，所以这边的售价是可以变化的
    /// </summary>
    public int value;
    
    public SingleDiceObj(SingleDiceModel model, int idInDice)
    {
        this.model = model;
        this.idInDice = idInDice;
        this.value = model.value;
        this.level = model.level;
    }
}

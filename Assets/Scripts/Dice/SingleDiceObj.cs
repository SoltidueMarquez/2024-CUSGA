using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// buff��runtime����
/// </summary>
public class SingleDiceObj
{
    public SingleDiceModel model;
    /// <summary>
    /// �������е�id
    /// </summary>
    public int idInDice;
    /// <summary>
    /// �ȼ�
    /// </summary>
    public int level;
    /// <summary>
    /// �ۼ�
    /// </summary>
    public int value;
    public SingleDiceObj(SingleDiceModel model, int idInDice)
    {
        this.model = model;
        this.idInDice = idInDice;
        this.value = model.value;
    }
}

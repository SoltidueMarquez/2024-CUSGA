using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDiceObj
{
    public SingleDiceModel model;

    public int idInDice;

    public SingleDiceObj(SingleDiceModel model, int idInDice)
    {
        this.model = model;
        this.idInDice = idInDice;
    }
}

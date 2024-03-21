using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDiceData
{
    public static Dictionary<string, SingleDiceModel> diceDictionary = new Dictionary<string, SingleDiceModel>()
    {
        {
            "Dice_1",new SingleDiceModel(0,DiceType.Attack,"normal1", "1",ChaResource.zero,ChaResource.zero, 1, 1,null
                ,null)
        },
        {
            "Dice_2",new SingleDiceModel(0,DiceType.Defense, "normal2", "2", ChaResource.zero, ChaResource.zero, 1, 1, null
            , null)
        },
        {
            "Dice_3",new SingleDiceModel(0,DiceType.Support, "normal3", "3", ChaResource.zero, ChaResource.zero, 1, 1, null
            , null)
        },
        {
            "Dice_4",new SingleDiceModel(1,DiceType.Attack, "normal4", "4", ChaResource.zero, ChaResource.zero, 1, 1, null
            , null)
        },
        {
            "Dice_5",new SingleDiceModel(1,DiceType.Attack, "normal5", "5", ChaResource.zero, ChaResource.zero, 1, 1, null
            , null)
        },

    };
}

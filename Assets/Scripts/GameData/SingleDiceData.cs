using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDiceData 
{
    public static Dictionary<string, SingleDiceModel> diceDictionary = new Dictionary<string, SingleDiceModel>()
    {
        {
            "Dice_1",new SingleDiceModel(DiceType.Attack,"normal1","1",ChaResource.zero,ChaResource.zero,new Damage(1,1),1,1,new BuffInfo[]
            {
                new BuffInfo(BuffDataTable.buffData["test"],null,null,1,false,null)
            }
                ,null)
        },
        {
            "Dice_2",new SingleDiceModel(DiceType.Attack, "normal2", "2", ChaResource.zero, ChaResource.zero, new Damage(1, 1), 1, 1, null, null)
        },
        {
            "Dice_3",new SingleDiceModel(DiceType.Attack, "normal3", "3", ChaResource.zero, ChaResource.zero, new Damage(1, 1), 1, 1, null, null)
        },
        {
            "Dice_4",new SingleDiceModel(DiceType.Attack, "normal4", "4", ChaResource.zero, ChaResource.zero, new Damage(1, 1), 1, 1, null, null)
        },
        {
            "Dice_5",new SingleDiceModel(DiceType.Attack, "normal5", "5", ChaResource.zero, ChaResource.zero, new Damage(1, 1), 1, 1, null, null)
        },
        {
            "Dice_6",new SingleDiceModel(DiceType.Attack, "normal6", "6", ChaResource.zero, ChaResource.zero, new Damage(1, 1), 1, 1, null, null)
        },
        
    };
}

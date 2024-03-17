using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDiceData 
{
    public static Dictionary<string, SingleDiceModel> diceDictionary = new Dictionary<string, SingleDiceModel>()
    {
        {
            "Dice_1",new SingleDiceModel("1",ChaResource.zero,ChaResource.zero,new Damage(1,1),1,1,null,null)
        },
        {
            "Dice_2",new SingleDiceModel("2",ChaResource.zero,ChaResource.zero,new Damage(1,1),1,1,null,null)
        },
        {
            "Dice_3",new SingleDiceModel("3",ChaResource.zero,ChaResource.zero,new Damage(1,1),1,1,null,null)
        },
        {
            "Dice_4",new SingleDiceModel("4",ChaResource.zero,ChaResource.zero,new Damage(1,1),1,1,null,null)
        },
        {
            "Dice_5",new SingleDiceModel("5",ChaResource.zero,ChaResource.zero,new Damage(1,1),1,1,null,null)
        },
        {
            "Dice_6",new SingleDiceModel("6",ChaResource.zero,ChaResource.zero,new Damage(1,1),1,1,null,null)
        }
    };
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SingleDiceModel singleDiceModel = RandomManager.Instance.GetSingleDiceModel(DiceType.Attack, 1, 1);
        SingleDiceObj singleDiceObj = new SingleDiceObj(singleDiceModel, 1);
        BattleManager.Instance.AddSingleDiceToPlayerBag(singleDiceObj);
    }

}

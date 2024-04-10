using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugTest : MonoBehaviour
{
    [SerializeField] private ChaState state;
    [SerializeField] private HalidomObject halidomObject;
    private void Start()
    {
        //输出所有骰面model

        RandomManager.GetSingleDiceModel(DiceType.Attack, 1, 1);
    }
    
}

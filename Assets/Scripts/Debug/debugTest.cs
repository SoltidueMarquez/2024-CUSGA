using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugTest : MonoBehaviour
{
    [SerializeField] private ChaState state;
    [SerializeField] private HalidomObject halidomObject;
    private void Start()
    {
        //�����������model

        RandomManager.Instance.GetSingleDiceModel(DiceType.Attack, 1, 1);
    }
    
}

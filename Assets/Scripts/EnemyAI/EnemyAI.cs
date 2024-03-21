using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private ChaState characterState;
    private void Start()
    {
        characterState = GetComponent<ChaState>();
    }


}

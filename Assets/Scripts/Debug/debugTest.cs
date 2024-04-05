using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugTest : MonoBehaviour
{
    [SerializeField] private ChaState state;
    [SerializeField] private HalidomObject halidomObject;
    private void Start()
    {
        halidomObject = DesignerScripts.HalidomData.halidomDictionary["Test"];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int i = (int)halidomObject.buffInfos[0].buffParam["testInt"];
            Debug.Log(i);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProducts : MonoBehaviour
{
    private bool isInit = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TestProducts");

        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            BattleStoreManager.Instance.OnEnterStore?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            BattleStoreManager.Instance.OnExitStore.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("按下空格");
            BattleStoreManager.Instance.OnRefreshStore.Invoke(BattleStoreManager.Instance.battleProducts);
        }
       


    }
}

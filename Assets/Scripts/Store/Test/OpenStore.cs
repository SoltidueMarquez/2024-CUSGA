using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenStore : MonoBehaviour
{
    public void OnClick()
    {
        StoreManager.Instance.OnEnterStore?.Invoke();
    }
}

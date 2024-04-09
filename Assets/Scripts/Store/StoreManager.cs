using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame.Core;
using UnityEngine.Events;

public class StoreManager : SingletonBase<StoreManager>
{
    public bool enableDebug = false;
    public GameObject StoreUIcanvas;
    protected override void Awake()
    {
        base.Awake();
        if (StoreUIcanvas != null)
        {
            StoreUIcanvas = transform.Find("Canvas").gameObject;
        }
        StoreUIcanvas.SetActive(false);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    // Start is called before the first frame update
    void Start()
    {
        OnEnterStore.AddListener(OpenStore);
        OnExitStore.AddListener(CloseStore);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 进入商店时调用
    /// </summary>
    public UnityEvent OnEnterStore;
    /// <summary>
    /// 离开商店时调用
    /// </summary>
    public UnityEvent OnExitStore;
    /// <summary>
    /// 点击强化点数按钮时调用
    /// </summary>
    public UnityEvent OnClickUpgrade;
    /// <summary>
    /// 点击重投按钮时调用
    /// </summary>
    public UnityEvent OnClickReroll;
    /// <summary>
    /// 刷新商品店时调用
    /// </summary>
    public UnityEvent OnRefreshStore;

    public void m_Debug(string log)
    {
        if (enableDebug)
        {
            Debug.Log("Store: " + log);
        }
    }

    private void OpenStore()
    {
        StoreUIcanvas.SetActive(true);
        OnRefreshStore?.Invoke();
    }

    private void CloseStore()
    {
        StartCoroutine(LateCloseStore());
    }
    IEnumerator LateCloseStore()
    {
        yield return new WaitForSeconds(2);
        StoreUIcanvas.SetActive(false);
    }
}

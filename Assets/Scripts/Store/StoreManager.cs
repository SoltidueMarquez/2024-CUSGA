using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Frame.Core;
using UnityEngine.Events;

public class StoreManager : SingletonBase<StoreManager>
{
    public bool enableDebug = false;
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// �����̵�ʱ����
    /// </summary>
    public UnityEvent OnEnterStore;
    /// <summary>
    /// �뿪�̵�ʱ����
    /// </summary>
    public UnityEvent OnExitStore;
    /// <summary>
    /// ���ǿ��������ťʱ����
    /// </summary>
    public UnityEvent OnClickUpgrade;
    /// <summary>
    /// �����Ͷ��ťʱ����
    /// </summary>
    public UnityEvent OnClickReroll;
    /// <summary>
    /// ˢ����Ʒ��ʱ����
    /// </summary>
    public UnityEvent OnRefreshStore;

    public void m_Debug(string log)
    {
        if (enableDebug)
        {
            Debug.Log("Store: " + log);
        }
    }
}

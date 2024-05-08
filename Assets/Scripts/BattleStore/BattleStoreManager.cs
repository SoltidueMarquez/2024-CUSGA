using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleStoreManager : MonoSingleton<BattleStoreManager>
{
    public List<BaseBattleProduct> battleProducts = new List<BaseBattleProduct>();
    public List<BaseBattleProduct> broughtBattleProducts = new List<BaseBattleProduct>();

    [Header("事件")]
    /// <summary>
    /// 进入商店时调用
    /// </summary>
    public UnityEvent OnEnterStore;
    /// <summary>
    /// 离开商店时调用
    /// </summary>
    public UnityEvent OnExitStore;

    private void CloseStore()
    {

    }

    private void OpenStore()
    {

    }

    private void RerollShop()
    {

    }
}

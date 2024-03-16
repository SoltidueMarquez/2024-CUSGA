using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalidomManager : MonoBehaviour
{
    /// <summary>
    /// 存放所有圣物的List
    /// </summary>
    [Header("圣物列表")]
    public List<HalidomObject> halidomList;

    public int halidomMaxCount;

    public void AddHalidom(HalidomObject halidom)
    {
        halidomList.Add(halidom);
    }
   //TODO排序算法

}

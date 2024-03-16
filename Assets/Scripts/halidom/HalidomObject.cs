using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalidomObject:MonoBehaviour
{
    /// <summary>
    /// 圣物唯一id标识
    /// </summary>
    public string id;
    /// <summary>
    /// 圣物名称
    /// </summary>
    public string halidomName;
    /// <summary>
    /// 圣物描述
    /// </summary>
    public string description;
    /// <summary>
    /// 圣物身上存的buff信息
    /// </summary>
    public List<BuffInfo> buffInfo;
    /// <summary>
    /// 圣物在格子中的序号
    /// </summary>
    public int halidomIndex;
    

    public HalidomObject(string id, string halidomName, string description,  List<BuffInfo> buffinfo )
    {
        this.id = id;
        this.halidomName = halidomName;
        this.description = description;
        this.buffInfo = buffinfo;
    }

    
}





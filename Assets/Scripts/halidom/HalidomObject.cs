using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalidomObject
{
    public string id;
    public string halidomName;
    public string description;
    
    public List<BuffInfo> buffInfo;

    public HalidomObject(string id, string halidomName, string description,  List<BuffInfo> buffinfo )
    {
        this.id = id;
        this.halidomName = halidomName;
        this.description = description;
        
        this.buffInfo = buffinfo;
    }
}





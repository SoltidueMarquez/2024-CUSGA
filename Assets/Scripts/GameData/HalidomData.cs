using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HalidomData
{
    public static Dictionary<string, HalidomObject> halidomDictionary = new Dictionary<string, HalidomObject>() {
        {
            //Halidom模板
            "Halidom_1",new HalidomObject("1","Halidom_1","this is something",new List<BuffInfo>()
            {
                 new BuffInfo(null,null,null,1,false),
                 new BuffInfo(null,null,null,1,false)
            })
        }


    };
}

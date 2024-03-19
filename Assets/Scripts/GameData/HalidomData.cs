using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HalidomData
{
    public static Dictionary<string, HalidomObject> halidomDictionary = new Dictionary<string, HalidomObject>() {
        {
            //Halidom模板
            
            "Halidom_1",new HalidomObject(
                //Halidom的唯一id
                "1",
                //Halidom的名字
                "Halidom_1",
                //Halidom的描述
                "this is something",
                //Halidom的BuffInfo List
                new List<BuffInfo>()
                    {
                        new BuffInfo(BuffDataTable.buffData["CheckMoneyAddHealth"],null,null,1,false,null)
                        
                    })
        }

        


    };
}

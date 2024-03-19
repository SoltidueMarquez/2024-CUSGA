using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DesignerScripts
{
    public class BuffDataTable
    {
        public static Dictionary<string, BuffData> buffData = new Dictionary<string, BuffData>()
        {
            { "test",new BuffData("1","TestBuff1","icon1",null,3,2,false,BuffUpdateEnum.Keep,BuffRemoveStackUpdateEnum.Clear,
            "test1",null,
            null,null,
            null,null,
            null,null,
            null,null,
            null,null,
            null,null,
            null,null,
            null,null,
            ChaControlState.origin,null
            ) 
            }
        };

    }

}

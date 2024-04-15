using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalidomTest : MonoBehaviour
{
    public List<HalidomDataSO> halidomSOData;

    private bool isRun = false;
    //根据data找字典中的数据
    //将字典中的数据加入halidom manager
    private void Update()
    {
        AddHalidomAtFirstFrame();
        //AddHalidom();
    }

    public void AddHalidomAtFirstFrame()
    {
        //判断是否是第一帧
        if (!isRun)
        {
            //遍历halidomSO列表
            foreach(var halidom in halidomSOData)
            {
                //找halidom字典里是否有这个键
                if(HalidomData.halidomDictionary.ContainsKey(halidom.halidomName.ToString()))
                {
                    HalidomManager.Instance.AddHalidom(HalidomData.halidomDictionary[halidom.halidomName.ToString()]);
                }
                
            }
            isRun = true;
        }
    }

    public void AddHalidom()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            if(!isRun)
            {
                foreach (var halidom in halidomSOData)
                {
                    if (HalidomData.halidomDictionary.ContainsKey(halidom.halidomName.ToString()))
                    {
                        HalidomManager.Instance.AddHalidom(HalidomData.halidomDictionary[halidom.halidomName.ToString()]);
                    }
                }
            }
            
            isRun = true;
        }
    }

    
}

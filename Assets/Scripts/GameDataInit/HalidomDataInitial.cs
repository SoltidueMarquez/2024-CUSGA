using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignerScripts;

public class HalidomDataInitial : MonoBehaviour
{
    private HalidomDataSO[] halidomDataSos;
    // Start is called before the first frame update
    private void Awake()
    {
        halidomDataSos = Resources.LoadAll<HalidomDataSO>("Data/HalidomData");
        for (int i = 0; i < halidomDataSos.Length; i++)
        {
            if (HalidomData.halidomDictionary.ContainsKey(halidomDataSos[i].halidomName.ToString()))
            {
                Debug.LogWarning("HalidomDataInitial:试图添加重复的HalidomData");
                continue;
            }
            HalidomData.halidomDictionary.Add(halidomDataSos[i].halidomName.ToString(),
                new HalidomObject(
                    halidomDataSos[i].rareType,
                    halidomDataSos[i].id,
                    halidomDataSos[i].halidomName.ToString(),
                    halidomDataSos[i].description,
                    halidomDataSos[i].value,
                    GetBuffInfoList(halidomDataSos[i].buffDataSos))
                );
        }
    }


    /// <summary>
    /// csy: create a new buffInfo list with buffDataSO list
    /// </summary>
    /// <param name="buffDataSOs"></param>
    /// <returns></returns>
    private List<BuffInfo> GetBuffInfoList(List<BuffDataSO> buffDataSOs)
    {
        if (buffDataSOs == null)
        {
            Debug.LogWarning("HalidomDataInitial:传入参数为空");
            return null;
        }
        List<BuffInfo> buffInfos= new List<BuffInfo>();
        foreach (var item in buffDataSOs)
        {
            Dictionary<string,System.Object> dict = new Dictionary<string,System.Object>();
            foreach (var param in item.paramList)
            {
                switch (param.type)
                {
                    case paramsType.intType:
                        dynamic temp = int.Parse(param.value);
                        dict.Add(param.name,temp);
                        break;
                    case paramsType.floatType:
                        dynamic temp2 = float.Parse(param.value);
                        dict.Add(param.name, temp2);
                        break;
                    case paramsType.stringType:
                        dynamic temp3 =param.value;
                        dict.Add(param.name, temp3);
                        break;
                    case paramsType.boolType:
                        dynamic temp4 =bool.Parse(param.value);
                        dict.Add(param.name, temp4);
                        break;
                    
                }
            }
            BuffInfo buffInfo = new BuffInfo(
                BuffDataTable.buffData[item.dataName.ToString()],
                null,null,1,
                BuffDataTable.buffData[item.dataName.ToString()].isPermanent,dict
                );
            buffInfos.Add(buffInfo);
        }
        return buffInfos;
    }
}

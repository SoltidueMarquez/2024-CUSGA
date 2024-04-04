using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignerScripts;

public class HalidomDataInitial : MonoBehaviour
{
    private HalidomDataSO[] halidomDataSos;
    // Start is called before the first frame update
    private void Start()
    {
        halidomDataSos = Resources.LoadAll<HalidomDataSO>("Data/HalidomData");
        for (int i = 0; i < halidomDataSos.Length; i++)
        {
            if (HalidomData.halidomDictionary.ContainsKey(halidomDataSos[i].halidomName.ToString()))
            {
                Debug.LogWarning("HalidomDataInitial:��ͼ����ظ���HalidomData");
                continue;
            }
            HalidomData.halidomDictionary.Add(halidomDataSos[i].halidomName.ToString(),
                new HalidomObject(
                    halidomDataSos[i].rareType,
                    halidomDataSos[i].id,
                    halidomDataSos[i].halidomName.ToString(),
                    halidomDataSos[i].description,
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
            Debug.LogWarning("HalidomDataInitial:�������Ϊ��");
            return null;
        }

        List<BuffInfo> buffInfos= new List<BuffInfo>();
        foreach (var item in buffDataSOs)
        {
            BuffInfo buffInfo = new BuffInfo(
                BuffDataTable.buffData[item.dataName.ToString()],
                null,null,1,
                BuffDataTable.buffData[item.dataName.ToString()].isPermanent,null
                );
            buffInfos.Add(buffInfo);
        }
        return buffInfos;
    }
}

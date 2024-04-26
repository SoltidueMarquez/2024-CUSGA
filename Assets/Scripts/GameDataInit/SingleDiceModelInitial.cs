using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDiceModelInitial : MonoBehaviour
{
    private SingleDiceModelSO[] singleDiceModelSOs;
    public void Start()
    {
        singleDiceModelSOs = Resources.LoadAll<SingleDiceModelSO>("Data/SingleDiceData");
        for (int i = 0; i < singleDiceModelSOs.Length; i++)
        {
            if (SingleDiceData.diceDictionary.ContainsKey(singleDiceModelSOs[i].singleDiceModelName.ToString()))
            {
                Debug.LogWarning("SingleDiceModelInitial:试图添加重复的SingleDiceModel");
                continue;
            }
            SingleDiceData.diceDictionary.Add(singleDiceModelSOs[i].singleDiceModelName.ToString(),
                new SingleDiceModel(
                    singleDiceModelSOs[i].side,
                    singleDiceModelSOs[i].type,
                    singleDiceModelSOs[i].singleDiceModelName.ToString(),
                    singleDiceModelSOs[i].id,
                    singleDiceModelSOs[i].condition,
                    singleDiceModelSOs[i].cost,
                    singleDiceModelSOs[i].value,
                    (int)singleDiceModelSOs[i].level + 1,
                    GetBuffInfoList(singleDiceModelSOs[i].buffDataConfigs),
                    singleDiceModelSOs[i].baseValue,
                    null)
                );
        }

    }



    /// <summary>
    /// csy: create a new buffInfo list with buffDataSO list
    /// </summary>
    /// <param name="buffDataSOs"></param>
    /// <returns></returns>
    private BuffInfo[] GetBuffInfoList(List<BuffDataConfig> buffDataConfigs)
    {
        if (buffDataConfigs == null)
        {
            Debug.LogWarning("HalidomDataInitial:传入参数为空");
            return null;
        }

        List<BuffInfo> buffInfos = new List<BuffInfo>();

        foreach (var item in buffDataConfigs)
        {
            Dictionary<string, System.Object> dict = new Dictionary<string, System.Object>();
            foreach (var param in item.buffDataSO.paramList)
            {
                switch (param.type)
                {
                    case paramsType.intType:
                        dynamic temp = int.Parse(param.value);
                        dict.Add(param.name, temp);
                        break;
                    case paramsType.floatType:
                        dynamic temp2 = float.Parse(param.value);
                        dict.Add(param.name, temp2);
                        break;
                    case paramsType.stringType:
                        dynamic temp3 = param.value;
                        dict.Add(param.name, temp3);
                        break;
                    case paramsType.boolType:
                        dynamic temp4 = bool.Parse(param.value);
                        dict.Add(param.name, temp4);
                        break;

                }
            }
            BuffInfo buffInfo = new BuffInfo(
                BuffDataTable.buffData[item.buffDataSO.dataName.ToString()],
                null, null, item.buffStack,
                BuffDataTable.buffData[item.buffDataSO.dataName.ToString()].isPermanent, dict
                );
            buffInfos.Add(buffInfo);
        }
        return buffInfos.ToArray();
    }
}



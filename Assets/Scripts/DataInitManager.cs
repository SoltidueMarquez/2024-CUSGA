using Cysharp.Threading.Tasks;
using DesignerScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataInitManager : MonoBehaviour
{
    public bool ifAsync;
    public BuffDataTable buffDataTable;
    public HalidomData halidomDataTable;
    public SingleDiceData singleDiceDataTable;
    private BuffDataSO[] buffDataSos;
    private HalidomDataSO[] halidomDataSos;
    private SingleDiceModelSO[] singleDiceModelSOs;
    public BaseBattleProduct[] battleProductArray;
    public static DataInitManager Instance { get; private set; }
    public void Awake()
    {
        Instance = this;
        buffDataTable = new BuffDataTable();
        halidomDataTable = new HalidomData();
        singleDiceDataTable = new SingleDiceData();
    }
    private void Start()
    {
        DontDestroyOnLoad(this);
        if (ifAsync)
        {
            LoadGameDataAsync();
        }
        else
        {
            LoadGameData();
        }
    }
    public async void LoadGameDataAsync()
    {
        await LoadBuffDataAsync();
        await LoadHalidomDataAysnc();
        await LoadSingleDiceModelAsync();
        await LoadBattleProductAsync();
        Debug.Log("success");

    }
    public void LoadGameData()
    {
        LoadBuffData();
        LoadSingleDiceModel();
        LoadHalidomData();
        LoadBattleProduct();
        Debug.Log("success");
    }
    public async UniTask LoadBuffDataAsync()
    {
        try
        {
            buffDataSos = Resources.LoadAll<BuffDataSO>("Data/BuffData");
            for (int i = 0; i < buffDataSos.Length; i++)
            {
                if (DataInitManager.Instance.buffDataTable.buffData.ContainsKey(buffDataSos[i].dataName.ToString()))
                {
                    Debug.LogWarning("BuffDataInitial:试图添加重复的buff");
                    continue;
                }
                DataInitManager.Instance.buffDataTable.buffData.Add(buffDataSos[i].dataName.ToString(),
                    new BuffData(
                    buffDataSos[i].id,
                    buffDataSos[i].dataName.ToString(),
                    "icon" + buffDataSos[i].id,
                    buffDataSos[i].tags,
                    buffDataSos[i].maxStack,
                    buffDataSos[i].duringCount,
                    buffDataSos[i].isPermanent,
                    buffDataSos[i].buffUpdateEnum,
                    buffDataSos[i].removeStackUpdateEnum,
                    buffDataSos[i].onCreate == 0 ? "" : buffDataSos[i].onCreate.ToString(), buffDataSos[i].onCreateParams,
                    buffDataSos[i].onRemove == 0 ? "" : buffDataSos[i].onRemove.ToString(), buffDataSos[i].onRemoveParams,
                    buffDataSos[i].onRoundStart == 0 ? "" : buffDataSos[i].onRoundStart.ToString(), buffDataSos[i].onRoundEndParams,
                    buffDataSos[i].onRoundEnd == 0 ? "" : buffDataSos[i].onRoundEnd.ToString(), buffDataSos[i].onRoundEndParams,
                    buffDataSos[i].onHit == 0 ? "" : buffDataSos[i].onHit.ToString(), buffDataSos[i].onHitParams,
                    buffDataSos[i].onBeHurt == 0 ? "" : buffDataSos[i].onBeHurt.ToString(), buffDataSos[i].onBeHurtParams,
                    buffDataSos[i].onRoll == 0 ? "" : buffDataSos[i].onRoll.ToString(), buffDataSos[i].onRollParams,
                    buffDataSos[i].onKill == 0 ? "" : buffDataSos[i].onKill.ToString(), buffDataSos[i].onKillParams,
                    buffDataSos[i].onBeKilled == 0 ? "" : buffDataSos[i].onBeKilled.ToString(), buffDataSos[i].onBeKilledParams,
                    buffDataSos[i].onCast == 0 ? "" : buffDataSos[i].onCast.ToString(), buffDataSos[i].onCastParams,
                    buffDataSos[i].onAddBuff == 0 ? "" : buffDataSos[i].onAddBuff.ToString(), buffDataSos[i].onAddBuffParams,
                    buffDataSos[i].onGetFinalDamage == 0 ? "" : buffDataSos[i].onGetFinalDamage.ToString(), buffDataSos[i].onGetFinalDamageParams,
                    buffDataSos[i].stateMod,
                    buffDataSos[i].propMod)
                    );
                //Debug.Log(i);
                await UniTask.Delay(1);

            }
        }
        catch (Exception e)
        {
            Debug.LogError("An exception occurred: " + e.Message);
        }
    }
    public void LoadBuffData()
    {
        buffDataSos = Resources.LoadAll<BuffDataSO>("Data/BuffData");
        for (int i = 0; i < buffDataSos.Length; i++)
        {
            if (DataInitManager.Instance.buffDataTable.buffData.ContainsKey(buffDataSos[i].dataName.ToString()))
            {
                Debug.LogWarning("BuffDataInitial:试图添加重复的buff");
                continue;
            }
            DataInitManager.Instance.buffDataTable.buffData.Add(buffDataSos[i].dataName.ToString(),
                new BuffData(
                buffDataSos[i].id,
                buffDataSos[i].dataName.ToString(),
                "icon" + buffDataSos[i].id,
                buffDataSos[i].tags,
                buffDataSos[i].maxStack,
                buffDataSos[i].duringCount,
                buffDataSos[i].isPermanent,
                buffDataSos[i].buffUpdateEnum,
                buffDataSos[i].removeStackUpdateEnum,
                buffDataSos[i].onCreate == 0 ? "" : buffDataSos[i].onCreate.ToString(), buffDataSos[i].onCreateParams,
                buffDataSos[i].onRemove == 0 ? "" : buffDataSos[i].onRemove.ToString(), buffDataSos[i].onRemoveParams,
                buffDataSos[i].onRoundStart == 0 ? "" : buffDataSos[i].onRoundStart.ToString(), buffDataSos[i].onRoundEndParams,
                buffDataSos[i].onRoundEnd == 0 ? "" : buffDataSos[i].onRoundEnd.ToString(), buffDataSos[i].onRoundEndParams,
                buffDataSos[i].onHit == 0 ? "" : buffDataSos[i].onHit.ToString(), buffDataSos[i].onHitParams,
                buffDataSos[i].onBeHurt == 0 ? "" : buffDataSos[i].onBeHurt.ToString(), buffDataSos[i].onBeHurtParams,
                buffDataSos[i].onRoll == 0 ? "" : buffDataSos[i].onRoll.ToString(), buffDataSos[i].onRollParams,
                buffDataSos[i].onKill == 0 ? "" : buffDataSos[i].onKill.ToString(), buffDataSos[i].onKillParams,
                buffDataSos[i].onBeKilled == 0 ? "" : buffDataSos[i].onBeKilled.ToString(), buffDataSos[i].onBeKilledParams,
                buffDataSos[i].onCast == 0 ? "" : buffDataSos[i].onCast.ToString(), buffDataSos[i].onCastParams,
                buffDataSos[i].onAddBuff == 0 ? "" : buffDataSos[i].onAddBuff.ToString(), buffDataSos[i].onAddBuffParams,
                buffDataSos[i].onGetFinalDamage == 0 ? "" : buffDataSos[i].onGetFinalDamage.ToString(), buffDataSos[i].onGetFinalDamageParams,
                buffDataSos[i].stateMod,
                buffDataSos[i].propMod)
                );
        }
    }
    #region 骰面
    public async UniTask LoadSingleDiceModelAsync()
    {
        try
        {
            singleDiceModelSOs = Resources.LoadAll<SingleDiceModelSO>("Data/SingleDiceData");
            for (int i = 0; i < singleDiceModelSOs.Length; i++)
            {
                if (DataInitManager.Instance.singleDiceDataTable.diceDictionary.ContainsKey(singleDiceModelSOs[i].singleDiceModelName.ToString()))
                {
                    Debug.LogWarning("SingleDiceModelInitial:试图添加重复的SingleDiceModel");
                    continue;
                }
                DataInitManager.Instance.singleDiceDataTable.diceDictionary.Add(singleDiceModelSOs[i].singleDiceModelName.ToString(),
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
                //Debug.Log(i);

                await UniTask.Delay(1);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("An exception occurred: " + e.Message);
        }

    }
    public void LoadSingleDiceModel()
    {
        singleDiceModelSOs = Resources.LoadAll<SingleDiceModelSO>("Data/SingleDiceData");
        for (int i = 0; i < singleDiceModelSOs.Length; i++)
        {
            if (DataInitManager.Instance.singleDiceDataTable.diceDictionary.ContainsKey(singleDiceModelSOs[i].singleDiceModelName.ToString()))
            {
                Debug.LogWarning("SingleDiceModelInitial:试图添加重复的SingleDiceModel");
                continue;
            }
            DataInitManager.Instance.singleDiceDataTable.diceDictionary.Add(singleDiceModelSOs[i].singleDiceModelName.ToString(),
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
            //Debug.Log(i);
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
                DataInitManager.Instance.buffDataTable.buffData[item.buffDataSO.dataName.ToString()],
                null, null, item.buffStack,
                DataInitManager.Instance.buffDataTable.buffData[item.buffDataSO.dataName.ToString()].isPermanent, dict
                );
            buffInfos.Add(buffInfo);
        }
        return buffInfos.ToArray();
    }
    #endregion
    #region 圣物初始化
    public async UniTask LoadHalidomDataAysnc()
    {
        try
        {
            halidomDataSos = Resources.LoadAll<HalidomDataSO>("Data/HalidomData");
            for (int i = 0; i < halidomDataSos.Length; i++)
            {
                if (DataInitManager.Instance.halidomDataTable.halidomDictionary.ContainsKey(halidomDataSos[i].halidomName.ToString()))
                {
                    Debug.LogWarning("HalidomDataInitial:试图添加重复的HalidomData");
                    continue;
                }
                DataInitManager.Instance.halidomDataTable.halidomDictionary.Add(halidomDataSos[i].halidomName.ToString(),
                    new HalidomObject(
                        halidomDataSos[i].rareType,
                        halidomDataSos[i].id,
                        halidomDataSos[i].halidomName.ToString(),
                        halidomDataSos[i].description,
                        halidomDataSos[i].value,
                        GetBuffInfoList(halidomDataSos[i].buffDataSos))
                    );
                //Debug.Log(i);

                await UniTask.Delay(1);

            }
        }
        catch (Exception e)
        {
            Debug.LogError("An exception occurred: " + e.Message);
        }
    }

    public void LoadHalidomData()
    {
        halidomDataSos = Resources.LoadAll<HalidomDataSO>("Data/HalidomData");
        for (int i = 0; i < halidomDataSos.Length; i++)
        {
            if (DataInitManager.Instance.halidomDataTable.halidomDictionary.ContainsKey(halidomDataSos[i].halidomName.ToString()))
            {
                Debug.LogWarning("HalidomDataInitial:试图添加重复的HalidomData");
                continue;
            }
            DataInitManager.Instance.halidomDataTable.halidomDictionary.Add(halidomDataSos[i].halidomName.ToString(),
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
        List<BuffInfo> buffInfos = new List<BuffInfo>();
        foreach (var item in buffDataSOs)
        {
            Dictionary<string, System.Object> dict = BuffDataSO.GetParamDic(item.paramList);
            BuffInfo buffInfo = new BuffInfo(
                DataInitManager.Instance.buffDataTable.buffData[item.dataName.ToString()],
                null, null, 1,
                DataInitManager.Instance.buffDataTable.buffData[item.dataName.ToString()].isPermanent, dict
                );
            buffInfos.Add(buffInfo);
        }
        return buffInfos;
    }
    #endregion

    public async UniTask LoadBattleProductAsync()
    {
        try
        {
            battleProductArray = Resources.LoadAll<BaseBattleProduct>("Data/BattleStoreData");
            
            await UniTask.Delay(1);

        }
        catch (Exception e)
        {
            Debug.LogError("An exception occurred: " + e.Message);
        }

    }

    public void LoadBattleProduct()
    {
        battleProductArray = Resources.LoadAll<BaseBattleProduct>("Data/BattleStoreData");
        
    }
}

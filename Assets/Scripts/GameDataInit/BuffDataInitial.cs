using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DesignerScripts;
using Cysharp.Threading.Tasks;

public class BuffDataInitial : MonoSingleton<BuffDataInitial>
{
    private BuffDataSO[] buffDataSos;
    // Start is called before the first frame update
    public override void Awake()
    {
        base.Awake();
        //buffDataSos = Resources.LoadAll<BuffDataSO>("Data/BuffData");
        //for (int i = 0; i < buffDataSos.Length; i++)
        //{
        //    if (BuffDataTable.buffData.ContainsKey(buffDataSos[i].dataName.ToString()))
        //    {
        //        Debug.LogWarning("BuffDataInitial:试图添加重复的buff");
        //        continue;
        //    }
        //    BuffDataTable.buffData.Add(buffDataSos[i].dataName.ToString(),
        //        new BuffData(
        //        buffDataSos[i].id,
        //        buffDataSos[i].dataName.ToString(),
        //        "icon" + buffDataSos[i].id,
        //        buffDataSos[i].tags,
        //        buffDataSos[i].maxStack,
        //        buffDataSos[i].duringCount,
        //        buffDataSos[i].isPermanent,
        //        buffDataSos[i].buffUpdateEnum,
        //        buffDataSos[i].removeStackUpdateEnum,
        //        buffDataSos[i].onCreate == 0 ? "" : buffDataSos[i].onCreate.ToString(), buffDataSos[i].onCreateParams,
        //        buffDataSos[i].onRemove == 0 ? "" : buffDataSos[i].onRemove.ToString(), buffDataSos[i].onRemoveParams,
        //        buffDataSos[i].onRoundStart == 0 ? "" : buffDataSos[i].onRoundStart.ToString(), buffDataSos[i].onRoundEndParams,
        //        buffDataSos[i].onRoundEnd == 0 ? "" : buffDataSos[i].onRoundEnd.ToString(), buffDataSos[i].onRoundEndParams,
        //        buffDataSos[i].onHit == 0 ? "" : buffDataSos[i].onHit.ToString(), buffDataSos[i].onHitParams,
        //        buffDataSos[i].onBeHurt == 0 ? "" : buffDataSos[i].onBeHurt.ToString(), buffDataSos[i].onBeHurtParams,
        //        buffDataSos[i].onRoll == 0 ? "" : buffDataSos[i].onRoll.ToString(), buffDataSos[i].onRollParams,
        //        buffDataSos[i].onKill == 0 ? "" : buffDataSos[i].onKill.ToString(), buffDataSos[i].onKillParams,
        //        buffDataSos[i].onBeKilled == 0 ? "" : buffDataSos[i].onBeKilled.ToString(), buffDataSos[i].onBeKilledParams,
        //        buffDataSos[i].onCast == 0 ? "" : buffDataSos[i].onCast.ToString(), buffDataSos[i].onCastParams,
        //        buffDataSos[i].onAddBuff == 0 ? "" : buffDataSos[i].onAddBuff.ToString(), buffDataSos[i].onAddBuffParams,
        //        buffDataSos[i].onGetFinalDamage == 0 ? "" : buffDataSos[i].onGetFinalDamage.ToString(), buffDataSos[i].onGetFinalDamageParams,
        //        buffDataSos[i].stateMod,
        //        buffDataSos[i].propMod)
        //        );
        //}
        //await LoadBuffDataAsync();

    }
    public async UniTask LoadBuffDataAsync()
    {
        buffDataSos = Resources.LoadAll<BuffDataSO>("Data/BuffData");
        for (int i = 0; i < buffDataSos.Length; i++)
        {
            if ( DataInitManager.Instance.buffDataTable.buffData.ContainsKey(buffDataSos[i].dataName.ToString()))
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
            Debug.Log(i);
            await UniTask.Delay(1);
            
        }
    }

}

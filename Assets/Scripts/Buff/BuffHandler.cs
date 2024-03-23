using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    [Description("Buff列表")]
    public List<BuffInfo> buffList = new List<BuffInfo>();

    public void AddBuff(BuffInfo buffInfo, GameObject creator)
    {
        BuffInfo findBuffInfo = buffList.Find(x => x.buffData.id == buffInfo.buffData.id);
        if (findBuffInfo != null)//获得相同的buff的情况
        {
            if (findBuffInfo.curStack < findBuffInfo.buffData.maxStack)
            {
                findBuffInfo.curStack++;
                //TODO:提示buff层数增加,根据buff的类型进行不同的处理
                findBuffInfo.buffData.onCreate?.Invoke(findBuffInfo);
            }
            else
            {
                //TODO:提示buff层数已满
            }
        }
        else
        {
            buffInfo.creator = creator;
            buffInfo.target = this.gameObject;
            buffInfo.buffData.onCreate?.Invoke(buffInfo);
            buffList.Add(buffInfo);
        }
    }

    /// <summary>
    /// 移除buff
    /// </summary>
    /// <param name="buffInfo"></param>
    public void RemoveBuff(BuffInfo buffInfo)
    {
        switch (buffInfo.buffData.removeStackUpdateEnum)
        {
            case BuffRemoveStackUpdateEnum.Clear:
                buffInfo.buffData.onRemove?.Invoke(buffInfo);
                buffList.Remove(buffInfo);
                break;
            case BuffRemoveStackUpdateEnum.Reduce:
                buffInfo.curStack--;
                buffInfo.buffData.onRemove?.Invoke(buffInfo);
                if (buffInfo.curStack == 0)
                {
                    buffList.Remove(buffInfo);
                }
                //TODO:关于buff层数减少的刷新
                break;
            default:
                break;
        }

    }
    /// <summary>
    /// 每个回合开始的时候，对buff的时间进行处理
    /// </summary>
    public void BuffRoundStartTick()
    {
        List<BuffInfo> removeList = new List<BuffInfo>();
        foreach (BuffInfo buffInfo in buffList)//遍历buff列表,先执行回合开始的事件
        {
            buffInfo.buffData.onRoundStart?.Invoke(buffInfo);
        }
        for (int i = 0; i < buffList.Count; i++)
        {
            if (buffList[i].isPermanent == false)//非永久buff
            {
                buffList[i].roundCount--;
                if (buffList[i].roundCount == 0)
                {
                    removeList.Add(buffList[i]);
                }
            }
        }

        for (int i = 0; i < removeList.Count; i++)
        {
            RemoveBuff(removeList[i]);
        }
    }
    //TODO:每个回合结束的时候，对buff的时间进行处理
    public void BuffRoundEndTick()
    {
        List<BuffInfo> removeList = new List<BuffInfo>();
        BuffInfo[] buffArray = buffList.ToArray();
        for (int i = 0; i < buffArray.Length; i++)
        {
            buffArray[i].buffData.onRoundEnd?.Invoke(buffArray[i]);
        }

        for (int i = 0; i < buffList.Count; i++)
        {
            if (buffList[i].isPermanent == false)//非永久buff
            {
                buffList[i].roundCount--;
                if (buffList[i].roundCount == 0)
                {
                    removeList.Add(buffList[i]);
                }
            }
        }
    }
    public void RecheckBuff(ChaProperty[] buffProp,ref ChaControlState chaControlState)
    {

        foreach (var buff in buffList)
        {
            buffProp[0] += buff.buffData.propMod[0] * buff.curStack;
            buffProp[1] += buff.buffData.propMod[1];
            chaControlState += buff.buffData.stateMod;
        }
    }
}

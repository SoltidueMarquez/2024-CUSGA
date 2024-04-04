using DesignerScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;


public class HalidomManager : MonoBehaviour
{
    /// <summary>
    /// 存放所有圣物的List
    /// </summary>
    [Header("圣物列表")]
    public HalidomObject[] halidomList;
    /// <summary>
    /// 最大圣物上限
    /// </summary>
    public int halidomMaxCount = 6;
    /// <summary>
    /// 记录上一次的玩家属性
    /// </summary>
    public ChaProperty currentCharacterProperty = ChaProperty.zero;
    /// <summary>
    /// 记录玩家属性的差值
    /// </summary>
    public ChaProperty deltaCharacterProperty = ChaProperty.zero;
    /// <summary>
    /// 存加和乘法的
    /// </summary>
    public ChaProperty[] buffProp = new ChaProperty[2] { ChaProperty.zero, ChaProperty.zero };
    /// <summary>
    /// 初始属性
    /// </summary>
    public ChaProperty baseProp = new ChaProperty(
        50, 400, 4, 0
    );
    /// <summary>
    /// 存放移除圣物的函数
    /// </summary>
    public Action<int> removeHalidomDelegate;



    public static HalidomManager Instance
    {
        get; private set;
    }


    private void Awake()
    {
        //单例模式
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // 否则，将自身设为实例，并保持在场景切换时不被销毁
        Instance = this;
        DontDestroyOnLoad(gameObject);

        //TODO:初始化圣物上限
        //设置数组上限为最大圣物上限
        halidomList = new HalidomObject[halidomMaxCount];
        //设置移除圣物的函数
        removeHalidomDelegate = RemoveHalidom;
    }

    private void Start()
    {
        //Test
        AddHalidom(HalidomData.halidomDictionary[HalidomName.偶数注.ToString()]);
        SacredObjectUIManager.Instance.CreateSacredUIObject(0, HalidomData.halidomDictionary[HalidomName.偶数注.ToString()].description, removeHalidomDelegate);
        AddHalidom(HalidomData.halidomDictionary[HalidomName.奇数注.ToString()]);
        SacredObjectUIManager.Instance.CreateSacredUIObject(1, HalidomData.halidomDictionary[HalidomName.奇数注.ToString()].description, removeHalidomDelegate);
        //打印圣物所有buff信息
        foreach(var halidom in halidomList)
        {
            if (halidom != null)
            {
                foreach (var buff in halidom.buffInfos)
                {
                    Debug.Log("圣物中的buff名称是"+buff.buffData.buffName);
                }
            }
        }
    }
    

    //配置圣物是不配圣物在格子里的顺序的，加进去的时候才获得序号

    public void AddHalidom(HalidomObject halidom)
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            //找到第一个空的格子
            if (halidomList[i] == null)
            {
                //将圣物加入圣物列表
                halidomList[i] = halidom;
                //获得圣物在格子中的序号
                halidom.halidomIndex = i ;
                //触发圣物OnCreate回调点
                foreach (var buffInfo in halidom.buffInfos)
                {
                    //获取圣物的创建者 给予buffinfo
                    buffInfo.creator = BattleManager.Instance.parameter.playerChaState.gameObject;
                    //获取圣物buff的对象（暂定 没有给敌人上buff）
                    buffInfo.target=BattleManager.Instance.parameter.enemyChaStates[0].gameObject;
                    //触发圣物的OnCreate回调点
                    buffInfo.buffData.onCreate?.Invoke(buffInfo);
                }
                RefreshAllHalidoms();
                BattleManager.Instance.parameter.playerChaState.AttrAndResourceRecheck();
                //找到空的格子后就跳出循环
                break;
            }
            else
            {
                Debug.Log("圣物格子已满");
            }
        }
    }
    //删去指定格子的圣物
    public void RemoveHalidom(int index)
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            //index为实际数组下标
            if (i == index )
            {
                //触发圣物OnRemove回调点
                foreach (var buffInfo in halidomList[i].buffInfos)
                {
                    buffInfo.buffData.onRemove?.Invoke(buffInfo);
                }
                //将圣物在格子中的序号置为0
                halidomList[i].halidomIndex = 0;
                //将圣物从圣物列表中移除
                halidomList[i] = null;
                //重新计算属性
                RefreshAllHalidoms();
                BattleManager.Instance.parameter.playerChaState.AttrAndResourceRecheck();

            }
        }
    }

    public void RefreshAllHalidoms()
    {

        //清空之前的加和乘
        for (var i = 0; i < buffProp.Length; i++)
        {
            buffProp[i].Zero();
        }
        //遍历圣物数组
        foreach (var halidom in halidomList)
        {
            //如果不为空
            if (halidom != null)
            {
                //遍历当前圣物的所有buffinfo
                foreach (var buffinfo in halidom.buffInfos)
                {
                    //计算相加和相乘
                    buffProp[0] += buffProp[0] += buffinfo.buffData.propMod[0] * buffinfo.curStack;
                    buffProp[1] += buffinfo.buffData.propMod[1];

                }
            }
        }
        //重新计算属性
        this.currentCharacterProperty = (this.baseProp + buffProp[0]) * this.buffProp[1];
        this.deltaCharacterProperty = this.currentCharacterProperty - this.baseProp;
    }


    public void SellHalidom(int index)
    {
        //获取圣物的售价
        int price = halidomList[index].value;
        //将圣物的售价加到玩家的金币上
        ChaState tempChaState =BattleManager.Instance.parameter.playerChaState;
        
        tempChaState.ModResources(new ChaResource(0, price, 0, 0));
        Debug.Log("售卖圣物成功，获得金币" + price + "个");
        //将圣物移除
        RemoveHalidom(index);

    }


    //交换圣物顺序
    public void SwapHalidom(int index1, int index2)
    {
        HalidomObject temp = halidomList[index1];
        halidomList[index1] = halidomList[index2];
        halidomList[index2] = temp;
    }
    //所有回调点触发Invoke
    public void OnRoundStart()
    {
        //存储要删除的buff
        List<BuffInfo> removeList = new List<BuffInfo>();
        for (int i = 0; i < halidomList.Length; i++)
        {
            if (halidomList[i] != null)
            {
                foreach (var buffInfo in halidomList[i].buffInfos)
                {
                    buffInfo.buffData.onRoundStart?.Invoke(buffInfo);
                    if (buffInfo.isPermanent == false)//非永久buff
                    {
                        buffInfo.roundCount--;
                        //Test
                        Debug.Log(buffInfo.roundCount);
                        if (buffInfo.roundCount == 0)
                        {
                            removeList.Add(buffInfo);
                        }
                    }
                }
                foreach(var removeBuff in removeList)
                {
                    switch (removeBuff.buffData.removeStackUpdateEnum)
                    {
                        case BuffRemoveStackUpdateEnum.Clear:
                            removeBuff.buffData.onRemove?.Invoke(removeBuff);
                            halidomList[i].buffInfos.Remove(removeBuff);
                            break;
                        case BuffRemoveStackUpdateEnum.Reduce:
                            removeBuff.curStack--;
                            removeBuff.buffData.onRemove?.Invoke(removeBuff);
                            if (removeBuff.curStack == 0)
                            {
                                halidomList[i].buffInfos.Remove(removeBuff);
                            }
                            //TODO:关于buff层数减少的刷新
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }

    public void OnRoundEnd()
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            if (halidomList[i] != null)
            {
                foreach (var buffInfo in halidomList[i].buffInfos)
                {
                    buffInfo.buffData.onRoundEnd?.Invoke(buffInfo);
                }
            }
        }
    }
    public void OnHit(DamageInfo damageInfo)
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            if (halidomList[i] != null)
            {
                foreach (var buffInfo in halidomList[i].buffInfos)
                {
                    buffInfo.buffData.onHit?.Invoke(buffInfo, damageInfo, damageInfo.defender);
                }
            }
        }
    }
    public void OnBeHurt(DamageInfo damageInfo)
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            if (halidomList[i] != null)
            {
                foreach (var buffInfo in halidomList[i].buffInfos)
                {
                    buffInfo.buffData.onBeHurt?.Invoke(buffInfo, damageInfo, damageInfo.attacker);
                }
            }
        }
    }
    public void OnKill(DamageInfo damageInfo)
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            if (halidomList[i] != null)
            {
                foreach (var buffInfo in halidomList[i].buffInfos)
                {
                    buffInfo.buffData.onKill?.Invoke(buffInfo, damageInfo, damageInfo.defender);
                }
            }
        }
    }
    public void OnBeKilled(DamageInfo damageInfo)
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            if (halidomList[i] != null)
            {
                foreach (var buffInfo in halidomList[i].buffInfos)
                {
                    buffInfo.buffData.onBeKilled?.Invoke(buffInfo, damageInfo, damageInfo.attacker);
                }
            }
        }
    }
    public void OnRoll()
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            if (halidomList[i] != null)
            {
                foreach (var buffInfo in halidomList[i].buffInfos)
                {
                    buffInfo.buffData.onRoll?.Invoke(buffInfo);
                }
            }
        }
    }
}

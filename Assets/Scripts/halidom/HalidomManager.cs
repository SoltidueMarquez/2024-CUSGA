using DesignerScripts;
using Map;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text.RegularExpressions;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows;


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
    /// 初始属性,由playerDataSO提供
    /// </summary>
    public ChaProperty baseProp;

    /// <summary>
    /// 圣物售卖时事件
    /// </summary>
    public UnityEvent OnSellHalidom;



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
    }
    #region 场景中的圣物管理
    //配置圣物是不配圣物在格子里的顺序的，加进去的时候才获得序号
    /// <summary>
    /// 朝圣物列表中添加圣物
    /// 这个是在战斗界面的时候用的
    /// </summary>
    /// <param name="halidom"></param>
    public void AddHalidom(HalidomObject halidom)
    {
        //这边先复制一份
        HalidomObject halidomObject = new HalidomObject(halidom.rareType, halidom.id, halidom.halidomName, halidom.description, halidom.value, halidom.buffInfos);

        for (int i = 0; i < halidomList.Length; i++)
        {
            //找到第一个空的格子
            if (halidomList[i] == null)
            {
                //将圣物加入圣物列表
                halidomList[i] = halidomObject;
                //获得圣物在格子中的序号
                halidomObject.halidomIndex = i;
                //触发圣物OnCreate回调点
                foreach (var buffInfo in halidomObject.buffInfos)
                {
                    //Debug.Log("<color=green>HalidomManager:</color>" + buffInfo.buffData.onAddBuff.ToString() + "添加成功");
                    //获取圣物的创建者 给予buffinfo
                    buffInfo.creator = BattleManager.Instance.parameter.playerChaState.gameObject;
                    //获取圣物buff的对象（暂定 没有给敌人上buff）
                    buffInfo.target = BattleManager.Instance.parameter.enemyChaStates[0].gameObject;
                    //需要整体判断这个委托是否为空
                    if (buffInfo.buffData.onCreate != null)
                    {
                        //触发圣物的OnCreate回调点
                        buffInfo.buffData.onCreate?.Invoke(buffInfo);
                        //触发圣物闪烁
                        SacredObjectUIManager.Instance.DoFlick(halidom.id);
                    }
                    OnAddBuff(buffInfo);

                }
                RefreshAllHalidoms();
                BattleManager.Instance.parameter.playerChaState.AttrAndResourceRecheck();
                SacredObjectUIManager.Instance.CreateSacredUIObject(SellHalidom, halidomObject);
                Debug.Log("<color=#3399FF>HalidomManager-添加圣物:</color>" + halidomObject.halidomName + "成功");
                
                //找到空的格子后就跳出循环
                break;
            }
            else
            {
                Debug.Log("圣物格子已满");
            }
        }
        UpdateHalidomDescription();
    }

    public void AddHalidomInMap(HalidomObject halidom)
    {
        //这边先复制一份
        HalidomObject halidomObject = new HalidomObject(halidom.rareType, halidom.id, halidom.halidomName, halidom.description, halidom.value, halidom.buffInfos);
        for (int i = 0; i < halidomList.Length; i++)
        {
            //找到第一个空的格子
            if (halidomList[i] == null)
            {
                //将圣物加入圣物列表
                halidomList[i] = halidomObject;
                //获得圣物在格子中的序号
                halidomObject.halidomIndex = i;
                //触发圣物OnCreate回调点
                foreach (var buffInfo in halidomObject.buffInfos)
                {
                    //获取圣物的创建者 给予buffinfo
                    buffInfo.creator = MapManager.Instance.playerChaState.gameObject;
                    //需要整体判断这个委托是否为空
                    if (buffInfo.buffData.onCreate != null)
                    {
                        //触发圣物的OnCreate回调点
                        buffInfo.buffData.onCreate?.Invoke(buffInfo);
                        //触发圣物闪烁
                        MapSacredUIManager.Instance.DoFlick(halidom.id);
                    }

                }
                RefreshAllHalidoms();
                MapManager.Instance.playerChaState.AttrAndResourceRecheck();

                //在地图中创建圣物UI
                MapSacredUIManager.Instance.CreateSacredUIObject(SellHalidomInMap, halidomObject);

                Debug.Log("<color=#3399FF>HalidomManager-添加圣物:</color>" + halidomObject.halidomName + "成功");
                //找到空的格子后就跳出循环
                break;
            }
            else
            {
                Debug.Log("圣物格子已满");
            }
        }
        UpdateHalidomDescription();
    }
    //删去指定格子的圣物
    public void RemoveHalidom(int index, GameScene gameScene)
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            //index为实际数组下标
            if (i == index)
            {
                //触发圣物OnRemove回调点
                foreach (var buffInfo in halidomList[i].buffInfos)
                {
                    buffInfo.buffData.onRemove?.Invoke(buffInfo);

                }
                //将圣物在格子中的序号置为0
                Debug.Log("移除圣物" + halidomList[i].halidomName + "成功");
                halidomList[i].halidomIndex = 0;
                //将圣物从圣物列表中移除
                halidomList[i] = null;
                //重新计算属性
                RefreshAllHalidoms();
                if (gameScene == GameScene.BattleScene)
                {
                    BattleManager.Instance.parameter.playerChaState.AttrAndResourceRecheck();
                }
                else if (gameScene == GameScene.MapScene)
                {
                    MapManager.Instance.playerChaState.AttrAndResourceRecheck();
                }


            }
        }
    }
    #endregion
    public void RefreshAllHalidoms()
    {
        //清空delta
        deltaCharacterProperty.Zero();
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
                    buffProp[0] += buffinfo.buffData.propMod[0] * buffinfo.curStack;
                    buffProp[1] += buffinfo.buffData.propMod[1];

                }
            }
        }
        //重新计算属性
        this.currentCharacterProperty = (this.baseProp + buffProp[0]) * this.buffProp[1];
        this.deltaCharacterProperty = this.currentCharacterProperty - this.baseProp;
    }
    #region 售卖圣物
    /// <summary>
    /// 售卖圣物
    /// </summary>
    /// <param name="halidomObject"></param>
    public void SellHalidom(HalidomObject halidomObject)
    {
        int index = halidomObject.halidomIndex;
        //获取圣物的售价
        int price = halidomList[index].saleValue;
        //将圣物的售价加到玩家的金币上
        ChaState tempChaState = BattleManager.Instance.parameter.playerChaState;

        tempChaState.ModResources(new ChaResource(0, price, 0, 0));
        Debug.Log("售卖圣物成功，获得金币" + price + "个");
        //将圣物移除
        RemoveHalidom(index, GameScene.BattleScene);
        if (BattleManager.Instance != null)
        {
            BattleManager.Instance.RefreshIfHalodomCanChoose();
        }
        //触发圣物售卖回调点函数
        OnSellHalidom.Invoke();
    }
    public void SellHalidomInMap(HalidomObject halidomObject)
    {
        int index = halidomObject.halidomIndex;
        //获取圣物的售价
        int price = halidomList[index].saleValue;
        //将圣物的售价加到玩家的金币上
        ChaState tempChaState = MapManager.Instance.playerChaState;
        tempChaState.ModResources(new ChaResource(0, price, 0, 0));
        Debug.Log("售卖圣物成功，获得金币" + price + "个");
        //将圣物移除
        RemoveHalidom(index, GameScene.MapScene);

    }
    #endregion
    /// <summary>
    /// 重新设置圣物列表,交换圣物用
    /// </summary>
    public void ResetHalidomList()
    {
        List<string> ids = SacredObjectUIManager.Instance.GetScaredObjectIDList();
        var halidomDic = this.GetCurrentHalidomIdListDic();
        this.ClearHalidomList();
        for (int i = 0; i < ids.Count; i++)
        {
            if (ids[i] != "")
            {
                halidomList[i] = halidomDic[ids[i]];
            }
        }
    }
    //所有回调点触发Invoke
    #region 存档相关
    public List<HalidomDataForSave> GetHalidomDataForSaves()
    {
        var halidomDataForSaveList = new List<HalidomDataForSave>();
        for (int i = 0; i < this.halidomList.Length; i++)
        {
            if (halidomList[i] != null)
            {
                HalidomDataForSave halidomDataForSave = new HalidomDataForSave();
                halidomDataForSave.halidomName = halidomList[i].halidomName;
                halidomDataForSave.halidomIndex = i;
                if (halidomList[i].buffInfos.Count > 0)
                {
                    halidomDataForSave.halidomDataParamsDict = halidomList[i].buffInfos[0].buffParam;
                }
                halidomDataForSaveList.Add(halidomDataForSave);
            }
        }
        return halidomDataForSaveList;
    }
    
    #endregion
    #region 圣物回调点
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
                    //需要整体判断这个委托是否为空
                    if (buffInfo.buffData.onRoundStart != null)
                    {
                        buffInfo.buffData.onRoundStart?.Invoke(buffInfo);
                        //触发圣物闪烁
                        SacredObjectUIManager.Instance.DoFlick(halidomList[i].id);
                    }

                    if (buffInfo.isPermanent == false)//非永久buff
                    {
                        buffInfo.roundCount--;
                        //Test
                        Debug.Log(buffInfo.roundCount);
                        if (buffInfo.roundCount <= 0)
                        {
                            removeList.Add(buffInfo);
                        }
                    }
                }
                foreach (var removeBuff in removeList)
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
                    //需要整体判断这个委托是否为空
                    if (buffInfo.buffData.onRoundEnd != null)
                    {
                        buffInfo.buffData.onRoundEnd?.Invoke(buffInfo);
                        //触发圣物闪烁
                        SacredObjectUIManager.Instance.DoFlick(halidomList[i].id);
                    }

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
                    //需要整体判断这个委托是否为空
                    if (buffInfo.buffData.onHit != null)
                    {
                        buffInfo.buffData.onHit?.Invoke(buffInfo, damageInfo, damageInfo.defender);
                        //触发圣物闪烁
                        SacredObjectUIManager.Instance.DoFlick(halidomList[i].id);
                    }

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
                    //需要整体判断这个委托是否为空
                    if (buffInfo.buffData.onBeHurt != null)
                    {
                        buffInfo.buffData.onBeHurt?.Invoke(buffInfo, damageInfo, damageInfo.attacker);
                        //触发圣物闪烁
                        SacredObjectUIManager.Instance.DoFlick(halidomList[i].id);
                    }

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
                    //需要整体判断这个委托是否为空
                    if (buffInfo.buffData.onKill != null)
                    {
                        buffInfo.buffData.onKill?.Invoke(buffInfo, damageInfo, damageInfo.defender);
                        //触发圣物闪烁
                        SacredObjectUIManager.Instance.DoFlick(halidomList[i].id);
                    }

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
                    //需要整体判断这个委托是否为空
                    if (buffInfo.buffData.onBeKilled != null)
                    {
                        buffInfo.buffData.onBeKilled?.Invoke(buffInfo, damageInfo, damageInfo.attacker);
                        //触发圣物闪烁
                        SacredObjectUIManager.Instance.DoFlick(halidomList[i].id);
                    }

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
                    //需要整体判断这个委托是否为空
                    if (buffInfo.buffData.onRoll != null)
                    {
                        buffInfo.buffData.onRoll?.Invoke(buffInfo);
                        //触发圣物闪烁
                        SacredObjectUIManager.Instance.DoFlick(halidomList[i].id);
                    }
                }

            }
        }
    }
    //在新增buff的时候触发
    public void OnAddBuff(BuffInfo addBuffInfo)
    {
        for (int i = 0; i < 6; i++)
        {
            if (halidomList[i] != null)
            {
                foreach (var buffInfo in halidomList[i].buffInfos)
                {
                    //需要整体判断这个委托是否为空

                    if (buffInfo.buffData.onAddBuff != null)
                    {
                        buffInfo.buffData.onAddBuff?.Invoke(addBuffInfo);
                        //触发圣物闪烁
                        SacredObjectUIManager.Instance.DoFlick(halidomList[i].id);
                    }
                }

            }
        }
    }

    #endregion
    #region 一些实用效果
    //判断圣物是否满
    public bool IsFull()
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            if (halidomList[i] == null)
            {
                return false;
            }
        }
        return true;
    }
    /// <summary>
    /// 清楚圣物列表，全部等于空
    /// </summary>
    public void ClearHalidomList()
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            halidomList[i] = null;
        }
    }
    /// <summary>
    /// 获取当前圣物列表
    /// </summary>
    /// <returns></returns>
    public Dictionary<String, HalidomObject> GetCurrentHalidomIdListDic()
    {
        var halidomDictionary = new Dictionary<string, HalidomObject>();
        for (int i = 0; i < halidomList.Length; i++)
        {
            if (halidomList[i] != null)
            {
                halidomDictionary.Add(halidomList[i].id, halidomList[i]);
            }
        }
        return halidomDictionary;
    }
    #endregion
    #region 战斗场景初始化
    public void InitHalidomInBattleScene()
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            //找到第一个空的格子
            if (halidomList[i] != null)
            {

                //触发圣物OnCreate回调点
                foreach (var buffInfo in halidomList[i].buffInfos)
                {
                    //如果当前格子有东西
                    buffInfo.creator = BattleManager.Instance.parameter.playerChaState.gameObject;
                    //获取圣物buff的对象（暂定 没有给敌人上buff）
                    buffInfo.target = BattleManager.Instance.parameter.enemyChaStates[0].gameObject;
                    //需要整体判断这个委托是否为空
                }
                RefreshAllHalidoms();
            }
        }
    }
    #endregion
    #region 地图场景初始化
    public void InitHalidomInMapScene()
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            //如果当前格子有东西
            if (halidomList[i] != null)
            {
                //触发圣物OnCreate回调点
                foreach (var buffInfo in halidomList[i].buffInfos)
                {
                    //获取圣物的创建者 给予buffinfo
                    buffInfo.creator = MapManager.Instance.playerChaState.gameObject;
                }
                RefreshAllHalidoms();
            }
        }
    }
    #endregion
    #region 初始化UI相关
    public void InitHalidomUI(GameScene currentScene)
    {
        for (int i = 0; i < halidomList.Length; i++)
        {
            if (halidomList[i] != null)
            {
                if (currentScene == GameScene.BattleScene)
                {
                    SacredObjectUIManager.Instance.CreateSacredUIObject(SellHalidom, halidomList[i]);
                }
                else if (currentScene == GameScene.MapScene)
                {
                    MapSacredUIManager.Instance.CreateSacredUIObject(SellHalidom, halidomList[i]);
                }
            }
        }
        UpdateHalidomDescription();
    }
    #endregion
    #region 更新圣物的信息
    public string GetCertainHalidomDescription(string id, List<Halidom2BuffParamMap> halidom2BuffParamMaps)
    {
        HalidomObject halidomObject = halidomList.Where((HalidomObject halidom) =>
        {
            return halidom.id == id;
        }).FirstOrDefault();
        string input = halidomObject.description;
        string result = "";
        if (halidomObject != null)
        {
            for (int i = 0; i < halidom2BuffParamMaps.Count; i++)
            {
                string pattern = halidom2BuffParamMaps[i].key;
                string replacement = $"<color=red>{(string)halidomObject.buffInfos[0].buffParam[halidom2BuffParamMaps[i].value]}</color>";
                result = Regex.Replace(input, pattern, replacement);
            }
            return result;
        }
        return input;
    }
    public void UpdateHalidomDescription()
    {
        for (int i = 0; i <halidomList.Length; i++)
        {
            if (halidomList[i] != null)
            {
                string des = halidomList[i].description;
                string result = ResourcesManager.GetHalidomDescription(halidomList[i].id);
                //更新UI描述
                if(SceneLoader.Instance.currentGameScene == GameScene.MapScene)
                {
                    Debug.Log("在地图执行");
                    MapSacredUIManager.Instance.UpdateDesc(halidomList[i].id, result);
                }
                else if(SceneLoader.Instance.currentGameScene == GameScene.BattleScene)
                {
                    Debug.Log("在战斗场景执行");
                    SacredObjectUIManager.Instance.UpdateDesc(halidomList[i].id, result);
                }
            }
        }
    }
    #endregion
}

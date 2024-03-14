using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffData
{

    /// <summary>
    /// buff的唯一id
    /// </summary>
    public string id;
    /// <summary>
    /// buff的名字
    /// </summary>
    public string buffName;
    /// <summary>
    /// 存储buff的icon图标的路径，resoueces读取
    /// </summary>
    public string buffIcon;
    /// <summary>
    /// 用于方便检索的tag，例如：火焰，冰冻，中毒等
    /// </summary>
    public string[] tags;
    /// <summary>
    /// buff的最高层数
    /// </summary>
    public int maxStack;
    //当buff的层数发生变化时，buff的更新策略
    public BuffUpdateEnum buffUpdateEnum;

    public BuffRemoveStackUpdateEnum removeStackUpdateEnum;

    //buff的时间信息
    public int duringCount;
    /// <summary>
    /// 是否是永久的buff
    /// </summary>
    public bool isPermanent;
    /// <summary>
    /// buff会给角色添加的属性，暂定两种，一种加算，一种乘算
    /// </summary>
    public ChaProperty[] propMod;
    /// <summary>
    /// buff对于角色状态的修改
    /// </summary>
    public ChaControlState stateMod;
    /// <summary>
    /// buff在创建的时候的事件
    /// </summary>
    public OnBuffCreate onCreate;
    public object[] onCreateParams;
    /// <summary>
    /// buff在移除的时候的事件
    /// </summary>
    public OnBuffRemove onRemove;
    public object[] onRemoveParams;
    /// <summary>
    /// 在回合开始的时候的触发的事件
    /// </summary>
    public OnRoundStart onRoundStart;
    public object[] onRoundStartParams;
    /// <summary>
    /// 在回合结束的时候触发的事件
    /// </summary>
    public OnRoundEnd onRoundEnd;
    public object[] onRoundEndParams;
    /// <summary>
    /// 在伤害流程中，持有这个buff的角色作为攻击者时触发的事件
    /// </summary>
    public BuffOnHit onHit;
    public object[] onHitParams;
    /// <summary>
    /// 在伤害流程中，持有这个buff的角色作为被攻击者时触发的事件
    /// </summary>
    public BuffOnBeHurt onBeHurt;
    public object[] onBeHurtParams;
    /// <summary>
    /// 在开局玩家操作阶段roll骰子时触发的事件
    /// </summary>
    public BuffOnRoll onRoll;
    public object[] onRollParams;
    /// <summary>
    /// 在伤害流程中，如果击杀目标，会触发的事件
    /// </summary>
    public BuffOnkill onKill;
    public object[] onKillParams;

    public BuffOnBeKilled onBeKilled;
    public object[] onBeKilledParams;
    /// <summary>
    /// 基本的构造函数
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <param name="icon"></param>
    /// <param name="tags"></param>
    /// <param name="maxStack"></param>
    /// <param name="duringCount"></param>
    /// <param name="isPermanent"></param>
    /// <param name="onCreate"></param>
    /// <param name="onCreateParams"></param>
    /// <param name="onRemove"></param>
    /// <param name="onRemoveParams"></param>
    /// <param name="onRoundStart"></param>
    /// <param name="onRoundStartParams"></param>
    /// <param name="onRoundEnd"></param>
    /// <param name="onRoundEndParams"></param>
    /// <param name="onHit"></param>
    /// <param name="onHitParams"></param>
    /// <param name="onBeHurt"></param>
    /// <param name="onBeHurtParams"></param>
    /// <param name="onRoll"></param>
    /// <param name="onRollParams"></param>
    /// <param name="onKill"></param>
    /// <param name="onKillParams"></param>
    /// <param name="onBeKilled"></param>
    /// <param name="onBeKilledParams"></param>
    /// <param name="propMod"></param>
    /// <param name="stateMod"></param>
    public BuffData(
        string id, string name, string icon, string[] tags, int maxStack, int duringCount, bool isPermanent,
        BuffUpdateEnum buffUpdateEnum, BuffRemoveStackUpdateEnum removeStackUpdateEnum,
        string onCreate, object[] onCreateParams,
        string onRemove, object[] onRemoveParams,
        string onRoundStart, object[] onRoundStartParams,
        string onRoundEnd, object[] onRoundEndParams,
        string onHit, object[] onHitParams,
        string onBeHurt, object[] onBeHurtParams,
        string onRoll, object[] onRollParams,
        string onKill, object[] onKillParams,
        string onBeKilled, object[] onBeKilledParams,
        ChaControlState stateMod, ChaProperty[] propMod = null
        )
    {
        this.id = id;
        this.buffName = name;
        this.buffIcon = icon;
        this.tags = tags;
        this.maxStack = maxStack;
        this.duringCount = duringCount;
        this.isPermanent = isPermanent;
        this.propMod = new ChaProperty[2]
            {ChaProperty.zero,
             ChaProperty.zero
        };

        if (propMod != null)
        {
            for (int i = 0; i < Mathf.Min(propMod.Length, 2); i++)
            {
                this.propMod[i] = propMod[i];
            }
        }
        this.stateMod = stateMod;
    }


}
/// <summary>
/// buff的创建，移除，回合开始，回合结束，被攻击，攻击，被击杀，击杀等事件
/// </summary>
/// <param name="buff"></param>
public delegate void OnBuffCreate(BuffInfo buff);
public delegate void OnBuffRemove(BuffInfo buff);
public delegate void OnRoundStart(BuffInfo buff);
public delegate void OnRoundEnd(BuffInfo buff);
public delegate void BuffOnHit(BuffInfo buff, DamageInfo damageInfo, GameObject target);
public delegate void BuffOnBeHurt(BuffInfo buff, DamageInfo damageInfo, GameObject attacker);
public delegate int BuffOnRoll(BuffInfo buffInfo);
public delegate void BuffOnkill(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target);
public delegate void BuffOnBeKilled(BuffInfo buffInfo, DamageInfo damageInfo, GameObject attacker);


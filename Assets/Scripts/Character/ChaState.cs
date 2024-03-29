using System.Collections;
using System.Collections.Generic;
using UI;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
/// <summary>
/// 玩家和敌人共有的类，是玩家和敌人的的总控，敌人通过AI控制，玩家通过在战斗时对UI进行交互，调用chaState中的函数
/// </summary>
[RequireComponent(typeof(BuffHandler))]
[RequireComponent(typeof(BattleDiceHandler))]

public class ChaState : MonoBehaviour
{
    [Header("需要的组件")]
    [SerializeField] private BuffHandler buffHandler;
    /// <summary>
    /// 负责玩家战斗的骰子管理器，其中包含了骰面的各种信息，骰子的面数，骰子的加成
    /// </summary>
    [SerializeField] private BattleDiceHandler battleDiceHandler;
    /// <summary>
    /// 角色的基础属性，每个角色不带任何buff的纯粹数值
    /// 先写死，正式的应该是从配置文件中读取
    /// </summary>
    public ChaProperty baseProp = new ChaProperty(
        50, 400, 4, 0
    );
    /// <summary>
    /// buff的属性加成,buffProp[0]是加法，buffProp[1]是乘法
    /// </summary>
    public ChaProperty[] buffProp = new ChaProperty[2] { ChaProperty.zero, ChaProperty.zero };
    /// <summary>
    /// 计算buff后的属性上限
    /// </summary>
    public ChaProperty prop { get; private set; } = ChaProperty.zero;
    /// <summary>
    /// 玩家当前的资源,这个在全局的过程中都不变，可能在局外变化
    /// </summary>
    public ChaResource resource = new();
    public ChaControlState controlState = new ChaControlState(true, true, false);

    /// <summary>
    /// 哪一边的
    /// </summary>
    public int side;
    private void Awake()
    {
        buffHandler = GetComponent<BuffHandler>();
        //获取battleDiceHandler暂时写在这边，因为还没有写完
        battleDiceHandler = GetComponent<BattleDiceHandler>();
    }
    #region 回调点
    /// <summary>
    /// 供外部调用的方法，用于调用回调点
    /// </summary>
    public void OnRoundStart()
    {
        buffHandler.BuffRoundStartTick();
        string temp = this.side == 0 ? "当前玩家的buff数" : "当前敌人的buff数";
        Debug.Log(temp+ this.buffHandler.buffList.Count);
    }

    public void OnRoundEnd()
    {
        buffHandler.BuffRoundEndTick();
    }
    #endregion
    #region 使用骰面
    public void UseDice(int index, GameObject target)
    {
        if (this.controlState.canUseDice == false)//如果不能使用骰子
        {
            Debug.Log("不能使用骰子");
            return;
        }
        battleDiceHandler.CastSingleDice(index, this, target);
        DamageManager.Instance.DealWithAllDamage();
    }
    /// <summary>
    /// 使用所有的骰面
    /// </summary>
    public void UseAllDice()
    {
        if (this.controlState.canUseDice == false)//如果不能使用骰子
        {
            Debug.Log("不能使用骰子");
            return;
        }
        //当没有骰子的时候，直接返回
        if (this.battleDiceHandler.IfSingleBattleDiceEmpty()) return;
        battleDiceHandler.CastDiceAll(this, BattleManager.Instance.currentSelectEnemy);
        DamageManager.Instance.DealWithAllDamage();
        if (this.side == 0)
        {
            RollingResultUIManager.Instance.RemoveAllResultUI(Strategy.UseAll);
        }
    }
    #endregion
    #region buff的操作
    public void AddBuff(BuffInfo buffInfo, GameObject creator)
    {
        buffHandler.AddBuff(buffInfo, creator);
        AttrAndResourceRecheck();
    }

    public void RemoveBuff(BuffInfo buffInfo)
    {
        buffHandler.RemoveBuff(buffInfo);
        AttrAndResourceRecheck();
    }
    #endregion
    /// <summary>
    /// 判断能否被伤害信息杀死
    /// </summary>
    /// <param name="damageInfo"></param>
    /// <returns></returns>
    public bool CanBeKilledByDamageInfo(DamageInfo damageInfo)
    {
        if (damageInfo.diceType == DiceType.Support || damageInfo.diceType == DiceType.Defense)
        {
            return false;
        }
        return damageInfo.finalDamage < this.resource.currentHp;
    }
    public void Kill()
    {
        //TODO:玩家死亡的逻辑
        Debug.Log(this.gameObject.name + "死亡");
        BattleManager.Instance.EndGame(this.side);
    }
    /// <summary>
    /// 重新计算属性,在buff添加或者删除的时候调用
    /// </summary>
    private void AttrAndResourceRecheck()
    {
        //创建一个新变量，先把原本的属性保存下来，用于后续计算差值
        ChaProperty chaProperty = new ChaProperty(this.prop);
        //输出chaProperty
        //清空buff属性加成
        for (var i = 0; i < buffProp.Length; i++)
        {
            buffProp[i].Zero();
        }
        //重新获取所有buff的加法总和和乘法总和
        buffHandler.RecheckBuff(buffProp, ref controlState);
        //获取玩家的圣物所有给的属性
        ChaProperty halidomProp = HalidomManager.Instance.deltaCharacterProperty;
        //重新计算属性
        this.prop = (this.baseProp + buffProp[0]) * (this.buffProp[1]) + halidomProp;
        //计算差值
        chaProperty = this.prop - chaProperty;
        //根据差值，重新计算资源
        this.resource += new ChaResource(chaProperty.health, chaProperty.money, chaProperty.maxRollTimes, chaProperty.shield);
    }
    public void ModResources(ChaResource value)
    {
        this.resource += value;
        this.resource.currentMoney = Mathf.Clamp(this.resource.currentMoney, 0, this.prop.money);
        this.resource.currentRollTimes = Mathf.Clamp(this.resource.currentRollTimes, 0, this.prop.maxRollTimes);
        //这边对盾条还是需要斟酌一下
        //this.resource.currentShield = Mathf.Clamp(this.resource.currentShield, 0, this.prop.shield);
        this.resource.currentHp = Mathf.Clamp(this.resource.currentHp, 0, this.prop.health);
        CharacterUIManager.Instance.ChangeHealthSlider((Character)side, this.resource.currentHp, this.prop.health);
        if (this.resource.currentHp <= 0)
        {
            this.Kill();
        }
        Debug.Log(this.gameObject.name + this.resource.currentHp);
        Debug.Log(this.gameObject.name + this.resource.currentShield);

    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void Initialize()
    {
        AttrAndResourceRecheck();

        //UI初始化
        CharacterUIManager.Instance.ChangeHealthSlider((Character)side, this.resource.currentHp, this.prop.health);
    }

    #region 获取组件
    public BuffHandler GetBuffHandler()
    {
        return buffHandler;
    }
    public BattleDiceHandler GetBattleDiceHandler()
    {
        return battleDiceHandler;
    }
    #endregion 
}

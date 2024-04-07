using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单一的骰子面的数据，其中包含了效果的数据
/// </summary>
public class SingleDiceModel
{
    /// <summary>
    /// 骰子单面的唯一id
    /// </summary>
    public string id;
    /// <summary>
    /// 骰面的名字
    /// </summary>
    public string name;
    /// <summary>
    /// 属于哪种类型的骰子
    /// </summary>
    public DiceType type;
    /// <summary>
    /// 骰子的使用条件
    /// </summary>
    public ChaResource condition;
    /// <summary>
    /// 骰子的消耗
    /// </summary>
    public ChaResource cost;
    /// <summary>
    /// 基础伤害,这个伤害是不包含等级的伤害
    /// </summary>
    public Damage damage;
    /// <summary>
    /// 等级
    /// </summary>
    public int level;
    /// <summary>
    /// 使用骰面的特效
    /// </summary>
    public BuffInfo[] buffInfos;
    /// <summary>
    /// 骰子的特效
    /// </summary>
    public VisualEffect visualEffect;
    /// <summary>
    /// 骰面的售价
    /// </summary>
    public int value;
    /// <summary>
    /// 分辨骰面属于玩家拥有的还是敌人拥有的，还是共有的
    /// 0表示玩家，1表示敌人，2表示共有
    /// </summary>
    public int side;
    /// <summary>
    /// 最基础的构造函数
    /// </summary>
    /// <param name="side">分辨骰面属于玩家拥有的还是敌人拥有的，还是共有的</param>
    /// <param name="name">骰面的名字</param>"
    /// <param name="id">每一个单独的骰面的唯一id,用于查找信息与逻辑视觉共享</param>
    /// <param name="condition">使用骰面需要的条件</param>
    /// <param name="cost">使用骰面需要扣除的资源</param>
    /// <param name="damage">可以造成的基础伤害</param>
    /// <param name="value">售卖的时候的价格</param>
    /// <param name="level">等级</param>
    /// <param name="buffInfos">附带的buffs</param>
    /// <param name="visualEffect">播放的视觉效果</param>
    public SingleDiceModel(int side,DiceType diceType,string name,string id, ChaResource condition, ChaResource cost, int value, int level, BuffInfo[] buffInfos, int baseValue,VisualEffect visualEffect)
    {
        this.side = side;
        this.name = name;
        this.type = diceType;
        this.id = id;
        this.condition = condition;
        this.cost = cost;
        this.damage = new Damage(0, 1);
        this.level = level;
        this.buffInfos = buffInfos;
        this.damage.baseDamage = baseValue;
        this.visualEffect = visualEffect;
        this.value = value;
    }
}



using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BaseBuffEffect_", menuName = "BattleStore/BattleProductEffect/BaseBuffEffect")]
public class BaseBuffEffect : BaseProductEffect
{
    [Header("buff施加的层数")]
    public int stack;
    [Header("buff施加的对象")]
    //public EffectTarget buffCreator;
    public EffectTarget buffTarget;
    [Header("施加哪个buff")]
    public BaseBuffEffectEnum buffEffect;

    public override void Use()
    {
        //获取gameobject对象
        var player = BattleManager.Instance.parameter.playerChaState.gameObject;
        var enemy = BattleManager.Instance.parameter.enemyChaStates[0].gameObject;
        if (buffTarget == EffectTarget.Enemy)
        {
            BuffInfo buff = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[buffEffect.ToString()],
                                         player, enemy, stack,
                                         DataInitManager.Instance.buffDataTable.buffData[buffEffect.ToString()].isPermanent);
            enemy.GetComponent<ChaState>().AddBuff(buff, player);
        }
        else if(buffTarget == EffectTarget.Player)
        {
            BuffInfo buff = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[buffEffect.ToString()],
                                         player, player, stack,
                                         DataInitManager.Instance.buffDataTable.buffData[buffEffect.ToString()].isPermanent);
            player.GetComponent<ChaState>().AddBuff(buff, player);
        }




    }
}

public enum BaseBuffEffectEnum
{
    Bleed,//流血

    Spirit,//精力

    Vulnerable,//易伤

    Tough,//坚韧

    Weak,//虚弱

    Strength,//力量

    Enhance,//强化

    Dodge,//闪避

    EnergyStorage,//蓄能

    Anger,//怒气

    LoseEnergy,//失能

    Thorns,//荆棘

    Reflect,//反射

    Split,//分裂

    Pox,//水痘

    Spike,//尖刺

    Corrosion,//腐蚀

    Sensitive,//敏感

    Brave,//勇敢
}



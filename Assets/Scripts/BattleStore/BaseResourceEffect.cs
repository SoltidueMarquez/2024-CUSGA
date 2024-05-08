using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseResourceEffect_", menuName = "BattleStore/BattleProductEffect/BaseResourceEffect")]
public class BaseResourceEffect : BaseProductEffect
{
    [Header("数值更改:(需要正负号)")]
    public int healthChange;
    public int shieldChange;
    [Header("作用的对象")]
    public EffectTarget target;


    public override void Use()
    {
        var player = BattleManager.Instance.parameter.playerChaState.GetComponent<ChaState>();
        var enemy = BattleManager.Instance.parameter.enemyChaStates[0].GetComponent<ChaState>();
        DamageInfo damageInfo = new DamageInfo(player.gameObject,enemy.gameObject,
                                                new Damage(Mathf.Abs(healthChange),0),
                                                DiceType.Attack,0,null);
        if(target == EffectTarget.Enemy)
        {
            
            enemy.ModResources(new ChaResource(healthChange, 0, 0, shieldChange,0),damageInfo);
        }
        if(target == EffectTarget.Player)
        {
            
            player.ModResources(new ChaResource(healthChange, 0, 0, shieldChange, 0),damageInfo);
        }
        
    }
}


public enum EffectTarget
{
    Player,
    Enemy,
}



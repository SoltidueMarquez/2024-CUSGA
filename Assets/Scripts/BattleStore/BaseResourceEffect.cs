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
        if(target == EffectTarget.Enemy)
        {
            var enemy = BattleManager.Instance.parameter.enemyChaStates[0].GetComponent<ChaState>();
            enemy.ModResources(new ChaResource(healthChange, 0, 0, shieldChange,0));
        }
        if(target == EffectTarget.Player)
        {
            var player = BattleManager.Instance.parameter.playerChaState.GetComponent<ChaState>();
            player.ModResources(new ChaResource(healthChange, 0, 0, shieldChange, 0));
        }
        
    }
}


public enum EffectTarget
{
    Player,
    Enemy,
}



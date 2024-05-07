using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "DefendEffect", menuName = "BattleStore/BattleProductEffect/DefendEffect")]
public class DefendEffect : BaseProductEffect
{
    public int defendValue;

    public override void Use()
    {
        var player=BattleManager.Instance.parameter.playerChaState.GetComponent<ChaState>();
        player.ModResources(new ChaResource(0, 0, 0, defendValue));
    }
}

using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private ChaState characterState;
    private void Start()
    {
        characterState = GetComponent<ChaState>();
    }

    public void SimpleAI()
    {
        StartCoroutine(SimpleTestEnemyAI());
    }

    IEnumerator SimpleTestEnemyAI()
    {
        yield return new WaitForSeconds(1);
            characterState.GetBattleDiceHandler().CastDiceAll(characterState, BattleManager.Instance.parameter.playerChaState.gameObject);
            DamageManager.Instance.DealWithAllDamage();
        yield return new WaitForSeconds(1);
        BattleManager.Instance.TransitionState(GameState.EnemyRoundEndResolution);
    }
}

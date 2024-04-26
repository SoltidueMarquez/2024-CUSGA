using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class EnemyAI : MonoBehaviour
{
    private ChaState characterState;
    private void Start()
    {
        characterState = GetComponent<ChaState>();
    }

    public void SimpleAI()
    {
        //characterState.GetBattleDiceHandler().CastDiceAll(characterState, BattleManager.Instance.parameter.playerChaState.gameObject);
        SimpleAIAsync();
    }
    public async void SimpleAIAsync()
    {
        for (int i = 0; i < characterState.GetBattleDiceHandler().diceCardsInUse.Length; i++)
        {
            characterState.GetBattleDiceHandler().CastSingleDice(i, characterState, BattleManager.Instance.parameter.playerChaState.gameObject);
            //Debug.Log("释放第"+i+"个骰子");
            await UniTask.Delay(1000);
        }
        BattleManager.Instance.TransitionState(GameState.EnemyRoundEndResolution);
    }
}

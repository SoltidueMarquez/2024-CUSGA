using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class EnemyAI : MonoBehaviour
{
    private ChaState characterState;
    private CancellationTokenSource cancellationToken;
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
        cancellationToken = new CancellationTokenSource();
        for (int i = 0; i < characterState.GetBattleDiceHandler().diceCardsInUse.Length; i++)
        {
            characterState.GetBattleDiceHandler().CastSingleDice(i, characterState, BattleManager.Instance.parameter.playerChaState.gameObject);
            if (BattleManager.Instance.GetCurrentState() == BattleManager.Instance.GetStates()[GameState.PlayerLose])
            {
                cancellationToken.Cancel();
                //return;
            }
            try
            {
                await UniTask.Delay(1000,false,PlayerLoopTiming.Update,cancellationToken.Token);
            }
            catch (System.OperationCanceledException)
            {
                Debug.Log("取消释放骰子");
            }
            
        }
        if(BattleManager.Instance.GetCurrentState() == BattleManager.Instance.GetStates()[GameState.EnemyAction])
        {
            BattleManager.Instance.TransitionState(GameState.EnemyRoundEndResolution);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UI;
using UnityEngine;

public class GameStartState : IState
{
    private BattleManager manager;
    
    public GameStartState(BattleManager manager)
    {
       
        this.manager = manager;
    }
    public void OnEnter()
    {
        Debug.Log("游戏开始");
        //TODO  读入所有数据
        manager.parameter.playerChaStates.GetBattleDiceHandler().InitDice();
        manager.parameter.playerChaStates.Initialize();
        //根据敌人的数量初始化敌人
        for (int i = 0; i < manager.parameter.enemyChaState.Length; i++)
        {
            manager.parameter.enemyChaState[i].GetBattleDiceHandler().InitDice();
            manager.parameter.enemyChaState[i].Initialize();
        }
        //清空回合计数器
        manager.ResetTurns();
    }

    public void OnExit()
    {
        Debug.Log("退出gamestartState");
    }

    public void OnUpdate()
    {
        manager.TransitionState(GameState.Preparation);
    }
}
/// <summary>
/// 预备阶段
/// </summary>
public class PreparationState : IState
{
    private BattleManager manager;

    public PreparationState(BattleManager manager)
    {

        this.manager = manager;
    }
    public void OnEnter()
    {
        

        Debug.Log("Enter PreparationState");

        //TODO 播动画

    }

    public void OnExit()
    {
        Debug.Log("Exit PreparationState");
    }

    public void OnUpdate()
    {
        //跳转到玩家判定阶段
        manager.TransitionState(GameState.PlayerRoundStartResolution);
    }
}

public class PlayerRoundStartResolutionState : IState
{
    private BattleManager manager;

    public PlayerRoundStartResolutionState(BattleManager manager)
    {

        this.manager = manager;
    }
    public void OnEnter()
    {
        Debug.Log("Enter PlayerRoundStartResolutionState");
        //回合数加一
        manager.AddTurns();
        // 更新UI
        DataUIManager.Instance.UpdateRunTimeText(manager.parameter.turns);
        //触发圣物里所有buff的OnRoundStart回调点
        HalidomManager.Instance.OnRoundStart();
        //触发角色里所有buff的OnRoundStart回调点
        manager.parameter.playerChaStates.OnRoundStart();


    }

    public void OnExit()
    {
        Debug.Log("Exit PlayerRoundStartResolutionState");
        //TODO播放自动投骰子动画
        Debug.Log("播骰子动画");
        //自动投骰子
        List<SingleDiceObj> singleDiceObjs = manager.parameter.playerChaStates.GetBattleDiceHandler().GetRandomSingleDices();
        manager.parameter.playerChaStates.GetBattleDiceHandler().AddBattleSingleDice(singleDiceObjs);
        for (int i = 0;i<singleDiceObjs.Count;i++)
        {
            Vector2Int pos = new Vector2Int(i, singleDiceObjs[i].idInDice);
            RollingResultManager.Instance.CreateResult(i, singleDiceObjs[i].model.id,pos);

        }
        //TODO:封装投骰子函数给UI调用
        //TODO:更新UI
        Debug.Log("根据投掷结果更新UI");
        //TODO:敌人投骰子
        Debug.Log("敌人投骰子"); 
    }

    public void OnUpdate()
    {
        manager.TransitionState(GameState.PlayerAction);
    }


}
/// <summary>
/// 玩家实时操作的阶段
/// </summary>
public class PlayerActionState : IState
{
    private BattleManager manager;

    public PlayerActionState(BattleManager manager)
    {

        this.manager = manager;
    }
    public void OnEnter()
    {
        Debug.Log("Enter PlayerActionState");
        //TODO:UI动画


    }

    public void OnExit()
    {
        Debug.Log("Exit PlayerActionState");
    }

    public void OnUpdate()
    {
        
    }
}
/// <summary>
/// 在玩家回合结束的时候会触发的东西
/// </summary>
public class PlayerRoundEndResolutionState : IState
{
    private BattleManager manager;

    public PlayerRoundEndResolutionState(BattleManager manager)
    {

        this.manager = manager;
    }
    public void OnEnter()
    {
        
        //触发圣物里所有buff的OnRoundEnd回调点
        HalidomManager.Instance.OnRoundEnd();
        //触发角色里所有buff的OnRoundEnd回调点
        manager.parameter.playerChaStates.OnRoundEnd();

        Debug.Log("Enter PlayerRoundEndResolutionState");


    }

    public void OnExit()
    {
        Debug.Log("Exit PlayerRoundEndResolutionState");
    }

    public void OnUpdate()
    {
        manager.TransitionState(GameState.EnemyRoundStartResolution);
    }
}

public class EnemyRoundStartResolutionState : IState
{
    private BattleManager manager;

    public EnemyRoundStartResolutionState(BattleManager manager)
    {

        this.manager = manager;
    }
    public void OnEnter()
    {
        //TODO：圣物所有在敌人判定阶段触发的回调点
        //触发所有敌人身上挂载的buff的OnRoundStart回调点
        foreach (var enemy in manager.parameter.enemyChaState)
        {
            enemy.OnRoundStart();
        }

        Debug.Log("Enter EnemyRoundStartResolutionState");


    }

    public void OnExit()
    {
        Debug.Log("Exit EnemyRoundStartResolutionState");
    }

    public void OnUpdate()
    {
        manager.TransitionState(GameState.EnemyAction);
    }
}

public class EnemyActionState : IState
{
    private BattleManager manager;

    public EnemyActionState(BattleManager manager)
    {

        this.manager = manager;
    }
    public void OnEnter()
    {


        Debug.Log("进入EnemyActionState");


    }

    public void OnExit()
    {
        Debug.Log("退出EnemyActionState");
    }

    public void OnUpdate()
    {
        //TODO:敌人AI逻辑
        manager.TransitionState(GameState.EnemyRoundEndResolution);
    }
}

public class EnemyRoundEndResolutionState : IState
{
    private BattleManager manager;

    public EnemyRoundEndResolutionState(BattleManager manager)
    {

        this.manager = manager;
    }
    public void OnEnter()
    {
        Debug.Log("Enter EnemyRoundEndResolutionState");
        //TODO：圣物所有在敌人回合结束判定阶段触发的回调点
        //触发所有敌人身上挂载的buff的OnRoundEnd回调点
        foreach (var enemy in manager.parameter.enemyChaState)
        {
            enemy.OnRoundEnd();
        }
    }

    public void OnExit()
    {
        Debug.Log("Exit EnemyRoundEndResolutionState");
    }

    public void OnUpdate()
    {
        manager.TransitionState(GameState.PlayerRoundStartResolution);
    }
}

public class PlayerLoseState : IState
{
    private BattleManager manager;

    public PlayerLoseState(BattleManager manager)
    {

        this.manager = manager;
    }
    public void OnEnter()
    {

        Debug.Log("Enter PlayerLoseState");

    }

    public void OnExit()
    {
        Debug.Log("Exit PlayerLoseState");
    }

    public void OnUpdate()
    {

    }
}

public class PlayerLoseResolutionState : IState
{
    private BattleManager manager;

    public PlayerLoseResolutionState(BattleManager manager)
    {

        this.manager = manager;
    }
    public void OnEnter()
    {

        Debug.Log("Enter PlayerLoseResolutionState");

    }

    public void OnExit()
    {
        Debug.Log("Exit PlayerLoseResolutionState");
    }

    public void OnUpdate()
    {

    }
}

public class PlayerWinState : IState
{
    private BattleManager manager;

    public PlayerWinState(BattleManager manager)
    {

        this.manager = manager;
    }
    public void OnEnter()
    {

        Debug.Log("Enter PlayerWinState");

    }

    public void OnExit()
    {
        Debug.Log("Exit PlayerWinState");
    }

    public void OnUpdate()
    {

    }
}

public class RewardState : IState
{
    private BattleManager manager;

    public RewardState(BattleManager manager)
    {

        this.manager = manager;
    }
    public void OnEnter()
    {

        Debug.Log("Enter RewardState");

    }

    public void OnExit()
    {
        Debug.Log("Exit RewardState");
    }

    public void OnUpdate()
    {

    }
}
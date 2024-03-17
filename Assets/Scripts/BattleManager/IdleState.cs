using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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
        //TODO 更新UI

        


    }

    public void OnExit()
    {
        Debug.Log("Exit PlayerRoundStartResolutionState");
    }

    public void OnUpdate()
    {

    }


}

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


    }

    public void OnExit()
    {
        Debug.Log("Exit PlayerActionState");
    }

    public void OnUpdate()
    {

    }
}

public class PlayerRoundEndResolutionState : IState
{
    private BattleManager manager;

    public PlayerRoundEndResolutionState(BattleManager manager)
    {

        this.manager = manager;
    }
    public void OnEnter()
    {


        Debug.Log("Enter PlayerRoundEndResolutionState");


    }

    public void OnExit()
    {
        Debug.Log("Exit PlayerRoundEndResolutionState");
    }

    public void OnUpdate()
    {

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


        Debug.Log("Enter EnemyRoundStartResolutionState");


    }

    public void OnExit()
    {
        Debug.Log("Exit EnemyRoundStartResolutionState");
    }

    public void OnUpdate()
    {

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

    }

    public void OnExit()
    {
        Debug.Log("Exit EnemyRoundEndResolutionState");
    }

    public void OnUpdate()
    {

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
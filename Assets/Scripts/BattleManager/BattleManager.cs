using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using System;
using UI;

[Serializable]
public class FSMParameter
{
    /// <summary>
    /// 记录回合数
    /// </summary>
    public int turns;
    /// <summary>
    /// 玩家重新投掷的次数
    /// </summary>
    public int playerRerollCount;
    
    
    

    public ChaState playerChaState;
    public ChaState[] enemyChaStates;

}

public enum GameState
{
    //游戏开始
    GameStart,
    Preparation,
    //玩家回合
    PlayerRoundStartResolution,
    PlayerAction,
    PlayerRoundEndResolution,
    //敌人回合
    EnemyRoundStartResolution,
    EnemyAction,
    EnemyRoundEndResolution,
    //战斗结束
    PlayerLose,
    PlayerLoseResolution,
    PlayerWin,
    Reward 
}
public class BattleManager : MonoBehaviour
{
    [Header("状态机部分")]
    private IState currentState;

    private Dictionary<GameState, IState> states = new Dictionary<GameState, IState>();

    [Header("战斗系统参数")]
    public FSMParameter parameter;
    public GameObject currentSelectEnemy;
    

    public static BattleManager Instance
    {
        get; private set;
    }

    

    private void Awake()
    {
        //单例模式
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // 否则，将自身设为实例，并保持在场景切换时不被销毁
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {

        //初始化字典，有一个状态就要在这里注册一个,在字典的值中实例化一个状态
        states.Add(GameState.GameStart, new GameStartState(this));
        states.Add(GameState.Preparation, new PreparationState(this));
        states.Add(GameState.PlayerRoundStartResolution, new PlayerRoundStartResolutionState(this));
        states.Add(GameState.PlayerAction, new PlayerActionState(this));
        states.Add(GameState.PlayerRoundEndResolution, new PlayerRoundEndResolutionState(this));
        states.Add(GameState.EnemyRoundStartResolution, new EnemyRoundStartResolutionState(this));
        states.Add(GameState.EnemyAction, new EnemyActionState(this));
        states.Add(GameState.EnemyRoundEndResolution, new EnemyRoundEndResolutionState(this));
        states.Add(GameState.PlayerLose, new PlayerLoseState(this));
        states.Add(GameState.PlayerLoseResolution, new PlayerLoseResolutionState(this));
        states.Add(GameState.PlayerWin, new PlayerWinState(this));
        states.Add(GameState.Reward, new RewardState(this));
        
        //设置初始状态
        TransitionState(GameState.GameStart);
    }

    private void Update()
    {
        //在Update中调用相应状态的OnUpdate()
        currentState.OnUpdate();
    }

    public void TransitionState(GameState type)//状态切换
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }
        currentState = states[type];//通过字典的键（枚举）来找到值（状态类）
        currentState.OnEnter();

    }
    public void ResetTurns()
    {
        this.parameter.turns = 0;

    }
    public void AddTurns()
    {
        this.parameter.turns++;
    }

    public void ReducePlayerRerollCount()
    {
        this.parameter.playerRerollCount--;
    }

    /// <summary>
    /// 给ui调用的方法，结束玩家回合
    /// </summary>
    public void EndPlayerRound()
    {
        this.parameter.playerChaState.GetBattleDiceHandler().ClearBattleSingleDices();
        RollingResultUIManager.Instance.RemoveAllResultUI(false);
        TransitionState(GameState.PlayerRoundEndResolution);
    }

    public void EndGame(int side)
    {
        if(side == 0)
        {
            Debug.Log("玩家死亡");
        }
        else
        {
            Debug.Log("敌人死亡");
        }
    }

    public void RollDice()
    {
        List<SingleDiceObj> singleDiceObjs = this.parameter.playerChaState.GetBattleDiceHandler().GetRandomSingleDices();
        this.parameter.playerChaState.GetBattleDiceHandler().AddBattleSingleDice(singleDiceObjs);
        for (int i = 0; i < singleDiceObjs.Count; i++)
        {
            Vector2Int pos = new Vector2Int(i, singleDiceObjs[i].idInDice);
            RollingResultUIManager.Instance.CreateResult(i, singleDiceObjs[i].model.id, pos);
        }
    }
    public void ReRollDice()
    {
        //如果玩家的重新投掷次数小于等于0，就不执行
        if(this.parameter.playerChaState.resource.currentRollTimes <= 0)
        {
            return;
        }
        this.parameter.playerChaState.ModResources(new ChaResource(0, 0, -1, 0));
        List<SingleDiceObj> singleDiceObjs = this.parameter.playerChaState.GetBattleDiceHandler().GetRandomSingleDices();
        this.parameter.playerChaState.GetBattleDiceHandler().AddBattleSingleDice(singleDiceObjs);
        //这边需要删除所有当前骰面的视觉
        for (int i = 0; i < singleDiceObjs.Count; i++)
        {
            Vector2Int pos = new Vector2Int(i, singleDiceObjs[i].idInDice);
            RollingResultUIManager.Instance.CreateResult(i, singleDiceObjs[i].model.id, pos);
        }
    }
    public void RollDiceForEnemy(ChaState chaState)
    {
        var singleDiceObjs = chaState.GetBattleDiceHandler().GetRandomSingleDices();
        chaState.GetBattleDiceHandler().AddBattleSingleDice(singleDiceObjs);
    }
}
//定义了一个回调，用于在UI动画结束时调用
public  delegate void OnUIAnimFinished();

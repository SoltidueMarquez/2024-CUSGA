using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;


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

    public ChaState playerChaStates;
    public ChaState[] enemyChaState;

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

   
}

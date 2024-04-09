using System.Collections.Generic;
using UnityEngine;
using System;
using UI;
using System.Linq;

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

    #region 奖励界面相关参数
    /// <summary>
    /// 在奖励界面，玩家是否选择了骰面
    /// </summary>
    public bool ifSelectedDice;
    /// <summary>
    /// 在奖励界面，玩家是否选择了圣物
    /// </summary>
    public bool ifSelectedHalidom;
    /// <summary>
    /// 暂时将当前玩家roll出的钱存起来，方便扣除，再重新添加
    /// </summary>
    [HideInInspector]
    public int currentRollMoney;
    #endregion
    public ChaState playerChaState;
    public ChaState[] enemyChaStates;
    public PlayerDataSO playerDataSO;
    //TODO:这边的敌人数据还没有
    public EnemyDataSO enemyDataSO;
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
    #region 参数设置
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
        this.parameter.playerChaState.ModResources(new ChaResource(0, 0, -1, 0));
    }
    #endregion
    /// <summary>
    /// 给ui调用的方法，结束玩家回合
    /// </summary>
    public void EndPlayerRound()
    {
        this.parameter.playerChaState.GetBattleDiceHandler().ClearBattleSingleDices();
        RollingResultUIManager.Instance.RemoveAllResultUI(Strategy.End);
        TransitionState(GameState.PlayerRoundEndResolution);
    }

    public void EndGame(int side)
    {
        if (side == 0)
        {
            Debug.Log("玩家死亡");
            TransitionState(GameState.PlayerLose);
        }
        else
        {
            Debug.Log("敌人死亡");
            TransitionState(GameState.PlayerWin);
        }
    }

    #region 投骰子相关
    public void RollDice()
    {
        DataUIManager.Instance.UpdateRerollText(this.parameter.playerChaState.resource.currentRollTimes);
        List<SingleDiceObj> singleDiceObjs = this.parameter.playerChaState.GetBattleDiceHandler().GetRandomSingleDices();
        this.parameter.playerChaState.GetBattleDiceHandler().AddBattleSingleDice(singleDiceObjs);
        for (int i = 0; i < singleDiceObjs.Count; i++)
        {
            Vector2Int pos = new Vector2Int(i, singleDiceObjs[i].positionInDice);
            var singleDiceUIData = ResourcesManager.GetSingleDiceUIData(singleDiceObjs[i]);
            RollingResultUIManager.Instance.CreateResult(i, singleDiceUIData, pos, false);
        }
    }
    //需要绑定到UI的按钮上的函数
    public void ReRollDice()
    {
        //如果玩家的重新投掷次数小于等于0，就不执行
        if (this.parameter.playerChaState.resource.currentRollTimes <= 0)
        {
            return;
        }
        ReducePlayerRerollCount();
        DataUIManager.Instance.UpdateRerollText(this.parameter.playerChaState.resource.currentRollTimes);
        //判断当前处于什么状态，如果是玩家回合，则重新投掷骰面，如果是奖励阶段，则只是显示重新投掷的骰面
        if (currentState is PlayerActionState)
        {
            ReRollDiceForPlayer();
        }
        else if (currentState is RewardState)
        {
            CreateRewards(3);
        }

    }
    /// <summary>
    /// 在战斗中为玩家重新投掷骰子
    /// </summary>
    public void ReRollDiceForPlayer()
    {
        //放置重新投掷的骰面的数组
        SingleDiceObj[] singleDiceObjs = new SingleDiceObj[this.parameter.playerChaState.GetBattleDiceHandler().battleDiceCount];
        //根据当前还剩多少战斗骰面，来决定重新投掷多少个
        for (int i = 0; i < singleDiceObjs.Length; i++)
        {
            if (this.parameter.playerChaState.GetBattleDiceHandler().diceCardsInUse[i] != null)
            {
                //获取剩余的骰面的战斗骰子在战斗骰子列表中的位置,并重新投掷
                singleDiceObjs[i] = this.parameter.playerChaState.GetBattleDiceHandler().GetRandomSingleDice(i);
            }
        }
        this.parameter.playerChaState.GetBattleDiceHandler().AddBattleSingleDice(singleDiceObjs.ToList<SingleDiceObj>());
        //这边需要删除所有当前骰面的视觉

        RollingResultUIManager.Instance.RemoveAllResultUI(Strategy.ReRoll);
        for (int i = 0; i < singleDiceObjs.Length; i++)
        {
            if (singleDiceObjs[i] == null)
            {
                continue;
            }
            Vector2Int pos = new Vector2Int(i, singleDiceObjs[i].positionInDice);
            var singleDiceUIData = ResourcesManager.GetSingleDiceUIData(singleDiceObjs[i]);
            RollingResultUIManager.Instance.CreateResult(i, singleDiceUIData, pos, false);
        }
    }
    /// <summary>
    /// 给敌人投掷骰子的方法，并且创建意图 
    /// </summary>
    /// <param name="chaState"></param>
    public void RollDiceForEnemy(ChaState chaState)
    {
        var singleDiceObjs = chaState.GetBattleDiceHandler().GetRandomSingleDices();
        chaState.GetBattleDiceHandler().AddBattleSingleDice(singleDiceObjs);
        foreach (var singleDice in singleDiceObjs)
        {
            CharacterUIManager.Instance.CreateIntentionUIObject(singleDice.model.id);
        }
    }
    #endregion
    #region 奖励界面相关函数
    /// <summary>
    /// 进入奖励界面时调用，给玩家加钱
    /// </summary>
    public void AddMoneyWhenEnterRewardState(List<SingleDiceObj> singleDiceObjs)
    {
        int money = RandomManager.Instance.GetMoneyViaRollResult(singleDiceObjs);
        Debug.Log("BattleManager:当前玩家改变金钱" + money);
        //先扣除之前roll出的钱
        var lastResource = new ChaResource(0, -this.parameter.currentRollMoney, 0, 0);
        this.parameter.playerChaState.ModResources(lastResource);
       //再添加现在roll出的钱
        var currentResource = new ChaResource(0, money, 0, 0);
        this.parameter.playerChaState.ModResources(currentResource);
        this.parameter.currentRollMoney = money;
        Debug.Log("BattleManager:当前玩家的金钱" + this.parameter.playerChaState.resource.currentMoney);
    }
    /// <summary>
    /// 判断骰面是否可以选择,在进入奖励界面时调用，售卖骰面时也要调用
    /// </summary>
    public void RefreshIfDiceCanChoose()
    {
        int currentBagCount = this.parameter.playerChaState.GetBattleDiceHandler().bagDiceCards.Count;
        int maxBagCount = this.parameter.playerChaState.GetBattleDiceHandler().maxDiceInBag;
        if (currentBagCount >= maxBagCount)
        {
            UIManager.Instance.rewardUIManager.DisableAllDices();
            Debug.Log("背包已满");
            return;
        }
        else
        {
            UIManager.Instance.rewardUIManager.EnableAllDices();
        }
        //如果玩家已经选择了骰面，就不再刷新是否能选择骰面
        if (this.parameter.ifSelectedDice == true)
        {
            return;
        }
    }
    /// <summary>
    /// 向玩家背包添加骰面的函数
    /// </summary>
    /// <param name="singleDiceObj"></param>
    public void AddSingleDiceToPlayerBag(SingleDiceObj singleDiceObj)
    {
        int currentBagCount = this.parameter.playerChaState.GetBattleDiceHandler().bagDiceCards.Count;
        int maxBagCount = this.parameter.playerChaState.GetBattleDiceHandler().maxDiceInBag;
        if (currentBagCount >= maxBagCount)
        {

            //Debug.Log("背包已满");
            return;
        }
        this.parameter.playerChaState.GetBattleDiceHandler().AddSingleBattleDiceToBag(singleDiceObj);
        //获取当前骰面在背包骰面中的位置
        int index = this.parameter.playerChaState.GetBattleDiceHandler().GetIndexOfSingleDiceInBag(singleDiceObj);
        var singleDiceUIData = ResourcesManager.GetSingleDiceUIData(singleDiceObj);
        BagDiceUIManager.Instance.CreateBagUIDice(index, singleDiceUIData, SellSingleDice, singleDiceObj);
        //已经选中过骰面
        this.parameter.ifSelectedDice = true;
    }
    /// <summary>
    /// 创建奖励界面的骰面
    /// </summary>
    /// <param name="count"></param>
    public void CreateRewards(int count)
    {
        Debug.Log("BattleManager:ifSelectedHalidom" + this.parameter.ifSelectedHalidom + "ifSelectedDice" + this.parameter.ifSelectedDice);
        //清空之前的奖励骰面
        for (int i = 0; i < count; i++)
        {
            UIManager.Instance.rewardUIManager.RemoveDiceUI(i);
        }
        //清空之前的圣物 
        UIManager.Instance.rewardUIManager.RemoveSacredObject(0);
        //清空之前的roll出的骰面
        RollingResultUIManager.Instance.RemoveAllResultUI(Strategy.ReRoll);
        //先roll出骰面
        List<SingleDiceObj> singleDiceObjs = this.parameter.playerChaState.GetBattleDiceHandler().GetRandomSingleDices();
        //创建roll骰子效果
        //创建roll骰子的UI效果
        for (int i = 0; i < singleDiceObjs.Count; i++)
        {
            Vector2Int pos = new Vector2Int(i, singleDiceObjs[i].positionInDice);
            var singleDiceUIData = ResourcesManager.GetSingleDiceUIData(singleDiceObjs[i]);
            RollingResultUIManager.Instance.CreateResult(i, singleDiceUIData, pos, true);
        }

        //获得根据roll出骰面的结果，获取奖励的骰面
        if(this.parameter.ifSelectedDice == false)
        {
            CreateRewardDices(singleDiceObjs, count);
        }
        if(this.parameter.ifSelectedHalidom == false)
        {
            CreateRewardHalidom(singleDiceObjs);
        }
        //给钱
        AddMoneyWhenEnterRewardState(singleDiceObjs);
    }
    /// <summary>
    /// 根据玩家的骰面，创建奖励界面的骰面
    /// </summary>
    /// <param name="singleDiceObjs"></param>
    public void CreateRewardDices(List<SingleDiceObj> singleDiceObjs, int count)
    {
        var resultDiceObjs = RandomManager.Instance.GetRewardSingleDiceObjsViaPlayerData(singleDiceObjs, count);
        for (int i = 0; i < resultDiceObjs.Count; i++)
        {
            var singleDiceUIData = ResourcesManager.GetSingleDiceUIData(resultDiceObjs[i]);
            UIManager.Instance.rewardUIManager.CreateDiceUI(singleDiceUIData, i, AddSingleDiceToPlayerBag, resultDiceObjs[i]);
        }
        RefreshIfDiceCanChoose();
    }
    /// <summary>
    /// 售卖单个骰面
    /// </summary>
    /// <param name="index"></param>
    public void SellSingleDice(SingleDiceObj singleDiceObj)
    {
        this.parameter.playerChaState.GetBattleDiceHandler().RemoveSingleBattleDiceFromBag(singleDiceObj);
        var resource = new ChaResource(0, singleDiceObj.model.value, 0, 0);
        this.parameter.playerChaState.ModResources(resource);
        //获取当前骰面在背包骰面中的位置
        RefreshIfDiceCanChoose();
    }
    /// <summary>
    /// 根据roll出的骰面，创建奖励界面的圣物
    /// </summary>
    /// <param name="singleDiceObjs"></param>
    public void CreateRewardHalidom(List<SingleDiceObj> singleDiceObjs)
    {
        var resultHalidom = RandomManager.Instance.GetRewardHalidomViaPlayerData(singleDiceObjs);
        if(resultHalidom != null)
        {
            UIManager.Instance.rewardUIManager.CreateSacredObject(resultHalidom.id, 0,AddHalidomToHalidomManager,resultHalidom);
        }
        UIManager.Instance.rewardUIManager.DisableAllSacredObject();
        RefreshIfHalodomCanChoose();
    }
    
    /// <summary>
    /// 刷新是否可以选择圣物，进入奖励界面时调用，售卖圣物时也要调用
    /// </summary>
    public void RefreshIfHalodomCanChoose()
    {
        if (!HalidomManager.Instance.IsFull())
        {
            UIManager.Instance.rewardUIManager.EnableAllSacredObject();
        }
        else
        {
            UIManager.Instance.rewardUIManager.DisableAllSacredObject();
        }
        if (this.parameter.ifSelectedHalidom == true)
        {
            UIManager.Instance.rewardUIManager.DisableAllSacredObject();
        }
    }
    /// <summary>
    /// 将选中的圣物添加到圣物管理器中，这边待定
    /// </summary>
    /// <param name="index"></param>
    public void AddHalidomToHalidomManager(HalidomObject halidomObject)
    {
        if(HalidomManager.Instance.IsFull())
        {
            return;
        }
        HalidomManager.Instance.AddHalidom(halidomObject);//创建视觉效果的也包含在这个函数中
        this.parameter.ifSelectedHalidom = true;
    }
    #endregion



}
//定义了一个回调，用于在UI动画结束时调用
public delegate void OnUIAnimFinished();

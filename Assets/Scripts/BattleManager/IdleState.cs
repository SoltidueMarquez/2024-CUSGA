using DesignerScripts;
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
        //TODO:读入所有数据，暂时是这样，后面会改
        manager.parameter.playerChaState.GetBattleDiceHandler().InitDice(0);
        //创建骰子页面
        for (int i = 0; i < manager.parameter.playerChaState.GetBattleDiceHandler().battleDiceCount; i++)
        {
            //获取
            var singleDices = manager.parameter.playerChaState.GetBattleDiceHandler().battleDices[i].GetBattleDiceSingleDices();
            string name = $"页面:{i + 1}";
            FightDicePageManager.Instance.CreatePageUI(name, singleDices);//UI创建page
        }
        manager.parameter.playerChaState.Initialize();
        //HalidomManager.Instance.AddHalidom(DesignerScripts.HalidomData.halidomDictionary["Halidom_1"]);
        //根据敌人的数量初始化敌人
        for (int i = 0; i < manager.parameter.enemyChaStates.Length; i++)
        {
            manager.parameter.enemyChaStates[i].GetBattleDiceHandler().InitDice(1);
            manager.parameter.enemyChaStates[i].Initialize();
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
        //Test
        Debug.Log("Test内容-------------------");
        Debug.Log("玩家的生命值是" + manager.parameter.playerChaState.prop.health);
        Debug.Log("Test内容-------------------");



        Debug.Log("Enter PreparationState");

        ProcessPromptUIManager.Instance.DoFightStartUIAnim(() => { manager.TransitionState(GameState.PlayerRoundStartResolution); });

    }

    public void OnExit()
    {
        Debug.Log("Exit PreparationState");
    }

    public void OnUpdate()
    {
        //跳转到玩家判定阶段
        //manager.TransitionState(GameState.PlayerRoundStartResolution);
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
        manager.parameter.playerChaState.OnRoundStart();

        //TODO播放自动投骰子动画
        Debug.Log("播骰子动画");
        //自动投骰子
        manager.RollDice();//这边也需要调用UI的投骰子动画
        //TODO:封装投骰子函数给UI调用
        //TODO:更新UI
        foreach (var enemy in manager.parameter.enemyChaStates)
        {
            manager.RollDiceForEnemy(enemy);
        }
        Debug.Log("根据投掷结果更新UI");
        //TODO:敌人投骰子
        Debug.Log("敌人投骰子");
        ProcessPromptUIManager.Instance.ShowTip(Turn.Player, () => { manager.TransitionState(GameState.PlayerAction); });


    }

    public void OnExit()
    {
        Debug.Log("Exit PlayerRoundStartResolutionState");

        //Test
        //if (manager.parameter.turns == 1)
        //{
        //    Debug.Log("Test---------------------");
        //    HalidomManager.Instance.AddHalidom(DesignerScripts.HalidomData.halidomDictionary[HalidomName.烤土豆.ToString()]);
        //    Debug.Log("添加圣物烤土豆");
        //    Debug.Log("玩家血量上限为"+manager.parameter.playerChaState.prop.health);
        //    Debug.Log("Test---------------------");
        //}

        //if(manager.parameter.turns == 2)
        //{
        //    Debug.Log("Test---------------------");
        //    HalidomManager.Instance.RemoveHalidom(2);
        //    Debug.Log("移除圣物烤土豆");
        //    Debug.Log("玩家血量上限为" + manager.parameter.playerChaState.prop.health);
        //    Debug.Log("Test---------------------");
        //}


    }

    public void OnUpdate()
    {

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
        Debug.Log("触发所有的圣物了");
        //触发角色里所有buff的OnRoundEnd回调点
        manager.parameter.playerChaState.OnRoundEnd();


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
        CharacterUIManager.Instance.RemoveAllIntentionUIObject();
        //TODO：圣物所有在敌人判定阶段触发的回调点
        //触发所有敌人身上挂载的buff的OnRoundStart回调点
        foreach (var enemy in manager.parameter.enemyChaStates)
        {
            enemy.OnRoundStart();
        }

        ProcessPromptUIManager.Instance.ShowTip(Turn.Enemy, () => { manager.TransitionState(GameState.EnemyAction); });
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
        foreach (var enemy in manager.parameter.enemyChaStates)
        {
            enemy.gameObject.GetComponent<EnemyAI>().SimpleAI();
        }

    }

    public void OnExit()
    {
        Debug.Log("退出EnemyActionState");
    }

    public void OnUpdate()
    {
        ////TODO: 敌人AI逻辑
        //    foreach (var enemy in manager.parameter.enemyChaStates)
        //    {
        //        enemy.GetBattleDiceHandler().CastDiceAll(enemy, manager.parameter.playerChaState.gameObject);
        //        DamageManager.Instance.DealWithAllDamage();
        //    }
        //    manager.TransitionState(GameState.EnemyRoundEndResolution);

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
        foreach (var enemy in manager.parameter.enemyChaStates)
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
        ProcessPromptUIManager.Instance.DoFightEndUIAnim(null);
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
        ProcessPromptUIManager.Instance.DoFightEndUIAnim(null);
        BattleManager.Instance.TransitionState(GameState.Reward);

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
        //创建三个奖励骰面和圣物
        manager.CreateRewards(3);

    }

    public void OnExit()
    {
        Debug.Log("Exit RewardState");
    }

    public void OnUpdate()
    {

    }
}
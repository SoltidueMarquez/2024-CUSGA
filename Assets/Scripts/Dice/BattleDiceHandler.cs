using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家和敌人身上挂载的战斗骰子管理器
/// </summary>
public class BattleDiceHandler : MonoBehaviour
{
    /// <summary>
    /// 玩家和敌人身上的战斗骰子
    /// </summary>
    public List<BattleDice> battleDices = new List<BattleDice>();
    /// <summary>
    /// 玩家和敌人身上的背包骰面
    /// </summary>
    public List<SingleDiceObj> bagDiceCards = new List<SingleDiceObj>();
    /// <summary>
    /// 玩家和敌人身上能使用的骰面
    /// </summary>
    public List<SingleDiceObj> diceCardsInUse = new List<SingleDiceObj>();


}

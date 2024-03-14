using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 玩家和敌人身上挂载的战斗骰子
/// </summary>
public class BattleDice
{
   /// <summary>
   /// 玩家当前的战斗用骰面
   /// </summary>
    public List<SingleDiceObj> currentBattleDice = new List<SingleDiceObj>();

    public List<SingleDiceObj> bagDices = new List<SingleDiceObj>();

    public void AddDice(SingleDiceModel dice)
    {
        
    }
    /// <summary>
    /// 使用骰面
    /// </summary>
    /// <param name="dice"></param>
    public void UseDice(SingleDiceObj dice)
    {
        
    }
}

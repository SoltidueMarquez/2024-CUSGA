using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ����ս�����ӵ�runtime����
/// </summary>
public class BattleDice
{
    /// <summary>
    /// BattleDice������
    /// </summary>
    List<SingleDiceObj> diceObjs = new List<SingleDiceObj>();
    /// <summary>
    /// ��ս�����ӵ����࣬�������ֲ�ͬ��ս������
    /// </summary>
    public DiceType diceType;

    public BattleDice(DiceType diceType)
    {
        this.diceType = diceType;
    }
    /// <summary>
    /// ��ʼ���������ӵ�ʱ����Ҫ���뵥�����ӵ�����
    /// </summary>
    /// <param name="singleDiceModel"></param>
    public void AddDice(SingleDiceModel singleDiceModel, int idInDice)
    {
        SingleDiceObj singleDiceObj = new SingleDiceObj(singleDiceModel, idInDice);
        diceObjs.Add(singleDiceObj);
    }


    
}
/// <summary>
/// ս�����ӵ�����
/// </summary>
public enum DiceType
{
    Attack,
    Defense,
    Support
}

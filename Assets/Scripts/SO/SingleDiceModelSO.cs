using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "SingleDiceData", menuName = "Data/SingleDiceData")]
public class SingleDiceModelSO : ScriptableObject
{
    [Tooltip("骰子单面的唯一id,用于和类型加在一起")]
    [Header("骰子单面的唯一id")]
    public string id;
    [Tooltip("骰面的名字")]
    [Header("骰面的名字")]
    public string singleDiceModelName;

    [Tooltip("属于哪种类型的骰子")]
    [Header("属于哪种类型的骰子")]
    public DiceType type;

    [Tooltip("骰子的使用条件")]
    [Header("骰子的使用条件")]
    public ChaResource condition;

    [Tooltip("骰子的消耗")]
    [Header("骰子的消耗")]
    public ChaResource cost;

    [Tooltip("骰面的等级")]
    [Header("骰面的等级")]
    public int level;
    [Tooltip("骰面的售价")]
    [Header("骰面的售价")]
    public int value;
    [Tooltip("骰面属于哪一方")]
    [Header("骰面属于哪一方")]
    public int side;
    [Tooltip("骰面的Bufflist")]
    public List<BuffDataSO> buffDataSOs;
    [Header("给UI的属性")]
    public Sprite sprite;
    [Multiline(5)]
    public string description;
    [Header("骰面的数值")]
    public int baseValue;

}

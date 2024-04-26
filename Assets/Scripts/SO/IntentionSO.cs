using UnityEngine;
/// <summary>
/// 敌人的意图
/// </summary>
[CreateAssetMenu(fileName = "Intention_", menuName = "Data/IntentionData")]
public class IntentionSO : ScriptableObject
{
    /// <summary>
    /// 描述
    /// </summary>
    [Header("骰子类型")]
    public DiceType diceType;
    [Header("意图描述")]
    public string description;
    [Header("意图名称")]
    public string intentionName;
    [Header("意图图标")]
    public Sprite icon;
}

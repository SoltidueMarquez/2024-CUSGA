using UnityEngine;
/// <summary>
/// ���˵���ͼ
/// </summary>
[CreateAssetMenu(fileName = "Intention_", menuName = "Data/IntentionData")]
public class IntentionSO : ScriptableObject
{
    /// <summary>
    /// ����
    /// </summary>
    [Header("��������")]
    public DiceType diceType;
    [Header("��ͼ����")]
    public string description;
    [Header("��ͼ����")]
    public string intentionName;
    [Header("��ͼͼ��")]
    public Sprite icon;
}

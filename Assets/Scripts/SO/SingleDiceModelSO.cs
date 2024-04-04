using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SingleDiceData", menuName = "Data/SingleDiceData")]
public class SingleDiceModelSO : ScriptableObject
{
    [Tooltip("���ӵ����Ψһid,���ں����ͼ���һ��")]
    [Header("���ӵ����Ψһid")]
    public string id;
    [Tooltip("���������")]
    [Header("���������")]
    public string singleDiceModelName;

    [Tooltip("�����������͵�����")]
    [Header("�����������͵�����")]
    public DiceType type;

    [Tooltip("���ӵ�ʹ������")]
    [Header("���ӵ�ʹ������")]
    public ChaResource condition;

    [Tooltip("���ӵ�����")]
    [Header("���ӵ�����")]
    public ChaResource cost;

    [Tooltip("����ĵȼ�")]
    [Header("����ĵȼ�")]
    public int level;
    [Tooltip("������ۼ�")]
    [Header("������ۼ�")]
    public int value;
    [Tooltip("����������һ��")]
    [Header("����������һ��")]
    public int side;
    [Tooltip("�����Bufflist")]
    public List<BuffDataSO> buffDataSOs;
}

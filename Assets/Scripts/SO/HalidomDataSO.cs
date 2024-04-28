using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Halidom2BuffParamMap
{
    [Header("圣物描述中的关键字")]
    public string key;
    [Header("圣物描述中的关键字对应的buffInfo中的buffParamkey")]
    public string value;
}
namespace DesignerScripts
{

    [CreateAssetMenu(fileName = "HalidomData_", menuName = "Data/HalidomData")]
    public class HalidomDataSO : ScriptableObject
    {
        [Tooltip("圣物唯一id标识")]
        public string id;

        [Tooltip("圣物名称")]
        public HalidomName halidomName;

        [Tooltip("圣物描述")]
        public string description;

        [Tooltip("圣物的稀有度")]
        public RareType rareType;

        [Tooltip("圣物的售价")]
        public int value = 1;

        [Tooltip("圣物身上存的buff信息")]
        public List<BuffDataSO> buffDataSos;

        [Tooltip("圣物的icon")]
        public Sprite sprite;

        public List<Halidom2BuffParamMap> halidom2BuffParamMaps;
    }
}
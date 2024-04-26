using System.Collections.Generic;
using UnityEngine;

namespace DesignerScripts
{
    [CreateAssetMenu(fileName = "HalidomData_", menuName = "Data/HalidomData")]
    public class HalidomDataSO : ScriptableObject
    {
        [Tooltip("ʥ��Ψһid��ʶ")]
        public string id;

        [Tooltip("ʥ������")]
        public HalidomName halidomName;
        
        [Tooltip("ʥ������")]
        public string description;

        [Tooltip("ʥ���ϡ�ж�")]
        public RareType rareType;

        [Tooltip("ʥ����ۼ�")]
        public int value=1;

        [Tooltip("ʥ�����ϴ��buff��Ϣ")] 
        public List<BuffDataSO> buffDataSos;

        [Tooltip("ʥ���icon")]
        public Sprite sprite;
    }
}
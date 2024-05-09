using System;
using Map;
using UnityEngine;
using UnityEngine.Events;

namespace Rest
{
    [Serializable]
    public enum RestType
    {
        Rest,
        Bless
    }
    public class RestManager : MonoSingleton<RestManager>
    {
        [Tooltip("进入休息界面事件")]public UnityEvent onEnterRest;
        [Tooltip("离开休息界面事件")]public UnityEvent onExitRest;
        [Tooltip("选择休息事件")]public UnityEvent onChooseRest;
        [Tooltip("选择祝福事件")] public UnityEvent onChooseBlessing;
        [Tooltip("更改描述")] public UnityEvent<RestType> changeDescText;
        [Tooltip("隐藏描述")] public UnityEvent hideDescText;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            onChooseRest.AddListener(() =>
            {
                Debug.Log("执行回血");
                MapManager.Instance.CurePlayer(999);
            });
            onChooseBlessing.AddListener(() =>
            {
                Debug.Log("执行加cost上限");
                MapManager.Instance.EnhanceMaxCost(1);
            });
        }
        
        #region 执行事件函数
        public void ChangeDesc(RestType type)
        {
            changeDescText.Invoke(type);
        }
        public void HideDesc()
        {
            hideDescText.Invoke();
        }
        public void EnterRest()
        {
            onEnterRest.Invoke();
        }
        public void ExitRest()
        {
            onExitRest.Invoke();
        }
        public void ChooseRest()
        {
            onChooseRest.Invoke();
        }
        public void ChooseBlessing()
        {
            onChooseBlessing.Invoke();
        }
        #endregion
    }
}

using System;
using JetBrains.Annotations;
using UnityEngine;

namespace UI
{
    public enum Turn
    {
        Player,
        Enemy
    }
    public class ProcessPromptUIManager : MonoSingleton<ProcessPromptUIManager>
    {
        
        [SerializeField,Tooltip("流程提示")]private ProcessPromptUIObject processPrompt;
        [SerializeField, Tooltip("玩家流程提示文字")] private string playerTip;
        [SerializeField, Tooltip("敌人流程提示文字")] private string enemyTip;
        [SerializeField,Tooltip("出现时间")]private float appearDurationTime;
        [SerializeField,Tooltip("消失时间")]private float fadeDurationTime;
        [SerializeField, Tooltip("UI")] private GameObject panel;

        /// <summary>
        /// 显示流程提示
        /// </summary>
        /// <param name="who">提示对象的枚举</param>
        public void ShowTip(Turn who)
        {
            panel.SetActive(true);
            string tip;
            switch (who)
            {
                case Turn.Player:
                    tip = playerTip;
                    break;
                case Turn.Enemy:
                    tip = enemyTip;
                    break;
                default:
                    tip = "";
                    break;
            }
            processPrompt.Appear(appearDurationTime, tip); //出现提示
            Invoke(nameof(HideTip),appearDurationTime);
        }
        
        private void HideTip()
        {
            processPrompt.Fade(fadeDurationTime);//出现提示
            Invoke(nameof(HidePanel),fadeDurationTime);
        }

        private void HidePanel(OnUIAnimFinished onUIAnimFinished)
        {
            panel.SetActive(false);
            if (onUIAnimFinished != null)
            {
                Invoke(nameof(onUIAnimFinished),0f);//结束时调用
            }
        }
    }
}

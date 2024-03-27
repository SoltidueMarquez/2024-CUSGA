using System;
using System.Collections;
using DG.Tweening;
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
        [SerializeField, Tooltip("敌人的位置(Y)")] private float enemyYPosition;
        [SerializeField, Tooltip("玩家的位置(Y)")] private float playerYPosition;

        public void DoFightStartUIAnim()
        {
            panel.SetActive(true);
            string tip = "战斗开始";
            processPrompt.Appear(appearDurationTime, tip); //出现提示
            StartCoroutine(HideTip());
            
            //敌人从屏幕外依次下降出现
            CharacterUIManager.Instance.enemy.DOMoveY(enemyYPosition, appearDurationTime);
            CharacterUIManager.Instance.player.DOMoveY(playerYPosition, appearDurationTime);
        }
        
        /// <summary>
        /// 显示流程提示
        /// </summary>
        /// <param name="who">提示对象的枚举</param>
        /// <param name="onUIAnimFinished">结束委托</param>
        public void ShowTip(Turn who,OnUIAnimFinished onUIAnimFinished)
        {
            panel.SetActive(true);
            string tip;
            switch (who)
            {
                case Turn.Player:
                    tip = playerTip;
                    ProcessAnimationManager.Instance.PlayerTurnStart();
                    break;
                case Turn.Enemy:
                    tip = enemyTip;
                    break;
                default:
                    tip = "";
                    break;
            }
            processPrompt.Appear(appearDurationTime, tip); //出现提示
            StartCoroutine(HideTip());
            
            if (onUIAnimFinished != null)
            {
                StartCoroutine(AnimFish(onUIAnimFinished)); //结束时调用
            }
        }
        
        IEnumerator AnimFish(OnUIAnimFinished onUIAnimFinished)
        {
            yield return new WaitForSeconds(appearDurationTime + fadeDurationTime);
            onUIAnimFinished.Invoke();
        }
        
        IEnumerator HideTip()
        {
            yield return new WaitForSeconds(appearDurationTime);
            processPrompt.Fade(fadeDurationTime);//出现提示
            StartCoroutine(HidePanel());
        }

        IEnumerator HidePanel()
        {
            yield return new WaitForSeconds(fadeDurationTime);
            panel.SetActive(false);
        }
    }
}

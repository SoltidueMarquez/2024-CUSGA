using System.Collections;
using Audio_Manager;
using DG.Tweening;
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
        [SerializeField, Tooltip("敌人的出现位置(Y)")] private float enemyStartYPosition;
        [SerializeField, Tooltip("敌人的离开位置(Y)")] private float enemyEndYPosition;
        [SerializeField, Tooltip("玩家的出现位置(Y)")] private float playerStartYPosition;
        [SerializeField, Tooltip("玩家的离开位置(Y)")] private float playerEndYPosition;

        private void Start()
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayMusic("Boss");
            }
        }
        
        /// <summary>
        /// 战斗结束UI
        /// </summary>
        /// <param name="onUIAnimFinished"></param>
        public void DoFightEndUIAnim(OnUIAnimFinished onUIAnimFinished)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.StopMusic();
            }
            panel.SetActive(true);
            string tip = "你赢了";
            processPrompt.Appear(appearDurationTime, tip); //出现提示
            StartCoroutine(HideTip());
            //敌人与玩家离开
            CharacterUIManager.Instance.enemy.DOMoveY(enemyEndYPosition, appearDurationTime);
            CharacterUIManager.Instance.player.DOMoveY(playerEndYPosition, appearDurationTime);
            
            if (onUIAnimFinished != null)
            {
                StartCoroutine(AnimFish(onUIAnimFinished)); //结束时调用
            }
            
            //TODO：奖励结算UI出现
            UIManager.Instance.rewardUIManager.ShowRewardUI(appearDurationTime);
            RollingResultUIManager.Instance.RemoveAllResultUI(Strategy.ReRoll);
        }
        
        /// <summary>
        /// 游戏结束流程提示
        /// </summary>
        /// <param name="onUIAnimFinished"></param>
        public void DoGameOverUIAnim(OnUIAnimFinished onUIAnimFinished)
        {
            StartCoroutine(LateGameOverTip(onUIAnimFinished));
        }

        IEnumerator LateGameOverTip(OnUIAnimFinished onUIAnimFinished)
        {
            yield return new WaitForSeconds(appearDurationTime * 0.25f);
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.StopMusic();
            }
            panel.SetActive(true);
            string tip = "你死了";
            processPrompt.Appear(appearDurationTime, tip); //出现提示
            StartCoroutine(HideTip());
            //敌人与玩家离开
            CharacterUIManager.Instance.enemy.DOMoveY(enemyEndYPosition, appearDurationTime);
            CharacterUIManager.Instance.player.DOMoveY(playerEndYPosition, appearDurationTime);
            
            if (onUIAnimFinished != null)
            {
                StartCoroutine(AnimFish(onUIAnimFinished)); //结束时调用
            }
        }
        
        public void DoGameWinUIAnim(OnUIAnimFinished onUIAnimFinished)
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.StopMusic();
            }
            panel.SetActive(true);
            string tip = "胜利，然后凯旋";
            processPrompt.Appear(appearDurationTime, tip); //出现提示
            StartCoroutine(HideTip());
            //敌人与玩家离开
            CharacterUIManager.Instance.enemy.DOMoveY(enemyEndYPosition, appearDurationTime);
            CharacterUIManager.Instance.player.DOMoveY(playerEndYPosition, appearDurationTime);
            
            if (onUIAnimFinished != null)
            {
                StartCoroutine(AnimFish(onUIAnimFinished)); //结束时调用
            }
        }

        /// <summary>
        /// 战斗开始流程UI动画
        /// </summary>
        /// <param name="onUIAnimFinished"></param>
        public void DoFightStartUIAnim(OnUIAnimFinished onUIAnimFinished)
        {
            panel.SetActive(true);
            string tip = "战斗开始";
            processPrompt.Appear(appearDurationTime, tip); //出现提示
            StartCoroutine(HideStartTip());
            
            //敌人与玩家出现
            CharacterUIManager.Instance.enemy.DOMoveY(enemyStartYPosition, appearDurationTime);
            CharacterUIManager.Instance.player.DOMoveY(playerStartYPosition, appearDurationTime);
            
            if (onUIAnimFinished != null)
            {
                StartCoroutine(AnimFish(onUIAnimFinished)); //结束时调用
            }
        }
        IEnumerator HideStartTip()
        {
            yield return new WaitForSeconds(appearDurationTime);
            processPrompt.Fade(fadeDurationTime);//出现提示
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
            yield return StartCoroutine(HidePanel());
        }

        IEnumerator HidePanel()
        {
            yield return new WaitForSeconds(fadeDurationTime);
            panel.SetActive(false);
        }
    }
}

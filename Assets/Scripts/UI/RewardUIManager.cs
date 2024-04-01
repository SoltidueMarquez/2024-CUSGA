using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RewardUIManager : MonoBehaviour
    {
        [SerializeField, Tooltip("奖励UI界面")] private GameObject rewardUI;
        [SerializeField, Tooltip("结束按钮")] private Button endButton;
        [SerializeField, Tooltip("结束按钮")] private Button useAllButton;

        public void ShowRewardUI(float waitSeconds)
        {
            endButton.interactable = false;
            useAllButton.interactable = false;
            //TODO:修改ReRoll函数
            StartCoroutine(RewardUI(waitSeconds));
        }
        IEnumerator RewardUI(float waitSeconds)
        {
            yield return new WaitForSeconds(waitSeconds);
            rewardUI.SetActive(true);
            ProcessAnimationManager.Instance.FightEnd();
        }
        
        
    }
}

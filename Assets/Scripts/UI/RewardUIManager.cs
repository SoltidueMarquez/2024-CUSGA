using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RewardUIManager : MonoBehaviour
    {
        [SerializeField, Tooltip("奖励UI界面")] private GameObject rewardUI;
        [SerializeField, Tooltip("结束按钮")] private Button endButton;
        [SerializeField, Tooltip("全部使用按钮")] private Button useAllButton;
        [SerializeField, Tooltip("骰子栏位列表")] public List<Column> diceColumns;
        [SerializeField, Tooltip("骰子模板")] private GameObject diceTemplate;
        
        [SerializeField, Tooltip("动画时长")] private float animTime;

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

        /// <summary>
        /// 创建骰面函数,其本质仍然是一个战斗骰面页的骰子(子类)
        /// </summary>
        /// <param name="id">id标识</param>
        /// <param name="index">栏位索引</param>
        /// <param name="onChoose">选择骰面后触发的逻辑函数</param>
        public void CreateDiceUI(string id, int index, Action<int> onChoose)
        {
            if (index > diceColumns.Count)
            {
                Debug.LogWarning("超出生成的栏位");
                return;
            }
            if (diceColumns[index].bagObject != null) 
            {
                Debug.LogWarning("栏位已经有骰面");
                return;
            }
            var parent = diceColumns[index].transform;
            var tmp = Instantiate(diceTemplate, parent, true);
            diceColumns[index].bagObject = tmp;
            tmp.transform.position = parent.position;//更改位置
            var tmpDice = tmp.GetComponent<RewardDiceUIObject>();
            tmpDice.Init(id, animTime, 2, onChoose, index);//初始化
            tmp.SetActive(true);
            tmpDice.DoAppearAnim(animTime); //出现动画
        }

        /// <summary>
        /// 移除骰面函数
        /// </summary>
        /// <param name="index">栏位索引</param>
        public void RemoveDiceUI(int index)
        {
            if (index > diceColumns.Count)
            {
                Debug.LogWarning("超出生成的栏位");
                return;
            }
            if (diceColumns[index].bagObject == null) 
            {
                Debug.LogWarning("骰面不存在");
                return;
            }
            var item = diceColumns[index].bagObject.GetComponent<RewardDiceUIObject>();
            diceColumns[index].bagObject = null;
            if (item != null)
            {
                item.DoDestroyAnim(animTime);
            }
        }
    }
}

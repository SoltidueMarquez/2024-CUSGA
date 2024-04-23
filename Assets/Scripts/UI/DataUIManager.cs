using System.Collections;
using DG.Tweening;
using UI.Store;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DataUIManager : MonoSingleton<DataUIManager>
    {
        [SerializeField, Tooltip("动画时长")] private float animTime;
        [SerializeField, Tooltip("回合数文本")] private Text runText;
        [SerializeField, Tooltip("剩余重投次数")] private Text reRollText;
        [SerializeField, Tooltip("金钱文本")] private Text moneyText;
        [SerializeField, Tooltip("血量文本")] private Text healthText;
        private int _currentMoney;//记录当前金币文本
        
        /// <summary>
        /// 更新回合数
        /// </summary>
        /// <param name="run"></param>
        public void UpdateRunTimeText(int run)
        {
            runText.text = $"{run}";
        }
        
        /// <summary>
        /// 更新重投数
        /// </summary>
        /// <param name="time"></param>
        public void UpdateRerollText(int time)
        {
            reRollText.text = $"{time}";
        }

        /// <summary>
        /// 更新金钱
        /// </summary>
        /// <param name="money"></param>
        public void UpdateMoneyText(int money)
        {
            var offset = _currentMoney - money;
            if (offset == 0) { return; }

            StartCoroutine(ChangeMoneyText(money));
        }
        private IEnumerator ChangeMoneyText(int target)
        {
            var offset = Mathf.Abs(_currentMoney - target);
            var step = offset switch
            {
                < 10 => 0.1f,
                < 100 => 0.01f,
                _ => 0.01f
            };
            while (true)
            {
                yield return new WaitForSeconds(step);
                if (_currentMoney < target)
                {
                    _currentMoney++;
                }
                else
                {
                    _currentMoney--;
                }
                moneyText.text = $"￥{_currentMoney}";
                if (_currentMoney == target)
                {
                    break;
                }
            }
        }

        /// <summary>
        /// 更新topUI的血量文本
        /// </summary>
        /// <param name="health"></param>
        /// <param name="maxHealth"></param>
        public void UpdateHealthText(int health, int maxHealth)
        {
            healthText.text = $"{health}/{maxHealth}";
        }
    }
}

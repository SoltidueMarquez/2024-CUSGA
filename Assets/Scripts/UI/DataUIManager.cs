using System;
using System.Collections;
using Audio_Manager;
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
        private int _newMoney;
        
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
        /// <param name="maxTime"></param>
        public void UpdateRerollText(int time, int maxTime)
        {
            reRollText.text = $"重投{time}/{maxTime}";
        }

        /// <summary>
        /// 更新金钱
        /// </summary>
        /// <param name="money"></param>
        public void UpdateMoneyText(int money)
        {
            StopCoroutine(ChangeMoneyText());
            _newMoney = money;
            var offset = _currentMoney - money;
            switch (offset)
            {
                case 0:
                    return;
                case > 0:
                    if (AudioManager.Instance != null)
                    {
                        AudioManager.Instance.PlaySound("loseMoney");
                    }
                    break;
                case <0:
                    if (AudioManager.Instance != null)
                    {
                        AudioManager.Instance.PlaySound("getMoney");
                    }
                    break;
            }
            StartCoroutine(ChangeMoneyText());
        }
        private IEnumerator ChangeMoneyText()
        {
            var offset = Mathf.Abs(_currentMoney - _newMoney);
            var step = offset switch
            {
                < 10 => 0.1f,
                < 100 => 0.01f,
                _ => 0.01f
            };
            while (_currentMoney != _newMoney)
            {
                yield return new WaitForSeconds(step);
                if (_currentMoney < _newMoney)
                {
                    _currentMoney++;
                }
                else
                {
                    _currentMoney--;
                }
                moneyText.text = $"￥{_currentMoney}";
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

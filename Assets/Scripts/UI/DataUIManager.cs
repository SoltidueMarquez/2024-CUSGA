using System;
using System.Collections;
using Audio_Manager;
using UI.Store;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public class DataUIManager : MonoSingleton<DataUIManager>
    {
        [SerializeField, Tooltip("动画时长")] private float animTime;
        [SerializeField, Tooltip("回合数文本")] private Text runText;
        [SerializeField, Tooltip("剩余重投次数")] private Text reRollText;
        [SerializeField, Tooltip("剩余进货次数")] private Text refreshStoreText;
        [SerializeField, Tooltip("金钱文本")] private Text moneyText;
        [SerializeField, Tooltip("血量文本")] private Text healthText;
        [SerializeField, Tooltip("费用文本")] public Text costText;
        [SerializeField, Tooltip("筹码文本")] public Text bargainText;
        
        private int _currentMoney;//记录当前金币文本
        private int _newMoney;
        private int _currentCost;//记录当前金币文本
        private int _newCost;
        private int _currentBargain;//记录当前筹码文本;
        private int _newBargain;

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
            if (reRollText == null)
            {
                reRollText = RollUIManager.Instance.GetReRollText();
            }
            reRollText.text = $"{time}/{maxTime}";
        }
        
        /// <summary>
        /// 更新进货次数
        /// </summary>
        /// <param name="time"></param>
        public void UpdateRefreshStoreText(int time)
        {
            if (refreshStoreText == null) { return; }
            refreshStoreText.text = $"×{time}";
        }

        #region 费用
        /// <summary>
        /// 更新费用文本
        /// </summary>
        /// <param name="cost"></param>
        public void UpdateCostText(int cost)
        {
            if (costText == null)
            {
                return;
            }
            StopCoroutine(ChangeCostText());
            _newCost = cost;
            var offset = _currentCost - cost;
            StartCoroutine(ChangeCostText());
        }
        private IEnumerator ChangeCostText()
        {
            var offset = Mathf.Abs(_currentCost - _newCost);
            var step = offset switch
            {
                < 10 => 0.1f,
                < 100 => 0.01f,
                _ => 0.01f
            };
            while (_currentCost != _newCost)
            {
                yield return new WaitForSeconds(step);
                if (_currentCost < _newCost)
                {
                    _currentCost++;
                }
                else
                {
                    _currentCost--;
                }
                costText.text = $"Cost:{_currentCost}/{BattleManager.Instance.GetCurrentMaxCost()}";
            }
        }
        #endregion
        
        #region 金钱
        /// <summary>
        /// 更新金钱
        /// </summary>
        /// <param name="money"></param>
        public void UpdateMoneyText(int money,bool ifPlaySound)
        {
            StopCoroutine(ChangeMoneyText());
            _newMoney = money;
            var offset = _currentMoney - money;
            if (ifPlaySound)
            {
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
        #endregion

        #region 筹码
        /// <summary>
        /// 更新费用文本
        /// </summary>
        /// <param name="bargain"></param>
        public void UpdateBargainText(int bargain)
        {
            if (bargainText == null)
            {
                return;
            }
            StopCoroutine(ChangeBargainText());
            _newBargain = bargain;
            StartCoroutine(ChangeBargainText());
        }
        private IEnumerator ChangeBargainText()
        {
            const float step = 0.01f;
            while (_currentBargain != _newBargain)
            {
                yield return new WaitForSeconds(step);
                if (_currentBargain < _newBargain)
                {
                    _currentBargain++;
                }
                else
                {
                    _currentBargain--;
                }
                bargainText.text = $"{_currentBargain}";
            }
        }
        #endregion
        
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

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
        private int _currentMoney;//记录当前金币文本
        
        public void UpdateRunTimeText(int run)
        {
            runText.text = $"{run}";
        }
        
        public void UpdateRerollText(int time)
        {
            reRollText.text = $"{time}";
        }

        public void UpdateMoneyText(int money)
        {
            var offset = _currentMoney - money;
            if (offset == 0) { return; }

            StartCoroutine(ChangeMoneyText(money));
        }
        IEnumerator ChangeMoneyText(int target)
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
                Debug.Log("每1秒执行一次");
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
    }
}

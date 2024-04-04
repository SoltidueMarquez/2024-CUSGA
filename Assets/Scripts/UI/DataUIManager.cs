using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DataUIManager : MonoSingleton<DataUIManager>
    {
        [SerializeField, Tooltip("回合数文本")] private Text runText;
        [SerializeField, Tooltip("剩余重投次数")] private Text reRollText;
        [SerializeField, Tooltip("金钱文本")] private Text moneyText;
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
            moneyText.DOText($"￥{money}", 0.1f);
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DataUIManager : MonoSingleton<DataUIManager>
    {
        [SerializeField, Tooltip("回合数文本")] private Text RunText;
        [SerializeField, Tooltip("剩余重投次数")] private Text ReRollText;
        public void UpdateRunTimeText(int run)
        {
            RunText.text = $"{run}";
        }
        
        public void UpdateReRollText(int time)
        {
            ReRollText.text = $"{time}";
        }
    }
}

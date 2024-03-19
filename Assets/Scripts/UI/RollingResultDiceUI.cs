using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class RollingResultDiceUI : MonoBehaviour
    {
        [SerializeField,Tooltip("投掷结果序列号")]private int index;
        [SerializeField,Tooltip("所在战斗骰面位置")]private Vector2Int pageAndIndex;

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="tmpIndex">序列号</param>
        /// <param name="position">坐标位置，先页面位置再页内位置</param>
        public void Init(int tmpIndex, Vector2Int position)
        {
            index = tmpIndex;
            pageAndIndex = position;
        }
        private void Start()
        {
            this.GetComponent<Button>().onClick.AddListener(() =>
            {
                BattleManager.Instance.parameter.playerChaStates.UseDice(index, BattleManager.Instance.currentSelectEnemy);
            });
        }

        private void JumpToPosition(Vector2Int position)
        {
            FightDicePageManager.Instance.SwitchToPosition(position);
        }
        
        //ToDo:写骰面预览效果
    }
}

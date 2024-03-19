using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class RollingResultDiceUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField,Tooltip("投掷结果序列号")]private int index;
        [SerializeField,Tooltip("所在战斗骰面位置")]private Vector2Int pageAndIndex;
        [SerializeField, Tooltip("说明UI")] private GameObject infoCanvas;
        [SerializeField, Tooltip("说明Text")] private Text infoText;

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="tmpIndex">结果序列号</param>
        /// <param name="position">坐标位置，先页面位置再页内位置</param>
        /// <param name="id">骰面对应id</param>
        public void Init(int tmpIndex, Vector2Int position, string id)
        {
            index = tmpIndex;
            pageAndIndex = position;
            this.GetComponent<Button>().onClick.AddListener(() =>
            {
                BattleManager.Instance.parameter.playerChaStates.UseDice(index, BattleManager.Instance.currentSelectEnemy);
                Destroy(gameObject);
            });
            
            //ToDo:根据id初始化信息
            infoText.text = id;
        }

        private void JumpToPosition(Vector2Int position)
        {
            FightDicePageManager.Instance.SwitchToPosition(position);
        }

        private void RevertMark()
        {
            FightDicePageManager.Instance.RevertMarkColumn();
        }

        //ToDo:写骰面预览效果
        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject,UIManager.Instance.previewSizeS);
            UIManager.Instance.DoShake(this.GetComponent<Image>(),UIManager.Instance.shakeAngleS);
            JumpToPosition(pageAndIndex);
            infoCanvas.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.ExitPreview(gameObject);
            RevertMark();
            infoCanvas.SetActive(false);
        }
    }
}

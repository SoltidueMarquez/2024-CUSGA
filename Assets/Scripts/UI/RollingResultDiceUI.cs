using DG.Tweening;
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

        private float useDuration;
        private float scale;
        private Vector3 moveOffset;
        private Image image;
        
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="tmpIndex">结果序列号</param>
        /// <param name="position">坐标位置，先页面位置再页内位置</param>
        /// <param name="id">骰面对应id</param>
        public void Init(int tmpIndex, Vector2Int position, string id)
        {
            image = this.GetComponent<Image>();
            useDuration = RollingResultUIManager.Instance.useTime;
            scale = RollingResultUIManager.Instance.scale;
            index = tmpIndex;
            pageAndIndex = position;
            this.GetComponent<Button>().onClick.AddListener(() =>
            {
                BattleManager.Instance.parameter.playerChaState.UseDice(index, BattleManager.Instance.currentSelectEnemy);

                OnUseDestory();//摧毁物体
            });
            
            //ToDo:根据id初始化信息
            infoText.text = id;
            Debug.Log("根据id初始化投掷结果信息");
        }

        public void OnUseDestory()
        {
            moveOffset = CharacterUIManager.Instance.player.position +
                         new Vector3(0, RollingResultUIManager.Instance.moveOffset, 0);
            transform.DOMove(moveOffset, useDuration / 2).OnComplete(() =>
            {
                image.DOFade(0, useDuration / 2);
                transform.DOScale(scale, useDuration / 2).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            });
        }
        
        private void JumpToPosition(Vector2Int position)
        {
            FightDicePageManager.Instance.SwitchToPosition(position);
        }

        private void RevertMark()
        {
            FightDicePageManager.Instance.RevertMarkColumn();
        }

        /// <summary>
        /// 骰面预览
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject,SacredObjectUIManager.Instance.previewSizeS);
            UIManager.Instance.DoShake(this.GetComponent<Image>(),SacredObjectUIManager.Instance.shakeAngleS);
            JumpToPosition(pageAndIndex);
            infoCanvas.SetActive(true);
        }

        /// <summary>
        /// 停止预览
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.ExitPreview(gameObject);
            RevertMark();
            infoCanvas.SetActive(false);
        }
    }
}

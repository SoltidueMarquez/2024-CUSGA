using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace UI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class RollingResultDiceUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField,Tooltip("投掷结果序列号")]private int index;
        [SerializeField,Tooltip("所在战斗骰面位置")]private Vector2Int pageAndIndex;
        [SerializeField, Tooltip("说明UI")] private GameObject infoCanvas;
        [SerializeField, Tooltip("说明Text")] private Text infoText;
        [SerializeField,Tooltip("名称Text")]protected Text nameText;
        [SerializeField,Tooltip("类型Text")]protected Text typeText;
        [SerializeField, Tooltip("稀有度Text")] protected Text levelText;
        [SerializeField,Tooltip("售价Text")]protected Text valueText;
        [SerializeField,Tooltip("基础数值Text")]protected Text baseValueText;
        [SerializeField,Tooltip("点数Text")]protected Text idInDiceText;

        private float useDuration;
        private float scale;
        private Vector3 moveOffset;
        private Image image;
        private Button button;

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="tmpIndex">结果序列号</param>
        /// <param name="position">坐标位置，先页面位置再页内位置</param>
        /// <param name="data">骰面对应data</param>
        public void Init(int tmpIndex, Vector2Int position, SingleDiceUIData data)
        {
            image = this.GetComponent<Image>();
            button = this.GetComponent<Button>();
            useDuration = RollingResultUIManager.Instance.useTime;
            scale = RollingResultUIManager.Instance.scale;
            index = tmpIndex;
            pageAndIndex = position;
            button?.onClick.AddListener(() =>
            {
                BattleManager.Instance.parameter.playerChaState.UseDice(index, BattleManager.Instance.currentSelectEnemy);
                OnUseDestroy();//摧毁物体
            });
            
            //根据id初始化信息
            //信息文本初始化
            nameText.text = data.name;
            typeText.text = $"类型:{data.type}";
            levelText.text = $"稀有度:{data.level}";
            valueText.text = $"售价￥{data.salevalue}";
            baseValueText.text = $"基础数值{data.baseValue}";
            infoText.text = $"描述:{data.description}" ;
            this.GetComponent<Image>().sprite = data.sprite;
            idInDiceText.text = data.idInDice.ToString();
        }

        /// <summary>
        /// 使用时销毁函数
        /// </summary>
        public void OnUseDestroy()
        {
            moveOffset = CharacterUIManager.Instance.player.position +
                         new Vector3(0, RollingResultUIManager.Instance.moveOffset, 0);
            transform.DOMove(moveOffset, useDuration / 2).OnComplete(() =>
            {
                idInDiceText.DOFade(0, useDuration / 2);
                image.DOFade(0, useDuration / 2);
                transform.DOScale(scale, useDuration / 2).OnComplete(() =>
                {
                    Destroy(gameObject);
                });
            });
        }
        
        /// <summary>
        /// 销毁动画
        /// </summary>
        /// <param name="animTime"></param>
        public void OnReRollDestroy()
        {
            Disable();
            idInDiceText.DOFade(0, useDuration / 2);
            this.transform.DOScale(new Vector3(0, 0, 0), useDuration / 2);
            image.DOColor(new Color(255, 0, 0, 0), useDuration / 2);
            StartCoroutine(DestroyGameObject(useDuration / 2));
        }
        IEnumerator DestroyGameObject(float animTime)
        {
            yield return new WaitForSeconds(animTime);
            Destroy(gameObject);
        }
        
        /// <summary>
        /// 点击无效化函数
        /// </summary>
        public void Disable()
        {
            button.interactable = false;
        }
        
        /// <summary>
        /// 出现动画
        /// </summary>
        public void DoAppearAnim(float animTime)
        {
            image.DOFade(1, animTime);
            idInDiceText.DOFade(1, useDuration / 2);
        }
        
        
        
        private void JumpToPosition(Vector2Int position)
        {
            if (FightDicePageManager.Instance == null) { return;}
            FightDicePageManager.Instance.SwitchToPosition(position);
        }

        private void RevertMark()
        {
            if (FightDicePageManager.Instance == null) { return;}
            FightDicePageManager.Instance.RevertMarkColumn();
        }

        /// <summary>
        /// 骰面预览
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject, SacredObjectUIManager.Instance.previewSizeS,
                SacredObjectUIManager.Instance.previewSizeS);
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

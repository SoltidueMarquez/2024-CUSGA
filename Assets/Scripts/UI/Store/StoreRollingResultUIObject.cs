using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Store
{
    [RequireComponent(typeof(Image))]
    public class StoreRollingResultUIObject : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [Tooltip("所在战斗骰面位置")]private Vector2Int _pageAndIndex;
        [SerializeField, Tooltip("说明UI")] private GameObject infoCanvas;
        [SerializeField, Tooltip("说明Text")] private Text infoText;
        [SerializeField, Tooltip("名称Text")]protected Text nameText;
        [SerializeField, Tooltip("类型Text")]protected Text typeText;
        [SerializeField, Tooltip("稀有度Text")] protected Text levelText;
        [SerializeField, Tooltip("售价Text")]protected Text valueText;
        [SerializeField, Tooltip("基础数值Text")]protected Text baseValueText;
        [SerializeField, Tooltip("点数Text")]protected Text idInDiceText;
        
        private Image _image;
        private bool _interactable;

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="tmpIndex">结果序列号</param>
        /// <param name="position">坐标位置，先页面位置再页内位置</param>
        /// <param name="data">骰面对应data</param>
        public void Init(Vector2Int position, SingleDiceUIData data)
        {
            _interactable = true;
            _image = this.GetComponent<Image>();
            _pageAndIndex = position;
            this.transform.localScale = new Vector3(1, 1, 1);
            //根据id初始化信息
            //信息文本初始化
            nameText.text = data.name;
            typeText.text = $"类型:{data.type}";
            levelText.text = $"稀有度:{data.level}";
            valueText.text = $"售价￥{data.value}";
            baseValueText.text = $"基础数值{data.baseValue}";
            infoText.text = $"描述:{data.description}" ;
            this.GetComponent<Image>().sprite = data.sprite;
            idInDiceText.text = data.idInDice.ToString();
        }
        
        /// <summary>
        /// 销毁动画
        /// </summary>
        /// <param name="animTime"></param>
        public void OnReRollDestroy(float animTime)
        {
            _interactable = false;//设置不可交互
            infoCanvas.SetActive(false);//禁止预览页面
            idInDiceText.DOFade(0, animTime);
            this.transform.DOScale(new Vector3(0, 0, 0), animTime);
            _image.DOColor(new Color(255, 0, 0, 0), animTime);
            StartCoroutine(LateDestroy(animTime));
        }
        private IEnumerator LateDestroy(float animTime)
        {
            yield return new WaitForSeconds(animTime);
            Destroy(gameObject);
        }

        /// <summary>
        /// 出现动画
        /// </summary>
        public void DoAppearAnim(float animTime)
        {
            _image.DOFade(1, animTime);
            idInDiceText.DOFade(1, animTime);
        }

        private void JumpToPosition(Vector2Int position)
        {
            if (!_interactable) { return; }
            EditableDiceUIManager.Instance.SwitchToPosition(position);
        }
        private void RevertMark()
        {
            if (!_interactable) { return; }
            EditableDiceUIManager.Instance.RevertMarkColumn();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_interactable) { return; }
            UIManager.Instance.EnterPreview(this.gameObject, 1.2f);
            UIManager.Instance.DoShake(this.GetComponent<Image>(), 5);
            JumpToPosition(_pageAndIndex);//显示是来自于哪个骰子的那个面
            infoCanvas.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_interactable) { return; }
            UIManager.Instance.ExitPreview(gameObject);
            RevertMark();
            infoCanvas.SetActive(false);
        }
    }
}

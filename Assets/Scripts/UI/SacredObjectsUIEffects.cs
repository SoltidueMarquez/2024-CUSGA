using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

enum  State
{
    PointerChosen,
    None
}

namespace UI
{
    public class SacredObjectsUIEffects : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {
        [Header("圣物信息")]
        [SerializeField,Tooltip("出售价格")]private float salePrice;//TODO:之后要和逻辑上的价格合并
        [Multiline(4)]
        [SerializeField, Tooltip("描述")] private string desc;
        
        [Header("UI组件")]
        [SerializeField,Tooltip("说明UI")]private GameObject descriptionCanvas;
        [SerializeField,Tooltip("说明Text")]private Text descriptionText;
        [SerializeField, Tooltip("出售按钮")] private GameObject saleButton;
        [SerializeField,Tooltip("出售按钮Text")]private Text saleButtonText;
        
        private State _state;
        private Vector3 _oldPosition;

        private void Start()
        {
            //初始化信息
            Init();
        }

        private void Init()
        {
            descriptionText.text = desc;
            saleButtonText.text = $"出售\n￥{salePrice}";
            _state = State.None;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject);
            UIManager.Instance.DoShake(this.GetComponent<Image>());
            descriptionCanvas.SetActive(true);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_state != State.PointerChosen)//鼠标没有点击就退出预览
            {
                UIManager.Instance.ExitPreview(this.gameObject);
            }
            descriptionCanvas.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_state != State.PointerChosen)
            {
                _oldPosition = this.gameObject.transform.position;//记录原来的位置
                UIManager.Instance.ClickFlow(this.gameObject);
                _state = State.PointerChosen;//设置选中状态
                saleButton.SetActive(true);//显示销售按钮
            }
            else
            {
                UIManager.Instance.CancelClick(this.gameObject);
                this.transform.position = _oldPosition;//回复位置
                _state = State.None;//重置状态
                saleButton.SetActive(false);//隐藏销售按钮
            }
        }
    }
}

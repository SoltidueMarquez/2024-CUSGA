using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public enum  State
{
    PointerChosen,
    None
}

namespace UI
{
    /// <summary>
    /// 所有UI物品的基类
    /// </summary>
    public class UIObjectEffects : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {
        //TODO:之后要和逻辑上的价格合并
        [Header("物品信息")]
        [SerializeField, Tooltip("id")] protected string id;
        [SerializeField,Tooltip("出售价格")]protected float salePrice;
        [Multiline(4)]
        [SerializeField, Tooltip("描述")] protected string desc;

        [Header("UI组件")]
        [SerializeField,Tooltip("说明UI")]protected GameObject descriptionCanvas;
        [SerializeField,Tooltip("说明Text")]protected Text descriptionText;
        [SerializeField,Tooltip("出售按钮")] protected GameObject saleButton;
        [SerializeField,Tooltip("出售按钮Text")]protected Text saleButtonText;

        protected State _state;
        protected Transform oldParent; //原本的父物体
        public Column _currentColumn;   //当前所在栏位
        
        /// <summary>
        /// 初始化函数
        /// </summary>
        public void Init()
        {
            descriptionText.text = desc;
            saleButtonText.text = $"出售\n￥{salePrice}";
            _state = State.None;
            _currentColumn = UIManager.Instance.DetectColumn(gameObject, UIManager.Instance.sacredObjectColumns); //检测当前所在的物品栏
            _currentColumn.bagObject = gameObject;//初始化当前所在的物品栏
        }
        
        /// <summary>
        /// 鼠标移动函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject);
            UIManager.Instance.DoShake(this.GetComponent<Image>());
            descriptionCanvas.SetActive(true);
        }
        
        /// <summary>
        /// 鼠标移开函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            if (_state != State.PointerChosen)//鼠标没有点击就退出预览
            {
                UIManager.Instance.ExitPreview(this.gameObject);
            }
            descriptionCanvas.SetActive(false);
        }
        
        /// <summary>
        /// 鼠标点击函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_state != State.PointerChosen)
            {
                UIManager.Instance.ClickFlow(this.gameObject);
                _state = State.PointerChosen;//设置选中状态
                saleButton.SetActive(true);//显示销售按钮
            }
            else
            {
                EndClick();
                this.transform.position = _currentColumn.transform.position;//回复位置
            }
        }
        
        protected void EndClick()
        {
            //结束选中状态
            UIManager.Instance.CancelClick(this.gameObject);
            _state = State.None;//重置状态
            saleButton.SetActive(false);//隐藏销售按钮
        }
    }
}

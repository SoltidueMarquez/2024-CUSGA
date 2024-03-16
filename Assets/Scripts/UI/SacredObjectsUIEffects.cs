using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SacredObjectsUIEffects : UIObjectEffects, IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {
        private void Start()
        {
            //TODO:初始化信息，之后要删掉
            Init();
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
                UIManager.Instance.ExitPreview(gameObject);
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
                UIManager.Instance.ClickFlow(gameObject);
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
            UIManager.Instance.CancelClick(gameObject);
            _state = State.None;//重置状态
            saleButton.SetActive(false);//隐藏销售按钮
        }
        
        /// <summary>
        /// 开始拖动函数
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnBeginDrag(PointerEventData eventData)
        {
            EndClick();
            _currentColumn =
                UIManager.Instance.DetectColumn(gameObject, UIManager.Instance.sacredObjectColumns); //记录原来的物品栏位置
            oldParent = gameObject.transform.parent;//记录原来的父物体
        }

        /// <summary>
        /// 结束拖动函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            EndClick();
            //更改位置
            UIManager.Instance.DetectPosition(gameObject, UIManager.Instance.sacredObjectColumns, _currentColumn);
            _currentColumn =
                UIManager.Instance.DetectColumn(gameObject, UIManager.Instance.sacredObjectColumns); //记录原来的物品栏位置
            gameObject.transform.SetParent(oldParent);//设置为原来的图层
        }

        /// <summary>
        /// 拖拽中函数
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnDrag(PointerEventData eventData)
        {
            gameObject.transform.SetParent(UIManager.Instance.dragCanvas);//设置为拖拽图层
            UIManager.Instance.OnDrag(gameObject);
        }

        
        
    }
}

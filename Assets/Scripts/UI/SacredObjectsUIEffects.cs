using System;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SacredObjectsUIEffects : UIObjectEffects, IBeginDragHandler,IEndDragHandler,IDragHandler
    {
        private void Start()
        {
            //TODO:初始化信息，之后要删掉
            Init();
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

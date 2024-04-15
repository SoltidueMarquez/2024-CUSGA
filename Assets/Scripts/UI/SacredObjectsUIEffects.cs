using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 圣物UI动效实现类
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class SacredObjectsUIEffects : UIObjectEffects, IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {
        public string id;
        /// <summary>
        /// 初始化函数
        /// </summary>
        public void Init(List<Column> columns, float offset, string id, Action<HalidomObject> remove, HalidomObject halidomObject)
        {
            this.id = id;
            var tmpData = ResourcesManager.GetHalidomUIData(id);
            nameText.text = tmpData.name;
            valueText.text = $"售价￥{tmpData.value}";
            descriptionText.text = tmpData.description;
            this.GetComponent<Image>().sprite = tmpData.sprite;
            saleButtonText.text = $"出售\n￥{tmpData.value}";
            
            //saleButton绑定移除圣物效果函数：增加一个委托类型的参数(就是对应的移除函数)
            saleButton.onClick.AddListener(() =>
            {
                DestroyUI();
                remove?.Invoke(halidomObject);
            });
            _state = State.None;
            _currentColumn = UIManager.Instance.DetectColumn(gameObject, columns, offset); //检测当前所在的物品栏
            if (_currentColumn != null) //初始化当前所在的物品栏
            {
                _currentColumn.bagObject = gameObject;
            }
        }

        /// <summary>
        /// 闪烁函数
        /// </summary>
        public void DoFlick()
        {
            var tmp = Instantiate(SacredObjectUIManager.Instance.flickEffect, transform, true);
            tmp.transform.position = transform.position;//更改位置
            tmp.SetActive(true);
            StartCoroutine(DestroyGameObject(tmp));
        }
        IEnumerator DestroyGameObject(GameObject objectToBeDes)
        {
            yield return new WaitForSeconds(0.75f);
            Destroy(objectToBeDes);
        }
        
        /// <summary>
        /// 鼠标移动函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject, SacredObjectUIManager.Instance.previewSizeS,
                SacredObjectUIManager.Instance.previewSizeS);
            UIManager.Instance.DoShake(this.GetComponent<Image>(),SacredObjectUIManager.Instance.shakeAngleS);
            descriptionCanvas.SetActive(true);
        }
        
        /// <summary>
        /// 鼠标移开函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.ExitPreview(gameObject);
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
                UIManager.Instance.ClickFlow(gameObject, 5f);
                _state = State.PointerChosen;//设置选中状态
                saleUI.SetActive(true);//显示销售按钮
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
            saleUI.SetActive(false);//隐藏销售按钮
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
                UIManager.Instance.DetectColumn(gameObject, SacredObjectUIManager.Instance.sacredObjectColumns, SacredObjectUIManager.Instance.offsetS); //记录原来的物品栏位置
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
            UIManager.Instance.DetectPosition(gameObject, SacredObjectUIManager.Instance.sacredObjectColumns, _currentColumn, SacredObjectUIManager.Instance.offsetS);
            _currentColumn = UIManager.Instance.DetectColumn(gameObject, SacredObjectUIManager.Instance.sacredObjectColumns, SacredObjectUIManager.Instance.offsetS); //记录原来的物品栏位置
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

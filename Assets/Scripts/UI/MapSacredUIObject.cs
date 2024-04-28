using System;
using System.Collections;
using System.Collections.Generic;
using Audio_Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class MapSacredUIObject : UIObjectEffects, IBeginDragHandler,IEndDragHandler,IDragHandler,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
    {
        public string id;

        #region 初始化
        /// <summary>
        /// 初始化函数
        /// </summary>
        public void Init(List<Column> columns, float offset, string idX, Action<HalidomObject> remove, HalidomObject halidomObject)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.id = idX;
            var tmpData = ResourcesManager.GetHalidomUIData(id);
            nameText.text = tmpData.name;
            descriptionText.text = tmpData.description;
            this.GetComponent<Image>().sprite = tmpData.sprite;
            saleButtonText.text = $"出售\n￥{tmpData.salevalue}";
            
            //saleButton绑定移除圣物效果函数：增加一个委托类型的参数(就是对应的移除函数)
            saleButton.onClick.AddListener(() =>
            {
                DestroyUI(0);
                remove?.Invoke(halidomObject);
            });
            _state = State.None;
            _currentColumn = UIManager.Instance.DetectColumn(gameObject, columns, offset); //检测当前所在的物品栏
            if (_currentColumn != null) //初始化当前所在的物品栏
            {
                _currentColumn.bagObject = gameObject;
            }
        }
        public void Init(List<Column> columns, float offset, string idX)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            this.id = idX;
            var tmpData = ResourcesManager.GetHalidomUIData(id);
            nameText.text = tmpData.name;
            descriptionText.text = tmpData.description;
            this.GetComponent<Image>().sprite = tmpData.sprite;
            saleButtonText.text = $"出售\n￥{tmpData.value}";
            
            //saleButton绑定移除圣物效果函数：增加一个委托类型的参数(就是对应的移除函数)
            saleButton.onClick.AddListener(()=>
            {
                DestroyUI(0);
            });
            _state = State.None;
            _currentColumn = UIManager.Instance.DetectColumn(gameObject, columns, offset); //检测当前所在的物品栏
            if (_currentColumn != null) //初始化当前所在的物品栏
            {
                _currentColumn.bagObject = gameObject;
            }
        }
        #endregion

        public void UpdateDesc(string desc)
        {
            descriptionText.text = desc;
        }
        
        /// <summary>
        /// 闪烁函数
        /// </summary>
        public void DoFlick()
        {
            var tmp = Instantiate(MapSacredUIManager.Instance.flickEffect, transform, true);
            tmp.transform.position = transform.position;//更改位置
            tmp.SetActive(true);
            StartCoroutine(DestroyGameObject(tmp));
        }
        IEnumerator DestroyGameObject(GameObject objectToBeDes)
        {
            yield return new WaitForSeconds(0.75f);
            Destroy(objectToBeDes);
        }

        #region 鼠标预览
        /// <summary>
        /// 鼠标移动函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject, MapSacredUIManager.Instance.previewSize,
                MapSacredUIManager.Instance.previewSize);
            UIManager.Instance.DoShake(this.GetComponent<Image>(),MapSacredUIManager.Instance.shakeAngle);
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
        #endregion

        #region 鼠标点击
        /// <summary>
        /// 鼠标点击函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (_state != State.PointerChosen)
            {
                UIManager.Instance.ClickFlow(gameObject, MapSacredUIManager.Instance.flowDis);
                _state = State.PointerChosen;//设置选中状态
                saleUI.SetActive(true);//显示销售按钮
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayRandomSound("clickDown");
                }
            }
            else
            {
                EndClick();
                this.transform.position = _currentColumn.transform.position;//回复位置
            }
        }
        private void EndClick()
        {
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayRandomSound("clickUp");
            }
            UIManager.Instance.CancelClick(gameObject);
            _state = State.None;//重置状态
            saleUI.SetActive(false);//隐藏销售按钮
        }
        #endregion

        #region 鼠标拖动
        /// <summary>
        /// 开始拖动函数
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnBeginDrag(PointerEventData eventData)
        {
            EndClick();
            _currentColumn =
                UIManager.Instance.DetectColumn(gameObject, MapSacredUIManager.Instance.sacredObjectColumns, MapSacredUIManager.Instance.offsetS); //记录原来的物品栏位置
        }

        /// <summary>
        /// 结束拖动函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            EndClick();
            //更改位置
            UIManager.Instance.DetectPosition(gameObject, MapSacredUIManager.Instance.sacredObjectColumns, _currentColumn, MapSacredUIManager.Instance.offsetS);
            _currentColumn = UIManager.Instance.DetectColumn(gameObject, MapSacredUIManager.Instance.sacredObjectColumns, MapSacredUIManager.Instance.offsetS); //记录原来的物品栏位置
            gameObject.transform.SetParent(MapSacredUIManager.Instance.parent);//设置为原来的图层
        }

        /// <summary>
        /// 拖拽中函数
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnDrag(PointerEventData eventData)
        {
            gameObject.transform.SetParent(UIManager.Instance.dragCanvas);//设置为拖拽图层
            var tmpPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(tmpPos.x, tmpPos.y, 0);
        }
        #endregion
        
        /// <summary>
        /// 摧毁UI函数
        /// </summary>
        public void DestroyUI(int nothing)
        {
            _currentColumn.bagObject = null; //所在的物品栏置空
            Destroy(gameObject);
        }
    }
}

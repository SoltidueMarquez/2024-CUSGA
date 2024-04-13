using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public enum EditState
    {
        BagDice,
        FightDice
    }

    [RequireComponent(typeof(Image))]
    public class EditableDiceUIObject : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler 
    {
        [Header("说明UI")] [SerializeField, Tooltip("说明UI")]
        protected GameObject descriptionCanvas;

        [SerializeField, Tooltip("名称Text")] private Text nameText;
        [SerializeField, Tooltip("类型Text")] private Text typeText;
        [SerializeField, Tooltip("稀有度Text")] private Text levelText;
        [SerializeField, Tooltip("售价Text")] private Text valueText;
        [SerializeField, Tooltip("基础数值Text")] private Text baseValueText;
        [SerializeField, Tooltip("说明Text")] private Text descriptionText;
        [SerializeField, Tooltip("点数Text")] private Text idInDiceText;

        [Header("出售UI")] 
        [SerializeField, Tooltip("出售UI")] private GameObject saleUI;

        [SerializeField, Tooltip("出售按钮")] private Button saleButton;
        [SerializeField, Tooltip("出售按钮Text")] private Text saleButtonText;

        private EditState _editState;
        private State _state;
        private Transform _oldParent; //原本的父物体
        public Column currentColumn; //当前所在栏位

        #region 初始化
        public void Init(List<Column> columns, float offset, SingleDiceObj singleDice, Action<SingleDiceObj> remove)
        {
            var data = ResourcesManager.GetSingleDiceUIData(singleDice);
            //信息文本初始化
            nameText.text = data.name;
            typeText.text = $"类型:{data.type}";
            levelText.text = $"稀有度:{data.level}";
            valueText.text = $"售价￥{data.value}";
            baseValueText.text = $"基础数值{data.baseValue}";
            descriptionText.text = $"描述:{data.description}";
            this.GetComponent<Image>().sprite = data.sprite;
            saleButtonText.text = $"出售\n￥{data.value}";
            idInDiceText.text = data.idInDice.ToString();
            //saleButton绑定移除骰面效果函数
            saleButton.onClick.AddListener(() =>
            {
                DestroyUI();
                remove?.Invoke(singleDice);
            });

            _editState = EditState.FightDice;
            _state = State.None;
            
            currentColumn = UIManager.Instance.DetectColumn(gameObject, columns, offset); //检测当前所在的物品栏
            if (currentColumn != null) //初始化当前所在的物品栏
            {
                currentColumn.bagObject = gameObject;
            }
        }
        #endregion

        #region 鼠标预览
        /// <summary>
        /// 鼠标移动函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject, EditableDiceUIManager.Instance.previewSize);
            UIManager.Instance.DoShake(this.GetComponent<Image>(), EditableDiceUIManager.Instance.shakeAngle);
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
                UIManager.Instance.ClickFlow(gameObject);//设置浮动动画
                _state = State.PointerChosen;//设置为选中状态
                saleUI.SetActive(true);//显示销售按钮
            }
            else
            {
                EndClick();//结束选中状态
                transform.position = currentColumn.transform.position;//回复位置
            }
        }
        
        private void EndClick()
        {
            UIManager.Instance.CancelClick(gameObject);
            _state = State.None;//重置状态
            saleUI.SetActive(false);//隐藏销售按钮
        }
        #endregion

        #region 拖动
        /// <summary>
        /// 开始拖动函数
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnBeginDrag(PointerEventData eventData)
        {
            EndClick();
            currentColumn = UIManager.Instance.DetectColumn(gameObject, BagDiceUIManager.Instance.bagColumns, BagDiceUIManager.Instance.offsetB); //记录原来的物品栏位置
            _oldParent = gameObject.transform.parent;//记录原来的父物体
        }

        /// <summary>
        /// 结束拖动函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            EndClick();
            //更改位置
            UIManager.Instance.DetectPosition(gameObject, BagDiceUIManager.Instance.bagColumns, currentColumn, BagDiceUIManager.Instance.offsetB);
            currentColumn = UIManager.Instance.DetectColumn(gameObject, BagDiceUIManager.Instance.bagColumns, BagDiceUIManager.Instance.offsetB); //记录原来的物品栏位置
            gameObject.transform.SetParent(_oldParent);//设置为原来的图层
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
        #endregion

        /// <summary>
        /// 摧毁UI函数
        /// </summary>
        public void DestroyUI()
        {
            currentColumn.bagObject = null; //所在的物品栏置空
            Destroy(gameObject);
        }
    }
}

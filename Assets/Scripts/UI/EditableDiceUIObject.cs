using System;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public enum EditState
    {
        BagDice,
        FightDice
    }

    [RequireComponent(typeof(Image))]
    public class EditableDiceUIObject : UIObjectEffects, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler 
    {
        
        public SingleDiceObj diceObj;

        #region 初始化
        public void Init(List<Column> columns, float offset, SingleDiceObj singleDice, Action<SingleDiceObj> remove)
        {
            diceObj = singleDice;
            this.transform.localScale = new Vector3(1, 1, 1);
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
                DestroyUI(0);
                remove?.Invoke(singleDice);
            });
            _state = State.None;
            
            _currentColumn = UIManager.Instance.DetectColumn(gameObject, columns, offset); //检测当前所在的物品栏
            if (_currentColumn != null) //初始化当前所在的物品栏
            {
                _currentColumn.bagObject = gameObject;
                editState = _currentColumn.state;
            }
        }

        /*public void Init(List<Column> columns, float offset)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
            nameText.text = nameof(gameObject);
            saleButtonText.text = $"出售\n￥0";
            idInDiceText.text = Random.Range(0,100).ToString();
            saleButton.onClick.AddListener(DestroyUI);
            _state = State.None;
            _currentColumn = UIManager.Instance.DetectColumn(gameObject, columns, offset); //检测当前所在的物品栏
            if (_currentColumn != null) //初始化当前所在的物品栏
            {
                _currentColumn.bagObject = gameObject;
                editState = _currentColumn.state;
            }
        }*/
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
            if (editState != EditState.BagDice) { return;}
            if (_state != State.PointerChosen)
            {
                UIManager.Instance.ClickFlow(gameObject, EditableDiceUIManager.Instance.flowDis);//设置浮动动画
                _state = State.PointerChosen;//设置为选中状态
                saleUI.SetActive(true);//显示销售按钮
            }
            else
            {
                EndClick();//结束选中状态
                transform.position = _currentColumn.transform.position;//回复位置
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
            if (editState != EditState.BagDice) { return;}
            EndClick();
            _currentColumn = UIManager.Instance.DetectColumn(gameObject, EditableDiceUIManager.Instance.allColumns, EditableDiceUIManager.Instance.offset); //记录原来的物品栏位置
        }

        /// <summary>
        /// 结束拖动函数
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            if (editState != EditState.BagDice) { return;}
            EndClick();
            //更改位置
            var switchObj = UIManager.Instance.DetectPosition(gameObject, EditableDiceUIManager.Instance.allColumns, _currentColumn, EditableDiceUIManager.Instance.offset);
            _currentColumn = UIManager.Instance.DetectColumn(gameObject, EditableDiceUIManager.Instance.allColumns, EditableDiceUIManager.Instance.offset); //更新物品栏位置
            gameObject.transform.SetParent(EditableDiceUIManager.Instance.parent);//设置为原来的图层

            //逻辑交换
            if (editState == EditState.FightDice)
            {
                var bagDice = gameObject.GetComponent<EditableDiceUIObject>().diceObj;
                var fightDice = switchObj.GetComponent<EditableDiceUIObject>().diceObj;
                MapManager.Instance.playerChaState.GetBattleDiceHandler().SwapDiceInBagAndBattle(bagDice, fightDice,EditableDiceUIManager.Instance.GetCurrentPage());
                Debug.Log(
                    $"<color=green>{gameObject.GetComponent<EditableDiceUIObject>().diceObj}交换了{EditableDiceUIManager.Instance.GetCurrentPage()}号骰子的{switchObj.GetComponent<EditableDiceUIObject>().diceObj}</color>");
            }
        }

        /// <summary>
        /// 拖拽中函数
        /// </summary>
        /// <param name="eventData"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnDrag(PointerEventData eventData)
        {
            if (editState != EditState.BagDice) { return;}
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

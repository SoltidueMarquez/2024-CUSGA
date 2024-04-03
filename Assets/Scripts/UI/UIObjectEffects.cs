using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    public class UIObjectEffects : MonoBehaviour
    {
        //TODO:之后要和逻辑上的价格合并
        [Header("物品信息")]
        [SerializeField,Tooltip("出售价格")]protected float salePrice;

        [Header("UI组件")]
        [SerializeField,Tooltip("说明UI")]protected GameObject descriptionCanvas;
        [SerializeField,Tooltip("说明Text")]protected Text descriptionText;
        [SerializeField,Tooltip("出售UI")] protected GameObject saleUI;
        [SerializeField, Tooltip("出售按钮")] protected Button saleButton;
        [SerializeField,Tooltip("出售按钮Text")]protected Text saleButtonText;

        protected State _state;
        protected Transform oldParent;  //原本的父物体
        public Column _currentColumn;   //当前所在栏位

        /// <summary>
        /// 初始化函数
        /// </summary>
        public void Init(List<Column> columns, float offset, string id, Action<int> remove, int index)
        {
            descriptionText.text = id;
            saleButtonText.text = $"出售\n￥{salePrice}";
            //saleButton绑定移除圣物/背包骰面效果函数：增加一个委托类型的参数(就是对应的移除函数)
            saleButton.onClick.AddListener(() =>
            {
                DestroyUI();
                remove?.Invoke(index);
            });
            _state = State.None;
            _currentColumn = UIManager.Instance.DetectColumn(gameObject, columns, offset); //检测当前所在的物品栏
            if (_currentColumn != null) //初始化当前所在的物品栏
            {
                _currentColumn.bagObject = gameObject;
            }
        }

        /// <summary>
        /// 摧毁UI函数
        /// </summary>
        private void DestroyUI()
        {
            _currentColumn.bagObject = null; //所在的物品栏置空
            Destroy(gameObject);
        }
    }
}

using System;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Store
{
    public class StoreAreaUIManager : MonoSingleton<StoreAreaUIManager>
    {
        [Header("通用")]
        [SerializeField, Tooltip("动画时长")] public float animTime;
        [SerializeField, Tooltip("离开按钮")] private Button exitButton;
        [SerializeField, Tooltip("升级按钮")] private Button upgradeButton;
        [Tooltip("预览大小")] public float previewSize;
        [Tooltip("晃动角度")] public float shakeAngle;

        [Header("出售骰面")]
        [SerializeField, Tooltip("骰子栏位列表")] public List<Column> diceColumns;
        [SerializeField, Tooltip("骰子模板")] private GameObject diceTemplate;
        [SerializeField, Tooltip("售价文本列表")] private List<Text> dicePriceTextList;

        [Header("出售圣物")]
        [SerializeField, Tooltip("圣物栏位列表")] public List<Column> sacredObjectColumns;
        [SerializeField, Tooltip("圣物模板")] private GameObject sacredObjectTemplate;
        [SerializeField, Tooltip("售价文本列表")] private List<Text> sacredPriceTextList;

        private void Start()
        {
            exitButton.onClick.AddListener(() =>
            {
                StoreManager.Instance.OnExitStore?.Invoke();
            });
            upgradeButton.onClick.AddListener(() =>
            {
                StoreManager.Instance.OnClickUpgrade?.Invoke();
            });

            StoreManager.Instance.OnRefreshStore.AddListener(RefreshHalidomUI);
            StoreManager.Instance.OnRefreshStore.AddListener(RefreshDiceUI);
        }

        public void JudgeValue()
        {
            ChaState player = MapManager.Instance.playerChaState;
            for (int i = 0; i < diceColumns.Count; i++)
            {
                var color = new Color();
                var tmpDice = diceColumns[i].transform.GetComponent<ProductDice>();
                if (tmpDice == null) { return;}
                if (tmpDice.product?.value > player.resource.currentMoney) 
                {
                    color = Color.red;
                }
                else
                {
                    color = Color.black;
                }
                dicePriceTextList[i].GetComponent<Text>().color = color;
            }
            for (int i = 0; i < sacredObjectColumns.Count; i++)
            {
                var color = new Color();
                var tmpSacred = sacredObjectColumns[i].transform.GetComponent<ProductHalidom>();
                if (tmpSacred == null) { return;}
                if (tmpSacred.product?.value > player.resource.currentMoney)
                {
                    color = Color.red;
                }
                else
                {
                    color = Color.black;
                }
                sacredPriceTextList[i].GetComponent<Text>().color = color;
            }
        }
        
        #region 出售骰面相关
        public void RefreshDiceUI()
        {
            for (int i = 0; i < diceColumns.Count; i++)
            {
                RemoveDiceUI(i);
            }

            for (int i = 0; i < diceColumns.Count; i++)
            {
                ProductDice productDice = diceColumns[i].transform.GetComponent<ProductDice>();

                CreateDiceUI(i, productDice.TryBuy, productDice.product);
            }
        }

        /// <summary>
        /// 创建骰面函数,其本质仍然是一个战斗骰面页的骰子(子类)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="index">栏位索引</param>
        /// <param name="onChoose">选择骰面后触发的逻辑函数</param>
        /// <param name="singleDiceObj">骰面物体</param>
        public void CreateDiceUI(int index, Action<SingleDiceObj> onChoose, SingleDiceObj singleDiceObj)
        {            

            if (index > diceColumns.Count)
            {
                Debug.LogWarning("超出生成的栏位");
                return;
            }
            if (diceColumns[index].bagObject != null)
            {
                Debug.LogWarning("栏位已经有骰面");
                return;
            }
            var parent = diceColumns[index].transform;
            var tmp = Instantiate(diceTemplate, parent, true);
            diceColumns[index].bagObject = tmp;
            tmp.transform.position = parent.position;//更改位置
            var tmpDice = tmp.GetComponent<StoreDiceUIObject>();
            var data = ResourcesManager.GetSingleDiceUIData(singleDiceObj);
            tmpDice.Init(data, animTime, 2, onChoose, singleDiceObj);//初始化
            dicePriceTextList[index].text = data.value.ToString(); 
            tmp.SetActive(true);
            tmpDice.DoAppearAnim(animTime); //出现动画
        }

        /// <summary>
        /// 移除骰面函数
        /// </summary>
        /// <param name="index">栏位索引</param>
        public void RemoveDiceUI(int index)
        {
            if (index > diceColumns.Count)
            {
                Debug.LogWarning("超出生成的栏位");
                return;
            }
            if (diceColumns[index].bagObject == null)
            {
                Debug.LogWarning("骰面不存在");
                return;
            }
            var item = diceColumns[index].bagObject.GetComponent<StoreDiceUIObject>();
            diceColumns[index].bagObject = null;
            if (item != null)
            {
                item.DoDestroyAnim(animTime);
            }
        }

        /// <summary>
        /// 禁用全部骰面函数
        /// </summary>
        public void DisableAllDices()
        {
            foreach (var col in diceColumns)
            {
                if (col.bagObject != null)
                {
                    var item = col.bagObject.GetComponent<StoreDiceUIObject>();
                    item.Disable();
                }
            }
        }

        /// <summary>
        /// 启用全部骰面函数
        /// </summary>
        public void EnableAllDices()
        {
            foreach (var col in diceColumns)
            {
                if (col.bagObject != null)
                {
                    var item = col.bagObject.GetComponent<StoreDiceUIObject>();
                    item.Enable();
                }
            }
        }
        #endregion


        #region 出售圣物相关

        public void RefreshHalidomUI()
        {

            for (int i = 0; i < sacredObjectColumns.Count; i++)
            {
                RemoveSacredObject(i);
            }

            for (int i = 0; i < sacredObjectColumns.Count; i++)
            {
                ProductHalidom productHalidom = sacredObjectColumns[i].transform.GetComponent<ProductHalidom>();
                CreateSacredObject(i, productHalidom.TryBuy, productHalidom.product);
            }

        }

        /// <summary>
        /// 创建圣物函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="index"></param>
        /// <param name="onChoose"></param>
        /// <param name="halidomObject"></param>
        public void CreateSacredObject(int index, Action onChoose, HalidomObject halidomObject)
        {
            if (index > sacredObjectColumns.Count)
            {
                Debug.LogWarning("超出生成的栏位");
                return;
            }
            if (sacredObjectColumns[index].bagObject != null)
            {
                Debug.LogWarning("栏位已经有圣物");
                return;
            }
            var parent = sacredObjectColumns[index].transform;
            var tmp = Instantiate(sacredObjectTemplate, parent, true);
            sacredObjectColumns[index].bagObject = tmp;
            tmp.transform.position = parent.position;//更改位置
            var tmpSacredObject = tmp.GetComponent<StoreSacredUIObject>();
            tmpSacredObject.Init(halidomObject.id, animTime, 2, onChoose, halidomObject);//初始化
            var data = ResourcesManager.GetHalidomUIData(halidomObject.id);
            sacredPriceTextList[index].text = data.value.ToString();
            tmp.SetActive(true);
            tmpSacredObject.DoAppearAnim(animTime); //出现动画
        }

        /// <summary>
        /// 移除圣物函数
        /// </summary>
        /// <param name="index">栏位索引</param>
        public void RemoveSacredObject(int index)
        {
            if (index > sacredObjectColumns.Count)
            {
                Debug.LogWarning("超出生成的栏位");
                return;
            }
            if (sacredObjectColumns[index].bagObject == null)
            {
                Debug.LogWarning("圣物不存在");
                return;
            }
            var item = sacredObjectColumns[index].bagObject.GetComponent<StoreSacredUIObject>();
            sacredObjectColumns[index].bagObject = null;
            if (item != null)
            {
                item.DoDestroyAnim(animTime);
            }
        }

        /// <summary>
        /// 禁用全部圣物函数
        /// </summary>
        public void DisableAllSacredObject()
        {
            foreach (var col in sacredObjectColumns)
            {
                if (col.bagObject != null)
                {
                    var item = col.bagObject.GetComponent<StoreSacredUIObject>();
                    item.Disable();
                }
            }
        }

        /// <summary>
        /// 启用全部圣物函数
        /// </summary>
        public void EnableAllSacredObject()
        {
            foreach (var col in sacredObjectColumns)
            {
                if (col.bagObject != null)
                {
                    var item = col.bagObject.GetComponent<StoreSacredUIObject>();
                    item.Enable();
                }
            }
        }
        #endregion

    }
}

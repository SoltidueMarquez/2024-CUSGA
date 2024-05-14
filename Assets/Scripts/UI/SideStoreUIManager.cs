using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SideStoreUIManager : MonoSingleton<SideStoreUIManager>
    {
        [SerializeField, Tooltip("骰子栏位列表")] public List<Column> productColumns;
        [SerializeField, Tooltip("货币文本")] public List<Text> costTextList;
        [SerializeField, Tooltip("骰子模板")] private GameObject productTemplate;
        [SerializeField, Tooltip("更新进度条")] private Slider updateSlider;
        [SerializeField, Tooltip("动画时长")] private float animTime;
        [SerializeField, Tooltip("预览大小")] private float previewSize;
        [Header("开启关闭相关")]
        [SerializeField, Tooltip("商店UI界面")] private GameObject sideStoreUI;
        [SerializeField, Tooltip("隐藏位置")] private Transform hidePosition;
        [SerializeField, Tooltip("出现位置")] private Transform showPosition;
        [SerializeField, Tooltip("遮罩")] private GameObject mask;
        [SerializeField, Tooltip("折叠按钮")] private Button slideButton;


        private void Start()
        {
            BattleStoreManager.Instance.OnEnterStore.AddListener(OpenStore);
            BattleStoreManager.Instance.OnExitStore.AddListener(CloseStore);
        }

        //创建商品
        private void CreateProductUI(int index, BaseBattleProduct product)
        {
            if (index > productColumns.Count)
            {
                Debug.LogWarning("超出生成的栏位");
                return;
            }
            if (productColumns[index].bagObject != null)
            {
                Debug.LogWarning("栏位已经有商品");
                return;
            }
            var parent = productColumns[index].transform;
            var tmp = Instantiate(productTemplate, parent, true);
            productColumns[index].bagObject = tmp;
            tmp.transform.position = parent.position;//更改位置

            var tmpProduct = tmp.GetComponent<SideStoreProductUIObject>();
            tmpProduct.Init(animTime, previewSize, product);//初始化

            //售价文本初始化
            costTextList[index].text = product.value.ToString();//售价文本初始化

            tmp.SetActive(true);
            tmpProduct.DoAppearAnim(animTime); //出现动画
        }

        //移除商品
        private void RemoveProductUI(int index)
        {
            if (index > productColumns.Count)
            {
                Debug.LogWarning("超出生成的栏位");
                return;
            }
            if (productColumns[index].bagObject == null)
            {
                return;
            }
            var item = productColumns[index].bagObject.GetComponent<SideStoreProductUIObject>();
            productColumns[index].bagObject = null;
            if (item != null)
            {
                item.DoDestroyAnim(animTime, 0);
            }
        }

        //刷新全部商品
        public void RefreshProductUI(List<BaseBattleProduct> productList)
        {
            for (int i = 0; i < productColumns.Count; i++)
            {
                RemoveProductUI(i);
            }
            for (int i = 0; i < productColumns.Count; i++)
            {
                if (productList[i] == null) { continue; }
                CreateProductUI(i, productList[i]);
            }


        }

        public void SlideStore()
        {
            if (mask.activeSelf)
            {
                OpenStore();
            }
            else
            {
                mask.SetActive(true);
                sideStoreUI.transform.DOMove(hidePosition.position, animTime);
            }
        }
        
        //打开商店
        public void OpenStore()
        {
            slideButton.interactable = true;
            sideStoreUI.transform.DOMove(showPosition.position, animTime).OnComplete(() =>
            {
                mask.SetActive(false);
            });
        }

        //关闭商店
        public void CloseStore()
        {
            mask.SetActive(true);
            slideButton.interactable = false;
            sideStoreUI.transform.DOMove(hidePosition.position, animTime);
        }

        //更新进度条
        public void UpdateSliderValue(float value, float maxValue)
        {
            updateSlider.value = value / maxValue;
        }

        /*private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RefreshProductUI();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                OpenStore();
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                CloseStore();
            }
        }*/
    }
}

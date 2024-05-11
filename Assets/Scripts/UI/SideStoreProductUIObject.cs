using System;
using System.Collections;
using Audio_Manager;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class SideStoreProductUIObject : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [Header("UI组件")] [SerializeField, Tooltip("说明UI")] private GameObject descriptionCanvas;
        [SerializeField, Tooltip("名称文本")] private Text nameText;
        [SerializeField, Tooltip("名称文本")] private Text descText;
        [SerializeField, Tooltip("图片")] private Image image;
        
        private float _animTime;
        private float _scale;
        private BaseBattleProduct _product;

        #region 初始化

        public void Init(float animTime, float scale, BaseBattleProduct product)
        {
            //大小初始化
            this.transform.localScale = new Vector3(1, 1, 1);
            _scale = scale;
            _animTime = animTime;
            _product = product;
            //信息文本初始化
            nameText.text = product.productName;
            descText.text = product.description;
            image.sprite = product.productSprite;
            
            //按钮事件绑定
            this.GetComponent<Button>().onClick.AddListener(()=>
            {
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayRandomSound("clickDown");
                }
                product.TryBuy();//调用购买函数
            });
            product.OnBuySuccess.AddListener(DoChosenAnim);
            //product.OnBuyFail.AddListener();
        }
        
        //删除监听事件
        private void RemoveListener()
        {
            _product.OnBuySuccess.AddListener(DoChosenAnim);
        }
        #endregion

        private void DoChosenAnim()
        {
            DoDestroyAnim(_animTime, _scale);
            RemoveListener();
        }

        #region 鼠标交互
        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject, _scale, _scale);
            UIManager.Instance.DoShake(this.GetComponent<Image>(), 5f);
            descriptionCanvas.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.ExitPreview(gameObject);
            descriptionCanvas.SetActive(false);
        }
        #endregion

        #region 动画函数

        /// <summary>
        /// 出现动画
        /// </summary>
        public void DoAppearAnim(float animTime)
        {
            this.GetComponent<Image>().DOFade(1, animTime);
        }

        /// <summary>
        /// 销毁动画
        /// </summary>
        /// <param name="animTime"></param>
        /// <param name="scale"></param>
        public void DoDestroyAnim(float animTime, float scale)
        {
            Disable();
            this.transform.DOScale(new Vector3(scale, scale, scale), animTime);
            this.GetComponent<Image>().DOFade(0, animTime);
            StartCoroutine(DestroyGameObject(animTime));
        }
        IEnumerator DestroyGameObject(float animTime)
        {
            yield return new WaitForSeconds(animTime);
            Destroy(gameObject);
        }
        // 点击无效化函数
        private void Disable()
        {
            this.GetComponent<Button>().interactable = false;
        }
        #endregion
    }
}

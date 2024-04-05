using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(Image))]
    public class RewardDiceUIObject : FightDiceUIEffect
    {
        /// <summary>
        /// 初始化函数
        /// </summary>
        public virtual void Init(string id, float animTime, float scale, Action<int> onChoose, int index)
        {
            descriptionText.text = id;
            
            //按钮事件绑定
            this.GetComponent<Button>().onClick.AddListener(()=>
            {
                Disable();
                DoChosenAnim(animTime, scale);//动画
                onChoose?.Invoke(index);
                UIManager.Instance.rewardUIManager.DisableAllDices();
            });
        }
        
        /// <summary>
        /// 点击无效化函数
        /// </summary>
        public void Disable()
        {
            this.GetComponent<Button>().interactable = false;
        }
        
        /// <summary>
        /// 有效化化函数
        /// </summary>
        public void Enable()
        {
            this.GetComponent<Button>().interactable = true;
        }

        /// <summary>
        /// 销毁动画
        /// </summary>
        /// <param name="animTime"></param>
        public void DoDestroyAnim(float animTime)
        {
            Disable();
            this.transform.DOScale(new Vector3(0, 0, 0), animTime);
            this.GetComponent<Image>().DOFade(0, animTime);
            StartCoroutine(DestroyGameObject(animTime));
        }
        
        /// <summary>
        /// 出现动画
        /// </summary>
        public void DoAppearAnim(float animTime)
        {
            this.GetComponent<Image>().DOFade(1, animTime);
        }

        /// <summary>
        /// 使用动画
        /// </summary>
        /// <param name="animTime"></param>
        /// <param name="scale"></param>
        public void DoChosenAnim(float animTime, float scale)
        {
            this.transform.DOScale(new Vector3(scale, scale, scale), animTime);
            this.GetComponent<Image>().DOFade(0, animTime);
            StartCoroutine(DestroyGameObject(animTime));
        }
        IEnumerator DestroyGameObject(float animTime)
        {
            yield return new WaitForSeconds(animTime);
            Destroy(gameObject);
        }
    }
}

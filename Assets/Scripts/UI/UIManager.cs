using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance; //单例调用

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 鼠标移动预览效果
        /// </summary>
        /// <param name="uiObject"></param>
        /// <param name="scale"></param>
        public void EnterPreview(GameObject uiObject, Vector3 scale)
        {
            uiObject.transform.DOScale(scale, 0.2f);
        }
        
        /// <summary>
        /// 晃动效果
        /// </summary>
        /// <param name="uiObject"></param>
        /// <param name="a"></param>
        public void DoShake(Image uiObject, float a)
        {
            Vector3 angle = new Vector3(0, 0, a);
            Vector3 backAngle = new Vector3(0, 0, -a);
            
            uiObject.rectTransform.DOLocalRotate(angle, 0.1f).OnComplete(() =>
            {
                uiObject.rectTransform.DOLocalRotate(backAngle, 0.1f).OnComplete(() =>
                {
                    uiObject.rectTransform.DOLocalRotate(Vector3.zero, 0.11f);
                });
            });
        }

        /// <summary>
        /// 鼠标移开预览效果
        /// </summary>
        /// <param name="uiObject"></param>
        /// <param name="scale"></param>
        public void ExitPreview(GameObject uiObject, Vector3 scale)
        {
            uiObject.transform.DOScale(scale, 0.2f);
        }
    }
}
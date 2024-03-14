using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance; //单例调用
        [SerializeField,Tooltip("圣物晃动的角度")]public float shakeAngle;
        [SerializeField,Tooltip("圣物变大的倍数")]public float previewSize;

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 鼠标移动预览效果
        /// </summary>
        /// <param name="uiObject"></param>
        public void EnterPreview(GameObject uiObject)
        {
            uiObject.transform.DOScale(new Vector3(previewSize,previewSize,previewSize), 0.2f);
        }
        
        /// <summary>
        /// 晃动效果
        /// </summary>
        /// <param name="uiObject"></param>
        public void DoShake(Image uiObject)
        {
            Vector3 angle = new Vector3(0, 0, shakeAngle);
            Vector3 backAngle = new Vector3(0, 0, -shakeAngle);
            
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
        public void ExitPreview(GameObject uiObject)
        {
            uiObject.transform.DOScale(new Vector3(1f,1f,1f), 0.2f);
        }
        
        /// <summary>
        /// 鼠标点击上下浮动
        /// </summary>
        /// <param name="uiObject"></param>
        public void ClickFlow(GameObject uiObject)
        {
            Vector2 pos = uiObject.transform.position;
            Vector2 up = pos + new Vector2(0f, 5f);
            Vector2 down = pos - new Vector2(0f, 5f);
            Sequence flowSquence = DOTween.Sequence();
            flowSquence.SetId("Flow");//设置动画序列别名
            flowSquence.Append(uiObject.transform.DOMove(up, 0.5f));
            flowSquence.Append(uiObject.transform.DOMove(pos, 0.5f));
            flowSquence.Append(uiObject.transform.DOMove(down, 0.5f));
            flowSquence.Append(uiObject.transform.DOMove(pos, 0.5f));
            flowSquence.SetLoops(-1);
        }

        /// <summary>
        /// 取消点击时杀死浮动动画
        /// </summary>
        /// <param name="uiObject"></param>
        public void CancelClick(GameObject uiObject)
        {
            DOTween.Kill("Flow");//依据动画别名Kill动画
        }
    }
}
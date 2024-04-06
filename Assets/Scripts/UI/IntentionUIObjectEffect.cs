using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class IntentionUIObjectEffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField] private GameObject descriptionUI;
        [SerializeField] private Text descriptionText;

        /// <summary>
        /// 鼠标预览
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.DOScale(new Vector3(-1.2f, 1.2f, 1.2f), 0.2f);
            descriptionUI.SetActive(true);
        }

        /// <summary>
        /// 退出预览
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            transform.DOScale(new Vector3(-1f, 1f, 1f), 0.2f);
            descriptionUI.SetActive(false);
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="id">序列号</param>
        /// <param name="desc">描述</param>
        /// <param name="durationTime">持续时间</param>
        public void Init(string id)
        {
            //依据内容初始化
            var tmp = ResourcesManager.GetIntentionUIData(id);
            descriptionText.text = $"{tmp.name}:{tmp.description}";
            this.transform.localScale = new Vector3(-1, 1, 1);
        }

       
        /// <summary>
        /// 销毁函数
        /// </summary>
        public void DoDestroy()
        {
            Destroy(gameObject);
        }
    }
}

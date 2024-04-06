using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class BuffUIObjectEfffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField] private GameObject descriptionUI;
        [SerializeField] private Text descriptionText;
        [SerializeField, Tooltip("持续回合数")] private Text durationText;

        /// <summary>
        /// 鼠标预览
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            UIManager.Instance.EnterPreview(this.gameObject, BuffUIManager.Instance.previewSize);
            descriptionUI.SetActive(true);
        }

        /// <summary>
        /// 退出预览
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            UIManager.Instance.ExitPreview(gameObject);
            descriptionUI.SetActive(false);
        }

        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="id">索引</param>
        /// <param name="durationTime">持续时间</param>
        public void Init(string id, int durationTime)
        {
            //TODO：依据内容初始化
            UpdateDuration(durationTime);
            var tmpData = ResourcesManager.GetBuffUIData(id);
            descriptionText.text = $"名称:{tmpData.name}+" +
                                   $"描述:{tmpData.description}/n";
            this.GetComponent<Image>().sprite = tmpData.sprite;
        }

        /// <summary>
        /// 更新持续时间
        /// </summary>
        /// <param name="time"></param>
        public void UpdateDuration(int time)
        {
            durationText.text = time.ToString();
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

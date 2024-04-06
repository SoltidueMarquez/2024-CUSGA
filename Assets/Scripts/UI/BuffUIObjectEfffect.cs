using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class BuffUIObjectEfffect : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField, Tooltip("描述UI")] private GameObject descriptionUI;
        [SerializeField, Tooltip("姓名Text")] private Text nameText;
        [SerializeField, Tooltip("描述Text")] private Text descriptionText;
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
            //依据内容初始化
            UpdateDuration(durationTime);
            var tmpData = ResourcesManager.GetBuffUIData(id);
            nameText.text = tmpData.name;
            descriptionText.text = tmpData.description;
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

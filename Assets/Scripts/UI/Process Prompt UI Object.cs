using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 流程提示物体UI效果
    /// </summary>
    public class ProcessPromptUIObject : MonoBehaviour
    {
        [SerializeField,Tooltip("提示框底图")]private Image background;
        [SerializeField,Tooltip("提示文字")]private Text tipText;

        public void Appear(float time, string value)
        {
            background.DOFade(1, time);
            tipText.DOText(value, time);
        }
        
        public void Fade(float time)
        {
            background.DOFade(0, time);
            tipText.DOText("        ", time);
        }
    }
}

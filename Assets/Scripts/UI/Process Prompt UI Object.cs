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
        [SerializeField, Tooltip("panel")] private Image panel;

        public void Appear(float time, string value)
        {
            //background.DOFade(1, time);
            background.transform.DOScaleY(1, time/3).SetEase(Ease.InOutElastic).OnComplete(() =>
            {
                tipText.DOText(value, time / 3 * 2);
            });
            panel.DOFade(0.25f, time);
        }
        
        public void Fade(float time)
        {
            //background.DOFade(0, time);
            panel.DOFade(0, time);
            tipText.DOText("        ", time / 3 * 2).OnComplete(() =>
            {
                background.transform.DOScaleY(0, time / 3).SetEase(Ease.InOutElastic);
            });
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

namespace UI.TextEffect
{
    [RequireComponent(typeof(Text))]
    public class FadeText : MonoBehaviour
    {
        //循环周期
        private float circletime = 1.5f;
 
        //定时器
        private float timer = 0f;
 
        //透明度 范围是0-1
        private float alpha = 1f;
 
        //状态标记
        private int status = 0;

        // Update is called once per frame
        void Update()
        {
            timer += Time.deltaTime;
            if (status == 0)
            {
                //随着循环进度逐渐变大
                alpha = timer % circletime / circletime;
                if (alpha > 0.99)
                {
                    status = 1;
                    timer = 0;
                }
            }
            else if (status == 1)
            {
                //随着循环进度逐渐变小
                alpha = 1 - timer % circletime / circletime;
                if (alpha < 0.01)
                {
                    status = 0;
                    timer = 0;
                }
            }
            var tmp = gameObject.GetComponent<Text>().color;
            gameObject.GetComponent<Text>().color = new Color(tmp.r, tmp.g, tmp.b, alpha);
        }
    }
}


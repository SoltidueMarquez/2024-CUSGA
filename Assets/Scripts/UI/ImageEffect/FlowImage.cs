using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.ImageEffect
{
    [RequireComponent(typeof(Image))]
    public class FlowImage : MonoBehaviour
    {
        [SerializeField] private float distance;
        [SerializeField] private float time;
        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("开始悬浮");
            Vector2 pos = this.transform.position;
            Vector2 up = pos + new Vector2(0f, distance);
            Vector2 down = pos - new Vector2(0f, distance);
            Sequence flowSquence = DOTween.Sequence();
            flowSquence.SetId("Flow"); //设置动画序列别名
            flowSquence.Append(this.transform.DOMove(up, time)).SetEase(Ease.Linear);
            flowSquence.Append(this.transform.DOMove(pos, time).SetEase(Ease.Linear));
            flowSquence.Append(this.transform.DOMove(down, time).SetEase(Ease.Linear));
            flowSquence.Append(this.transform.DOMove(pos, time).SetEase(Ease.Linear));
            flowSquence.SetLoops(-1); 
        }
    }
}

using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Image))]
    public class CharacterEffects : MonoBehaviour
    {
        public void Init(float time,Sprite sprite)
        {
            var image = this.GetComponent<Image>();
            image.sprite = sprite;
            image.DOFade(1, time).OnComplete(DoDestroy);
        }

        private void DoDestroy()
        {
            Destroy(this.gameObject);
        }
    }
}

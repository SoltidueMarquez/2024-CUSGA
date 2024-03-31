using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ShieldUIObject : MonoBehaviour
    {
        [SerializeField, Tooltip("护盾文本")] private Text shieldText;

        public void UpdateShieldNum(int shieldNum)
        {
            shieldText.DOText(shieldNum.ToString(), 0.1f);
        }
    }
}

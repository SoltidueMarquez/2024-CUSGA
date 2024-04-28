using Audio_Manager;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class ShieldUIObject : MonoBehaviour
    {
        [SerializeField, Tooltip("护盾文本")] private Text shieldText;
        public float curShield;
        
        public void UpdateShieldNum(int shieldNum)
        {
            var offset = curShield - shieldNum;
            curShield = shieldNum;
            switch (offset)
            {
                case 0:
                    return;
                case > 0:
                    if (AudioManager.Instance != null)
                    {
                        AudioManager.Instance.PlaySound("shieldAttack");
                    }
                    break;
                case <0:
                    if (AudioManager.Instance != null)
                    {
                        AudioManager.Instance.PlaySound("getShield");
                    }
                    break;
            }
            shieldText.DOText(shieldNum.ToString(), 0.1f);
        }
    }
}

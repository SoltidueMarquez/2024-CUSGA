using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AttackText : MonoBehaviour
    {
        [SerializeField, Tooltip("伤害数字文本")] private Text attackText;

        public void Init(int hit,float attackTime)
        {
            attackText.text = hit.ToString();
            this.transform.DOShakePosition(attackTime, 10).OnComplete(() =>//数字晃动
            {
                attackText.DOFade(0, attackTime);//数字消失
            });
        }
        
        public void InitCure(int num,float attackTime)
        {
            attackText.text = num.ToString();
            attackText.color = Color.green;
            this.transform.DOShakePosition(attackTime, 10).OnComplete(() =>//数字晃动
            {
                attackText.DOFade(0, attackTime);//数字消失
            });
        }
        
        public void InitShield(int num,float attackTime)
        {
            attackText.text = num.ToString();
            attackText.color = Color.black;
            this.transform.DOShakePosition(attackTime, 10).OnComplete(() =>//数字晃动
            {
                attackText.DOFade(0, attackTime);//数字消失
            });
        }

    }
}

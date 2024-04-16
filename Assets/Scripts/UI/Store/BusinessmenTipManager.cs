using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Store
{
    public class BusinessmenTipManager : MonoSingleton<BusinessmenTipManager>
    {
        [SerializeField] private GameObject failTip;
        [SerializeField] private Text failTipText;
        [SerializeField,Tooltip("失败提示")] private List<string> failTipContent;
        [SerializeField,Tooltip("成功提示")] private List<string> successTipContent;
        [SerializeField] private float animTime;

        private float _counter;
        
        public void ShowTip(BuyFailType failType)
        {
            if (_counter > 0) { return;}//计时器没归零就不执行
            _counter = animTime * 2f;
            StartCoroutine(Count());//开始计时
            
            failTip.SetActive(true);
            if ((int)failType >= failTipContent.Count) { return; }
            var content = failTipContent[(int)failType];
            failTipText.text = "";
            failTipText.DOText(content,animTime);
            StartCoroutine(LateHide());
        }
        IEnumerator LateHide()
        {
            yield return new WaitForSeconds(animTime * 1.5f);
            
            Hide();
        }
        IEnumerator Count()
        {
            while (_counter > 0)
            {
                yield return new WaitForSeconds(0.1f);
                Debug.Log(_counter -= 0.1f);
            }
        }
        private void Hide()
        {
            failTip.SetActive(false);
            _counter = 0;//计时器置零
        }
        
        public void ShowTip()
        {
            if (_counter > 0) { return;}//计时器没归零就不执行
            _counter = animTime * 2f;
            StartCoroutine(Count());//开始计时
            
            failTip.SetActive(true);
            var content = successTipContent[Random.Range(0, successTipContent.Count)];
            failTipText.text = "";
            failTipText.DOText(content,animTime);
            StartCoroutine(LateHide());
        }
    }
}

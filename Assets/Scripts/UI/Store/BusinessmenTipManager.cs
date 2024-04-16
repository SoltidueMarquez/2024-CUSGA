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
        [SerializeField,Tooltip("缺钱失败提示")] private List<string> moneyFailTipContent;
        [SerializeField,Tooltip("没空位失败提示")] private List<string> columnFailTipContent;
        [SerializeField,Tooltip("强化失败提示")] private List<string> strengthenFailTipContent;
        [SerializeField,Tooltip("成功提示")] private List<string> successTipContent;
        [SerializeField] private float animTime;

        private float _counter;

        private void Start()
        {
            StoreManager.Instance.OnUpgradeFail.AddListener(ShowTip);//增加强化失败的语音
        }

        public void ShowTip(BuyFailType failType)
        {
            if (_counter > 0) { return;}//计时器没归零就不执行
            _counter = animTime * 2f;
            StartCoroutine(Count());//开始计时
            
            failTip.SetActive(true);
            var content = new List<string>();
            switch (failType)
            {
                case BuyFailType.NoMoney:
                    content = moneyFailTipContent;
                    break;
                case BuyFailType.NoBagSpace:
                    content = columnFailTipContent;
                    break;
                case BuyFailType.DicePointMax:
                    content = strengthenFailTipContent;
                    break;
            }
            failTipText.text = "";
            failTipText.DOText(content[Random.Range(0, content.Count)], animTime);
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

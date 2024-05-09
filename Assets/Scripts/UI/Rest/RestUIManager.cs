using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Map;
using Rest;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Rest
{
    public class RestUIManager : MonoBehaviour
    {
        [SerializeField,Tooltip("休息遮罩")] private GameObject panel;
        [SerializeField,Tooltip("休息界面")] private Transform restUI;
        [SerializeField,Tooltip("出现位置")] private Transform enterPosition;
        [SerializeField,Tooltip("离开位置")] private Transform exitPosition;
        [Header("文本设置")]
        [SerializeField,Tooltip("初始的描述文本")] private string enterString;
        [SerializeField,Tooltip("选择休息前的描述文本")] private string beforeRestString;
        [SerializeField,Tooltip("选择休息后的描述文本")] private List<string> afterRestString;
        [SerializeField,Tooltip("选择祝福前的描述文本")] private string beforeBlessString;
        [SerializeField,Tooltip("选择祝福后的描述文本")] private List<string> afterBlessString;
        [Header("UI物体")]
        [SerializeField, Tooltip("休息按钮")] private Button restButton;
        [SerializeField, Tooltip("祝福按钮")] private Button blessButton;
        [SerializeField, Tooltip("离开按钮")] private Button exitButton;
        [SerializeField, Tooltip("台词文本")] private Text wordText;
        [SerializeField, Tooltip("描述文本")] private Text descText;
        [Header("动画设置")]
        [SerializeField, Tooltip("动画时长")] private float animTime;

        private void Start()
        {
            RestManager.Instance.onChooseRest.AddListener(HideButton);
            RestManager.Instance.onChooseBlessing.AddListener(HideButton);
            RestManager.Instance.onEnterRest.AddListener(EnterAnim);
            RestManager.Instance.onExitRest.AddListener(ExitAnim);
            RestManager.Instance.changeDescText.AddListener(ChangeDescText);
            RestManager.Instance.hideDescText.AddListener(HideDescText);
            
            exitButton.onClick.AddListener(() => { RestManager.Instance.ExitRest(); });
            restButton.onClick.AddListener(() =>
            {
                RestTextAnim();
                RestManager.Instance.ChooseRest();
            });
            blessButton.onClick.AddListener(() =>
            {
                BlessTextAnim();
                RestManager.Instance.ChooseBlessing();
            });
        }

        #region 按钮
        private void HideButton()
        {
            StopCoroutine(LateHideButton());
            restButton.interactable = false;
            blessButton.interactable = false;
            StartCoroutine(LateHideButton());
        }
        IEnumerator LateHideButton()
        {
            yield return new WaitForSeconds(0f);
            restButton.gameObject.SetActive(false);
            blessButton.gameObject.SetActive(false);
        }
        private void ShowButton()
        {
            restButton.gameObject.SetActive(true);
            blessButton.gameObject.SetActive(true);
            restButton.interactable = true;
            blessButton.interactable = true;
        }
        #endregion

        #region 文本
        private void RestTextAnim()
        {
            descText.gameObject.SetActive(false);
            wordText.text = "";
            var index = Random.Range(0, afterRestString.Count);
            wordText.DOText(afterRestString[index], animTime);
        }
        private void BlessTextAnim()
        {
            descText.gameObject.SetActive(false);
            wordText.text = "";
            var index = Random.Range(0, afterBlessString.Count);
            wordText.DOText(afterBlessString[index], animTime);
        }
        private void InitWordText()
        {
            wordText.text = "";
            wordText.DOText(enterString, animTime);
        }
        private void ChangeDescText(RestType type)
        {
            descText.text = "";
            var tmpString = type switch
            {
                RestType.Rest => beforeRestString,
                RestType.Bless => beforeBlessString+$"(当前cost上限:{MapManager.Instance.GetCurrentPlayerMaxCost()})",
                _ => ""
            };
            descText.DOText(tmpString, animTime);
        }
        private void HideDescText()
        {
            descText.text = "";
        }
        #endregion

        #region 唤醒界面相关
        private void EnterAnim()
        {
            SettingsManager.Instance.FreezeMap(true);
            wordText.text = "";
            descText.text = "";
            descText.gameObject.SetActive(true);
            panel.SetActive(true);
            panel.GetComponent<Image>().DOFade(1, animTime);
            restUI.DOMove(enterPosition.position, animTime).OnComplete(InitWordText);
            ShowButton();
        }
        #endregion
        
        #region 离开界面相关
        private void ExitAnim()
        {
            restUI.DOMove(exitPosition.position, animTime);
            panel.GetComponent<Image>().DOFade(0, animTime).OnComplete(() =>
            {
                panel.SetActive(false);
                SettingsManager.Instance.FreezeMap(false);
            });
        }
        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RestManager.Instance.EnterRest();
            }
        }
    }
}

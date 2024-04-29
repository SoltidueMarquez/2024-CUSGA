using System.Collections.Generic;
using UI.Store;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Tutorial
{
    public class TutorialPage : MonoBehaviour
    {
        [SerializeField, Tooltip("场景教程")] private PageView pageView;
        [SerializeField, Tooltip("场景教程")] private RectTransform pageViewRect;
        [SerializeField, Tooltip("页码框")] private List<Toggle> toggleList;
        private int _curIndex;
        
        private void Start()
        {
            pageView.Init(pageViewRect);
        }

        private void toggleOn(int index)
        {
            toggleList[index].isOn = true;
        }
        
        private void Update()
        {
            _curIndex = pageView.GetCurIndex();
            toggleOn(_curIndex);
        }

        public void Active()
        {
            gameObject.SetActive(true);
        }
        public void Inactive()
        {
            gameObject.SetActive(false);
        }
    }
}

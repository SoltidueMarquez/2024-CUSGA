using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// 战斗流程UI动画管理器
    /// </summary>
    public class ProcessAnimationManager : MonoBehaviour
    {
        [Header("我方回合开始动画相关设置:")] 
        [SerializeField, Tooltip("旋转物体列表")] private List<RotateObject> rotateObjects;
        [SerializeField, Tooltip("旋转物体列表")] private List<Transform> results;

        /// <summary>
        /// 轮盘与按钮旋转进入
        /// </summary>
        private void RotateIn()
        {
            foreach (var rotateObject in rotateObjects)
            {
                var v3 = new Vector3(0, 0, rotateObject.rotateAngle);
                rotateObject.transform.DOLocalRotate(v3, rotateObject.rotateTime, RotateMode.FastBeyond360);
            }
        }

        private void ResultAppear()
        {
            
        }
        
        private void Start()
        {
            //RotateIn();
        }
    }

    [Serializable]
    class RotateObject
    {
        [SerializeField,Tooltip("Transform组件")]public Transform transform;
        [SerializeField,Tooltip("转时间")]public float rotateTime;
        [SerializeField,Tooltip("旋转到的角度")]public float rotateAngle;
    }
}

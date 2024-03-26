using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum PlayerTurnState
    {
        PlayerTurnStart,
        PlayerTurnEnd
    }
    /// <summary>
    /// 战斗流程UI动画管理器
    /// </summary>
    public class ProcessAnimationManager : MonoSingleton<ProcessAnimationManager>
    {
        [Header("我方回合开始动画相关设置:")] 
        [SerializeField, Tooltip("旋转物体列表")] private List<RotateObject> rotateObjects;
        [SerializeField, Tooltip("出现结果列表")] private List<Transform> results;
        [SerializeField, Tooltip("出现时间间隔")] public float timeInterval;
        [SerializeField, Tooltip("出现位置(Y)")] private float finalPosition;
        
        /// <summary>
        /// 轮盘与按钮旋转进入
        /// </summary>
        private void RotateIn(PlayerTurnState state)
        {
            float offset = 0f;
            switch (state)
            {
                case PlayerTurnState.PlayerTurnStart:
                    offset = 0;
                    break;
                case PlayerTurnState.PlayerTurnEnd:
                    offset = 180;
                    break;
                default:
                    break;
            }
            foreach (var rotateObject in rotateObjects)
            {
                var v3 = new Vector3(0, 0, rotateObject.rotateAngle-offset);
                rotateObject.transform.DOLocalRotate(v3, rotateObject.rotateTime, RotateMode.FastBeyond360);
            }
        }

        private void ResultAppear(PlayerTurnState state)
        {
            float position = 0f;
            switch (state)
            {
                case PlayerTurnState.PlayerTurnStart:
                    position = finalPosition;
                    break;
                case PlayerTurnState.PlayerTurnEnd:
                    position = -finalPosition * 2;
                    break;
                default:
                    position = finalPosition;
                    break;
            }
            float time = 0;
            foreach (var result in results)
            {
                time += timeInterval;
                result.DOMoveY(position, time);
            }
        }

        public void PlayerTurnStart()
        {
            RotateIn(PlayerTurnState.PlayerTurnStart);
            ResultAppear(PlayerTurnState.PlayerTurnStart);
        }
        
        public void PlayerTurnEnd()
        {
            RotateIn(PlayerTurnState.PlayerTurnEnd);
            ResultAppear(PlayerTurnState.PlayerTurnEnd);
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

using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace UI.Store
{
    public class RollUIManager : MonoBehaviour
    {
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

        /// <summary>
        /// 结果出现
        /// </summary>
        /// <param name="state"></param>
        private void ResultAppear(PlayerTurnState state)
        {
            float position = 0f;
            switch (state)
            {
                case PlayerTurnState.PlayerTurnStart:
                    position = finalPosition;
                    break;
                case PlayerTurnState.PlayerTurnEnd:
                    position = finalPosition * 2;
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
        
        public void StoreAppear()
        {
            RotateIn(PlayerTurnState.PlayerTurnStart);
            ResultAppear(PlayerTurnState.PlayerTurnStart);
        }
        public void StoreDisAppear()
        {
            RotateIn(PlayerTurnState.PlayerTurnEnd);
            ResultAppear(PlayerTurnState.PlayerTurnEnd);
        }

    }
}

using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// UI管理器
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance; //单例调用
        [Header("通用")] 
        [Tooltip("拖拽图层")] public Transform dragCanvas;
        [Tooltip("奖励界面管理器")] public RewardUIManager rewardUIManager;

        private void Awake()
        {
            Instance = this;
        }
        
        public int FindFirstEmptyColumn(List<Column> columns)
        {
            for (int i = 0; i < columns.Count; i++)
            {
                if (columns[i].bagObject == null)
                {
                    return i;
                }
            }
            return -1;
        }
        
        /// <summary>
        /// 鼠标移动预览效果
        /// </summary>
        /// <param name="uiObject"></param>
        /// <param name="pSize"></param>
        public void EnterPreview(GameObject uiObject, float pSize)
        {
            var oldScale = uiObject.transform.localScale;
            pSize = oldScale.x * pSize;
            uiObject.transform.DOScale(new Vector3(pSize, pSize, pSize), 0.2f);
        }
        public void EnterPreview(GameObject uiObject, float pSize,float maxSize)
        {
            var oldScale = uiObject.transform.localScale;
            pSize = oldScale.x * pSize;
            pSize = (pSize > maxSize) ? maxSize : pSize;
            uiObject.transform.DOScale(new Vector3(pSize, pSize, pSize), 0.2f);
        }

        /// <summary>
        /// 晃动效果
        /// </summary>
        /// <param name="uiObject"></param>
        /// <param name="sAngle"></param>
        public void DoShake(Image uiObject, float sAngle)
        {
            Vector3 angle = new Vector3(0, 0, sAngle);
            Vector3 backAngle = new Vector3(0, 0, -sAngle);

            uiObject.rectTransform.DOLocalRotate(angle, 0.1f).OnComplete(() =>
            {
                uiObject.rectTransform.DOLocalRotate(backAngle, 0.1f).OnComplete(() =>
                {
                    uiObject.rectTransform.DOLocalRotate(Vector3.zero, 0.11f);
                });
            });
        }

        /// <summary>
        /// 鼠标移开预览效果
        /// </summary>
        /// <param name="uiObject"></param>
        public void ExitPreview(GameObject uiObject)
        {
            uiObject.transform.DOScale(new Vector3(1f, 1f, 1f), 0.2f);
        }

        /// <summary>
        /// 鼠标点击上下浮动
        /// </summary>
        /// <param name="uiObject"></param>
        /// <param name="distance"></param>
        public void ClickFlow(GameObject uiObject,float distance)
        {
            Vector2 pos = uiObject.transform.position;
            Vector2 up = pos + new Vector2(0f, distance);
            Vector2 down = pos - new Vector2(0f, distance);
            Sequence flowSquence = DOTween.Sequence();
            flowSquence.SetId("Flow"); //设置动画序列别名
            flowSquence.Append(uiObject.transform.DOMove(up, 0.5f)).SetEase(Ease.Linear);
            flowSquence.Append(uiObject.transform.DOMove(pos, 0.5f).SetEase(Ease.Linear));
            flowSquence.Append(uiObject.transform.DOMove(down, 0.5f).SetEase(Ease.Linear));
            flowSquence.Append(uiObject.transform.DOMove(pos, 0.5f).SetEase(Ease.Linear));
            flowSquence.SetLoops(-1);
        }

        /// <summary>
        /// 取消点击时杀死浮动动画
        /// </summary>
        /// <param name="uiObject"></param>
        public void CancelClick(GameObject uiObject)
        {
            DOTween.Kill("Flow"); //依据动画别名Kill动画
        }

        /// <summary>
        /// 鼠标拖拽跟随函数
        /// </summary>
        /// <param name="uiObject"></param>
        public void OnDrag(GameObject uiObject)
        {
            uiObject.transform.position = Input.mousePosition;
        }

        /// <summary>
        /// 物品栏拖拽换位函数，返回值为被交换的物体
        /// </summary>
        /// <param name="uiObject"></param>
        /// <param name="columns"></param>
        /// <param name="oldColumn"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public GameObject DetectPosition(GameObject uiObject, List<Column> columns, Column oldColumn, float offset)
        {
            GameObject switchObject = null;
            Vector3 pos = oldColumn.transform.position;
            foreach (var column in columns)
            {
                if (Between(uiObject.transform.position.x, column.transform.position.x - offset,
                        column.transform.position.x + offset)
                    && Between(uiObject.transform.position.y, column.transform.position.y - offset,
                        column.transform.position.y + offset))
                {
                    if (column.bagObject!=null&&column.bagObject!=uiObject)//如果新的栏有物体则交换位置
                    {
                        column.bagObject.GetComponent<UIObjectEffects>()._currentColumn = oldColumn; //需要更新旧物体的_currentColumn
                        column.bagObject.transform.position = oldColumn.transform.position;//原来的格子里的物体换到旧的格子里去
                        column.bagObject.GetComponent<UIObjectEffects>().editState = oldColumn.state;//设置原来的格子里的物体的可编辑状态
                        switchObject = column.bagObject;    //返回值为被交换的物体
                        oldColumn.bagObject = column.bagObject; //旧的格子放着交换来的物体
                    }
                    else
                    {
                        oldColumn.bagObject = null;//旧的栏目不再存放物体
                    }
                    //物体移动到相应位置
                    column.bagObject = uiObject;//新的格子放移过来的物体
                    pos = column.transform.position;
                    uiObject.GetComponent<UIObjectEffects>().editState = column.state;//设置状态
                }
            }
            uiObject.transform.position = pos;

            return switchObject;
        }
        private bool Between(float value, float left, float right)
        {
            return value >= left && value <= right;
        }

        /// <summary>
        /// 检测物品所在栏函数
        /// </summary>
        /// <param name="uiObject"></param>
        /// <param name="columns"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Column DetectColumn(GameObject uiObject, List<Column> columns, float offset)
        {
            foreach (var column in columns)
            {
                if (Between(uiObject.transform.position.x, column.transform.position.x - offset,
                        column.transform.position.x + offset)
                    && Between(uiObject.transform.position.y, column.transform.position.y - offset,
                        column.transform.position.y + offset))
                {
                    return column;
                }
            }

            return null;
        }
    }
}
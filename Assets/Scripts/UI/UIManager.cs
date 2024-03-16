using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance; //单例调用
        [Header("通用")] 
        [Tooltip("拖拽图层")] public Transform dragCanvas;
        
        [Header("圣物相关")] 
        [SerializeField, Tooltip("圣物晃动的角度")] private float shakeAngle;
        [SerializeField, Tooltip("圣物变大的倍数")] private float previewSize;
        [Tooltip("圣物栏列表")] public List<Column> sacredObjectColumns;
        [Tooltip("圣物栏目判定")] public float offset;
        [Tooltip("圣物模板")] public GameObject template;

        private void Awake()
        {
            Instance = this;
        }

        /// <summary>
        /// 鼠标移动预览效果
        /// </summary>
        /// <param name="uiObject"></param>
        public void EnterPreview(GameObject uiObject)
        {
            uiObject.transform.DOScale(new Vector3(previewSize, previewSize, previewSize), 0.2f);
        }

        /// <summary>
        /// 晃动效果
        /// </summary>
        /// <param name="uiObject"></param>
        public void DoShake(Image uiObject)
        {
            Vector3 angle = new Vector3(0, 0, shakeAngle);
            Vector3 backAngle = new Vector3(0, 0, -shakeAngle);

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
        public void ClickFlow(GameObject uiObject)
        {
            Vector2 pos = uiObject.transform.position;
            Vector2 up = pos + new Vector2(0f, 5f);
            Vector2 down = pos - new Vector2(0f, 5f);
            Sequence flowSquence = DOTween.Sequence();
            flowSquence.SetId("Flow"); //设置动画序列别名
            flowSquence.Append(uiObject.transform.DOMove(up, 0.5f));
            flowSquence.Append(uiObject.transform.DOMove(pos, 0.5f));
            flowSquence.Append(uiObject.transform.DOMove(down, 0.5f));
            flowSquence.Append(uiObject.transform.DOMove(pos, 0.5f));
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
        /// 物品栏拖拽换位函数
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="oldPosition"></param>
        /// <returns></returns>
        public void DetectPosition(GameObject uiObject, List<Column> columns, Column oldColumn)
        {
            Vector3 pos = oldColumn.transform.position;;
            foreach (var column in columns)
            {
                if (Between(Input.mousePosition.x, column.transform.position.x - offset,
                        column.transform.position.x + offset)
                    && Between(Input.mousePosition.y, column.transform.position.y - offset,
                        column.transform.position.y + offset))
                {
                    if (column.bagObject!=null&&column.bagObject!=uiObject)//如果新的栏有物体则交换位置
                    {
                        column.bagObject.GetComponent<UIObjectEffects>()._currentColumn = oldColumn; //需要更新旧物体的_currentColumn
                        column.bagObject.transform.position = oldColumn.transform.position;//原来的格子里的物体换到旧的格子里去
                        oldColumn.bagObject = column.bagObject; //旧的格子放着交换来的物体
                    }
                    else
                    {
                        oldColumn.bagObject = null;//旧的栏目不再存放物体
                    }
                    //物体移动到相应位置
                    column.bagObject = uiObject;//新的格子放移过来的物体
                    pos = column.transform.position;
                }
            }
            uiObject.transform.position = pos;
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
        /// <returns></returns>
        public Column DetectColumn(GameObject uiObject,List<Column> columns)
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

        // public void AddItem(string id)
        // {
        //     //TODO:根据id读取数据
        //     GameObject newSacredObject = Instantiate(template);
        //     newSacredObject.GetComponent<SacredObjectsUIEffects>().Init();
        // }
    }
}
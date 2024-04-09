using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Store
{
    /// <summary>
    /// 滑动页面效果
    /// </summary>
    public class PageView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private float speed = 3; //滑动速度
        [SerializeField, Tooltip("内容页面")] private RectTransform content;
        
        private ScrollRect _rect; //ScrollRect组件
        private float _targetHorizontal;
        private readonly List<float> _posList = new(); //存图片的位置(0, 0.333, 0.666, 1) 
        private bool _isDrag = true; //是否拖动
        private float _startTime;
        private int _curIndex;

        private static void OnUpdateAnimation(int index, int lastIndex) //拖动结束时的切换事件
        {
            //在下面加动画变换函数或者在别的脚本里检测位置变换
            
            //号标更换
        }

        private static void EndUpdateAnimation(int index)//拖动开始时的事件
        {
            //在下面加动画变换函数或者在别的脚本里检测位置变换
        }

        public void Init(RectTransform canvas)
        {
            _rect = this.GetComponent<ScrollRect>();
            var childCount = _rect.content.transform.childCount;
            //根据子物体数动态设置内容页长度
            float width = canvas.rect.width;
            float height = content.rect.height;
            content.sizeDelta = new Vector2(width * (childCount-1), height);
            //添加content下的物体的位置信息到posList里去
            for (var i = 0; i < childCount; i++)
            {
                _posList.Add((i)*(1f/(_rect.content.transform.childCount-1)));//存图片位置
            }
            
            _curIndex = 0;//直接跳转到上次的章节
            _rect.horizontalNormalizedPosition = Mathf.Lerp(_rect.horizontalNormalizedPosition, _posList[_curIndex], 10);
            OnUpdateAnimation(_curIndex, -1);
        }

        private void Update()
        {
            if (!_isDrag)
            {
                _startTime += Time.deltaTime;
                var t = _startTime * speed;
                //加速滑动效果
                _rect.horizontalNormalizedPosition = Mathf.Lerp(_rect.horizontalNormalizedPosition, _targetHorizontal, t);//插值函数
                //缓慢匀速滑动效果
                //rect.horizontalNormalizedPosition = Mathf.Lerp(rect.horizontalNormalizedPosition, _targetHorizontal, Time.deltaTime * speed);
            }
        }


        public void OnBeginDrag(PointerEventData eventData)//开始拖动
        {
            _isDrag = true;
            //根据UI切换控制动画变换事件函数
            EndUpdateAnimation(_curIndex);
        }

        public void OnEndDrag(PointerEventData eventData)//结束拖动
        {
            var posX = _rect.horizontalNormalizedPosition;
            
            //计算_curIndex应该改变到哪一个页面的index
            var index = _curIndex;
            var offset = _posList[index] - posX;
            if (offset < 0 && _curIndex < _posList.Count - 1) index++;
            else if(offset > 0 && _curIndex >0) index--;

            //根据UI切换控制动画变换事件函数
            OnUpdateAnimation(index, _curIndex);
            
            _curIndex = index;
            _targetHorizontal = _posList[_curIndex]; //设置当前坐标，更新函数进行插值  
            _isDrag = false;
            _startTime = 0;
        }

        public int GetCurIndex()
        {
            return _curIndex;
        }
    }
}

//根据content下子物体的位置和宽度将整个scrollview内容划分成多个区域，鼠标没拖动到一定程度坐标就锁死在某个区域内，拖动超过阈值isDrug=true直接插值渐变坐标切换到相邻的区域
//GetComponent<RectTransform>().rect.width用于获取到当前UI的宽高
//注意是isDrug=false时执行切换
using UnityEngine;
using System.Collections.Generic;

namespace UI
{
    public class BuffUIManager : MonoSingleton<BuffUIManager>
    {
        [Header("Buff相关：")] 
        [SerializeField, Tooltip("生成模板")] private GameObject template;
        [SerializeField, Tooltip("预览时放大倍数")] public float previewSize;
        [SerializeField, Tooltip("敌人buff父物体")] public Transform enemyParent;
        [SerializeField, Tooltip("玩家buff父物体")] public Transform playerParent;
        [SerializeField] private List<BuffUIObjectEfffect> playerBuffList;
        [SerializeField] private List<BuffUIObjectEfffect> enemyBuffList;

        public void CreateBuffUIObject(Character character, string desc, int durationTime)
        {
            Transform parent = null;
            List<BuffUIObjectEfffect> buffList = null;
            switch (character)
            {
                case Character.Player:
                    parent = playerParent;
                    buffList = playerBuffList;
                    break;
                case Character.Enemy:
                    parent = enemyParent;
                    buffList = enemyBuffList;
                    break;
            }
            var tmp = Instantiate(template, parent, true);
            var tmpBuff = tmp.GetComponent<BuffUIObjectEfffect>();
            tmpBuff.Init(desc, durationTime);//初始化
            tmp.SetActive(true);
            buffList?.Add(tmpBuff);
        }

        /// <summary>
        /// 移除索引为index的buff UI
        /// </summary>
        /// <param name="character"></param>
        /// <param name="index"></param>
        public void RemoveBuffUIObject(Character character, int index)
        {
            List<BuffUIObjectEfffect> buffList = null;
            switch (character)
            {
                case Character.Player:
                    buffList = playerBuffList;
                    break;
                case Character.Enemy:
                    buffList = enemyBuffList;
                    break;
            }
            if (buffList?.Count <= index) { return;}
            buffList?[index]?.DoDestroy();
        }

        /// <summary>
        /// 更新buff UI的持续回合文本
        /// </summary>
        /// <param name="character"></param>
        /// <param name="index"></param>
        /// <param name="durationTime"></param>
        public void UpdateBuffDurationTime(Character character, int index, int durationTime)
        {
            List<BuffUIObjectEfffect> buffList = null;
            switch (character)
            {
                case Character.Player:
                    buffList = playerBuffList;
                    break;
                case Character.Enemy:
                    buffList = enemyBuffList;
                    break;
            }
            if (buffList?.Count <= index) { return;}
            buffList?[index]?.UpdateDuration(durationTime);
        }
    }
}

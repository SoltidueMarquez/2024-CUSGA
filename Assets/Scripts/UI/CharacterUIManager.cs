using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterUIManager : MonoSingleton<CharacterUIManager>
    {
        [Header("敌人相关")] 
        [SerializeField, Tooltip("敌人")] private Transform enemy;
        [SerializeField, Tooltip("敌人血条")] private Slider enemyHealthSlider;
        
        
        [SerializeField, Tooltip("晃动幅度")] private Vector3 punchAmplitude;
        [SerializeField, Tooltip("晃动时间")] private float durationTime;
        [SerializeField, Tooltip("晃动次数")] private int punchTime;
        
        [SerializeField, Tooltip("玩家血条")] private Slider playerHealthSlider;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                enemy.DOPunchPosition(punchAmplitude, durationTime, punchTime);
            }
        }

        /// <summary>
        /// 血条控制函数，0表示玩家，1表示敌人
        /// </summary>
        /// <param name="id">0表示玩家，1表示敌人</param>
        /// <param name="currentHealth">当前生命值</param>
        /// <param name="maxHealth">最大生命值</param>
        public void ChangeHealthSlider(int id, int currentHealth, int maxHealth)
        {
            switch (id)
            {
                case 1:
                    enemyHealthSlider.value = (float)currentHealth / maxHealth;
                    break;
                case 0:
                    playerHealthSlider.value = (float)currentHealth / maxHealth;
                    break;
            }
        }
        
        
    }
}

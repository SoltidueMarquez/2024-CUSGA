using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public enum Character
    {
        Player,
        Enemy
    }
    public class CharacterUIManager : MonoSingleton<CharacterUIManager>
    {
        [Header("通用")]
        [SerializeField, Tooltip("晃动幅度")] private Vector3 punchAmplitude;
        [SerializeField, Tooltip("晃动时间")] private float durationTime;
        [SerializeField, Tooltip("晃动次数")] private int punchTime;
        
        [Header("敌人相关")] 
        [SerializeField, Tooltip("敌人")] private Transform enemy;
        [SerializeField, Tooltip("敌人血条")] private Slider enemyHealthSlider;
        
        [Header("玩家相关")]
        [SerializeField, Tooltip("玩家")] private Transform player;
        [SerializeField, Tooltip("玩家血条")] private Slider playerHealthSlider;

        /// <summary>
        /// 角色受击动画
        /// </summary>
        /// <param name="character">0表示玩家，1表示敌人</param>
        public void BeAttacked(Character character)
        {
            switch (character)
            {
                case Character.Enemy:
                    enemy.DOPunchPosition(punchAmplitude, durationTime, punchTime);
                    break;
                case Character.Player:
                    player.DOPunchPosition(punchAmplitude, durationTime, punchTime);
                    break;
            }
        }

        /// <summary>
        /// 血条控制函数，0表示玩家，1表示敌人
        /// </summary>
        /// <param name="character">0表示玩家，1表示敌人</param>
        /// <param name="currentHealth">当前生命值</param>
        /// <param name="maxHealth">最大生命值</param>
        public void ChangeHealthSlider(Character character, int currentHealth, int maxHealth)
        {
            switch (character)
            {
                case Character.Enemy:
                    enemyHealthSlider.value = (float)currentHealth / maxHealth;
                    break;
                case Character.Player:
                    playerHealthSlider.value = (float)currentHealth / maxHealth;
                    break;
            }
        }
        
        
    }
}

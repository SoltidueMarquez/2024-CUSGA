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
        [Header("受击")]
        [SerializeField, Tooltip("晃动幅度")] private Vector3 punchAmplitude;
        [SerializeField, Tooltip("晃动时间")] private float durationTime;
        [SerializeField, Tooltip("晃动次数")] private int punchTime;
        
        [Header("攻击")]
        [SerializeField, Tooltip("使用其他骰面时的跳跃偏移量")] private Vector3 useOtherOffset;
        [SerializeField, Tooltip("攻击幅度")] private int attackAmplitude;
        [SerializeField, Tooltip("攻击时间")] private float attackTime;
        [SerializeField, Tooltip("伤害文字模板")] private GameObject attackTextTemplate;
        [SerializeField, Tooltip("伤害文字父物体")] private Transform attackTextParent;
        [SerializeField, Tooltip("伤害文字偏移量")] private Vector3 attackTextOffset;
        
        [Header("敌人相关")] 
        [SerializeField, Tooltip("敌人")] private Transform enemy;
        [SerializeField, Tooltip("敌人血条")] private Slider enemyHealthSlider;
        
        [Header("玩家相关")]
        [SerializeField, Tooltip("玩家")] public Transform player;
        [SerializeField, Tooltip("玩家血条")] private Slider playerHealthSlider;

        private Vector3 offsetPosition;


        private void CreateAttackText(Transform parent,int hit,Vector3 position)
        {
            var tmp = Instantiate(attackTextTemplate, parent, true);
            tmp.transform.position = position;//更改位置
            tmp.GetComponent<AttackText>().Init(hit,attackTime);//初始化
            tmp.SetActive(true);
        }

        /// <summary>
        /// 角色受击动画
        /// </summary>
        /// <param name="character">0表示玩家，1表示敌人</param>
        public void BeAttacked(Character character, int hitNum)
        {
            Vector3 attackTextPosition = attackTextOffset;
            switch (character)
            {
                case Character.Enemy:
                    enemy.DOPunchPosition(punchAmplitude, durationTime, punchTime);
                    attackTextPosition += enemy.position;
                    break;
                case Character.Player:
                    player.DOPunchPosition(punchAmplitude, durationTime, punchTime);
                    attackTextPosition += player.position;
                    break;
            }

            CreateAttackText(attackTextParent, hitNum, attackTextPosition);//创建伤害数字
        }

        /// <summary>
        /// 角色攻击动画
        /// </summary>
        /// <param name="character"></param>
        public void Attack(Character character)
        {
            switch (character)
            {
                case Character.Enemy:
                    offsetPosition = (player.position - enemy.position).normalized * attackAmplitude;
                    enemy.DOPunchPosition(offsetPosition, attackTime, 1);
                    break;
                case Character.Player:
                    offsetPosition = (enemy.position - player.position).normalized * attackAmplitude;
                    player.DOPunchPosition(offsetPosition, attackTime, 1);
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

        /// <summary>
        /// 角色使用其他骰面的动画
        /// </summary>
        public void UseOtherDice(Character character)
        {
            switch (character)
            {
                case Character.Enemy:
                    enemy.DOPunchPosition(useOtherOffset, attackTime, 1);
                    break;
                case Character.Player:
                    player.DOPunchPosition(useOtherOffset, attackTime, 1);
                    break;
            }
        }
        
    }
}

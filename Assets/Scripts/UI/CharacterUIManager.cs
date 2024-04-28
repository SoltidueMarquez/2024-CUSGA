using System;
using System.Collections;
using System.Collections.Generic;
using Audio_Manager;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI
{
    public enum Character
    {
        Player,
        Enemy
    }
    /// <summary>
    /// TODO:动画的顺序播放队列需要实现，因为之后的动画可能会打断之前的动画，但是要求是即选即用，感觉不能用队列，暂时先这样吧
    /// </summary>
    public class CharacterUIManager : MonoBehaviour
    {
        public static CharacterUIManager Instance;
        public void Awake()
        {
            Instance = this;
        }

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
        [SerializeField, Tooltip("敌人")] public Transform enemy;
        [SerializeField, Tooltip("敌人血条")] private Slider enemyHealthSlider;
        [SerializeField, Tooltip("敌人血量Text")] private Text enemyHealthText;
        [SerializeField, Tooltip("敌人护盾UI")] private ShieldUIObject enemyShieldUI;
        [Header("意图相关")]
        [SerializeField, Tooltip("父物体")] private Transform intentionParent;
        [SerializeField, Tooltip("生成模板")] private GameObject intentionTemplate;
        [SerializeField] private List<IntentionUIObjectEffect> intentionList;
        
        [Header("玩家相关")]
        [SerializeField, Tooltip("玩家")] public Transform player;
        [SerializeField, Tooltip("玩家血条")] private Slider playerHealthSlider;
        [SerializeField, Tooltip("敌人血量Text")] private Text playerHealthText;
        [SerializeField, Tooltip("玩家护盾UI")] private ShieldUIObject playerShieldUI;

        private Vector3 offsetPosition;

        #region 攻击与受击
        /// <summary>
        /// 创建伤害文本
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="hit"></param>
        /// <param name="position"></param>
        private void CreateAttackText(Transform parent,int hit,Vector3 position)
        {
            var tmp = Instantiate(attackTextTemplate, parent, true);
            var randomOffset = Random.Range(-10, 20);
            position = new Vector3(position.x + randomOffset, position.y, position.z);
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
            switch (character)
            {
                case Character.Enemy:
                    StartCoroutine(DoEnemyBeAtkAnim(hitNum));
                    break;
                case Character.Player:
                    PlayBeAttackedAnim(player, hitNum);
                    break;
            }
        }
        IEnumerator DoEnemyBeAtkAnim(int hitNum)
        {
            yield return new WaitForSeconds(RollingResultUIManager.Instance.useTime * 0.75f);
            PlayBeAttackedAnim(enemy, hitNum);
        }
        private void PlayBeAttackedAnim(Transform character,int hitNum)
        {
            Vector3 attackTextPosition = attackTextOffset;
            character.DOPunchPosition(punchAmplitude, durationTime, punchTime);
            attackTextPosition += character.position;
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
                    if (AudioManager.Instance != null && playerShieldUI.curShield <= 0)
                    {
                        AudioManager.Instance.PlaySound("attack");
                    }
                    break;
                case Character.Player:
                    StartCoroutine(DoPlayerAtkAnim());
                    break;
            }
        }
        IEnumerator DoPlayerAtkAnim()
        {
            yield return new WaitForSeconds(RollingResultUIManager.Instance.useTime * 0.75f);
            offsetPosition = (enemy.position - player.position).normalized * attackAmplitude;
            player.DOPunchPosition(offsetPosition, attackTime, 1);
            if (AudioManager.Instance != null && enemyShieldUI.curShield <= 0)
            {
                AudioManager.Instance.PlaySound("attack");
            }
        }
        #endregion
        
        /// <summary>
        /// 角色使用其他骰面的动画
        /// </summary>
        public void UseOtherDice(Character character)
        {
            Debug.LogWarning("角色动画");
            switch (character)
            {
                case Character.Enemy:
                    enemy.DOPunchPosition(useOtherOffset, attackTime, 1);
                    break;
                case Character.Player:
                    StartCoroutine(DoPlayerOtherAnim());
                    break;
            }
        }
        IEnumerator DoPlayerOtherAnim()
        {
            yield return new WaitForSeconds(RollingResultUIManager.Instance.useTime * 0.75f);
            player.DOPunchPosition(useOtherOffset, attackTime, 1);
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
                    StartCoroutine(DoChangeEnemyHp(currentHealth, maxHealth));
                    break;
                case Character.Player:
                    playerHealthSlider.value = (float)currentHealth / maxHealth;
                    playerHealthText.text = $"{currentHealth}/{maxHealth}";
                    break;
            }
        }
        IEnumerator DoChangeEnemyHp(int currentHealth, int maxHealth)
        {
            yield return new WaitForSeconds(RollingResultUIManager.Instance.useTime * 0.75f);
            enemyHealthSlider.value = (float)currentHealth / maxHealth;
            enemyHealthText.text = $"{currentHealth}/{maxHealth}";
        }
        
        /// <summary>
        /// 创建治疗文本
        /// </summary>
        /// <param name="character"></param>
        /// <param name="num"></param>
        public void CreateCureText(Character character, int num)
        {
            Vector3 attackTextPosition = attackTextOffset;
            switch (character)
            {
                case Character.Enemy:
                    attackTextPosition += enemy.position;
                    DoCure(attackTextPosition, num);
                    break;
                case Character.Player:
                    attackTextPosition += player.position;
                    StartCoroutine(DoPlayerCureAnim(attackTextPosition, num));
                    break;
            }
        }
        private void DoCure(Vector3 attackTextPosition,int num)
        {
            var tmp = Instantiate(attackTextTemplate, attackTextParent, true);
            tmp.transform.position = attackTextPosition;//更改位置
            tmp.GetComponent<AttackText>().InitCure(num, attackTime);//初始化
            tmp.SetActive(true);
        }
        IEnumerator DoPlayerCureAnim(Vector3 attackTextPosition, int num)
        {
            yield return new WaitForSeconds(RollingResultUIManager.Instance.useTime * 0.75f);
            DoCure(attackTextPosition, num);
        }

        #region 护盾
        private void CreateShieldText(Transform parent,int hit,Vector3 position)
        {
            if (hit == 0)
            {
                return;
            }
            var tmp = Instantiate(attackTextTemplate, parent, true);
            var randomOffset = Random.Range(-10, 20);
            position = new Vector3(position.x + randomOffset, position.y, position.z);
            tmp.transform.position = position;//更改位置
            tmp.GetComponent<AttackText>().InitShield(hit,attackTime);//初始化
            tmp.SetActive(true);
        }
        public void CreateShieldText(Character character,int shieldNum)
        {
            Vector3 attackTextPosition = attackTextOffset;
            switch (character)
            {
                case Character.Enemy:
                    attackTextPosition += enemy.position;
                    CreateShieldText(attackTextParent, shieldNum, attackTextPosition);//创建护盾文字
                    break;
                case Character.Player:
                    attackTextPosition += player.position;
                    StartCoroutine(LateShieldAnim(shieldNum, attackTextPosition));
                    break;
            }
        }
        IEnumerator LateShieldAnim(int shieldNum,Vector3 position)
        {
            yield return new WaitForSeconds(RollingResultUIManager.Instance.useTime * 0.75f);
            CreateShieldText(attackTextParent, shieldNum, position);//创建护盾文字
        }
        
        /// <summary>
        /// 更新角色护盾UI
        /// </summary>
        /// <param name="character"></param>
        /// <param name="shieldNum"></param>
        public void UpdateShieldUI(Character character,int shieldNum)
        {
            switch (character)
            {
                case Character.Enemy:
                    enemyShieldUI.UpdateShieldNum(shieldNum);
                    break;
                case Character.Player:
                    StartCoroutine(DoPlayerShieldAnim(shieldNum));
                    break;
            }
        }
        IEnumerator DoPlayerShieldAnim(int shieldNum)
        {
            yield return new WaitForSeconds(RollingResultUIManager.Instance.useTime * 0.75f);
            playerShieldUI.UpdateShieldNum(shieldNum);
        }
        #endregion

        #region 意图
        /// <summary>
        /// 创建意图
        /// </summary>
        /// <param name="dice"></param>
        public void CreateIntentionUIObject(SingleDiceObj dice)
        {
            var tmp = Instantiate(intentionTemplate, intentionParent, true);
            var tmpIntention = tmp.GetComponent<IntentionUIObjectEffect>();
            tmpIntention.Init(dice);//初始化
            tmp.SetActive(true);
            intentionList.Add(tmpIntention);
        }
        
        /// <summary>
        /// 移除所有意图UI
        /// </summary>
        public void RemoveAllIntentionUIObject()
        {
            if (intentionList == null) return;
            if (intentionList.Count <= 0) { return;}
            foreach (var intention in intentionList)
            {
                intention.DoDestroy();
            }
            intentionList.Clear();
        }
        #endregion

    }
}

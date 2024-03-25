using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 存储buff的各个回调点
/// </summary>
namespace DesignerScripts
{
    /// <summary>
    /// 
    /// </summary>
    public enum BuffEventName
    {

        Bleed,//流血

        Spirit,//精力

        Vulnerable,//易伤

        Tough,//坚韧

        Weak,//虚弱

        Strength,//力量

        Enhance,//强化

        BuffStackMinus1,//Buff层数减1

        #region 圣物buff
        Add2ValueIfResultIsEven,
        Add2ValueIfResultIsOdd,
        Add3ValueIfResultBelow3,
        Add3ValueIfResultAbove4,
        GainAndChoose2DicesGiveRandomCoating,
        GainAndChoose2DicesGive1PermanentEnhance,
        EnhanceEnemyVulnerability,
        Add1StackIfEnemyHaveBleed,
        Add1StackIfEnemyHaveDebuff,
        Add1StackIfPlayerHaveStrength,
        Add1StackIfPlayerHavePositiveBuff,
        Add4MoneyWhenBattleEnd,
        Add50PercentAttackEvery3TimesLoseHealth,
        Add90PercentAttackEvery9TimesUseDice,
        Recover20HealthWhenEnterStore,
        Get5MaxHealthWhenGain,
        Recover25HealthWhenHealthBelowHalf,
        Add1Reroll,
        HalfInStore,
        ReuseDiceWhenDiceIs1,
        Add2MoneyWhenDiceIs2,
        Recover5HealthWhenDiceIs3,
        Add1EnemyBleedStackWhenDiceIs4,
        Add1PlayerStrengthStackWhenDiceIs5,
        Add1PermanentValueWhenDiceIs6,

        #endregion
    }
    public class BuffEvents
    {
        public static Dictionary<string, OnBuffCreate> onCreateFunc = new Dictionary<string, OnBuffCreate>()
        {


        };
        public static Dictionary<string, OnBuffRemove> onRemoveFunc = new Dictionary<string, OnBuffRemove>();
        public static Dictionary<string, OnRoundStart> onRoundStartFunc = new Dictionary<string, OnRoundStart>()
        {
            {
                BuffEventName.BuffStackMinus1.ToString(),BuffStackMinus1
            },
        };
        public static Dictionary<string, OnRoundEnd> onRoundEndFunc = new Dictionary<string, OnRoundEnd>()
        {
            {
                BuffEventName.Bleed.ToString(),Bleed
            },
        }
            ;
        public static Dictionary<string, BuffOnHit> onBuffHitFunc = new Dictionary<string, BuffOnHit>()
        {
            {
                BuffEventName.Weak.ToString(),Weak
            },
            {
                BuffEventName.Strength.ToString(),Strength
            },
            {
                BuffEventName.Enhance.ToString(),Enhance
            }

        };
        public static Dictionary<string, BuffOnBeHurt> onBeHurtFunc = new Dictionary<string, BuffOnBeHurt>()
        {
            {
                BuffEventName.Vulnerable.ToString(),Vulnerable
            },
            {
                BuffEventName.Tough.ToString(),Tough
            },
        };
        public static Dictionary<string, BuffOnkill> onKillFunc = new Dictionary<string, BuffOnkill>();
        public static Dictionary<string, BuffOnBeKilled> onBeKillFunc = new Dictionary<string, BuffOnBeKilled>();

        public static Dictionary<string, BuffOnRoll> onRollFunc = new Dictionary<string, BuffOnRoll>();



        //流血效果，每回合造成2*层数的伤害（数值脚填）
        public static void Bleed(BuffInfo buffInfo)
        {
            int bleedDamage = 2 * buffInfo.curStack;
            //这边还没决定好，是直接扣血，还是调用伤害函数，因为可能在damageManager里会有接视觉表现，例如跳数字的效果，但是如果直接在这边扣血，那就需要在这边也调用视觉表现
            buffInfo.target.GetComponent<ChaState>().ModResources(new ChaResource(-bleedDamage, 0, 0, 0));
            Debug.Log("流血造成" + bleedDamage + "伤害");
        }

        public static void Vulnerable(BuffInfo buffInfo, DamageInfo damageInfo, GameObject attack)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                damageInfo.addDamageArea += 0.25f;
            }

        }


        public static void Tough(BuffInfo buffInfo, DamageInfo damageInfo, GameObject attack)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                damageInfo.addDamageArea -= 0.25f;
            }

        }

        public static void Weak(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {

                damageInfo.addDamageArea -= 0.25f;
            }
        }

        public static void Strength(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {

                damageInfo.addDamageArea += 0.25f;
            }
        }

        public static void Enhance(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                damageInfo.addDamageArea += 0.5f;

            }
        }
        //这边不需要了，因为在BuffInfo里面已经有了
        public static void BuffStackMinus1(BuffInfo buffInfo)
        {
            buffInfo.target.GetComponent<ChaState>().RemoveBuff(buffInfo);
        }

        public static void Add2ValueIfResultIsEven(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate % 2 == 0)
            {
                damageInfo.damage.indexDamageRate += 2;
            }

        }
        public static void Add2ValueIfResultIsOdd(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate % 2 == 1)
            {
                damageInfo.damage.indexDamageRate += 2;
            }
        }

        public static void Add3ValueIfResultBelow3(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate <= 3)
            {
                damageInfo.damage.indexDamageRate += 3;
            }
        }

        public static void Add3ValueIfResultAbove4(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate >= 4)
            {
                damageInfo.damage.indexDamageRate += 3;
            }
        }





        public static void EnhanceEnemyVulnerability(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (target.GetComponent<BuffHandler>() != null)
            {
                BuffHandler targetBuffHandler = target.GetComponent<BuffHandler>();
                //查询流血buff
                BuffInfo findBuffInfo = targetBuffHandler.buffList.Find(x => x.buffData.id == "1");

                //如果找到流血buff
                if (findBuffInfo != null)
                {
                    //再增伤0.15f即0.25->0.4
                    damageInfo.addDamageArea += 0.15f;
                }

            }
        }

        public static void Add50PercentAttackEvery3TimesLoseHealth(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            //在buffinfo的额外参数字典中存储了玩家受到伤害的次数
            //每次OnBeHurt回调点触发时，将次数+1
            //如果次数是3的倍数，增加0.5f的攻击力
            if (buffInfo.buffParam.ContainsKey("PlayerLoseHealthCount"))
            {
                int attackCount = (int)buffInfo.buffParam["PlayerLoseHealthCount"];
                attackCount++;
                if (attackCount % 3 == 0)
                {
                    damageInfo.addDamageArea += 0.5f;
                }
                buffInfo.buffParam["PlayerLoseHealthCount"] = attackCount;
            }
            else
            {
                buffInfo.buffParam.Add("PlayerLoseHealthCount", 0);
            }
        }


        public static void Add90PercentAttackEvery9TimesUseDice(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (buffInfo.buffParam.ContainsKey("PlayerUseDiceCount"))
            {
                int attackCount = (int)buffInfo.buffParam["PlayerUseDiceCount"];
                attackCount++;
                if (attackCount % 9 == 0)
                {
                    damageInfo.addDamageArea += 0.5f;
                }
                buffInfo.buffParam["PlayerUseDiceCount"] = attackCount;
            }
            else
            {
                buffInfo.buffParam.Add("PlayerUseDiceCount", 0);
            }
        }

        public static void Get5MaxHealthWhenGain(BuffInfo buffInfo)
        {
            
            buffInfo.buffData.propMod = new ChaProperty[]
            {
                //加算
                new ChaProperty(5,0,0,0),
                //乘算 Property乘法重载加过1了
                new ChaProperty(0,0,0,0)
            };

        }

        public static void Add1Reroll(BuffInfo buffInfo)
        {
            //RerollCount在BattleManager中
            BattleManager.Instance.parameter.playerRerollCount++;
        }

        //暂定不加回调点了，判断回合数是否是1判断游戏开始
        public static void Recover25HealthWhenHealthBelowHalf(BuffInfo buffInfo)
        {
            if (BattleManager.Instance.parameter.turns == 1)
            {
                //获取玩家的状态
                ChaState tempChaState = buffInfo.creator.GetComponent<ChaState>();
                //访问当前的资源
                if (tempChaState.resource.currentHp > 0)
                {
                    tempChaState.ModResources(new ChaResource(25, 0, 0, 0));
                   
                }
            }
        }

        public static void ReuseDiceWhenDiceIs1(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if(damageInfo.damage.indexDamageRate == 1)
            {
                //TODO:Reuse
            }
        }

        public static void Add2MoneyWhenDiceIs2(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate == 2)
            {
                ChaState tempChaState = buffInfo.creator.GetComponent<ChaState>();
                //访问当前的资源
                if (tempChaState.resource.currentMoney > 0)
                {
                    tempChaState.ModResources(new ChaResource(0, 4, 0, 0));

                }
            }
        }







    }



}

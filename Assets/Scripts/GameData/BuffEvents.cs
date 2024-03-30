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

        Dodge,//闪避

        EnergyStorage,//蓄能

        Anger,//怒气

        LoseEnergy,//失能

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
        Add1PermanentValueWhenDiceIs6

        #endregion
    }
    public class BuffEvents
    {
        #region 存回调点函数的字典
        public static Dictionary<string, OnBuffCreate> onCreateFunc = new Dictionary<string, OnBuffCreate>()
        {
            {
                BuffEventName.Get5MaxHealthWhenGain.ToString(),Get5MaxHealthWhenGain
            },
            {
                BuffEventName.Add1Reroll.ToString(),Add1Reroll
            }

        };
        public static Dictionary<string, OnBuffRemove> onRemoveFunc = new Dictionary<string, OnBuffRemove>();
        public static Dictionary<string, OnRoundStart> onRoundStartFunc = new Dictionary<string, OnRoundStart>()
        {
            {
                BuffEventName.BuffStackMinus1.ToString(),BuffStackMinus1
            },
            {
                BuffEventName.EnergyStorage.ToString(),EnergyStorage
            },

            {
                BuffEventName.Recover25HealthWhenHealthBelowHalf.ToString(),Recover25HealthWhenHealthBelowHalf
            }
        };
        public static Dictionary<string, OnRoundEnd> onRoundEndFunc = new Dictionary<string, OnRoundEnd>()
        {
            {
                BuffEventName.Bleed.ToString(),Bleed
            },
            {
                BuffEventName.Spirit.ToString(),Spirit
            },
            {
                BuffEventName.BuffStackMinus1.ToString(),BuffStackMinus1
            }
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
            },
            {
                BuffEventName.Dodge.ToString(),Dodge
            },
            {
                BuffEventName.Anger.ToString(),Anger
            },
            {
                BuffEventName.Add2ValueIfResultIsEven.ToString(),Add2ValueIfResultIsEven
            },
            {
                BuffEventName.Add2ValueIfResultIsOdd.ToString(),Add2ValueIfResultIsOdd
            },
            {
                BuffEventName.Add3ValueIfResultBelow3.ToString(),Add3ValueIfResultBelow3
            },
            {
                BuffEventName.Add3ValueIfResultAbove4.ToString(),Add3ValueIfResultAbove4
            },
            {
                BuffEventName.EnhanceEnemyVulnerability.ToString(),EnhanceEnemyVulnerability
            },

            {
                BuffEventName.Add90PercentAttackEvery9TimesUseDice.ToString(),Add90PercentAttackEvery9TimesUseDice
            },

            {
                BuffEventName.ReuseDiceWhenDiceIs1.ToString(),ReuseDiceWhenDiceIs1
            },
            {
                BuffEventName.Add2MoneyWhenDiceIs2.ToString(),Add2MoneyWhenDiceIs2
            },
            {
                BuffEventName.Recover5HealthWhenDiceIs3.ToString(),Recover5HealthWhenDiceIs3
            },
            {
                BuffEventName.Add1EnemyBleedStackWhenDiceIs4.ToString(),Add1EnemyBleedStackWhenDiceIs4
            },
            {
                BuffEventName.Add1PlayerStrengthStackWhenDiceIs5.ToString(),Add1PlayerStrengthStackWhenDiceIs5
            },
            {
                BuffEventName.Add1PermanentValueWhenDiceIs6.ToString(),Add1PermanentValueWhenDiceIs6
            },




        };
        public static Dictionary<string, BuffOnBeHurt> onBeHurtFunc = new Dictionary<string, BuffOnBeHurt>()
        {
            {
                BuffEventName.Vulnerable.ToString(),Vulnerable
            },
            {
                BuffEventName.Tough.ToString(),Tough
            },

            {
                BuffEventName.Add50PercentAttackEvery3TimesLoseHealth.ToString(),Add50PercentAttackEvery3TimesLoseHealth
            },
        };
        //Player胜利回调点
        public static Dictionary<string, BuffOnkill> onKillFunc = new Dictionary<string, BuffOnkill>()
        {
            {
                BuffEventName.Add4MoneyWhenBattleEnd.ToString(),Add4MoneyWhenBattleEnd
            }

        };
        public static Dictionary<string, BuffOnBeKilled> onBeKillFunc = new Dictionary<string, BuffOnBeKilled>();

        public static Dictionary<string, BuffOnRoll> onRollFunc = new Dictionary<string, BuffOnRoll>();

        public static Dictionary<string, BuffOnCast> onCastFunc = new Dictionary<string, BuffOnCast>()
        {
            {
                BuffEventName.LoseEnergy.ToString(),LoseEnergy
            }
        };


        #endregion

        #region 具体buff效果函数

        //流血效果，每回合造成2*层数的伤害（数值脚填）
        public static void Bleed(BuffInfo buffInfo)
        {
            int bleedDamage = 2 * buffInfo.curStack;
            //这边还没决定好，是直接扣血，还是调用伤害函数，因为可能在damageManager里会有接视觉表现，例如跳数字的效果，但是如果直接在这边扣血，那就需要在这边也调用视觉表现
            buffInfo.target.GetComponent<ChaState>().ModResources(new ChaResource(-bleedDamage, 0, 0, 0));
            Debug.Log("流血造成" + bleedDamage + "伤害");
            buffInfo.target.GetComponent<ChaState>().RemoveBuff(buffInfo);
            Debug.Log("回合结束层数-1");

        }

        public static void Spirit(BuffInfo buffInfo)
        {
            int health = buffInfo.curStack * 2;
            buffInfo.target.GetComponent<ChaState>().ModResources(new ChaResource(health, 0, 0, 0));
            Debug.Log("精力回复" + health + "生命");
            buffInfo.target.GetComponent<ChaState>().RemoveBuff(buffInfo);
            Debug.Log("回合结束层数-1");
        }

        public static void Vulnerable(BuffInfo buffInfo, DamageInfo damageInfo, GameObject attack)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                damageInfo.addDamageArea += 0.25f;
                Debug.Log("易伤效果生效");

            }


        }


        public static void Tough(BuffInfo buffInfo, DamageInfo damageInfo, GameObject attack)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                damageInfo.addDamageArea -= 0.25f;
                Debug.Log("坚韧效果生效");
            }


        }

        public static void Weak(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                damageInfo.addDamageArea -= 0.25f;
                Debug.Log("虚弱效果生效");
            }

        }

        public static void Strength(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                damageInfo.addDamageArea += 0.25f;
                Debug.Log("力量效果生效");
            }

        }

        public static void Enhance(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                damageInfo.addDamageArea += 0.5f;
                Debug.Log("强化效果生效，增加50%伤害");
                //触发后-1层
                buffInfo.target.GetComponent<ChaState>().RemoveBuff(buffInfo);
                Debug.Log("buff层数-1");
            }

        }

        public static void Dodge(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            damageInfo.damage.baseDamage = 0;
            Debug.Log("闪避生效，伤害为0");
            //触发后-1层
            buffInfo.target.GetComponent<ChaState>().RemoveBuff(buffInfo);
            Debug.Log("buff层数-1");
        }


        //这里根据buffhandler的逻辑应该是判断是否为1，是1就变为4层
        public static void EnergyStorage(BuffInfo buffInfo)
        {
            //回合开始时-1层
            buffInfo.target.GetComponent<ChaState>().RemoveBuff(buffInfo);
            Debug.Log("buff层数-1");
            if (buffInfo.curStack == 1)
            {
                buffInfo.curStack += 3;
                //获得一层怒气
                BuffInfo newAngerBuff = new BuffInfo(BuffDataTable.buffData[BuffDataName.Anger.ToString()], buffInfo.creator, buffInfo.target);
                buffInfo.target.GetComponent<ChaState>().AddBuff(newAngerBuff,buffInfo.target);
                Debug.Log("蓄能生效，增加3层并获得怒气");
            }
        }

        public static void Anger(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            damageInfo.addDamageArea += buffInfo.curStack * 5 * 0.1f;
            Debug.Log("怒气生效，增加" + buffInfo.curStack * 5 * 0.1f + "伤害");
        }

        public static SingleDiceObj LoseEnergy(BuffInfo buffInfo, SingleDiceObj singleDiceObj)
        {
            singleDiceObj.model.damage.baseDamage = 0;
            singleDiceObj.model.damage.indexDamageRate = 0;
            singleDiceObj.model.buffInfos = null;
            Debug.Log("失能生效，伤害为0");
            //触发后-1层
            buffInfo.target.GetComponent<ChaState>().RemoveBuff(buffInfo);
            Debug.Log("buff层数-1");
            return singleDiceObj;

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

        public static void Add4MoneyWhenBattleEnd(BuffInfo buffInfo, DamageInfo damageInfo, GameObject attacker)
        {
            //creator就是玩家
            ChaState tempChaState = buffInfo.creator.GetComponent<ChaState>();
            //访问当前的资源
            if (tempChaState.resource.currentMoney > 0)
            {
                tempChaState.ModResources(new ChaResource(0, 4, 0, 0));

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
            if (damageInfo.damage.indexDamageRate == 1)
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

        public static void Recover5HealthWhenDiceIs3(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate == 3)
            {
                ChaState tempChaState = buffInfo.creator.GetComponent<ChaState>();
                //访问当前的资源
                if (tempChaState.resource.currentHp > 0)
                {
                    tempChaState.ModResources(new ChaResource(5, 0, 0, 0));

                }
            }
        }

        public static void Add1EnemyBleedStackWhenDiceIs4(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate == 4)
            {
                if (target.GetComponent<BuffHandler>() != null)
                {
                    BuffHandler targetBuffHandler = target.GetComponent<BuffHandler>();
                    //查询流血buff
                    BuffInfo findBuffInfo = targetBuffHandler.buffList.Find(x => x.buffData.id == "1");

                    //如果找到流血buff
                    if (findBuffInfo != null)
                    {
                        //增加层数(引用传递）
                        findBuffInfo.curStack++;
                    }

                }
            }
        }

        public static void Add1PlayerStrengthStackWhenDiceIs5(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate == 5)
            {
                if (target.GetComponent<BuffHandler>() != null)
                {
                    BuffHandler targetBuffHandler = target.GetComponent<BuffHandler>();
                    //查询力量buff
                    BuffInfo findBuffInfo = targetBuffHandler.buffList.Find(x => x.buffData.id == "6");

                    //如果找到力量buff
                    if (findBuffInfo != null)
                    {
                        //增加层数(引用传递）
                        findBuffInfo.curStack++;
                    }

                }
            }
        }

        public static void Add1PermanentValueWhenDiceIs6(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate == 6)
            {
                //TODO
            }
        }




        #endregion


    }



}

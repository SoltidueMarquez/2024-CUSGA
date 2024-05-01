using Map;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using UI;
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
        #region 普通buff

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

        LoseEnergy,//失能已废弃

        Stun,//失能

        BuffStackMinus1,//Buff层数减1

        Thorns,//荆棘

        Reflect,//反射

        Split,//分裂

        Pox,//水痘

        Spike,//尖刺

        Corrosion,//腐蚀

        Sensitive,//敏感

        Brave,//勇敢

        #endregion

        #region 圣物buff
        Add2ValueIfResultIsEven,//1
        Add2ValueIfResultIsOdd,//2
        Add3ValueIfResultBelow3,//5
        Add3ValueIfResultAbove4,//6
        GainAndChoose2DicesGiveRandomCoating,
        GainAndChoose2DicesGive1PermanentEnhance,

        Add1StackIfEnemyHaveBleed,//8
        CheckForBleed,//8
        Add1StackIfEnemyHaveDebuff,//2-05
        Add1StackIfPlayerHaveStrength,//9
        CheckForStrength,//9
        EnhancePlayerStrength,//10
        EnhanceEnemyVulnerability,//11
        Add1StackIfPlayerHavePositiveBuff,//2-06
        Add4MoneyWhenBattleEnd,//12
        GainHalfMoney,//13
        HalfInStore,//14
        HalfInStoreOnDestroy,//14
        GainOverflowMoney,//15
        Add50PercentAttackEvery3TimesLoseHealth,//16
        Add50PercentAttack,//16

        Add90PercentAttackEvery9TimesUseDice,
        Recover20HealthWhenEnterStore,//17
        Recover20HealthWhenEnterStoreOnDestroy,//17
        Get5MaxHealthWhenGain,//18
        RecoverHalfHealthWhenGain,//19
        Recover25HealthWhenHealthBelowHalf,//20
        Add20ValueWhenHit15Times,//21
        Add1RerollAfterReroll,//22
        Add1Reroll,//2-13

        ReuseDiceWhenDiceIs1,//23
        RefreashReuseDiceWhenDiceIs1,
        Add2MoneyWhenDiceIs2,//24
        Recover5HealthWhenDiceIs3,//25
        Add1EnemyBleedStackWhenDiceIs4,//26
        Add1PlayerStrengthStackWhenDiceIs5,//27
        Add1PermanentValueWhenDiceIs6,//28
        Hit5AfterDodge,//29
        Gain1DodgeWhenBattleStart,//30
        Gain1EnhanceWhenBattleStart,//31
        Gain2StrengthWhenBattleStart,//32
        Gain2ToughWhenBattleStart,//33
        Gain2VulnerableWhenBattleStart,//34
        Gain2WeakWhenBattleStart,//35

        Add4ValueIfResultIsEven,//2-1
        Add4ValueIfResultIsOdd,//2-2

        #endregion

        #region 骰子用buff
        GetHurt,//受击
        RecoverHealth,//回血
        GainMoney,//拿钱
        EnemyBleed,//敌人流血
        EnemyVulnerable,//敌人易伤
        EnemyWeak,//敌人虚弱
        PlayerStrength,//玩家力量
        PlayerEnhance,//玩家强化
        PlayerDodge,//玩家闪避
        PlayerSpirit,//玩家精力
        ClearEnemyPositiveBuff,//清除敌人正面buff
        ClearPlayerNegativeBuff,//清除玩家负面buff

        RerollDice,

        DoubleHit,
        TripleHit,

        Imitate,
        #endregion

        #region 稀有圣物
        GainMoneyAfterBattle,//战斗后获得金币
        Choose2DiceUpdateWhenGain,
        Gain2NormalHalidomWhenGain,
        Hit3DamageWhenLoseHealth,
        GainDodgeWhenLoseHealth,
        GainStrengthWhenLoseHealth,
        RecoverHalfHealthWhenDie,
        Add1ValueWhenDiceBelow3,
        #endregion

        #region 传说圣物
        Add6ValueIfResultIsEven,
        Add6ValueIfResultIsOdd,
        Add1ValueToAddOdd,
        Add1ValueToAddEven,

        EnhanceAttackAfterSellDiceOnCreate,
        EnhanceAttackAfterSellDice,

        Enhance25AttackWhenHalfHealth,

        EnhanceAttackWhenHitOnRoundStart,
        EnhanceAttackWhenHit,

        EnhanceAttackBaseOnMoney,
        EnhanceAttackAndHurt,
        Gain2DiceAndHurt,
        Update2DiceAndHurt,
        Gain2RareHalidom,
        EnhanceAttackAfterKillEnemy,
        #endregion
    }

    public class BuffEvents
    {
        #region 存回调点函数的字典
        public static Dictionary<string, OnBuffCreate> onCreateFunc = new Dictionary<string, OnBuffCreate>()
        {
            //普通buff
            {
                BuffEventName.CheckForBleed.ToString(),CheckForBleed
            },
            {
                BuffEventName.CheckForStrength.ToString(),CheckForStrength
            },
            //圣物buff
            {
                BuffEventName.Get5MaxHealthWhenGain.ToString(),Get5MaxHealthWhenGain
            },

            {
                BuffEventName.GainHalfMoney.ToString(),GainHalfMoney
            },
            {
                BuffEventName.RecoverHalfHealthWhenGain.ToSafeString(),RecoverHalfHealthWhenGain
            },
            {
                BuffEventName.Recover20HealthWhenEnterStore.ToString(),Recover20HealthWhenEnterStore
            },
            {
                BuffEventName.HalfInStore.ToString(),HalfInStore
            },
            //稀有圣物buff
            {
                BuffEventName.Gain2NormalHalidomWhenGain.ToString(),Gain2NormalHalidomWhenGain
            },
            {
                BuffEventName.Add1Reroll.ToString(),Add1Reroll
            },
            //传说圣物buff
            {
                BuffEventName.Gain2RareHalidom.ToString(),Gain2RareHalidom
            },
            {
                BuffEventName.EnhanceAttackAfterSellDiceOnCreate.ToString(),EnhanceAttackAfterSellDiceOnCreate
            },
            //骰子buff
            {
                BuffEventName.GetHurt.ToString(),GetHurt
            },
            {
                BuffEventName.RecoverHealth.ToString(),RecoverHealth
            },
            {
                BuffEventName.GainMoney.ToString(),GainMoney
            },
            {
                BuffEventName.EnemyBleed.ToString(),EnemyBleed
            },
            {
                BuffEventName.EnemyVulnerable.ToString(),EnemyVulnerable
            },
            {
                BuffEventName.EnemyWeak.ToString(),EnemyWeak
            },
            {
                BuffEventName.PlayerStrength.ToString(),PlayerStrength
            },
            {
                BuffEventName.PlayerEnhance.ToString(),PlayerEnhance
            },
            {
                BuffEventName.PlayerDodge.ToString(),PlayerDodge
            },
            {
                BuffEventName.PlayerSpirit.ToString(),PlayerSpirit
            },



        };
        public static Dictionary<string, OnBuffRemove> onRemoveFunc = new Dictionary<string, OnBuffRemove>()
        {
            {
                BuffEventName.Recover20HealthWhenEnterStoreOnDestroy.ToString(),Recover20HealthWhenEnterStoreOnDestroy
            },
            {
                BuffEventName.HalfInStoreOnDestroy.ToString(),HalfInStoreOnDestroy
            },
        };
        public static Dictionary<string, OnRoundStart> onRoundStartFunc = new Dictionary<string, OnRoundStart>()
        {
            //普通buff
            {
                BuffEventName.BuffStackMinus1.ToString(),BuffStackMinus1
            },
            {
                BuffEventName.EnergyStorage.ToString(),EnergyStorage
            },
            {
                BuffEventName.Thorns.ToString(),Thorns
            },
            {
                BuffEventName.Split.ToString(),Split
            },
            //圣物buff
            {
                BuffEventName.Recover25HealthWhenHealthBelowHalf.ToString(),Recover25HealthWhenHealthBelowHalf
            },
            {
                BuffEventName.Gain1DodgeWhenBattleStart.ToString(),Gain1DodgeWhenBattleStart
            },
            {
                BuffEventName.Gain1EnhanceWhenBattleStart.ToString(),Gain1EnhanceWhenBattleStart
            },
            {
                BuffEventName.Gain2StrengthWhenBattleStart.ToString(),Gain2StrengthWhenBattleStart
            },
            {
                BuffEventName.Gain2ToughWhenBattleStart.ToString(),Gain2ToughWhenBattleStart
            },
            {
                BuffEventName.Gain2VulnerableWhenBattleStart.ToString(),Gain2VulnerableWhenBattleStart
            },
            {
                BuffEventName.Gain2WeakWhenBattleStart.ToString(),Gain2WeakWhenBattleStart
            },
            //传说圣物
            {
                BuffEventName.EnhanceAttackWhenHitOnRoundStart.ToString(),EnhanceAttackWhenHitOnRoundStart
            },
        };
        public static Dictionary<string, OnRoundEnd> onRoundEndFunc = new Dictionary<string, OnRoundEnd>()
        {
            //普通buff
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
            //普通buff
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
                BuffEventName.Anger.ToString(),Anger
            },
            //圣物buff
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
                BuffEventName.Add1StackIfEnemyHaveBleed.ToString(),Add1StackIfEnemyHaveBleed
            },
            {
                BuffEventName.EnhancePlayerStrength.ToString(),EnhancePlayerStrength
            },
            {
                BuffEventName.Add50PercentAttack.ToString(),Add50PercentAttack
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
            {
                BuffEventName.Add20ValueWhenHit15Times.ToString(),Add20ValueWhenHit15Times
            },
            //稀有圣物buff
            {
                BuffEventName.Add4ValueIfResultIsEven.ToString(),Add4ValueIfResultIsEven
            },
            {
                BuffEventName.Add4ValueIfResultIsOdd.ToString(),Add4ValueIfResultIsOdd
            },
            {
                BuffEventName.Add1ValueWhenDiceBelow3.ToString(),Add1ValueWhenDiceBelow3
            },
            //传说圣物buff
            {
                BuffEventName.Add6ValueIfResultIsEven.ToString(),Add6ValueIfResultIsEven
            },
            {
                BuffEventName.Add6ValueIfResultIsOdd.ToString(),Add6ValueIfResultIsOdd
            },
            {
                BuffEventName.Enhance25AttackWhenHalfHealth.ToString(),Enhance25AttackWhenHalfHealth
            },
            {
                BuffEventName.EnhanceAttackBaseOnMoney.ToString(),EnhanceAttackBaseOnMoney
            },
            {
                BuffEventName.EnhanceAttackAndHurt.ToString(),EnhanceAttackAndHurt
            },
            {
                BuffEventName.EnhanceAttackWhenHit.ToString(),EnhanceAttackWhenHit
            },
            {
                BuffEventName.EnhanceAttackAfterSellDice.ToString(),EnhanceAttackAfterSellDice
            },
            //骰子buff




        };
        public static Dictionary<string, BuffOnBeHurt> onBeHurtFunc = new Dictionary<string, BuffOnBeHurt>()
        {
            //普通buff
            {
                BuffEventName.Vulnerable.ToString(),Vulnerable
            },
            {
                BuffEventName.Tough.ToString(),Tough
            },
            {
                BuffEventName.Dodge.ToString(),Dodge
            },
            /*{
                BuffEventName.Reflect.ToString(),Reflect
            },*/
            {
                BuffEventName.Pox.ToString(),Pox
            },
            {
                BuffEventName.Spike.ToString(),Spike
            },
            {
                BuffEventName.Corrosion.ToString(),Corrosion
            },
            //圣物buff
            {
                BuffEventName.Add50PercentAttackEvery3TimesLoseHealth.ToString(),Add50PercentAttackEvery3TimesLoseHealth
            },
            {
                BuffEventName.EnhanceEnemyVulnerability.ToString(),EnhanceEnemyVulnerability
            },
            {
                BuffEventName.Hit5AfterDodge.ToString(),Hit5AfterDodge
            },
            //稀有圣物buff
            {
                BuffEventName.Hit3DamageWhenLoseHealth.ToString(),Hit3DamageWhenLoseHealth
            },
            {
                BuffEventName.GainDodgeWhenLoseHealth.ToString(),GainDodgeWhenLoseHealth
            },
            {
                BuffEventName.GainStrengthWhenLoseHealth.ToString(),GainStrengthWhenLoseHealth
            },
        };
        //Player胜利回调点
        public static Dictionary<string, BuffOnkill> onKillFunc = new Dictionary<string, BuffOnkill>()
        {
            //圣物buff
            {
                BuffEventName.Add4MoneyWhenBattleEnd.ToString(),Add4MoneyWhenBattleEnd
            },
            {
                BuffEventName.GainOverflowMoney.ToSafeString(),GainOverflowMoney
            },
            //稀有圣物buff
            {
                BuffEventName.GainMoneyAfterBattle.ToString(),GainMoneyAfterBattle
            },

        };
        public static Dictionary<string, BuffOnBeKilled> onBeKillFunc = new Dictionary<string, BuffOnBeKilled>()
        {
            
            //稀有圣物buff
            {
                BuffEventName.RecoverHalfHealthWhenDie.ToString(),RecoverHalfHealthWhenDie
            },
        };

        public static Dictionary<string, BuffOnRoll> onRollFunc = new Dictionary<string, BuffOnRoll>()
        {
            //buff
            {
                BuffEventName.Sensitive.ToString(),Sensitive
            },
            {
                BuffEventName.Brave.ToString(),Brave
            },
            //普通圣物
            {
                BuffEventName.Add1RerollAfterReroll.ToString(),Add1RerollAfterReroll
            }


        };

        public static Dictionary<string, BuffOnCast> onCastFunc = new Dictionary<string, BuffOnCast>()
        {
            //普通buff
            
            

            
            //圣物buff
            {
                BuffEventName.Add1StackIfPlayerHaveStrength.ToString(),Add1StackIfPlayerHaveStrength
            },
            //骰子
            {
                BuffEventName.Imitate.ToString(),Imitate
            },
            {
                BuffEventName.ClearEnemyPositiveBuff.ToString(),ClearEnemyPositiveBuff
            },
            {
                BuffEventName.ClearPlayerNegativeBuff.ToString(),ClearPlayerNegativeBuff
            },
            {
                BuffEventName.RerollDice.ToString(),RerollDice
            },
            {
                BuffEventName.DoubleHit.ToString(),DoubleHit
            },
            {
                BuffEventName.TripleHit.ToString(),TripleHit
            },
            {
                BuffEventName.Stun.ToString(),Stun
            }
        };
        public static Dictionary<string, OnAddBuff> onAddFunc = new Dictionary<string, OnAddBuff>()
        {
            //稀有圣物buff
            {
                BuffEventName.Add1StackIfEnemyHaveDebuff.ToString(),Add1StackIfEnemyHaveDebuff
            },
            {
                BuffEventName.Add1StackIfPlayerHavePositiveBuff.ToString(),Add1StackIfPlayerHavePositiveBuff
            }
        };
        public static Dictionary<string, OnGetFinalDamage> onGetFinalDamageFunc = new Dictionary<string, OnGetFinalDamage>
        {
            {
                BuffEventName.Reflect.ToString(),Reflect
            },

            {
                BuffEventName.LoseEnergy.ToString(),LoseEnergy
            },
            {
                BuffEventName.RefreashReuseDiceWhenDiceIs1.ToString(),RefreashReuseDiceWhenDiceIs1
            }

        };




        #endregion

        #region 所有buff效果函数

        #region 普通buff效果函数


        #region 玩家敌人公用buff
        //流血效果，每回合造成2*层数的伤害（数值脚填）
        public static void Bleed(BuffInfo buffInfo)
        {
            int bleedDamage = 2 * buffInfo.curStack;
            //这边还没决定好，是直接扣血，还是调用伤害函数，因为可能在damageManager里会有接视觉表现，例如跳数字的效果，但是如果直接在这边扣血，那就需要在这边也调用视觉表现
            buffInfo.target.GetComponent<ChaState>().ModResources(new ChaResource(-bleedDamage, 0, 0, 0));
            Debug.Log("流血造成" + bleedDamage + "伤害");

        }


        public static void CheckForBleed(BuffInfo buffInfo)
        {
            if (HalidomManager.Instance.halidomList != null)
            {
                for (int i = 0; i < HalidomManager.Instance.halidomList.Length; i++)
                {
                    if (HalidomManager.Instance.halidomList[i] != null)
                    {
                        if (HalidomManager.Instance.halidomList[i].id == "1_08")
                        {
                            if (buffInfo.target == BattleManager.Instance.parameter.enemyChaStates[0].gameObject)
                            {
                                buffInfo.curStack++;
                                Debug.Log("因为手术刀，敌人流血层数+1");
                            }
                        }
                    }
                }
            }
        }

        public static void Spirit(BuffInfo buffInfo)
        {
            int health = buffInfo.curStack ;
            buffInfo.target.GetComponent<ChaState>().ModResources(new ChaResource(health, 0, 0, 0));
            Debug.Log("精力回复" + health + "生命");

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

        public static void CheckForStrength(BuffInfo buffInfo)
        {
            if (HalidomManager.Instance.halidomList != null)
            {
                for (int i = 0; i < HalidomManager.Instance.halidomList.Length; i++)
                {
                    if (HalidomManager.Instance.halidomList[i] != null)
                    {
                        //检测圣物有没有蛋白粉
                        if (HalidomManager.Instance.halidomList[i].id == "1_09")
                        {
                            //检测buff施加的对象是否是玩家
                            if (buffInfo.target == BattleManager.Instance.parameter.playerChaState.gameObject)
                            {
                                //如果对象是敌人，且圣物管理器有手术刀，流血层数+1
                                buffInfo.curStack++;
                                Debug.Log("因为蛋白粉，我方力量层数+1");
                            }

                        }
                    }


                }
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
                buffInfo.curStack--;
                Debug.Log("buff层数-1");
                if (buffInfo.curStack == 0)
                {
                    buffInfo.isPermanent = false;
                    int index = buffInfo.target.GetComponent<ChaState>().GetBuffHandler().buffList.IndexOf(buffInfo);
                    var characterSide = (Character)buffInfo.target.GetComponent<ChaState>().side;
                    BuffUIManager.Instance.RemoveBuffUIObject(characterSide, index);
                }
            }

        }

        public static void Dodge(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            damageInfo.damage.baseDamage = 0;
            Debug.Log("闪避生效，伤害为0");
            //触发后-1层
            buffInfo.curStack--;
            Debug.Log("buff层数-1");
            if (buffInfo.curStack == 0)
            {
                buffInfo.isPermanent = false;
                int index = buffInfo.target.GetComponent<ChaState>().GetBuffHandler().buffList.IndexOf(buffInfo);
                var characterSide = (Character)buffInfo.target.GetComponent<ChaState>().side;
                BuffUIManager.Instance.RemoveBuffUIObject(characterSide, index);
            }
        }


        //这里根据buffhandler的逻辑应该是判断是否为1，是1就变为4层
        public static void EnergyStorage(BuffInfo buffInfo)
        {
            if(buffInfo.curStack == 1)
            {
                
                buffInfo.curStack += 2;
                var charac2 = (Character)buffInfo.target.GetComponent<ChaState>().side;
                int index2 = buffInfo.target.GetComponent<ChaState>().GetBuffHandler().buffList.IndexOf(buffInfo);
                BuffUIManager.Instance.UpdateBuffDurationTime(charac2, index2, buffInfo.curStack);
                BuffInfo newAngerBuff = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Anger.ToString()], buffInfo.creator, buffInfo.target, 1, true);
                buffInfo.target.GetComponent<ChaState>().AddBuff(newAngerBuff, buffInfo.target);
                Debug.Log("蓄能生效，增加3层并获得怒气");
            }
            else
            {
                buffInfo.curStack--;
                Debug.Log("buff层数-1");
                //刷新ui
                var charac1 = (Character)buffInfo.target.GetComponent<ChaState>().side;
                int index1 = buffInfo.target.GetComponent<ChaState>().GetBuffHandler().buffList.IndexOf(buffInfo);
                BuffUIManager.Instance.UpdateBuffDurationTime(charac1, index1, buffInfo.curStack);
            }
            

            //回合开始时-1层
            /*buffInfo.target.GetComponent<ChaState>().RemoveBuff(buffInfo);
            Debug.Log("buff层数-1");
            if (buffInfo.curStack == 1)
            {
                buffInfo.curStack += 3;
                BuffUIManager.Instance.UpdateBuffDurationTime((Character)buffInfo.target.GetComponent<ChaState>().side, buffInfo.target.GetComponent<ChaState>().GetBuffHandler().buffList.IndexOf(buffInfo), buffInfo.curStack);
                //获得一层怒气
                BuffInfo newAngerBuff = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Anger.ToString()], buffInfo.creator, buffInfo.target,1,true);
                buffInfo.target.GetComponent<ChaState>().AddBuff(newAngerBuff, buffInfo.target);
                Debug.Log("蓄能生效，增加3层并获得怒气");
            }*/
        }

        public static void Anger(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                damageInfo.addDamageArea += buffInfo.curStack * 5 * 0.1f;
                Debug.Log("怒气生效，增加" + buffInfo.curStack * 5 * 0.1f + "伤害");
                //刷新ui
                var charac1 = (Character)buffInfo.target.GetComponent<ChaState>().side;
                int index1 = buffInfo.target.GetComponent<ChaState>().GetBuffHandler().buffList.IndexOf(buffInfo);
                BuffUIManager.Instance.UpdateBuffDurationTime(charac1, index1, buffInfo.curStack);
            }
            
        }

        public static void LoseEnergy(BuffInfo buffInfo, DamageInfo damageInfo)
        {
            damageInfo.finalDamage = 0;
            damageInfo.addBuffs = new List<BuffInfo>();

            Debug.Log("失能生效，伤害为0");
            //触发后-1层
            buffInfo.curStack--;
            Debug.Log("buff层数-1");
            if (buffInfo.curStack == 0)
            {
                buffInfo.isPermanent = false;
                int index = buffInfo.target.GetComponent<ChaState>().GetBuffHandler().buffList.IndexOf(buffInfo);
                var characterSide = (Character)buffInfo.target.GetComponent<ChaState>().side;
                BuffUIManager.Instance.RemoveBuffUIObject(characterSide, index);
            }


        }

        //已废弃
        public static SingleDiceObj Stun(BuffInfo buffInfo, SingleDiceObj singleDiceObj)//Tag：Target onbehurt
        {


            if (buffInfo.creator == BattleManager.Instance.parameter.playerChaState.gameObject)
            {
                BuffInfo loseEnergy = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.LoseEnergy.ToString()],
                     BattleManager.Instance.parameter.playerChaState.gameObject,
                    BattleManager.Instance.parameter.enemyChaStates[0].gameObject);
                BattleManager.Instance.parameter.enemyChaStates[0].GetComponent<ChaState>().AddBuff(loseEnergy, BattleManager.Instance.parameter.enemyChaStates[0].gameObject);
            }
            else
            {
                BuffInfo loseEnergynew = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.LoseEnergy.ToString()],
                     BattleManager.Instance.parameter.enemyChaStates[0].gameObject,
                    BattleManager.Instance.parameter.playerChaState.gameObject);
                BattleManager.Instance.parameter.playerChaState.GetComponent<ChaState>().AddBuff(loseEnergynew, BattleManager.Instance.parameter.playerChaState.gameObject);
            }



            buffInfo.curStack = 0;
            buffInfo.isPermanent = false;
            return singleDiceObj;

        }

        //这边不需要了，因为在BuffInfo里面已经有了
        public static void BuffStackMinus1(BuffInfo buffInfo)
        {
            buffInfo.target.GetComponent<ChaState>().RemoveBuff(buffInfo);
        }
        #endregion

        #region 新增敌方buff 4.20


        public static void Thorns(BuffInfo buffInfo)//OnRoundStart调用
        {
            //添加的反伤需要一开始为true
            BuffInfo newReflectBuff = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Reflect.ToString()], buffInfo.creator, buffInfo.target, 1, true);
            buffInfo.target.GetComponent<ChaState>().AddBuff(newReflectBuff, buffInfo.target);
            Debug.Log("荆棘生效，持有者获得反射");
        }


        //这里应该是一个获取最终伤害后的回调点
        public static void Reflect(BuffInfo buffInfo, DamageInfo damageInfo)//onfinaldamage调用
        {
            if (damageInfo.diceType == DiceType.Attack && damageInfo.defender == buffInfo.creator)
            {
                //计算出超出护盾伤害
                int damage = Mathf.Abs(damageInfo.finalDamage - damageInfo.attacker.GetComponent<ChaState>().resource.currentShield);
                damageInfo.attacker.GetComponent<ChaState>().ModResources(new ChaResource(-damage, 0, 0, 0));
                Debug.Log("反射造成" + damage + "伤害");
                buffInfo.curStack--;
                if (buffInfo.curStack == 0)
                {
                    Debug.Log("反射当前层数为" + buffInfo.curStack);
                    buffInfo.isPermanent = false;
                    int index = buffInfo.target.GetComponent<ChaState>().GetBuffHandler().buffList.IndexOf(buffInfo);
                    var characterSide = (Character)buffInfo.target.GetComponent<ChaState>().side;
                    BuffUIManager.Instance.RemoveBuffUIObject(characterSide, index);
                    //buffInfo.target.GetComponent<ChaState>().GetBuffHandler().RemoveBuff(buffInfo);
                }

            }


        }

        public static void Split(BuffInfo buffInfo)//OnRoundStart调用
        {
            //添加的水痘需要一开始为true
            BuffInfo newPoxBuff = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Pox.ToString()], buffInfo.creator, buffInfo.target, 1, true);
            buffInfo.target.GetComponent<ChaState>().AddBuff(newPoxBuff, buffInfo.target);
            Debug.Log("分裂生效,持有者获得水痘");
        }

        public static void Pox(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)//OnHurt调用
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                damageInfo.damage.baseDamage *= 2;

                buffInfo.curStack--;
                if (buffInfo.curStack == 0)
                {
                    buffInfo.isPermanent = false;
                    int index = buffInfo.target.GetComponent<ChaState>().GetBuffHandler().buffList.IndexOf(buffInfo);
                    var characterSide = (Character)buffInfo.target.GetComponent<ChaState>().side;
                    BuffUIManager.Instance.RemoveBuffUIObject(characterSide, index);
                }
                Debug.Log("因为水痘，收到伤害翻倍");
            }

        }

        public static void Spike(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)//OnHurt调用
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                //获取攻击方的状态
                ChaState tempChaState = damageInfo.attacker.GetComponent<ChaState>();
                if (tempChaState.resource.currentShield == 0)
                {
                    int damage = (int)buffInfo.buffParam["Value"];
                    tempChaState.ModResources(new ChaResource(-damage, 0, 0, 0));
                    Debug.Log("尖刺生效，攻击方受到" + damage + "伤害");
                }
            }

        }

        public static void Corrosion(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)//OnHurt调用
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                //获取攻击方的状态
                ChaState tempChaState = damageInfo.attacker.GetComponent<ChaState>();
                if (tempChaState.resource.currentShield > 0)
                {
                    int shieldDamage = (int)buffInfo.buffParam["Value"];
                    tempChaState.ModResources(new ChaResource(0, 0, 0, -shieldDamage));
                    Debug.Log("腐蚀生效，攻击方减去" + shieldDamage + "层护盾");
                }
            }

        }

        public static void Sensitive(BuffInfo buffInfo)//onRoll调用
        {
            BuffInfo newVulnerableBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Vulnerable.ToString()], buffInfo.creator, buffInfo.target, (int)buffInfo.buffParam["Value"], false);

            buffInfo.target.GetComponent<ChaState>().AddBuff(newVulnerableBuff1, buffInfo.target);

            Debug.Log("由于敏感获得易伤");
        }

        public static void Brave(BuffInfo buffInfo)
        {
            BuffInfo newToughBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Tough.ToString()], buffInfo.creator, buffInfo.target, (int)buffInfo.buffParam["Value"], false);

            buffInfo.target.GetComponent<ChaState>().AddBuff(newToughBuff1, buffInfo.target);

            Debug.Log("由于勇敢获得坚韧");
        }

        #endregion

        #endregion

        #region 圣物buff效果函数
        public static void Add2ValueIfResultIsEven(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate % 2 == 0)
            {
                damageInfo.damage.baseDamage += 2;
                Debug.Log("骰子为偶数，增加2点伤害");
            }

        }
        public static void Add2ValueIfResultIsOdd(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate % 2 == 1)
            {
                damageInfo.damage.baseDamage += 2;
                Debug.Log("骰子为奇数，增加2点伤害");
            }
        }

        public static void Add3ValueIfResultBelow3(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate <= 3)
            {
                damageInfo.damage.baseDamage += 3;
                Debug.Log("骰子小于等于3，增加3点伤害");
            }
        }

        public static void Add3ValueIfResultAbove4(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate >= 4)
            {
                damageInfo.damage.baseDamage += 3;
                Debug.Log("骰子大于等于4，增加3点伤害");
            }
        }

        public static void Add1StackIfEnemyHaveBleed(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            //4.1 放到Bleed的OnCreate中了
            //找damageinfo中的addbuffs有没有流血
            /*if (damageInfo.addBuffs != null)
            {
                BuffInfo findBuffInfo = damageInfo.addBuffs.Find(x => x.buffData.id == "1_1");
                if (findBuffInfo != null)
                {
                    findBuffInfo.curStack++;
                    Debug.Log("敌人流血层数+1");
                }
            }*/
        }

        public static SingleDiceObj Add1StackIfPlayerHaveStrength(BuffInfo buffInfo, SingleDiceObj singleDiceObj)
        {
            //4.1 放到Strength的OnCreate中了
            /*if (singleDiceObj.model.buffInfos != null)
            {
                foreach(var  diceBuffInfo in singleDiceObj.model.buffInfos)
                {
                    //查询到力量buff
                    if(diceBuffInfo.buffData.id == "1_6")
                    {
                        diceBuffInfo.curStack++;
                        Debug.Log("玩家力量层数+1");
                    }
                }
            }*/
            return singleDiceObj;
        }

        public static void EnhancePlayerStrength(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (buffInfo.creator.GetComponent<BuffHandler>() != null)
            {
                BuffHandler targetBuffHandler = buffInfo.creator.GetComponent<BuffHandler>();
                //查询力量buff
                BuffInfo findBuffInfo = targetBuffHandler.buffList.Find(x => x.buffData.id == "1_6");

                //如果找到力量buff
                if (findBuffInfo != null)
                {
                    //再增伤0.15f即0.25->0.4
                    damageInfo.addDamageArea += 0.15f;
                    Debug.Log("增加玩家力量效果");
                }
            }
        }
        public static void EnhanceEnemyVulnerability(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            Debug.Log("进入EnhanceEnemyVulnerability");
            if (buffInfo.target.GetComponent<BuffHandler>() != null)
            {
                BuffHandler targetBuffHandler = buffInfo.target.GetComponent<BuffHandler>();
                //查询流血buff
                BuffInfo findBuffInfo = targetBuffHandler.buffList.Find(x => x.buffData.id == "1_3");

                //如果找到流血buff
                if (findBuffInfo != null)
                {
                    //再增伤0.15f即0.25->0.4
                    damageInfo.addDamageArea += 0.15f;
                    Debug.Log("增加敌人易伤效果");
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
                Debug.Log("增加4金币");
            }
        }

        public static void GainHalfMoney(BuffInfo buffInfo)
        {
            ChaState tempChaState = buffInfo.creator.GetComponent<ChaState>();
            if (tempChaState.resource.currentMoney > 0)
            {
                //增加一半金币
                tempChaState.ModResources(new ChaResource(0, tempChaState.resource.currentMoney / 2, 0, 0));
                Debug.Log("增加一半金币");
            }
        }

        public static void HalfInStore(BuffInfo buffInfo)
        {
            StoreManager.Instance.ChangeDiscount(0.5f);
        }
        public static void HalfInStoreOnDestroy(BuffInfo buffInfo)
        {
            StoreManager.Instance.ChangeDiscount(1f);
        }

        public static void GainOverflowMoney(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.finalDamage > damageInfo.defender.GetComponent<ChaState>().resource.currentHp)
            {
                int overflowMoney = damageInfo.finalDamage - damageInfo.defender.GetComponent<ChaState>().resource.currentHp;
                buffInfo.creator.GetComponent<ChaState>().ModResources(new ChaResource(0, overflowMoney, 0, 0));
                Debug.Log("获得" + overflowMoney + "金币");
                DataUIManager.Instance.UpdateHealthText(buffInfo.creator.GetComponent<ChaState>().resource.currentHp, buffInfo.creator.GetComponent<ChaState>().prop.health);
            }
        }



        public static void Add50PercentAttackEvery3TimesLoseHealth(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            Debug.Log("进入了Add50PercentAttackEvery3TimesLoseHealth会丢爱哦的");
            //在buffinfo的额外参数字典中存储了玩家受到伤害的次数
            //每次OnBeHurt回调点触发时，将次数+1
            //如果次数是3的倍数，增加0.5f的攻击力

            //现在受到伤害只检查是不是配置的时候有这个键值对，有则++
            if (buffInfo.buffParam.ContainsKey("PlayerLoseHealthCount"))
            {

                int attackCount = (int)buffInfo.buffParam["PlayerLoseHealthCount"];
                attackCount++;
                /*if (attackCount % 3 == 0)
                {
                    damageInfo.addDamageArea += 0.5f;
                }*/
                buffInfo.buffParam["PlayerLoseHealthCount"] = attackCount;
                Debug.Log("受到伤害次数" + attackCount);
            }
            /*else
            {
                buffInfo.buffParam.Add("PlayerLoseHealthCount", 0);
                Debug.Log("Test:--------------设置初始收到伤害次数为0");
            }*/
        }

        public static void Add50PercentAttack(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (buffInfo.buffParam.ContainsKey("PlayerLoseHealthCount"))
            {

                int attackCount = (int)buffInfo.buffParam["PlayerLoseHealthCount"];

                if (attackCount % 3 == 0)
                {
                    damageInfo.addDamageArea += 0.5f;
                    Debug.Log("增加50%攻击力");
                }


            }
        }

        public static void Recover20HealthWhenEnterStore(BuffInfo buffInfo)
        {
            StoreManager.Instance.OnEnterStore.AddListener(Recover20Health);
        }

        public static void Recover20HealthWhenEnterStoreOnDestroy(BuffInfo buffInfo)
        {
            StoreManager.Instance.OnEnterStore.RemoveListener(Recover20Health);
        }

        public static void Recover20Health()
        {
            MapManager.Instance.playerChaState.GetComponent<ChaState>().ModResources(new ChaResource(20, 0, 0, 0));
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
                Debug.Log("使用骰子次数" + attackCount);
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
            Debug.Log("获得5点最大生命值");

        }

        public static void RecoverHalfHealthWhenGain(BuffInfo buffInfo)
        {
            if (SceneLoader.Instance.currentGameScene == GameScene.BattleScene)
            {
                //获取玩家的状态
                ChaState tempChaState = buffInfo.creator.GetComponent<ChaState>();
                //访问当前的资源
                if (tempChaState.resource.currentHp > 0)
                {
                    tempChaState.ModResources(new ChaResource(tempChaState.baseProp.health / 2, 0, 0, 0));
                    Debug.Log("获得时回复一半生命");
                }
            }
            else if (SceneLoader.Instance.currentGameScene == GameScene.MapScene)
            {
                MapManager.Instance.playerChaState.GetComponent<ChaState>().ModResources(new ChaResource(MapManager.Instance.playerChaState.GetComponent<ChaState>().baseProp.health / 2, 0, 0, 0));
                MapManager.Instance.UpdatePlayerUI();
                Debug.Log("获得时回复一半生命在地图");
            }



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
                    Debug.Log("回合1回复25点生命");

                }
            }
        }

        public static void Add20ValueWhenHit15Times(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (buffInfo.buffParam.ContainsKey("PlayerHitCount"))
            {
                int hitCount = (int)buffInfo.buffParam["PlayerHitCount"];
                hitCount++;
                if (hitCount % 15==0)
                {
                    damageInfo.damage.baseDamage += 20;
                }
                buffInfo.buffParam["PlayerHitCount"] = hitCount;
                //刷新描述
                HalidomManager.Instance.UpdateHalidomDescription();
                Debug.Log("攻击次数" + hitCount);
            }
            else
            {
                buffInfo.buffParam.Add("PlayerHitCount", 0);
            }
        }

        public static void Add1RerollAfterReroll(BuffInfo buffInfo)
        {
            int count = (int)buffInfo.buffParam["Value"];
            if (count <= 2)
            {
                buffInfo.creator.GetComponent<ChaState>().resource.currentRollTimes++;
                Debug.Log("不消耗重投次数");
                count++;
                buffInfo.buffParam["Value"] = count;
            }
            else
            {
                Debug.Log("三指失效");
            }

        }

        public static void ReuseDiceWhenDiceIs1(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            
            //深拷贝一条damageinfo信息加入伤害队列
            if (damageInfo.damage.indexDamageRate == 1 && !(bool)buffInfo.buffParam["IsFirstDeal"])
            {
                DamageInfo damageInfoCopy = new DamageInfo(damageInfo.attacker, damageInfo.defender, damageInfo.damage, damageInfo.diceType, damageInfo.level, damageInfo.addBuffs);
                DamageManager.Instance.DoDamage(damageInfoCopy);

                Debug.Log("重复打出");
                buffInfo.buffParam["IsFirstDeal"] = true;
            }
            else
            {
                buffInfo.buffParam["IsFirstDeal"] = false;
            }

        }

        public static void RefreashReuseDiceWhenDiceIs1(BuffInfo buffInfo, DamageInfo damageInfo)
        {
            buffInfo.buffParam["IsFirstDeal"] = false;
            Debug.Log("--------------------------------------------------------");
            Debug.Log(buffInfo.buffParam["IsFirstDeal"]);
            Debug.Log("--------------------------------------------------------");
        }

        public static void Add2MoneyWhenDiceIs2(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate == 2)
            {
                ChaState tempChaState = buffInfo.creator.GetComponent<ChaState>();
                //访问当前的资源
                if (tempChaState.resource.currentMoney > 0)
                {
                    tempChaState.ModResources(new ChaResource(0, 2, 0, 0));
                    Debug.Log("增加2金币");

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
                    Debug.Log("回复5点生命");

                }
            }
        }

        public static void Add1EnemyBleedStackWhenDiceIs4(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate == 4)
            {
                /*if (buffInfo.target.GetComponent<BuffHandler>() != null)
                {
                    BuffHandler targetBuffHandler = buffInfo.target.GetComponent<BuffHandler>();
                    //查询流血buff
                    BuffInfo findBuffInfo = targetBuffHandler.buffList.Find(x => x.buffData.id == "1_01");

                    //如果找到流血buff
                    if (findBuffInfo != null)
                    {
                        //增加层数(引用传递）
                        findBuffInfo.curStack++;
                        Debug.Log("敌人流血层数+1");
                    }

                }*/

                BuffInfo newBleedBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Bleed.ToString()], buffInfo.creator, buffInfo.target, 1);
                buffInfo.target.GetComponent<ChaState>().AddBuff(newBleedBuff1, buffInfo.target);
                Debug.Log("敌方流血+1");
                BuffInfo findBuffInfo = buffInfo.target.GetComponent<BuffHandler>().buffList.Find(x => x.buffData.id == "1_01");
                Debug.Log(findBuffInfo.buffData.buffName);
            }
        }

        public static void Add1PlayerStrengthStackWhenDiceIs5(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate == 5)
            {
                if (buffInfo.creator.GetComponent<BuffHandler>() != null)
                {
                    /*BuffHandler targetBuffHandler = buffInfo.creator.GetComponent<BuffHandler>();
                    //查询力量buff
                    BuffInfo findBuffInfo = targetBuffHandler.buffList.Find(x => x.buffData.id == "1_06");

                    //如果找到力量buff
                    if (findBuffInfo != null)
                    {
                        //增加层数(引用传递）
                        findBuffInfo.curStack++;
                        Debug.Log("玩家力量层数+1");
                    }*/

                    BuffInfo newStrengthBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Strength.ToString()], buffInfo.creator, buffInfo.target, 1);
                    buffInfo.creator.GetComponent<ChaState>().AddBuff(newStrengthBuff1, buffInfo.creator);
                    Debug.Log("玩家力量+1");
                    BuffInfo findBuffInfo = buffInfo.creator.GetComponent<BuffHandler>().buffList.Find(x => x.buffData.id == "1_06");
                    Debug.Log(findBuffInfo.buffData.buffName);


                }
            }
        }

        public static void Add1PermanentValueWhenDiceIs6(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate == 6)
            {
                damageInfo.damage.baseDamage += 6;
                Debug.Log("骰子为6，增加6点伤害");
            }
        }



        public static void Hit5AfterDodge(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            BuffInfo findBuffInfo = buffInfo.creator.GetComponent<BuffHandler>().buffList.Find(x => x.buffData.id == "1_08");
            if (findBuffInfo != null)
            {
                damageInfo.attacker.GetComponent<ChaState>().ModResources(new ChaResource(-5, 0, 0, 0));
                Debug.Log("恶毒嘲笑生效 敌方-5hp");
            }
        }


        public static void Gain1DodgeWhenBattleStart(BuffInfo buffInfo)
        {
            if (BattleManager.Instance.parameter.turns == 1)
            {
                BuffInfo newDodgeBuff = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Dodge.ToString()], buffInfo.creator, buffInfo.target, 1, true);
                //给对面添加伤害为0的buff
                buffInfo.creator.GetComponent<ChaState>().AddBuff(newDodgeBuff, buffInfo.creator);
                Debug.Log("战斗开始获得1层闪避");
            }
        }

        public static void Gain1EnhanceWhenBattleStart(BuffInfo buffInfo)
        {
            if (BattleManager.Instance.parameter.turns == 1)
            {
                BuffInfo newEnhanceBuff = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Enhance.ToString()], buffInfo.creator, buffInfo.target);
                buffInfo.creator.GetComponent<ChaState>().AddBuff(newEnhanceBuff, buffInfo.creator);
                Debug.Log("战斗开始获得1层强化");
            }
        }

        public static void Gain2StrengthWhenBattleStart(BuffInfo buffInfo)
        {
            if (BattleManager.Instance.parameter.turns == 1)
            {
                BuffInfo newStrengthBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Strength.ToString()], buffInfo.creator, buffInfo.target, 2);
                //BuffInfo newStrengthBuff2 = new BuffInfo(BuffDataTable.buffData[BuffDataName.Strength.ToString()], buffInfo.creator, buffInfo.target);
                buffInfo.creator.GetComponent<ChaState>().AddBuff(newStrengthBuff1, buffInfo.creator);
                //buffInfo.creator.GetComponent<ChaState>().AddBuff(newStrengthBuff2, buffInfo.creator);
                Debug.Log("战斗开始获得2层力量");
            }
        }

        public static void Gain2ToughWhenBattleStart(BuffInfo buffInfo)
        {
            if (BattleManager.Instance.parameter.turns == 1)
            {
                //因为buff会在回合结束时-1层，所以这边要加三次
                BuffInfo newToughBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Tough.ToString()], buffInfo.creator, buffInfo.target, 3);
                //BuffInfo newToughBuff2 = new BuffInfo(BuffDataTable.buffData[BuffDataName.Tough.ToString()], buffInfo.creator, buffInfo.target);
                //BuffInfo newToughBuff3 = new BuffInfo(BuffDataTable.buffData[BuffDataName.Tough.ToString()], buffInfo.creator, buffInfo.target);
                buffInfo.creator.GetComponent<ChaState>().AddBuff(newToughBuff1, buffInfo.creator);
                //buffInfo.creator.GetComponent<ChaState>().AddBuff(newToughBuff2, buffInfo.creator);
                //buffInfo.creator.GetComponent<ChaState>().AddBuff(newToughBuff3, buffInfo.creator);
                Debug.Log("战斗开始获得2层坚韧");
            }
        }

        public static void Gain2VulnerableWhenBattleStart(BuffInfo buffInfo)
        {
            if (BattleManager.Instance.parameter.turns == 1)
            {
                BuffInfo newVulnerableBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Vulnerable.ToString()], buffInfo.creator, buffInfo.target, 2);
                //BuffInfo newVulnerableBuff2 = new BuffInfo(BuffDataTable.buffData[BuffDataName.Vulnerable.ToString()], buffInfo.creator, buffInfo.target);
                buffInfo.target.GetComponent<ChaState>().AddBuff(newVulnerableBuff1, buffInfo.target);
                //buffInfo.target.GetComponent<ChaState>().AddBuff(newVulnerableBuff2, buffInfo.target);
                Debug.Log("战斗开始获得2层易伤");
            }
        }

        public static void Gain2WeakWhenBattleStart(BuffInfo buffInfo)
        {
            if (BattleManager.Instance.parameter.turns == 1)
            {
                BuffInfo newWeakBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Weak.ToString()], buffInfo.creator, buffInfo.target, 2);
                //BuffInfo newWeakBuff2 = new BuffInfo(BuffDataTable.buffData[BuffDataName.Weak.ToString()], buffInfo.creator, buffInfo.target);
                buffInfo.target.GetComponent<ChaState>().AddBuff(newWeakBuff1, buffInfo.target);
                //buffInfo.target.GetComponent<ChaState>().AddBuff(newWeakBuff2, buffInfo.target);
                Debug.Log("战斗开始获得2层虚弱");
            }
        }
        #endregion

        #region 稀有圣物
        public static void Add4ValueIfResultIsEven(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate % 2 == 0)
            {
                damageInfo.damage.baseDamage += 4;
                Debug.Log("骰子为偶数，增加4点伤害");
            }
        }

        public static void Add4ValueIfResultIsOdd(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate % 2 == 1)
            {
                damageInfo.damage.baseDamage += 4;
                Debug.Log("骰子为奇数，增加4点伤害");
            }
        }

        public static void Add1StackIfEnemyHaveDebuff(BuffInfo buffInfo)
        {
            //buff添加的target是否是敌方
            if (buffInfo.target == BattleManager.Instance.parameter.enemyChaStates[0].gameObject)
            {
                //如果这个buff的标签是debuff
                if (buffInfo.buffData.tags.Contains("Negative"))
                {
                    buffInfo.curStack++;
                    Debug.Log("敌方负面buff加一");
                }
            }
        }

        public static void Add1StackIfPlayerHavePositiveBuff(BuffInfo buffInfo)
        {
            //buff添加的target是否是玩家
            if (buffInfo.target == BattleManager.Instance.parameter.playerChaState.gameObject)
            {
                Debug.Log("buff添加的target是玩家");
                //如果这个buff的标签是buff
                if (buffInfo.buffData.tags.Contains("Positive"))
                {
                    buffInfo.curStack++;
                    Debug.Log("榴莲千层生效，我方正面buff加一");
                }
            }
        }

        public static void GainMoneyAfterBattle(BuffInfo buffInfo, DamageInfo damageInfo, GameObject attacker)
        {
            //获取玩家的状态
            ChaState tempChaState = buffInfo.creator.GetComponent<ChaState>();
            //访问当前的资源
            if (tempChaState.resource.currentMoney >= 0)
            {
                if (tempChaState.resource.currentMoney <= 50)
                {
                    int money = tempChaState.resource.currentMoney / 5;
                    tempChaState.ModResources(new ChaResource(0, money, 0, 0));
                    Debug.Log("增加" + money + "金币");
                }
                else
                {
                    tempChaState.ModResources(new ChaResource(0, 10, 0, 0));
                    Debug.Log("增加" + 10 + "金币");
                }
            }
        }

        public static void Gain2NormalHalidomWhenGain(BuffInfo buffInfo)
        {
            if (SceneLoader.Instance.currentGameScene == GameScene.BattleScene)
            {
                HalidomManager.Instance.AddHalidom(RandomManager.GetRandomHalidomObj(RareType.Common));
                HalidomManager.Instance.AddHalidom(RandomManager.GetRandomHalidomObj(RareType.Common));
                Debug.Log("增加两个普通圣物");
            }
            else if (SceneLoader.Instance.currentGameScene == GameScene.MapScene)
            {
                HalidomManager.Instance.AddHalidomInMap(RandomManager.GetRandomHalidomObj(RareType.Common));
                HalidomManager.Instance.AddHalidomInMap(RandomManager.GetRandomHalidomObj(RareType.Common));
                Debug.Log("在地图增加两个普通圣物");
            }

        }


        public static void Hit3DamageWhenLoseHealth(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            //获取地方的状态
            ChaState tempChaState = buffInfo.target.GetComponent<ChaState>();

            //因为buff的回调点不知道最终伤害，所以就暂且直接扣血
            tempChaState.ModResources(new ChaResource(-3, 0, 0, 0));
            Debug.Log("受到伤害时对方受到3点伤害");

        }

        public static void GainDodgeWhenLoseHealth(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            int probability = Random.Range(1, 11);
            if (probability == 1)
            {
                BuffInfo newDodgeBuff = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Dodge.ToString()], buffInfo.creator, buffInfo.target, 1, true);
                buffInfo.creator.GetComponent<ChaState>().AddBuff(newDodgeBuff, buffInfo.creator);
                Debug.Log("战斗开始获得1层闪避");
            }
        }


        public static void GainStrengthWhenLoseHealth(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            int probability = Random.Range(1, 11);
            if (probability == 1)
            {
                BuffInfo newStrengthBuff = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Strength.ToString()], buffInfo.creator, buffInfo.target, 1, true);
                buffInfo.creator.GetComponent<ChaState>().AddBuff(newStrengthBuff, buffInfo.creator);
                Debug.Log("战斗开始获得1层力量");
            }
        }


        public static void Add1Reroll(BuffInfo buffInfo)
        {
            buffInfo.buffData.propMod = new ChaProperty[]
            {
                //加算
                new ChaProperty(0,0,1,0),
                //乘算 Property乘法重载加过1了
                new ChaProperty(0,0,0,0)
            };
            Debug.Log("增加最大重投次數");
        }

        public static void RecoverHalfHealthWhenDie(BuffInfo buffInfo, DamageInfo damageInfo, GameObject attacker)
        {
            if ((bool)buffInfo.buffParam["isUsed"] == false)
            {
                ChaState tempChaState = buffInfo.creator.GetComponent<ChaState>();
                if (tempChaState.resource.currentHp <= 0)
                {
                    tempChaState.ModResources(new ChaResource(tempChaState.baseProp.health / 2, 0, 0, 0));
                    Debug.Log("死亡时回复一半生命");
                    buffInfo.buffParam["isUsed"] = true;
                }
            }

        }

        public static void Add1ValueWhenDiceBelow3(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate <= 3)
            {
                damageInfo.damage.baseDamage += 1;
                Debug.Log("点数是小数(<=3),则打出时点数+1");
            }
        }



        #endregion

        # region 传说圣物
        public static void Add6ValueIfResultIsEven(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate % 2 == 0)
            {
                damageInfo.damage.baseDamage += 6;
                Debug.Log("骰子为偶数，增加6点伤害");
            }
        }

        public static void Add6ValueIfResultIsOdd(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.damage.indexDamageRate % 2 == 1)
            {
                damageInfo.damage.baseDamage += 6;
                Debug.Log("骰子为奇数，增加6点伤害");
            }
        }







        public static void EnhanceAttackAfterSellDiceOnCreate(BuffInfo buffInfo)
        {
            HalidomManager.Instance.OnSellHalidom.AddListener(() => AddValueAfterSellDice());
        }

        public static void AddValueAfterSellDice()
        {
            for (int i = 0; i < BattleManager.Instance.parameter.playerChaState.GetComponent<HalidomManager>().halidomList.Length; i++)
            {
                //如果有杂货铺这个圣物
                if (BattleManager.Instance.parameter.playerChaState.GetComponent<HalidomManager>().halidomList[i].id == "3_05")
                {
                    HalidomObject tempHalidom = BattleManager.Instance.parameter.playerChaState.GetComponent<HalidomManager>().halidomList[i];
                    Debug.Log("有杂货铺这个圣物");
                    foreach (var buffInfo in tempHalidom.buffInfos)
                    {

                        if (buffInfo.buffData.id == "4_05")
                        {
                            Debug.Log("有EnhanceAttackAfterSellDice这个buff");
                            buffInfo.buffParam["Value"] = 1 + (int)buffInfo.buffParam["Value"];
                            Debug.Log("售卖数加一");
                            //刷新描述
                            HalidomManager.Instance.UpdateHalidomDescription();
                        }
                    }

                }
            }
        }
        public static void EnhanceAttackAfterSellDice(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if ((int)buffInfo.buffParam["Value"] >= 30)
            {
                damageInfo.addDamageArea += 0.3f;
                Debug.Log("增加30%伤害");

            }
            else
            {
                damageInfo.addDamageArea += (int)buffInfo.buffParam["Value"] * 0.01f;
                Debug.Log("增加伤害" + (int)buffInfo.buffParam["Value"] + "%");
            }

        }





        public static void Enhance25AttackWhenHalfHealth(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            ChaState tempChaState = buffInfo.creator.GetComponent<ChaState>();
            if (tempChaState.resource.currentHp <= tempChaState.baseProp.health / 2)
            {
                damageInfo.addDamageArea += 0.25f;
                Debug.Log("生命值低于一半，增加25%攻击力");
            }
        }



        public static void EnhanceAttackWhenHitOnRoundStart(BuffInfo buffInfo)
        {
            buffInfo.buffParam["Value"] = 0;
            Debug.Log("清空攻击次数" + buffInfo.buffParam["Value"]);
            //刷新描述
            HalidomManager.Instance.UpdateHalidomDescription();
        }

        public static void EnhanceAttackWhenHit(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if ((int)buffInfo.buffParam["Value"] >= 6)
            {
                damageInfo.addDamageArea += 0.3f;
                Debug.Log("伤害增加了30%");
            }
            else
            {
                damageInfo.addDamageArea += (int)buffInfo.buffParam["Value"] * 0.01f;
                Debug.Log("伤害增加了" + (int)buffInfo.buffParam["Value"] + "%");
                buffInfo.buffParam["Value"] = (int)buffInfo.buffParam["Value"] + 5;
                Debug.Log((int)buffInfo.buffParam["Value"]);
                //刷新描述
                HalidomManager.Instance.UpdateHalidomDescription();

            }

        }




        public static void EnhanceAttackBaseOnMoney(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            ChaState tempChaState = buffInfo.creator.GetComponent<ChaState>();
            if (tempChaState.resource.currentMoney >= 0)
            {
                if (tempChaState.resource.currentMoney <= 150)
                {
                    int addAttack = tempChaState.resource.currentMoney / 5;
                    damageInfo.addDamageArea += addAttack * 0.01f;
                    Debug.Log("增加" + addAttack * 0.01f + "%的攻击力");
                }
                else
                {
                    damageInfo.addDamageArea += 0.3f;
                    Debug.Log("金币增加30%攻击力");
                }
            }
        }

        public static void EnhanceAttackAndHurt(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            damageInfo.addDamageArea += 0.6f;
            int probability = Random.Range(1, 11);
            if (probability == 1)
            {
                ChaState tempChaState = buffInfo.creator.GetComponent<ChaState>();
                tempChaState.ModResources(new ChaResource(-50, 0, 0, 0));
            }
        }

        public static void Gain2RareHalidom(BuffInfo buffInfo)
        {
            if (SceneLoader.Instance.currentGameScene == GameScene.BattleScene)
            {
                HalidomManager.Instance.AddHalidom(RandomManager.GetRandomHalidomObj(RareType.Rare));
                HalidomManager.Instance.AddHalidom(RandomManager.GetRandomHalidomObj(RareType.Rare));
                Debug.Log("增加两个稀有圣物");
            }
            else if (SceneLoader.Instance.currentGameScene == GameScene.MapScene)
            {
                HalidomManager.Instance.AddHalidomInMap(RandomManager.GetRandomHalidomObj(RareType.Rare));
                HalidomManager.Instance.AddHalidomInMap(RandomManager.GetRandomHalidomObj(RareType.Rare));
                Debug.Log("在地图增加两个稀有圣物");
            }
        }

        #endregion



        #region 骰子用buff效果函数


        #region 已废弃
        public static void GetHurt(BuffInfo buffInfo)
        {

            //获取玩家的状态
            ChaState tempChaState = BattleManager.Instance.parameter.playerChaState.GetComponent<ChaState>();
            //访问当前的资源
            if (tempChaState.resource.currentHp > 0)
            {
                int damage = (int)buffInfo.buffParam["GetHurt"];
                tempChaState.ModResources(new ChaResource(-damage, 0, 0, 0));
                Debug.Log("受到" + damage + "伤害");
            }
        }

        public static void RecoverHealth(BuffInfo buffInfo)
        {
            //获取玩家的状态
            ChaState tempChaState = BattleManager.Instance.parameter.playerChaState.GetComponent<ChaState>();
            //访问当前的资源
            if (tempChaState.resource.currentHp > 0)
            {
                int recoverHealth = (int)buffInfo.buffParam["RecoverHealth"];
                tempChaState.ModResources(new ChaResource(recoverHealth, 0, 0, 0));
                Debug.Log("恢复" + recoverHealth + "血量");
            }
        }

        public static void GainMoney(BuffInfo buffInfo)
        {
            //获取玩家的状态
            ChaState tempChaState = BattleManager.Instance.parameter.playerChaState.GetComponent<ChaState>();
            //访问当前的资源
            if (tempChaState.resource.currentMoney >= 0)
            {
                int money = (int)buffInfo.buffParam["GainMoney"];
                tempChaState.ModResources(new ChaResource(0, money, 0, 0));
                Debug.Log("增加" + money + "金币");
            }
        }

        public static void EnemyVulnerable(BuffInfo buffInfo)
        {
            for (int i = 0; i < (int)buffInfo.buffParam["EnemyVulnerable"]; i++)
            {
                BuffInfo newVulnerableBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Vulnerable.ToString()], buffInfo.creator, buffInfo.target);
                buffInfo.target.GetComponent<ChaState>().AddBuff(newVulnerableBuff1, buffInfo.target);
            }
        }

        public static void EnemyWeak(BuffInfo buffInfo)
        {
            for (int i = 0; i < (int)buffInfo.buffParam["EnemyWeak"]; i++)
            {
                BuffInfo newWeakBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Weak.ToString()], buffInfo.creator, buffInfo.target);
                buffInfo.target.GetComponent<ChaState>().AddBuff(newWeakBuff1, buffInfo.target);
            }
        }

        public static void EnemyBleed(BuffInfo buffInfo)
        {
            for (int i = 0; i < (int)buffInfo.buffParam["EnemyBleed"]; i++)
            {
                BuffInfo newBleedBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Bleed.ToString()], buffInfo.creator, buffInfo.target);
                buffInfo.target.GetComponent<ChaState>().AddBuff(newBleedBuff1, buffInfo.target);
            }
        }

        public static void PlayerStrength(BuffInfo buffInfo)
        {
            for (int i = 0; i < (int)buffInfo.buffParam["PlayerStrength"]; i++)
            {
                BuffInfo newStrengthBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Strength.ToString()], buffInfo.creator, buffInfo.target);
                buffInfo.creator.GetComponent<ChaState>().AddBuff(newStrengthBuff1, buffInfo.creator);
            }
        }



        public static void PlayerDodge(BuffInfo buffInfo)
        {
            for (int i = 0; i < (int)buffInfo.buffParam["PlayerDodge"]; i++)
            {
                BuffInfo newDodgeBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Dodge.ToString()], buffInfo.creator, buffInfo.target);
                buffInfo.creator.GetComponent<ChaState>().AddBuff(newDodgeBuff1, buffInfo.creator);
            }
        }



        public static void PlayerEnhance(BuffInfo buffInfo)
        {
            for (int i = 0; i < (int)buffInfo.buffParam["PlayerEnhance"]; i++)
            {
                BuffInfo newEnhanceBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Enhance.ToString()], buffInfo.creator, buffInfo.target);
                buffInfo.creator.GetComponent<ChaState>().AddBuff(newEnhanceBuff1, buffInfo.creator);
            }
        }

        public static void PlayerSpirit(BuffInfo buffInfo)
        {
            for (int i = 0; i < (int)buffInfo.buffParam["PlayerSpirit"]; i++)
            {
                BuffInfo newSpiritBuff1 = new BuffInfo(DataInitManager.Instance.buffDataTable.buffData[BuffDataName.Spirit.ToString()], buffInfo.creator, buffInfo.target);
                buffInfo.creator.GetComponent<ChaState>().AddBuff(newSpiritBuff1, buffInfo.creator);
            }
        }
        #endregion


        public static SingleDiceObj ClearEnemyPositiveBuff(BuffInfo buffInfo, SingleDiceObj singleDiceObj)
        {
            //暴力ifelse判断到底是谁的回合
            if (buffInfo.creator == BattleManager.Instance.parameter.playerChaState.gameObject)
            {
                //获取对方的buffhandler
                if (BattleManager.Instance.parameter.enemyChaStates[0].GetComponent<BuffHandler>() != null)
                {
                    //从buffhandler中找到一个的带有Positive tag的 buff
                    BuffInfo findBuffInfo = BattleManager.Instance.parameter.enemyChaStates[0].GetComponent<BuffHandler>().buffList.Find(x => x.buffData.tags.Contains("Positive"));
                    if (findBuffInfo != null)
                    {
                        //清除buff目前需要手动设置为0 false 并且移除buffUI
                        findBuffInfo.curStack = 0;
                        findBuffInfo.isPermanent = false;
                        int index = BattleManager.Instance.parameter.enemyChaStates[0].GetComponent<ChaState>().GetBuffHandler().buffList.IndexOf(findBuffInfo);
                        var characterSide = (Character)BattleManager.Instance.parameter.enemyChaStates[0].GetComponent<ChaState>().side;
                        BuffUIManager.Instance.RemoveBuffUIObject(characterSide, index);
                        Debug.Log("清除一个敌方positivebuff");
                    }

                }
            }
            else if (buffInfo.creator == BattleManager.Instance.parameter.enemyChaStates[0].gameObject)
            {
                if (BattleManager.Instance.parameter.playerChaState.GetComponent<BuffHandler>() != null)
                {
                    BuffInfo findBuffInfo = BattleManager.Instance.parameter.playerChaState.GetComponent<BuffHandler>().buffList.Find(x => x.buffData.tags.Contains("Positive"));
                    if (findBuffInfo != null)
                    {
                        findBuffInfo.curStack = 0;
                        findBuffInfo.isPermanent = false;
                        int index = BattleManager.Instance.parameter.playerChaState.GetComponent<ChaState>().GetBuffHandler().buffList.IndexOf(findBuffInfo);
                        var characterSide = (Character)BattleManager.Instance.parameter.playerChaState.GetComponent<ChaState>().side;
                        BuffUIManager.Instance.RemoveBuffUIObject(characterSide, index);
                        Debug.Log("清除一个敌方positivebuff");
                    }
                }
            }

            return singleDiceObj;
        }

        public static SingleDiceObj ClearPlayerNegativeBuff(BuffInfo buffInfo, SingleDiceObj singleDiceObj)
        {
            //因为creator一定是自身，所以不涉及到对方的不需要暴力ifelse

            if (buffInfo.creator.GetComponent<BuffHandler>() != null)
            {
                //从buffhandler中找到一个的Negative buff
                BuffInfo findBuffInfo = buffInfo.creator.GetComponent<BuffHandler>().buffList.Find(x => x.buffData.tags.Contains("Negative"));
                if (findBuffInfo != null)
                {
                    findBuffInfo.curStack = 0;
                    findBuffInfo.isPermanent = false;
                    int index = buffInfo.creator.GetComponent<ChaState>().GetBuffHandler().buffList.IndexOf(findBuffInfo);
                    var characterSide = (Character)buffInfo.creator.GetComponent<ChaState>().side;
                    BuffUIManager.Instance.RemoveBuffUIObject(characterSide, index);
                    Debug.Log("清除一个我方负面buff");

                }
            }


            //获取玩家的buffhandler

            return singleDiceObj;
        }

        public static SingleDiceObj RerollDice(BuffInfo buffInfo, SingleDiceObj singleDiceObj)
        {
            BattleManager.Instance.ReRollDiceForPlayer();
            return singleDiceObj;
        }

        public static SingleDiceObj DoubleHit(BuffInfo buffInfo, SingleDiceObj singleDiceObj)
        {
            /*if (!(bool)buffInfo.buffParam["IsFirstDeal"])
            {
                DamageInfo damageInfoCopy = new DamageInfo(damageInfo.attacker, damageInfo.defender, damageInfo.damage, damageInfo.diceType, damageInfo.level, damageInfo.addBuffs);
                DamageManager.Instance.DoDamage(damageInfoCopy);

                Debug.Log("重复打出");
                buffInfo.buffParam["IsFirstDeal"] = true;
            }*/

            if (!(bool)buffInfo.buffParam["IsFirstDeal"])
            {
                //拼接一个新的damageinfo
                Damage damage = singleDiceObj.model.damage;
                damage.indexDamageRate = singleDiceObj.idInDice;
                //拼装buffInfo

                if (buffInfo.creator == BattleManager.Instance.parameter.playerChaState.gameObject)
                {
                    var damageInfo = new DamageInfo(BattleManager.Instance.parameter.playerChaState.gameObject,
                                                    BattleManager.Instance.parameter.enemyChaStates[0].gameObject,
                                                    damage, singleDiceObj.model.type, singleDiceObj.level, null);
                    DamageManager.Instance.DoDamage(damageInfo);

                }
                else
                {
                    var damageInfo = new DamageInfo(BattleManager.Instance.parameter.enemyChaStates[0].gameObject,
                                                    BattleManager.Instance.parameter.playerChaState.gameObject,
                                                    damage, singleDiceObj.model.type, singleDiceObj.level, null);
                    DamageManager.Instance.DoDamage(damageInfo);

                }
                buffInfo.buffParam["IsFirstDeal"] = true;
            }
            return singleDiceObj;
        }

        public static SingleDiceObj TripleHit(BuffInfo buffInfo, SingleDiceObj singleDiceObj)
        {

            if (!(bool)buffInfo.buffParam["IsFirstDeal"])
            {

                /* DamageInfo damageInfoCopy1 = new DamageInfo(damageInfo.attacker, damageInfo.defender, damageInfo.damage, damageInfo.diceType, damageInfo.level, damageInfo.addBuffs);
                 DamageInfo damageInfoCopy2 = new DamageInfo(damageInfo.attacker, damageInfo.defender, damageInfo.damage, damageInfo.diceType, damageInfo.level, damageInfo.addBuffs);
                 DamageManager.Instance.DoDamage(damageInfoCopy1);
                 DamageManager.Instance.DoDamage(damageInfoCopy2);*/
                Damage damage = singleDiceObj.model.damage;
                damage.indexDamageRate = singleDiceObj.idInDice;
                //拼装buffInfo
                if (buffInfo.creator == BattleManager.Instance.parameter.playerChaState.gameObject)
                {

                    var damageInfo = new DamageInfo(BattleManager.Instance.parameter.playerChaState.gameObject,
                                                    BattleManager.Instance.parameter.enemyChaStates[0].gameObject,
                                                    damage, singleDiceObj.model.type, singleDiceObj.level, null);
                    DamageManager.Instance.DoDamage(damageInfo);
                    DamageManager.Instance.DoDamage(damageInfo);
                }
                else
                {

                    var damageInfo = new DamageInfo(BattleManager.Instance.parameter.enemyChaStates[0].gameObject,
                                                    BattleManager.Instance.parameter.playerChaState.gameObject,
                                                    damage, singleDiceObj.model.type, singleDiceObj.level, null);
                    DamageManager.Instance.DoDamage(damageInfo);
                    DamageManager.Instance.DoDamage(damageInfo);
                }



                Debug.Log("重复打出");
                buffInfo.buffParam["IsFirstDeal"] = true;

            }
            return singleDiceObj;
        }

        public static SingleDiceObj Imitate(BuffInfo buffInfo, SingleDiceObj singleDiceObj)
        {
            BattleDiceHandler tempBattleDiceHandler = buffInfo.creator.GetComponent<ChaState>().GetBattleDiceHandler();
            var stack = tempBattleDiceHandler.GetPreviousSingleDicesStack();
            return stack.Pop();
        }


        #endregion

        #endregion


    }



}

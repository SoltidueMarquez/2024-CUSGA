using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DesignerScripts
{
    public enum HalidomName
    {
        
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

    }
    public static class HalidomData
    {
        public static Dictionary<string, HalidomObject> halidomDictionary = new Dictionary<string, HalidomObject>() 
        {
            /*{
                //Halidom模板

                "Halidom_1",new HalidomObject(
                    //Halidom的唯一id
                    "1",
                    //Halidom的名字
                    "Halidom_1",
                    //Halidom的描述
                    "当身上的金钱大于50时，每回合回复4点血量",
                    //Halidom的BuffInfo List
                    new List<BuffInfo>()
                        {
                            new BuffInfo(BuffDataTable.buffData["CheckMoneyAddHealth"],null,null,1,false,null)

                        })
            }*/
            {
             HalidomName.Add2ValueIfResultIsEven.ToString(),new HalidomObject(
             "1",
             HalidomName.Add2ValueIfResultIsEven.ToString(),
             "当骰子点数为偶数时，增加2点伤害",
            new List<BuffInfo>()
                {
                    new BuffInfo(BuffDataTable.buffData["Add2ValueIfResultIsEven"],null,null,1,false,null)
                })
            },
            {
             HalidomName.Add2ValueIfResultIsOdd.ToString(),
            new HalidomObject(
             "2",
            HalidomName.Add2ValueIfResultIsOdd.ToString(),
             "当骰子点数为奇数时，增加2点伤害",
            new List<BuffInfo>()
                {
                    new BuffInfo(BuffDataTable.buffData["Add2ValueIfResultIsOdd"],null,null,1,false,null)
                })
            },
            {
            HalidomName.Add3ValueIfResultBelow3.ToString(),
            new HalidomObject(
            "3",
            HalidomName.Add3ValueIfResultBelow3.ToString(),
            "当骰子点数小于3时，增加3点伤害",
            new List<BuffInfo>()
                {
                    new BuffInfo(BuffDataTable.buffData["Add3ValueIfResultBelow3"],null,null,1,false,null)
                })
            },
            {
            HalidomName.Add3ValueIfResultAbove4.ToString(),
            new HalidomObject(
            "4",
            HalidomName.Add3ValueIfResultAbove4.ToString(),
            "当骰子点数大于4时，增加3点伤害",
            new List<BuffInfo>()
                {   
                    new BuffInfo(BuffDataTable.buffData["Add3ValueIfResultAbove4"],null,null,1,false,null)
                })
            },
            {
            HalidomName.GainAndChoose2DicesGiveRandomCoating.ToString(),
            new HalidomObject(
            "5",
            HalidomName.GainAndChoose2DicesGiveRandomCoating.ToString(),
            "获得并选择2个骰子，给予随机涂层",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["GainAndChoose2DicesGiveRandomCoating"],null,null,1,false,null)
                })
            },
            {
            HalidomName.GainAndChoose2DicesGive1PermanentEnhance.ToString(),
            new HalidomObject(
            "6",
            HalidomName.GainAndChoose2DicesGive1PermanentEnhance.ToString(),
            "获得并选择2个骰子，给予1个永久强化",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["GainAndChoose2DicesGive1PermanentEnhance"],null,null,1,false,null)
                })
            },

            {
            HalidomName.EnhanceEnemyVulnerability.ToString(),
            new HalidomObject(
            "7",
            HalidomName.EnhanceEnemyVulnerability.ToString(),
            "敌方受到易伤影响提升",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["EnhanceEnemyVulnerability"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add1StackIfEnemyHaveBleed.ToString(),
            new HalidomObject(
            "8",
            HalidomName.Add1StackIfEnemyHaveBleed.ToString(),
            "敌方获得流血时, 流血层数+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1StackIfEnemyHaveBleed"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add1StackIfEnemyHaveDebuff.ToString(),
            new HalidomObject(
            "9",
            HalidomName.Add1StackIfEnemyHaveDebuff.ToString(),
            "敌方获得DEBUFF时，层数+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1StackIfEnemyHaveDebuff"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add1StackIfPlayerHaveStrength.ToString(),
            new HalidomObject(
            "10",
            HalidomName.Add1StackIfPlayerHaveStrength.ToString(),
            "我方获得力量时，层数+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1StackIfPlayerHaveStrength"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add1StackIfPlayerHavePositiveBuff.ToString(),
            new HalidomObject(
            "11",
            HalidomName.Add1StackIfPlayerHavePositiveBuff.ToString(),
            "我方获得正面BUFF时，层数+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1StackIfPlayerHavePositiveBuff"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add4MoneyWhenBattleEnd.ToString(),
            new HalidomObject(
            "12",
            HalidomName.Add4MoneyWhenBattleEnd.ToString(),
            "战斗结束时，金钱+4",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add4MoneyWhenBattleEnd"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add50PercentAttackEvery3TimesLoseHealth.ToString(),
            new HalidomObject(
            "13",
            HalidomName.Add50PercentAttackEvery3TimesLoseHealth.ToString(),
            "每失去血量3次，下一次攻击造成伤害提升50%",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add50PercentAttackEvery3TimesLoseHealth"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add90PercentAttackEvery9TimesUseDice.ToString(),
            new HalidomObject(
            "14",
            HalidomName.Add90PercentAttackEvery9TimesUseDice.ToString(),
            "每打出9个骰面，下一次攻击造成伤害提升90%",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add90PercentAttackEvery9TimesUseDice"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Recover20HealthWhenEnterStore.ToString(),
            new HalidomObject(
            "15",
            HalidomName.Recover20HealthWhenEnterStore.ToString(),
            "每进入商店，回复20血量",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Recover20HealthWhenEnterStore"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Get5MaxHealthWhenGain.ToString(),
            new HalidomObject(
            "16",
            HalidomName.Get5MaxHealthWhenGain.ToString(),
            "拾取时，获得5点最大血量",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Get5MaxHealthWhenGain"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Recover25HealthWhenHealthBelowHalf.ToString(),
            new HalidomObject(
            "17",
            HalidomName.Recover25HealthWhenHealthBelowHalf.ToString(),
            "战斗开始时，血量不满一半则+25",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Recover25HealthWhenHealthBelowHalf"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add1Reroll.ToString(),
            new HalidomObject(
            "18",
            HalidomName.Add1Reroll.ToString(),
            "重投次数+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1Reroll"],null,null,1,false,null)
                })
            },

            {
            HalidomName.HalfInStore.ToString(),
            new HalidomObject(
            "19",
            HalidomName.HalfInStore.ToString(),
            "商店半价",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["HalfInStore"],null,null,1,false,null)
                })
            },

            {
            HalidomName.ReuseDiceWhenDiceIs1.ToString(),
            new HalidomObject(
            "20",
            HalidomName.ReuseDiceWhenDiceIs1.ToString(),
            "点数为1，则打出时重复打出",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["ReuseDiceWhenDiceIs1"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add2MoneyWhenDiceIs2.ToString(),
            new HalidomObject(
            "21",
            HalidomName.Add2MoneyWhenDiceIs2.ToString(),
            "点数为2，则打出时金钱+2",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add2MoneyWhenDiceIs2"],null,null,1,false,null)
                })
            },
            {
            HalidomName.Recover5HealthWhenDiceIs3.ToString(),
            new HalidomObject(
            "22",
            HalidomName.Recover5HealthWhenDiceIs3.ToString(),
            "点数为3，则打出时回复5点血量",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Recover5HealthWhenDiceIs3"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add1EnemyBleedStackWhenDiceIs4.ToString(),
            new HalidomObject(
            "23",
            HalidomName.Add1EnemyBleedStackWhenDiceIs4.ToString(),
            "点数为4，则打出时敌方流血+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1EnemyBleedStackWhenDiceIs4"],null,null,1,false,null)
                })
            },
            {
            HalidomName.Add1PlayerStrengthStackWhenDiceIs5.ToString(),
            new HalidomObject(
            "24",
            HalidomName.Add1PlayerStrengthStackWhenDiceIs5.ToString(),
            "点数为5，则打出时我方力量+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1PlayerStrengthStackWhenDiceIs5"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add1PermanentValueWhenDiceIs6.ToString(),
            new HalidomObject(
            "25",
            HalidomName.Add1PermanentValueWhenDiceIs6.ToString(),
            "点数为6，则打出时value永久+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1PermanentValueWhenDiceIs6"],null,null,1,false,null)
                })
            },


        };
    }
}


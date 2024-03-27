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
             HalidomName.Add2ValueIfResultIsEven.ToString(),
            new HalidomObject(
            RareType.Common,
             "1",
             "偶数注",
             "当骰子点数为偶数时，增加2点伤害",
            new List<BuffInfo>()
                {
                    new BuffInfo(BuffDataTable.buffData["Add2ValueIfResultIsEven"],null,null,1,false,null)
                })
            },
            {
             HalidomName.Add2ValueIfResultIsOdd.ToString(),
            new HalidomObject(
            RareType.Common,
             "2",
            "奇数注",
             "当骰子点数为奇数时，增加2点伤害",
            new List<BuffInfo>()
                {
                    new BuffInfo(BuffDataTable.buffData["Add2ValueIfResultIsOdd"],null,null,1,false,null)
                })
            },
            //TODO:3个4：选择最多三个骰面位，点数变成4


            //TODO:2个5，选择最多两个骰面位，点数变成5
            {
            HalidomName.Add3ValueIfResultBelow3.ToString(),
            new HalidomObject(
            RareType.Common,
            "5",
            "小数注",
            "当骰子点数小于3时，增加3点伤害",
            new List<BuffInfo>()
                {
                    new BuffInfo(BuffDataTable.buffData["Add3ValueIfResultBelow3"],null,null,1,false,null)
                })
            },
            {
            HalidomName.Add3ValueIfResultAbove4.ToString(),
            new HalidomObject(
            RareType.Common,
            "6",
            "大数注",
            "当骰子点数大于4时，增加3点伤害",
            new List<BuffInfo>()
                {   
                    new BuffInfo(BuffDataTable.buffData["Add3ValueIfResultAbove4"],null,null,1,false,null)
                })
            },
            {
            "好事成双",
            new HalidomObject(
            RareType.Common,
            "7",
            "好事成双",
            "获得时，选择两个骰面位，其点数永久+1",
            new List<BuffInfo>()
                {
                //TODO:还没写，这边需要新UI
                    new BuffInfo(BuffDataTable.buffData["好事成双"],null,null,1,false,null)
                })
            },
            {
            HalidomName.Add1StackIfEnemyHaveBleed.ToString(),
            new HalidomObject(
            RareType.Common,
            "8",
            "手术刀",
            "敌方获得流血时, 流血层数+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1StackIfEnemyHaveBleed"],null,null,1,false,null)
                })
            },
            {
            HalidomName.Add1StackIfPlayerHaveStrength.ToString(),
            new HalidomObject(
            RareType.Common,
            "9",
            "蛋白粉",
            "我方获得力量时，层数+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1StackIfPlayerHaveStrength"],null,null,1,true,null)
                })
            },
            {
            "狗牙",
            new HalidomObject(
            RareType.Common,
            "10",
            "狗牙",
            "如果当前又力量提升效果，我方的力量效能提升",
            new List<BuffInfo>()
            {       //TODO:还没写这个buffData
                    new BuffInfo(BuffDataTable.buffData["狗牙"],null,null,1,true,null)
                })
            },
            {
            "盐盒",
            new HalidomObject(
            RareType.Common,
            "11",
            HalidomName.EnhanceEnemyVulnerability.ToString(),
            "敌方受到易伤影响提升",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["EnhanceEnemyVulnerability"],null,null,1,false,null)
                })
            },
            {
            HalidomName.Add4MoneyWhenBattleEnd.ToString(),
            new HalidomObject(
            RareType.Common,
            "12",
            "肉包子",
            "战斗结束时，金钱+4",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add4MoneyWhenBattleEnd"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add50PercentAttackEvery3TimesLoseHealth.ToString(),
            new HalidomObject(
            RareType.Rare,
            "13",
            "事不过三",
            "每失去血量3次，下一次攻击造成伤害提升50%",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add50PercentAttackEvery3TimesLoseHealth"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add90PercentAttackEvery9TimesUseDice.ToString(),
            new HalidomObject(
            RareType.Common,
            "14",
            "999",
            "每打出9个骰面，下一次攻击造成伤害提升90%",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add90PercentAttackEvery9TimesUseDice"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Recover20HealthWhenEnterStore.ToString(),
            new HalidomObject(
            RareType.Common,
            "15",
            "饭票",
            "每进入商店，回复20血量",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Recover20HealthWhenEnterStore"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Get5MaxHealthWhenGain.ToString(),
            new HalidomObject(
            RareType.Common,
            "16",
            "烤土豆",
            "拾取时，获得5点最大血量",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Get5MaxHealthWhenGain"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Recover25HealthWhenHealthBelowHalf.ToString(),
            new HalidomObject(
            RareType.Common,
            "17",
            "医疗包",
            "战斗开始时，血量不满一半则+25",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Recover25HealthWhenHealthBelowHalf"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add1Reroll.ToString(),
            new HalidomObject(
            RareType.Common,
            "18",
            "圣手",
            "重投次数+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1Reroll"],null,null,1,false,null)
                })
            },

            {
            HalidomName.HalfInStore.ToString(),
            new HalidomObject(
            RareType.Common,
            "19",
            "打折券",
            "商店半价",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["HalfInStore"],null,null,1,false,null)
                })
            },

            {
            HalidomName.ReuseDiceWhenDiceIs1.ToString(),
            new HalidomObject(
            RareType.Common,
            "20",
            "1号球",
            "点数为1，则打出时重复打出",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["ReuseDiceWhenDiceIs1"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add2MoneyWhenDiceIs2.ToString(),
            new HalidomObject(
            RareType.Common,
            "21",
            "2号球",
            "点数为2，则打出时金钱+2",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add2MoneyWhenDiceIs2"],null,null,1,false,null)
                })
            },
            {
            HalidomName.Recover5HealthWhenDiceIs3.ToString(),
            new HalidomObject(
            RareType.Common,
            "22",
            "3号球",
            "点数为3，则打出时回复5点血量",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Recover5HealthWhenDiceIs3"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add1EnemyBleedStackWhenDiceIs4.ToString(),
            new HalidomObject(
            RareType.Common,
            "23",
            "4号球",
            "点数为4，则打出时敌方流血+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1EnemyBleedStackWhenDiceIs4"],null,null,1,false,null)
                })
            },
            {
            HalidomName.Add1PlayerStrengthStackWhenDiceIs5.ToString(),
            new HalidomObject(
            RareType.Common,
            "24",
            "5号球",
            "点数为5，则打出时我方力量+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1PlayerStrengthStackWhenDiceIs5"],null,null,1,false,null)
                })
            },

            {
            HalidomName.Add1PermanentValueWhenDiceIs6.ToString(),
            new HalidomObject(
            RareType.Common,
            "25",
            "6号球",
            "点数为6，则打出时value永久+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1PermanentValueWhenDiceIs6"],null,null,1,false,null)
                })
            },
            //开始Rare
            {
            HalidomName.Add1StackIfEnemyHaveDebuff.ToString(),
            new HalidomObject(
            RareType.Rare,
            "9",
            "烂番茄",
            "敌方获得DEBUFF时，层数+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1StackIfEnemyHaveDebuff"],null,null,1,false,null)
                })
            },
            {
            HalidomName.Add1StackIfPlayerHavePositiveBuff.ToString(),
            new HalidomObject(
            RareType.Rare,
            "11",
            "榴莲千层",
            "我方获得正面BUFF时，层数+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1StackIfPlayerHavePositiveBuff"],null,null,1,false,null)
                })
            },
        };
    }
}


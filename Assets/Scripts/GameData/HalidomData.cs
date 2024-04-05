using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DesignerScripts
{
    public enum HalidomName
    {
        test,
        //普通
        偶数注,
        奇数注,
        三个四,
        三个五,
        小数注,
        大数注,
        好事成双,
        手术刀,
        蛋白粉,
        狗牙,
        盐盒,
        肉包子,
        分裂金币,
        打折券,
        讲究手套,
        事不过三,
        饭票,
        烤土豆,
        创可贴,
        医疗包,
        铅笔,
        三指,
        一号球,
        二号球,
        三号球,
        四号球,
        五号球,
        六号球,
        恶毒嘲笑,
        见切,
        见面礼,
        鬼神面具,
        肉瘤,
        一盒钉子,
        臭鸡蛋,
        //稀有
        偶数大注,
        奇数大注,
        四号骰,
        五号骰,
        烂番茄,
        榴莲千层,
        利滚利,
        升格,
        软饭,
        静脉曲张,
        小波奇,
        受虐面具,
        圣手,
        蚯蚓,
        生长素,
        //传说
        偶数天注,
        奇数天注,
        奇数增长,
        偶数增长,
        杂货铺,
        肾上腺素,
        诸葛连弩,
        金钱等于力量,
        俄罗斯轮盘赌,
        达摩克利斯之剑,
        伊卡洛斯之翼,
        潘多拉魔盒,
        十二试炼,

    }
    public static class HalidomData
    {
        public static Dictionary<string, HalidomObject> halidomDictionary = new Dictionary<string, HalidomObject>() 
        {
           /*#region
            *//*{
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
            }*//*
            //{
            // "偶数注",
            //new HalidomObject(
            //RareType.Common,
            // "1",
            // "偶数注",
            // "当骰子点数为偶数时，增加2点伤害",
            //new List<BuffInfo>()
            //    {
            //        new BuffInfo(BuffDataTable.buffData["Add2ValueIfResultIsEven"],null,null,1,false,null)
            //    })
            //},
            {
             "奇数注",
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
            HalidomName.小数注.ToString(),
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
            HalidomName.大数注.ToString(),
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
                    //new BuffInfo(BuffDataTable.buffData["好事成双"],null,null,1,false,null)
                })
            },
            {
            HalidomName.手术刀.ToString(),
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
            HalidomName.蛋白粉.ToString(),
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
            HalidomName.狗牙.ToString(),
            new HalidomObject(
            RareType.Common,
            "10",
            "狗牙",
            "如果当前有力量提升效果，我方的力量效能提升",
            new List<BuffInfo>()
            {       
                    new BuffInfo(BuffDataTable.buffData["EnhancePlayerStrength"],null,null,1,true,null)
                })
            },
            {
            "盐盒",
            new HalidomObject(
            RareType.Common,
            "11",
            HalidomName.盐盒.ToString(),
            "敌方受到易伤影响提升",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["EnhanceEnemyVulnerability"],null,null,1,false,null)
                })
            },
            {
            HalidomName.肉包子.ToString(),
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
            HalidomName.分裂金币.ToString(),
            new HalidomObject(
            RareType.Rare,
            "13",
            "分裂金币",
            "拾取时，获得数量等同于当前金钱一半的金钱",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["GainHalfMoney"],null,null,1,false,null)
                })
            },

            {
            HalidomName.打折券.ToString(),
            new HalidomObject(
            RareType.Common,
            "14",
            "打折券",
            "商店半价",
            new List<BuffInfo>()
            {
                    //new BuffInfo(BuffDataTable.buffData[""],null,null,1,false,null)
                })
            },

            {
            HalidomName.讲究手套.ToString(),
            new HalidomObject(
            RareType.Common,
            "15",
            "讲究手套",
            "杀死敌人时，获得溢出伤害的金钱",
            new List<BuffInfo>()
            {
                    //new BuffInfo(BuffDataTable.buffData[""],null,null,1,false,null)
                })
            },

            {
            HalidomName.事不过三.ToString(),
            new HalidomObject(
            RareType.Common,
            "16",
            "事不过三",
            "每失去血量3次，下一次攻击造成伤害提升50%",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add50PercentAttackEvery3TimesLoseHealth"],null,null,1,false,null)
                })
            },

            {
            HalidomName.饭票.ToString(),
            new HalidomObject(
            RareType.Common,
            "17",
            "饭票",
            "每进入商店，回复20血量",
            new List<BuffInfo>()
            {
                    //new BuffInfo(BuffDataTable.buffData["Recover25HealthWhenHealthBelowHalf"],null,null,1,false,null)
                })
            },

            {
            HalidomName.烤土豆.ToString(),
            new HalidomObject(
            RareType.Common,
            "18",
            "烤土豆",
            "拾取时，获得5点最大血量",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Get5MaxHealthWhenGain"],null,null,1,false,null)
                })
            },

            {
            HalidomName.创可贴.ToString(),
            new HalidomObject(
            RareType.Common,
            "19",
            "创可贴",
            "拾取时，回复一半血量",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["RecoverHalfHealthWhenGain"],null,null,1,false,null)
                })
            },

            {
            HalidomName.医疗包.ToString(),
            new HalidomObject(
            RareType.Common,
            "20",
            "医疗包",
            "战斗开始时，血量不满一半则+25",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Recover25HealthWhenHealthBelowHalf"],null,null,1,false,null)
                })
            },

            {
            HalidomName.铅笔.ToString(),
            new HalidomObject(
            RareType.Common,
            "21",
            "铅笔",
            "每对敌方造成15次伤害，下一次攻击伤害+20",
            new List<BuffInfo>()
            {
                    //new BuffInfo(BuffDataTable.buffData[""],null,null,1,false,null)
                })
            },
            {
            HalidomName.三指.ToString(),
            new HalidomObject(
            RareType.Common,
            "22",
            "三指(断的)",
            "重投时不消耗重投次数，触发3次后失效",
            new List<BuffInfo>()
            {
                    //new BuffInfo(BuffDataTable.buffData[""],null,null,1,false,null)
                })
            },

            {
            HalidomName.一号球.ToString(),
            new HalidomObject(
            RareType.Common,
            "23",
            "1号球(台球)",
            "点数为1，则打出时重复打出",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["ReuseDiceWhenDiceIs1"],null,null,1,false,null)
                })
            },
            {
            HalidomName.二号球.ToString(),
            new HalidomObject(
            RareType.Common,
            "24",
            "2号球",
            "点数为2，则打出时金钱+2",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add2MoneyWhenDiceIs2"],null,null,1,false,null)
                })
            },

            {
            HalidomName.三号球.ToString(),
            new HalidomObject(
            RareType.Common,
            "25",
            "3号球",
            "点数为3，则打出时回复5点血量",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Recover5HealthWhenDiceIs3"],null,null,1,false,null)
                })
            },

            {
            HalidomName.四号球.ToString(),
            new HalidomObject(
            RareType.Common,
            "26",
            "4号球",
            "点数为4，则打出时敌方流血+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1EnemyBleedStackWhenDiceIs4"],null,null,1,false,null)
                })
            },


            {
            HalidomName.五号球.ToString(),
            new HalidomObject(
            RareType.Common,
            "27",
            "5号球",
            "点数为5，则打出时我方力量+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1PlayerStrengthStackWhenDiceIs5"],null,null,1,false,null)
                })
            },

            {
            HalidomName.六号球.ToString(),
            new HalidomObject(
            RareType.Common,
            "28",
            "6号球",
            "点数为5，则打出时我方力量+1",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Add1PermanentValueWhenDiceIs6"],null,null,1,false,null)
                })
            },

            {
            HalidomName.见切.ToString(),
            new HalidomObject(
            RareType.Common,
            "30",
            "见切",
            "进入战斗时，获得1层闪避",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Gain1DodgeWhenBattleStart"],null,null,1,false,null)
                })
            },

            {
            HalidomName.见面礼.ToString(),
            new HalidomObject(
            RareType.Common,
            "31",
            "见面礼",
            "进入战斗时，获得1层强化",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Gain1EnhanceWhenBattleStart"],null,null,1,false,null)
                })
            },


            {
            HalidomName.鬼神面具.ToString(),
            new HalidomObject(
            RareType.Common,
            "32",
            "鬼神面具",
            "进入战斗时，获得2层力量",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Gain2StrengthWhenBattleStart"],null,null,1,false,null)
                })
            },

            {
            HalidomName.肉瘤.ToString(),
            new HalidomObject(
            RareType.Common,
            "33",
            "肉瘤",
            "进入战斗时，获得2层坚韧",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Gain2ToughWhenBattleStart"],null,null,1,false,null)
                })
            },

            {
            HalidomName.一盒钉子.ToString(),
            new HalidomObject(
            RareType.Common,
            "34",
            "一盒钉子",
            "进入战斗时，敌人获得2层易伤",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Gain2VulnerableWhenBattleStart"],null,null,1,false,null)
                })
            },

            {
            HalidomName.臭鸡蛋.ToString(),
            new HalidomObject(
            RareType.Common,
            "35",
            "臭鸡蛋",
            "进入战斗时，敌人获得2层虚弱",
            new List<BuffInfo>()
            {
                    new BuffInfo(BuffDataTable.buffData["Gain2WeakWhenBattleStart"],null,null,1,false,null)
                })
            },




            //开始Rare
            {
            HalidomName.烂番茄.ToString(),
            new HalidomObject(
            RareType.Rare,
            "9",
            "烂番茄",
            "敌方获得DEBUFF时，层数+1",
            new List<BuffInfo>()
            {
                    //new BuffInfo(BuffDataTable.buffData["Add1StackIfEnemyHaveDebuff"],null,null,1,false,null)
                })
            },
            {
            HalidomName.榴莲千层.ToString(),
            new HalidomObject(
            RareType.Rare,
            "11",
            "榴莲千层",
            "我方获得正面BUFF时，层数+1",
            new List<BuffInfo>()
            {
                    //new BuffInfo(BuffDataTable.buffData["Add1StackIfPlayerHavePositiveBuff"],null,null,1,false,null)
                })
            },
                    #endregion*/

        };
    }
}


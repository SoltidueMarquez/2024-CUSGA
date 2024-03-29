using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DesignerScripts
{
    public enum BuffDataName
    {
        Bleed,//流血

        Spirit,//精力

        Vulnerable,//易伤

        Tough,//坚韧

        Weak,//虚弱

        Strength,//力量

        Enhance,//强化


        #region 圣物buff
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

        #endregion
    }
    public class BuffDataTable
    {
        public static Dictionary<string, BuffData> buffData = new Dictionary<string, BuffData>()
        {
            { "test",new BuffData(
                //buff的唯一id
                "0",
                //buff的名字
                "TestBuff0",
                //存储buff的icon图标图片的名字（Resources文件夹下）
                "icon0",
                //用于方便检索的tag，例如：火焰，冰冻，中毒等
                null,
                //buff的最高层数
                3,
                //buff的持续回合数量
                2,
                //是否是永久的buff(这个的优先级更高）
                false,
            //buff更新层数时的策略（Add：增加回合，Replace：替换回合数为指定数，Keep：什么都不做）
            BuffUpdateEnum.Keep,
            //buff移除层数时的策略（Reduce：减少层数，Clear：清除所有层数）
            BuffRemoveStackUpdateEnum.Clear,
            "",null,//OnCreate回调点 ，回调点参数额外设置
            "",null,//OnRemove回调点 ，回调点参数额外设置
            "",null,//OnRoundStart回调点 ，回调点参数额外设置
            "",null,//OnRoundEnd回调点 ，回调点参数额外设置
            "",null,//OnHit回调点 ，回调点参数额外设置
            "",null,//OnBeHurt回调点 ，回调点参数额外设置
            "",null,//OnRoll回调点 ，回调点参数额外设置
            "",null,//OnKill回调点 ，回调点参数额外设置
            "",null,//OnBeKill回调点 ，回调点参数额外设置
             "",null,//OnCasr回调点 ，回调点参数额外设置
            //buff对玩家的状态修改（无敌，能否出牌，局内骰面更改），
            ChaControlState.origin,
            //buff对玩家的属性(血量，金钱，护盾，重新投掷次数）修改
            null
            ) 
            },
            {//这边是流血的buff,要实现层数衰减为延迟回合数的效果，将permanent设置为false,初始持续回合数设置为0
                BuffDataName.Bleed.ToString(),new BuffData
                (
                    "1",
                    BuffDataName.Bleed.ToString(),
                    "icon1",
                    new [] {"Target"},
                    5,
                    0,
                    false,
                    BuffUpdateEnum.Keep,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Bleed.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null, 
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )
                
            },

            {
                BuffDataName.Spirit.ToString(),new BuffData
                (
                    "2",
                    BuffDataName.Spirit.ToString(),
                    "icon2",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.BuffStackMinus1.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

            },

            {
                BuffDataName.Vulnerable.ToString(),new BuffData
                (
                    "3",
                    BuffDataName.Vulnerable.ToString(),
                    "icon3",
                    new [] {"Target"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.BuffStackMinus1.ToString(),null,
                    "",null,
                    "",null,
                    BuffEventName.Vulnerable.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

            },

            {
                BuffDataName.Tough.ToString(),new BuffData
                (
                    "4",
                    BuffDataName.Tough.ToString(),
                    "icon4",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.BuffStackMinus1.ToString(),null,
                    "",null,
                    "",null,
                    BuffEventName.Tough.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

            },

            {
                BuffDataName.Weak.ToString(),new BuffData
                (
                    "5",
                    BuffDataName.Weak.ToString(),
                    "icon5",
                    new []{"Target"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.BuffStackMinus1.ToString(),null,
                    "",null,
                    BuffEventName.Weak.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

            },

             {
                BuffDataName.Strength.ToString(),new BuffData
                (
                    "6",
                    BuffDataName.Strength.ToString(),
                    "icon6",
                    new []{"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.BuffStackMinus1.ToString(),null,
                    "",null,
                    BuffEventName.Strength.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Enhance.ToString(),new BuffData
                (
                    "7",
                    BuffDataName.Enhance.ToString(),
                    "icon7",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.BuffStackMinus1.ToString(),null,
                    "",null,
                    BuffEventName.Enhance.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Add2ValueIfResultIsEven.ToString(),new BuffData
                (
                    "8",
                    BuffDataName.Add2ValueIfResultIsEven.ToString(),
                    "icon8",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Add2ValueIfResultIsEven.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Add2ValueIfResultIsOdd.ToString(),new BuffData
                (
                    "9",
                    BuffDataName.Add2ValueIfResultIsOdd.ToString(),
                    "icon9",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Add2ValueIfResultIsOdd.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Add3ValueIfResultBelow3.ToString(),new BuffData
                (
                    "10",
                    BuffDataName.Add3ValueIfResultBelow3.ToString(),
                    "icon10",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Add3ValueIfResultBelow3.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Add3ValueIfResultAbove4.ToString(),new BuffData
                (
                    "11",
                    BuffDataName.Add3ValueIfResultAbove4.ToString(),
                    "icon11",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Add3ValueIfResultAbove4.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },
             {
                BuffDataName.EnhanceEnemyVulnerability.ToString(),new BuffData
                (
                    "14",
                    BuffDataName.EnhanceEnemyVulnerability.ToString(),
                    "icon14",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.EnhanceEnemyVulnerability.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },
             //TODO:没加event
             {
                BuffDataName.Add1StackIfEnemyHaveBleed.ToString(),new BuffData
                (
                    "15",
                    BuffDataName.Add1StackIfEnemyHaveBleed.ToString(),
                    "icon15",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },
             //TODO:没加event
             {
                BuffDataName.Add1StackIfEnemyHaveDebuff.ToString(),new BuffData
                (
                    "16",
                    BuffDataName.Add1StackIfEnemyHaveDebuff.ToString(),
                    "icon16",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             //TODO:没加event
             {
                BuffDataName.Add1StackIfPlayerHaveStrength.ToString(),new BuffData
                (
                    "17",
                    BuffDataName.Add1StackIfPlayerHaveStrength.ToString(),
                    "icon17",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             //TODO:没加event
             {
                BuffDataName.Add1StackIfPlayerHavePositiveBuff.ToString(),new BuffData
                (
                    "18",
                    BuffDataName.Add1StackIfPlayerHavePositiveBuff.ToString(),
                    "icon18",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Add4MoneyWhenBattleEnd.ToString(),new BuffData
                (
                    "19",
                    BuffDataName.Add4MoneyWhenBattleEnd.ToString(),
                    "icon19",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Add4MoneyWhenBattleEnd.ToString(),null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Add50PercentAttackEvery3TimesLoseHealth.ToString(),new BuffData
                (
                    "20",
                    BuffDataName.Add50PercentAttackEvery3TimesLoseHealth.ToString(),
                    "icon20",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Add50PercentAttackEvery3TimesLoseHealth.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Add90PercentAttackEvery9TimesUseDice.ToString(),new BuffData
                (
                    "21",
                    BuffDataName.Add90PercentAttackEvery9TimesUseDice.ToString(),
                    "icon21",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Add90PercentAttackEvery9TimesUseDice.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },
             //TODO:没加event
             {
                BuffDataName.Recover20HealthWhenEnterStore.ToString(),new BuffData
                (
                    "22",
                    BuffDataName.Recover20HealthWhenEnterStore.ToString(),
                    "icon22",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },
             {
                BuffDataName.Get5MaxHealthWhenGain.ToString(),new BuffData
                (
                    "23",
                    BuffDataName.Get5MaxHealthWhenGain.ToString(),
                    "icon23",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    BuffEventName.Get5MaxHealthWhenGain.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Recover25HealthWhenHealthBelowHalf.ToString(),new BuffData
                (
                    "24",
                    BuffDataName.Recover25HealthWhenHealthBelowHalf.ToString(),
                    "icon24",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.Recover25HealthWhenHealthBelowHalf.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Add1Reroll.ToString(),new BuffData
                (
                    "25",
                    BuffDataName.Add1Reroll.ToString(),
                    "icon25",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    BuffEventName.Add1Reroll.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },
             //TODO:没加event
             {
                BuffDataName.HalfInStore.ToString(),new BuffData
                (
                    "26",
                    BuffDataName.HalfInStore.ToString(),
                    "icon26",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },
             //TODO:没加event
             {
                BuffDataName.ReuseDiceWhenDiceIs1.ToString(),new BuffData
                (
                    "27",
                    BuffDataName.ReuseDiceWhenDiceIs1.ToString(),
                    "icon27",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Add2MoneyWhenDiceIs2.ToString(),new BuffData
                (
                    "28",
                    BuffDataName.Add2MoneyWhenDiceIs2.ToString(),
                    "icon28",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Add2MoneyWhenDiceIs2.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Recover5HealthWhenDiceIs3.ToString(),new BuffData
                (
                    "29",
                    BuffDataName.Recover5HealthWhenDiceIs3.ToString(),
                    "icon29",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Recover5HealthWhenDiceIs3.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Add1EnemyBleedStackWhenDiceIs4.ToString(),new BuffData
                (
                    "30",
                    BuffDataName.Add1EnemyBleedStackWhenDiceIs4.ToString(),
                    "icon30",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Add1EnemyBleedStackWhenDiceIs4.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Add1PlayerStrengthStackWhenDiceIs5.ToString(),new BuffData
                (
                    "31",
                    BuffDataName.Add1PlayerStrengthStackWhenDiceIs5.ToString(),
                    "icon31",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Add1PlayerStrengthStackWhenDiceIs5.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },

             {
                BuffDataName.Add1PermanentValueWhenDiceIs6.ToString(),new BuffData
                (
                    "32",
                    BuffDataName.Add1PermanentValueWhenDiceIs6.ToString(),
                    "icon32",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Add1PermanentValueWhenDiceIs6.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },



        };

    }

}

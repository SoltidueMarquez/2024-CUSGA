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

        Dodge,//闪避

        EnergyStorage,//蓄能

        Anger,//怒气

        LoseEnergy,//失能


        #region 圣物buff
        Add2ValueIfResultIsEven,//1
        Add2ValueIfResultIsOdd,//2
        Add3ValueIfResultBelow3,//5
        Add3ValueIfResultAbove4,//6
        GainAndChoose2DicesGiveRandomCoating,
        GainAndChoose2DicesGive1PermanentEnhance,
        Add1StackIfEnemyHaveBleed,//8
        Add1StackIfEnemyHaveDebuff,
        Add1StackIfPlayerHaveStrength,//9
        EnhancePlayerStrength,//10
        EnhanceEnemyVulnerability,//11
        Add1StackIfPlayerHavePositiveBuff,
        Add4MoneyWhenBattleEnd,//12
        GainHalfMoney,//13
        Add50PercentAttackEvery3TimesLoseHealth,//16
        
        Add90PercentAttackEvery9TimesUseDice,
        Recover20HealthWhenEnterStore,
        Get5MaxHealthWhenGain,//18
        RecoverHalfHealthWhenGain,//19
        Recover25HealthWhenHealthBelowHalf,//20
        Add1Reroll,
        HalfInStore,
        ReuseDiceWhenDiceIs1,//23
        Add2MoneyWhenDiceIs2,//24
        Recover5HealthWhenDiceIs3,//25
        Add1EnemyBleedStackWhenDiceIs4,//26
        Add1PlayerStrengthStackWhenDiceIs5,//27
        Add1PermanentValueWhenDiceIs6,//28
        Gain1DodgeWhenBattleStart,//30
        Gain1EnhanceWhenBattleStart,//31
        Gain2StrengthWhenBattleStart,//32
        Gain2ToughWhenBattleStart,//33
        Gain2VulnerableWhenBattleStart,//34
        Gain2WeakWhenBattleStart,//35

        Add4ValueIfResultIsEven,
        Add4ValueIfResultIsOdd,

        Add50PercentAttack,//16


        //骰子buff
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
        #endregion

        GetHurt2,
        GetHurt3,
        GetHurt4,
        RecoverHealth2,
        RecoverHealth3,
        GainMoney2,
        GainMoney3,
        RerollDice,

        //稀有圣物
        GainMoneyAfterBattle,//战斗后获得金币
        Choose2DiceUpdateWhenGain,
        Gain2NormalHalidomWhenGain,
        Hit3DamageWhenLoseHealth,
        GainDodgeWhenLoseHealth,
        GainStrengthWhenLoseHealth,
        RecoverHalfHealthWhenDie,
        Add1ValueWhenDiceBelow3,

        //传说圣物
        Add6ValueIfResultIsEven,
        Add6ValueIfResultIsOdd,
        Add1ValueToAddOdd,
        Add1ValueToAddEven,
        EnhanceAttackAfterSellDice,
        Enhance25AttackWhenHalfHealth,
        EnhanceAttackWhenHit,
        EnhanceAttackBaseOnMoney,
        EnhanceAttackAndHurt,
        Gain2DiceAndHurt,
        Update2DiceAndHurt,
        Gain2RareHalidom,
        EnhanceAttackAfterKillEnemy,
    }
    public class BuffDataTable
    {
        public static Dictionary<string, BuffData> buffData = new Dictionary<string, BuffData>()
        {
            //{ "test",new BuffData(
            //    //buff的唯一id
            //    "0",
            //    //buff的名字
            //    "TestBuff0",
            //    //存储buff的icon图标图片的名字（Resources文件夹下）
            //    "icon0",
            //    //用于方便检索的tag，例如：火焰，冰冻，中毒等
            //    null,
            //    //buff的最高层数
            //    3,
            //    //buff的持续回合数量
            //    2,
            //    //是否是永久的buff(这个的优先级更高）
            //    false,
            ////buff更新层数时的策略（Add：增加回合，Replace：替换回合数为指定数，Keep：什么都不做）
            //BuffUpdateEnum.Keep,
            ////buff移除层数时的策略（Reduce：减少层数，Clear：清除所有层数）
            //BuffRemoveStackUpdateEnum.Clear,
            //"",null,//OnCreate回调点 ，回调点参数额外设置
            //"",null,//OnRemove回调点 ，回调点参数额外设置
            //"",null,//OnRoundStart回调点 ，回调点参数额外设置
            //"",null,//OnRoundEnd回调点 ，回调点参数额外设置
            //"",null,//OnHit回调点 ，回调点参数额外设置
            //"",null,//OnBeHurt回调点 ，回调点参数额外设置
            //"",null,//OnRoll回调点 ，回调点参数额外设置
            //"",null,//OnKill回调点 ，回调点参数额外设置
            //"",null,//OnBeKill回调点 ，回调点参数额外设置
            //"",null,//OnCast回调点 ，回调点参数额外设置
            ////buff对玩家的状态修改（无敌，能否出牌，局内骰面更改），
            //ChaControlState.origin,
            ////buff对玩家的属性(血量，金钱，护盾，重新投掷次数）修改
            //null
            //)
            //},

            /*#region 基础buff
            {//这边是流血的buff,要实现层数衰减为延迟回合数的效果，将permanent设置为false,初始持续回合数设置为0
                //回合结束收到伤害+层数-1
                BuffDataName.Bleed.ToString(),new BuffData
                (
                    "1_1",
                    BuffDataName.Bleed.ToString(),
                    "icon1_1",
                    new [] {"Target"},
                    5,
                    0,
                    false,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    BuffEventName.CheckForBleed.ToString(),null,
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
            //回合结束加血+层数-1
            {
                BuffDataName.Spirit.ToString(),new BuffData
                (
                    "1_2",
                    BuffDataName.Spirit.ToString(),
                    "icon1_2",
                    new [] {"Self"},
                    5,
                    0,
                    false,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Spirit.ToString(),null,
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
            //OnBehurt触发增加伤害，在对方的回合结束-1层
            {
                BuffDataName.Vulnerable.ToString(),new BuffData
                (
                    "1_3",
                    BuffDataName.Vulnerable.ToString(),
                    "icon1_3",
                    new [] {"Target"},
                    5,
                    0,
                    false,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
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
            //不走回合数走层数，将isPermanent设置为true
            //OnBehurt触发减少伤害，在自己的回合结束-1层
            {
                BuffDataName.Tough.ToString(),new BuffData
                (
                    "1_4",
                    BuffDataName.Tough.ToString(),
                    "icon1_4",
                    new [] {"Self"},
                    5,
                    0,
                    false,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
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
            //不走回合数走层数，将isPermanent设置为true
            //OnHit触发减少伤害，在自己的回合结束-1层
            {
                BuffDataName.Weak.ToString(),new BuffData
                (
                    "1_5",
                    BuffDataName.Weak.ToString(),
                    "icon1_5",
                    new []{"Target"},
                    5,
                    0,
                    false,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
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
            //不走回合数走层数，将isPermanent设置为true
            //OnHit触发增加伤害，在自己的回合结束-1层
             {
                BuffDataName.Strength.ToString(),new BuffData
                (
                    "1_6",
                    BuffDataName.Strength.ToString(),
                    "icon1_6",
                    new []{"Self"},
                    5,
                    0,
                    false,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    BuffEventName.CheckForStrength.ToString(),null,
                    "",null,
                    "",null,
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
                    "1_7",
                    BuffDataName.Enhance.ToString(),
                    "icon1_7",
                    new [] {"Self"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
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
                BuffDataName.Dodge.ToString(),new BuffData
                (
                    "1_8",
                    BuffDataName.Dodge.ToString(),
                    "icon1_8",
                    new [] {"Target"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Dodge.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )
             },
             {
                BuffDataName.EnergyStorage.ToString(),new BuffData
                (
                    "1_9",
                    BuffDataName.EnergyStorage.ToString(),
                    "icon1_9",
                    new [] {"Self"},
                    5,
                    0,
                    false,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.EnergyStorage.ToString(),null,
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
                BuffDataName.Anger.ToString(),new BuffData
                (
                    "1_10",
                    BuffDataName.Anger.ToString(),
                    "icon1_10",
                    new [] {"Self"},
                    100,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Anger.ToString(),null,
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
                BuffDataName.LoseEnergy.ToString(),new BuffData
                (
                    "1_11",
                    BuffDataName.LoseEnergy.ToString(),
                    "icon1_11",
                    new [] {"Target"},
                    5,
                    0,
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
                    BuffEventName.LoseEnergy.ToString(),null,
                    ChaControlState.origin,
                    null
                 )
            },

            #endregion


            #region 圣物buff

            //{
            //    BuffDataName.Add2ValueIfResultIsEven.ToString(),new BuffData
            //    (
            //        "2_1",
            //        BuffDataName.Add2ValueIfResultIsEven.ToString(),
            //        "icon2_1",
            //        new [] {"Self"},
            //        5,
            //        0,
            //        true,
            //        BuffUpdateEnum.Add,
            //        BuffRemoveStackUpdateEnum.Reduce,
            //        "",null,
            //        "",null,
            //        "",null,
            //        "",null,
            //        BuffEventName.Add2ValueIfResultIsEven.ToString(),null,
            //        "",null,
            //        "",null,
            //        "",null,
            //        "",null,
            //        "",null,
            //        ChaControlState.origin,
            //        null
            //     )

            // },

             {
                BuffDataName.Add2ValueIfResultIsOdd.ToString(),new BuffData
                (
                    "2_2",
                    BuffDataName.Add2ValueIfResultIsOdd.ToString(),
                    "icon2_2",
                    new [] {"Self"},
                    5,
                    0,
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
                    "2_5",
                    BuffDataName.Add3ValueIfResultBelow3.ToString(),
                    "icon2_5",
                    new [] {"Self"},
                    5,
                    0,
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
                    "2_6",
                    BuffDataName.Add3ValueIfResultAbove4.ToString(),
                    "icon2_6",
                    new [] {"Self"},
                    5,
                    0,
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
                BuffDataName.Add1StackIfEnemyHaveBleed.ToString(),new BuffData
                (
                    "2_8",
                    BuffDataName.Add1StackIfEnemyHaveBleed.ToString(),
                    "icon2_8",
                    new [] {"Target"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Add1StackIfEnemyHaveBleed.ToString(),null,
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
                BuffDataName.Add1StackIfPlayerHaveStrength.ToString(),new BuffData
                (
                    "2_9",
                    BuffDataName.Add1StackIfPlayerHaveStrength.ToString(),
                    "icon2_9",
                    new [] {"Self"},
                    5,
                    0,
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
                    BuffEventName.Add1StackIfPlayerHaveStrength.ToString(),null,
                    ChaControlState.origin,
                    null
                 )

             },
             {
                BuffDataName.EnhancePlayerStrength.ToString(),new BuffData
                (
                    "2_10",
                    BuffDataName.EnhancePlayerStrength.ToString(),
                    "icon2_10",
                    new [] {"Self"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.EnhancePlayerStrength.ToString(),null,
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
                    "2_11",
                    BuffDataName.EnhanceEnemyVulnerability.ToString(),
                    "icon2_11",
                    new [] {"Target"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.EnhanceEnemyVulnerability.ToString(),null,
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
                    "2_12",
                    BuffDataName.Add4MoneyWhenBattleEnd.ToString(),
                    "icon2_12",
                    new [] {"Self"},
                    5,
                    0,
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
                    BuffEventName.Add4MoneyWhenBattleEnd.ToString(),null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },
             {
                BuffDataName.GainHalfMoney.ToString(),new BuffData
                (
                    "2_13",
                    BuffDataName.GainHalfMoney.ToString(),
                    "icon2_13",
                    new [] {"Self"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    BuffEventName.GainHalfMoney.ToString(),null,
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
                BuffDataName.Add50PercentAttackEvery3TimesLoseHealth.ToString(),new BuffData
                (
                    "2_16",
                    BuffDataName.Add50PercentAttackEvery3TimesLoseHealth.ToString(),
                    "icon2_16",
                    new [] {"Self"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Add50PercentAttackEvery3TimesLoseHealth.ToString(),null,
                    "",null,
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
                    0,
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
                    0,
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
                    "2_18",
                    BuffDataName.Get5MaxHealthWhenGain.ToString(),
                    "icon2_18",
                    new [] {"Self"},
                    5,
                    0,
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
                BuffDataName.RecoverHalfHealthWhenGain.ToString(),new BuffData
                (
                    "2_19",
                    BuffDataName.RecoverHalfHealthWhenGain.ToString(),
                    "icon2_19",
                    new [] {"Self"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    BuffEventName.RecoverHalfHealthWhenGain.ToString(),null,
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
                    "2_20",
                    BuffDataName.Recover25HealthWhenHealthBelowHalf.ToString(),
                    "icon2_20",
                    new [] {"Self"},
                    5,
                    0,
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

             
             //TODO:没加event
             {
                BuffDataName.HalfInStore.ToString(),new BuffData
                (
                    "26",
                    BuffDataName.HalfInStore.ToString(),
                    "icon26",
                    new [] {"Self"},
                    5,
                    0,
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
                    "2_23",
                    BuffDataName.ReuseDiceWhenDiceIs1.ToString(),
                    "icon2_23",
                    new [] {"Self"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.ReuseDiceWhenDiceIs1.ToString(),null,
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
                    "2_24",
                    BuffDataName.Add2MoneyWhenDiceIs2.ToString(),
                    "icon2_24",
                    new [] {"Self"},
                    5,
                    0,
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
                    "2_25",
                    BuffDataName.Recover5HealthWhenDiceIs3.ToString(),
                    "icon2_25",
                    new [] {"Self"},
                    5,
                    0,
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
                    "2_26",
                    BuffDataName.Add1EnemyBleedStackWhenDiceIs4.ToString(),
                    "icon2_26",
                    new [] {"Self"},
                    5,
                    0,
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
                    "2_27",
                    BuffDataName.Add1PlayerStrengthStackWhenDiceIs5.ToString(),
                    "icon2_27",
                    new [] {"Self"},
                    5,
                    0,
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
                    "2_28",
                    BuffDataName.Add1PermanentValueWhenDiceIs6.ToString(),
                    "icon2_28",
                    new [] {"Self"},
                    5,
                    0,
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

             {
                BuffDataName.Gain1DodgeWhenBattleStart.ToString(),new BuffData
                (
                    "2_30",
                    BuffDataName.Gain1DodgeWhenBattleStart.ToString(),
                    "icon2_30",
                    new [] {"Self"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.Gain1DodgeWhenBattleStart.ToString(),null,
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
                BuffDataName.Gain1EnhanceWhenBattleStart.ToString(),new BuffData
                (
                    "2_31",
                    BuffDataName.Gain1EnhanceWhenBattleStart.ToString(),
                    "icon2_31",
                    new [] {"Self"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.Gain1EnhanceWhenBattleStart.ToString(),null,
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
                BuffDataName.Gain2StrengthWhenBattleStart.ToString(),new BuffData
                (
                    "2_32",
                    BuffDataName.Gain2StrengthWhenBattleStart.ToString(),
                    "icon2_32",
                    new [] {"Self"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.Gain2StrengthWhenBattleStart.ToString(),null,
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
                BuffDataName.Gain2ToughWhenBattleStart.ToString(),new BuffData
                (
                    "2_33",
                    BuffDataName.Gain2ToughWhenBattleStart.ToString(),
                    "icon2_33",
                    new [] {"Self"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.Gain2ToughWhenBattleStart.ToString(),null,
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
                BuffDataName.Gain2VulnerableWhenBattleStart.ToString(),new BuffData
                (
                    "2_34",
                    BuffDataName.Gain2VulnerableWhenBattleStart.ToString(),
                    "icon2_34",
                    new [] {"Target"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.Gain2VulnerableWhenBattleStart.ToString(),null,
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
                BuffDataName.Gain2WeakWhenBattleStart.ToString(),new BuffData
                (
                    "2_35",
                    BuffDataName.Gain2WeakWhenBattleStart.ToString(),
                    "icon2_35",
                    new [] {"Target"},
                    5,
                    0,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.Gain2WeakWhenBattleStart.ToString(),null,
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
                    "3_13",
                    BuffDataName.Add1Reroll.ToString(),
                    "icon3_13",
                    new [] {"Self"},
                    5,
                    0,
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

#endregion*/

        };

    }

}

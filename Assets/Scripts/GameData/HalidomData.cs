using DesignerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DesignerScripts
{
    public enum HalidomName
    {
        Add1BattleDice,
        AllDicesIdAre5,
        Add3ValueIfResultIsEven,
        Add3ValueIfResultIsOdd,
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
             HalidomName.Add3ValueIfResultIsEven.ToString(),new HalidomObject(
             "1",
             HalidomName.Add3ValueIfResultIsEven.ToString(),
             "当骰子点数为偶数时，增加3点伤害",
            new List<BuffInfo>()
                {
                    new BuffInfo(BuffDataTable.buffData["Add3ValueIfResultIsEven"],null,null,1,false,null)
                })
            },
            {
             HalidomName.Add3ValueIfResultIsOdd.ToString(),
            new HalidomObject(
             "2",
            HalidomName.Add3ValueIfResultIsOdd.ToString(),
             "当骰子点数为奇数时，增加3点伤害",
            new List<BuffInfo>()
                {
                    new BuffInfo(BuffDataTable.buffData["Add3ValueIfResultIsOdd"],null,null,1,false,null)
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
        };
    }
}


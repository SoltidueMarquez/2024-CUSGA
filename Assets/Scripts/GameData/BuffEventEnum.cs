using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum onCreateEnum
{
    None,

    CheckForBleed,//8

    CheckForStrength,//9

    Get5MaxHealthWhenGain,//18

    Add1Reroll,

    GainHalfMoney,//13

    RecoverHalfHealthWhenGain,

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
    //稀有圣物
    Gain2NormalHalidomWhenGain,


}

public enum onRemoveEnum
{
    None,

}

public enum onRoundStartEnum
{
    None,
    BuffStackMinus1,
    EnergyStorage,
    Recover25HealthWhenHealthBelowHalf,
    Gain1DodgeWhenBattleStart,
    Gain1EnhanceWhenBattleStart,
    Gain2StrengthWhenBattleStart,
    Gain2ToughWhenBattleStart,
    Gain2VulnerableWhenBattleStart,
    Gain2WeakWhenBattleStart,

}

public enum onRoundEndEnum
{
    None,
    Bleed,
    Spirit,
    BuffStackMinus1,

}

public enum onBuffHitEnum
{
    None,
    Weak,
    Strength,
    Enhance,
    
    Anger,
    Add2ValueIfResultIsEven,
    Add2ValueIfResultIsOdd,
    Add3ValueIfResultBelow3,
    Add3ValueIfResultAbove4,
    Add1StackIfEnemyHaveBleed,
    EnhancePlayerStrength,
    
    Add90PercentAttackEvery9TimesUseDice,
    ReuseDiceWhenDiceIs1,
    Add2MoneyWhenDiceIs2,
    Recover5HealthWhenDiceIs3,
    Add1EnemyBleedStackWhenDiceIs4,
    Add1PlayerStrengthStackWhenDiceIs5,
    Add1PermanentValueWhenDiceIs6,
    Add4ValueIfResultIsEven,
    Add4ValueIfResultIsOdd,

    Add50PercentAttack,
    //稀有圣物buff
    Add1ValueWhenDiceBelow3,
}

public enum onBeHurtEnum
{
    None,
    Vulnerable,
    Tough,
    Dodge,
    Add50PercentAttackEvery3TimesLoseHealth,
    EnhanceEnemyVulnerability,
    //稀有圣物buff
    Hit3DamageWhenLoseHealth,
    GainDodgeWhenLoseHealth,
    GainStrengthWhenLoseHealth,

}

public enum onKillEnum
{
    None,
    Add4MoneyWhenBattleEnd,
    GainMoneyAfterBattle,
}
public enum onBeKillEnum
{
    None,
    RecoverHalfHealthWhenDie,

}
public enum onRollEnum
{
    None,

}
public enum onCastEnum
{
    None,
    LoseEnergy,
    Add1StackIfPlayerHaveStrength,

}
public enum onAddBuffEnum
{
    None,
    Add1StackIfEnemyHaveDebuff,
    Add1StackIfPlayerHavePositiveBuff,
}


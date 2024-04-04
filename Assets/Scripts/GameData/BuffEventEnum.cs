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
    Dodge,
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


}

public enum onBeHurtEnum
{
    None,
    Vulnerable,
    Tough,
    Add50PercentAttackEvery3TimesLoseHealth,
    EnhanceEnemyVulnerability,

}

public enum onKillEnum
{
    None,
    Add4MoneyWhenBattleEnd,
}
public enum onBeKillEnum
{
    None,

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


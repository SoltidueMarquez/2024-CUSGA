using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum onCreateEnum
{
    None,
    Add2ValueIfResultIsEven,//1
    Add2ValueIfResultIsOdd,//2
    Add3ValueIfResultBelow3,//5
    Add3ValueIfResultAbove4,//6
    GainAndChoose2DicesGiveRandomCoating,
    GainAndChoose2DicesGive1PermanentEnhance,

    Add1StackIfEnemyHaveBleed,//8
    CheckForBleed,//8
    Add1StackIfEnemyHaveDebuff,
    Add1StackIfPlayerHaveStrength,//9
    CheckForStrength,//9
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
}

public enum onRemoveEnum
{
    None,
    Add2ValueIfResultIsEven,//1
    Add2ValueIfResultIsOdd,//2
    Add3ValueIfResultBelow3,//5
    Add3ValueIfResultAbove4,//6
    GainAndChoose2DicesGiveRandomCoating,
    GainAndChoose2DicesGive1PermanentEnhance,

    Add1StackIfEnemyHaveBleed,//8
    CheckForBleed,//8
    Add1StackIfEnemyHaveDebuff,
    Add1StackIfPlayerHaveStrength,//9
    CheckForStrength,//9
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
}

public enum onRoundStartEnum
{
    None,
    Add2ValueIfResultIsEven,//1
    Add2ValueIfResultIsOdd,//2
    Add3ValueIfResultBelow3,//5
    Add3ValueIfResultAbove4,//6
    GainAndChoose2DicesGiveRandomCoating,
    GainAndChoose2DicesGive1PermanentEnhance,

    Add1StackIfEnemyHaveBleed,//8
    CheckForBleed,//8
    Add1StackIfEnemyHaveDebuff,
    Add1StackIfPlayerHaveStrength,//9
    CheckForStrength,//9
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
}

public enum onRoundEndEnum
{
    None,
    Add2ValueIfResultIsEven,//1
    Add2ValueIfResultIsOdd,//2
    Add3ValueIfResultBelow3,//5
    Add3ValueIfResultAbove4,//6
    GainAndChoose2DicesGiveRandomCoating,
    GainAndChoose2DicesGive1PermanentEnhance,

    Add1StackIfEnemyHaveBleed,//8
    CheckForBleed,//8
    Add1StackIfEnemyHaveDebuff,
    Add1StackIfPlayerHaveStrength,//9
    CheckForStrength,//9
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
}

public enum onBuffHitEnum
{
    None,
    Add2ValueIfResultIsEven,//1
    Add2ValueIfResultIsOdd,//2
    Add3ValueIfResultBelow3,//5
    Add3ValueIfResultAbove4,//6
    GainAndChoose2DicesGiveRandomCoating,
    GainAndChoose2DicesGive1PermanentEnhance,

    Add1StackIfEnemyHaveBleed,//8
    CheckForBleed,//8
    Add1StackIfEnemyHaveDebuff,
    Add1StackIfPlayerHaveStrength,//9
    CheckForStrength,//9
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
}

public enum onBeHurtEnum
{
    None,
    Add2ValueIfResultIsEven,//1
    Add2ValueIfResultIsOdd,//2
    Add3ValueIfResultBelow3,//5
    Add3ValueIfResultAbove4,//6
    GainAndChoose2DicesGiveRandomCoating,
    GainAndChoose2DicesGive1PermanentEnhance,

    Add1StackIfEnemyHaveBleed,//8
    CheckForBleed,//8
    Add1StackIfEnemyHaveDebuff,
    Add1StackIfPlayerHaveStrength,//9
    CheckForStrength,//9
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
}

public enum onKillEnum
{
    None,
    Add2ValueIfResultIsEven,//1
    Add2ValueIfResultIsOdd,//2
    Add3ValueIfResultBelow3,//5
    Add3ValueIfResultAbove4,//6
    GainAndChoose2DicesGiveRandomCoating,
    GainAndChoose2DicesGive1PermanentEnhance,

    Add1StackIfEnemyHaveBleed,//8
    CheckForBleed,//8
    Add1StackIfEnemyHaveDebuff,
    Add1StackIfPlayerHaveStrength,//9
    CheckForStrength,//9
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
}
public enum onBeKillEnum
{
    None,
    Add2ValueIfResultIsEven,//1
    Add2ValueIfResultIsOdd,//2
    Add3ValueIfResultBelow3,//5
    Add3ValueIfResultAbove4,//6
    GainAndChoose2DicesGiveRandomCoating,
    GainAndChoose2DicesGive1PermanentEnhance,

    Add1StackIfEnemyHaveBleed,//8
    CheckForBleed,//8
    Add1StackIfEnemyHaveDebuff,
    Add1StackIfPlayerHaveStrength,//9
    CheckForStrength,//9
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
}
public enum onRollEnum
{
    None,
    Add2ValueIfResultIsEven,//1
    Add2ValueIfResultIsOdd,//2
    Add3ValueIfResultBelow3,//5
    Add3ValueIfResultAbove4,//6
    GainAndChoose2DicesGiveRandomCoating,
    GainAndChoose2DicesGive1PermanentEnhance,

    Add1StackIfEnemyHaveBleed,//8
    CheckForBleed,//8
    Add1StackIfEnemyHaveDebuff,
    Add1StackIfPlayerHaveStrength,//9
    CheckForStrength,//9
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
}
public enum onCastEnum
{
    None,
    Add2ValueIfResultIsEven,//1
    Add2ValueIfResultIsOdd,//2
    Add3ValueIfResultBelow3,//5
    Add3ValueIfResultAbove4,//6
    GainAndChoose2DicesGiveRandomCoating,
    GainAndChoose2DicesGive1PermanentEnhance,

    Add1StackIfEnemyHaveBleed,//8
    CheckForBleed,//8
    Add1StackIfEnemyHaveDebuff,
    Add1StackIfPlayerHaveStrength,//9
    CheckForStrength,//9
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
}


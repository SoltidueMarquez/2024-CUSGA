using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BuffHandler))]
[RequireComponent(typeof(BattleDiceHandler))]
public class ChaState : MonoBehaviour
{
    [Header("需要的组件")]
    [SerializeField] private BuffHandler buffHandler;
    /// <summary>
    /// 负责玩家战斗的骰子管理器，其中包含了骰面的各种信息，骰子的面数，骰子的加成
    /// </summary>
    [SerializeField] private BattleDiceHandler battleDiceHandler;
    private void Awake()
    {
        buffHandler = GetComponent<BuffHandler>();
        battleDiceHandler = GetComponent<BattleDiceHandler>();
    }
    /// <summary>
    /// 角色的基础属性，每个角色不带任何buff的纯粹数值
    /// 先写死，正式的应该是从配置文件中读取
    /// </summary>
    public ChaProperty baseProp = new ChaProperty(
        200, 400, 4, 0
    );
    public ChaProperty[] buffProp = new ChaProperty[2] { ChaProperty.zero, ChaProperty.zero };

    public ChaProperty prop { get; private set; } = ChaProperty.zero;
    public ChaControlState controlState = new ChaControlState();
    //临时的变量，用于先简单的判断是否读档
    public bool ifExist;

    private void Start()
    {
        
    }

    private void AttrRecheck()
    {
        controlState.Origin();
        this.prop.Zero();
        for (var i = 0; i < buffProp.Length; i++)
        {
            buffProp[i].Zero();
        }
        buffHandler.RecheckBuff(buffProp,ref controlState);
        this.prop = (this.baseProp + buffProp[0]) * this.buffProp[1];
    }
    /// <summary>
    /// 判断能否被伤害信息杀死
    /// </summary>
    /// <param name="damageInfo"></param>
    /// <returns></returns>
    public bool CanBeKilledByDamageInfo(DamageInfo damageInfo)
    {
        //return damageInfo.damage >= this.prop.health;
        return false;
    }

    //TODO:其他的方法签名，方法的完善
}

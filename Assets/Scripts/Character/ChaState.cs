using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BuffHandler))]
[RequireComponent(typeof(BattleDiceHandler))]
public class ChaState : MonoBehaviour
{
    [Header("需要的组件")]
    [SerializeField] private BuffHandler buffHandler;
    [SerializeField] private BattleDiceHandler battleDiceHandler;
    private void Awake()
    {
        buffHandler = GetComponent<BuffHandler>();
    }
    /// <summary>
    /// 角色的基础属性，每个角色不带任何buff的纯粹数值
    /// 先写死，正式的应该是从配置文件中读取
    /// </summary>
    public ChaProperty baseProp = new ChaProperty(
        200, 400, 4, 0
    );

    public ChaProperty[] buffProp = new ChaProperty[2] { ChaProperty.zero, ChaProperty.zero };
    //临时的变量，用于先简单的判断是否读档
    /// <summary>
    /// 负责玩家战斗的骰子，其中包含了骰面的各种信息，骰子的面数，骰子的加成
    /// </summary>
    public bool ifExist;

    private void Start()
    {
        
    }
}

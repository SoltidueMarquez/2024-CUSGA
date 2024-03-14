using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BuffHandler))]
public class ChaState : MonoBehaviour
{
    [Header("需要的组件")]
    [SerializeField] private BuffHandler buffHandler;
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
    public bool ifExist;
}

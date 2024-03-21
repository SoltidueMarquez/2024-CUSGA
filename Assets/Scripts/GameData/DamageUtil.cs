using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DesignerScripts
{
    public class DamageUtil
    {
        /// <summary>
        /// 根据不同的等级获取基础伤害的数值
        /// </summary>
        /// <param name="level">骰面的等级</param>
        /// <returns></returns>
        public static int GetlevelBasedDamage(int level)
        {
            switch (level)
            {
                case 1:
                    return 6;
                case 2:
                    return 13;
                case 3:
                    return 26;
                default:
                    return 6;
            }
        }
        /// <summary>
        /// 根据不同的等级获取基础护盾的数值
        /// </summary>
        /// <param name="level">等级</param>
        /// <returns></returns>
        public static int GetIndexLevelBasedShield(int level)
        {
            switch (level)
            {
                case 1:
                    return 5;
                case 2:
                    return 11;
                case 3:
                    return 22;
                default:
                    return 5;
            }
        }
        /// <summary>
        /// 根据不同的等级获取基础治疗的数值
        /// </summary>
        /// <param name="level">骰面的等级</param>
        /// <returns></returns>
        public static int GetIndexLevelBasedHeal(int level)
        {
            switch (level)
            {
                case 1:
                    return 2;
                case 2:
                    return 5;
                case 3:
                    return 10;
                default:
                    return 2;
            }
        }
    }
}


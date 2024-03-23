using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 存储buff的各个回调点
/// </summary>
namespace DesignerScripts
{
    /// <summary>
    /// 
    /// </summary>
    public enum BuffEventName
    {

        Bleed,//流血

        Spirit,//精力

        Vulnerable,//易伤

        Tough,//坚韧

        Weak,//虚弱

        Strength,//力量

        Enhance,//强化

        BuffStackMinus1,//Buff层数减1
    }
    public class BuffEvents
    {
        public static Dictionary<string, OnBuffCreate> onCreateFunc = new Dictionary<string, OnBuffCreate>()
        {


        };
        public static Dictionary<string, OnBuffRemove> onRemoveFunc = new Dictionary<string, OnBuffRemove>();
        public static Dictionary<string, OnRoundStart> onRoundStartFunc = new Dictionary<string, OnRoundStart>()
        {
            {
                BuffEventName.BuffStackMinus1.ToString(),BuffStackMinus1
            },
        };
        public static Dictionary<string, OnRoundEnd> onRoundEndFunc = new Dictionary<string, OnRoundEnd>()
        {
            {
                BuffEventName.Bleed.ToString(),Bleed
            },
        }
            ;
        public static Dictionary<string, BuffOnHit> onBuffHitFunc = new Dictionary<string, BuffOnHit>()
        {
            {
                BuffEventName.Weak.ToString(),Weak
            },
            {
                BuffEventName.Strength.ToString(),Strength
            },
            {
                BuffEventName.Enhance.ToString(),Enhance
            }

        };
        public static Dictionary<string, BuffOnBeHurt> onBeHurtFunc = new Dictionary<string, BuffOnBeHurt>()
        {
            {
                BuffEventName.Vulnerable.ToString(),Vulnerable
            },
            {
                BuffEventName.Tough.ToString(),Tough
            },
        };
        public static Dictionary<string, BuffOnkill> onKillFunc = new Dictionary<string, BuffOnkill>();
        public static Dictionary<string, BuffOnBeKilled> onBeKillFunc = new Dictionary<string, BuffOnBeKilled>();

        public static Dictionary<string, BuffOnRoll> onRollFunc = new Dictionary<string, BuffOnRoll>();



        //流血效果，每回合造成2*层数的伤害（数值脚填）
        public static void Bleed(BuffInfo buffInfo)
        {
            int bleedDamage = 2 * buffInfo.curStack;
            //这边还没决定好，是直接扣血，还是调用伤害函数，因为可能在damageManager里会有接视觉表现，例如跳数字的效果，但是如果直接在这边扣血，那就需要在这边也调用视觉表现
            buffInfo.target.GetComponent<ChaState>().ModResources(new ChaResource(-bleedDamage, 0, 0, 0));
            Debug.Log("流血造成" + bleedDamage + "伤害");
        }

        public static void Vulnerable(BuffInfo buffInfo, DamageInfo damageInfo, GameObject attack)
        {
            if (damageInfo.diceType == DiceType.Attack)  
            {
                damageInfo.addDamageArea += 0.25f;
            }

        }


        public static void Tough(BuffInfo buffInfo, DamageInfo damageInfo, GameObject attack)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                damageInfo.addDamageArea -= 0.25f;
            }

        }

        public static void Weak(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {

                damageInfo.addDamageArea -= 0.25f;
            }
        }

        public static void Strength(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {

                damageInfo.addDamageArea += 0.25f;
            }
        }

        public static void Enhance(BuffInfo buffInfo, DamageInfo damageInfo, GameObject target)
        {
            if (damageInfo.diceType == DiceType.Attack)
            {
                damageInfo.addDamageArea += 0.5f;

            }
        }
        //层数--，如果层数为0，则移除buff
        public static void BuffStackMinus1(BuffInfo buffInfo)
        {
            buffInfo.target.GetComponent<ChaState>().RemoveBuff(buffInfo);
        }






    }



}

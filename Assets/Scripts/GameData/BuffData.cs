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
    }
    public class BuffDataTable
    {
        public static Dictionary<string, BuffData> buffData = new Dictionary<string, BuffData>()
        {
            { "test",new BuffData(
                //buff的唯一id
                "0",
                //buff的名字
                "TestBuff0",
                //存储buff的icon图标图片的名字（Resources文件夹下）
                "icon0",
                //用于方便检索的tag，例如：火焰，冰冻，中毒等
                null,
                //buff的最高层数
                3,
                //buff的持续回合数量
                2,
                //是否是永久的buff(这个的优先级更高）
                false,
            //buff更新层数时的策略（Add：增加回合，Replace：替换回合数为指定数，Keep：什么都不做）
            BuffUpdateEnum.Keep,
            //buff移除层数时的策略（Reduce：减少层数，Clear：清除所有层数）
            BuffRemoveStackUpdateEnum.Clear,
            "",null,//OnCreate回调点 ，回调点参数额外设置
            "",null,//OnRemove回调点 ，回调点参数额外设置
            "",null,//OnRoundStart回调点 ，回调点参数额外设置
            "",null,//OnRoundEnd回调点 ，回调点参数额外设置
            "",null,//OnHit回调点 ，回调点参数额外设置
            "",null,//OnBeHurt回调点 ，回调点参数额外设置
            "",null,//OnRoll回调点 ，回调点参数额外设置
            "",null,//OnKill回调点 ，回调点参数额外设置
            "",null,//OnBeKill回调点 ，回调点参数额外设置
            //buff对玩家的状态修改（无敌，能否出牌，局内骰面更改），
            ChaControlState.origin,
            //buff对玩家的属性(血量，金钱，护盾，重新投掷次数）修改
            null
            ) 
            },
            {
                BuffDataName.Bleed.ToString(),new BuffData
                (
                    "1",
                    BuffDataName.Bleed.ToString(),
                    "icon1",
                    new [] {"Target"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    "",null,
                    BuffEventName.Bleed.ToString(),null,
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
                BuffDataName.Spirit.ToString(),new BuffData
                (
                    "2",
                    BuffDataName.Spirit.ToString(),
                    "icon2",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.BuffStackMinus1.ToString(),null,
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
                BuffDataName.Vulnerable.ToString(),new BuffData
                (
                    "3",
                    BuffDataName.Vulnerable.ToString(),
                    "icon3",
                    new [] {"Target"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.BuffStackMinus1.ToString(),null,
                    "",null,
                    "",null,
                    BuffEventName.Vulnerable.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

            },

            {
                BuffDataName.Tough.ToString(),new BuffData
                (
                    "4",
                    BuffDataName.Tough.ToString(),
                    "icon4",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.BuffStackMinus1.ToString(),null,
                    "",null,
                    "",null,
                    BuffEventName.Tough.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

            },

            {
                BuffDataName.Weak.ToString(),new BuffData
                (
                    "5",
                    BuffDataName.Weak.ToString(),
                    "icon5",
                    new []{"Target"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.BuffStackMinus1.ToString(),null,
                    "",null,
                    BuffEventName.Weak.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

            },

             {
                BuffDataName.Strength.ToString(),new BuffData
                (
                    "6",
                    BuffDataName.Strength.ToString(),
                    "icon6",
                    new []{"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.BuffStackMinus1.ToString(),null,
                    "",null,
                    BuffEventName.Strength.ToString(),null,
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
                    "7",
                    BuffDataName.Enhance.ToString(),
                    "icon7",
                    new [] {"Self"},
                    5,
                    100,
                    true,
                    BuffUpdateEnum.Add,
                    BuffRemoveStackUpdateEnum.Reduce,
                    "",null,
                    "",null,
                    BuffEventName.BuffStackMinus1.ToString(),null,
                    "",null,
                    BuffEventName.Enhance.ToString(),null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                 )

             },



        };

    }

}

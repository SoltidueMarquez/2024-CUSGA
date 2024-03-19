using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DesignerScripts
{
    public class BuffDataTable
    {
        public static Dictionary<string, BuffData> buffData = new Dictionary<string, BuffData>()
        {
            { "test",new BuffData(
                //buff的唯一id
                "1",
                //buff的名字
                "TestBuff1",
                //存储buff的icon图标图片的名字（Resources文件夹下）
                "icon1",
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
            //OnCreate回调点 ，回调点参数额外设置
            "test1",null,
            //OnRemove回调点 ，回调点参数额外设置
            "",null,
            //OnRoundStart回调点 ，回调点参数额外设置
            "",null,
            //OnRoundEnd回调点 ，回调点参数额外设置
            "",null,
            //OnHit回调点 ，回调点参数额外设置
            "",null,
            //OnBeHurt回调点 ，回调点参数额外设置
            "",null,
            //OnRoll回调点 ，回调点参数额外设置
            "",null,
            //OnKill回调点 ，回调点参数额外设置
            "",null,
            //OnBeKill回调点 ，回调点参数额外设置
            "",null,
            //buff对玩家的状态修改（无敌，能否出牌，局内骰面更改），
            ChaControlState.origin,
            //buff对玩家的属性(血量，金钱，护盾，重新投掷次数）修改
            null
            ) 
            },


            {
                "CheckMoneyAddHealth",new BuffData(
                    "2",
                    "CheckMoneyAddHealth",
                    "icon2",
                    null,
                    1,
                    10,
                    false,
                    BuffUpdateEnum.Keep,
                    BuffRemoveStackUpdateEnum.Clear,
                    "",null,
                    "",null,
                    "",null,
                    "CheckMoneyAddHealth",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    "",null,
                    ChaControlState.origin,
                    null
                    )
            }

        };

    }

}

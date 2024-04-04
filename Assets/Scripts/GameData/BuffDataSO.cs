using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static OneLine.Examples.ComplexExample;

namespace DesignerScripts
{
    [CreateAssetMenu(fileName = "BuffData_", menuName = "Data/BuffData")]
    public class BuffDataSO : ScriptableObject
    {
        [Tooltip("buff的唯一id")]
        public string id;

        [Tooltip("buff的名字")]
        public BuffDataName dataName;

        //[Tooltip("存储buff的icon图标图片的名字（Resources文件夹下）")]
        //public string icon;

        [Tooltip("用于方便检索的tag，例如：火焰，冰冻，中毒等")]
        public string[] tags;

        [Tooltip("buff的最高层数")]
        public int maxStack;

        [Tooltip("buff的持续回合数量")]
        public int duringCount;

        [Tooltip("是否是永久的buff(这个的优先级更高）")]
        public bool isPermanent;

        [Tooltip("buff更新层数时的策略（Add：增加回合，Replace：替换回合数为指定数，Keep：什么都不做）")]
        public BuffUpdateEnum buffUpdateEnum;

        [Tooltip("buff移除层数时的策略（Reduce：减少层数，Clear：清除所有层数）")]
        public BuffRemoveStackUpdateEnum removeStackUpdateEnum;

        #region ------回调点------
        [Tooltip("OnCreate回调点 ，回调点参数额外设置")]
        public onCreateEnum onCreate = onCreateEnum.None;
        [Tooltip("")]
        public object[] onCreateParams = null;

        [Tooltip("")]
        public onRemoveEnum onRemove = onRemoveEnum.None;
        [Tooltip("")]
        public object[] onRemoveParams = null;

        [Tooltip("")]
        public onRoundEndEnum onRoundStart = onRoundEndEnum.None;
        [Tooltip("")]
        public object[] onRoundStartParams = null;

        [Tooltip("")]
        public onRoundEndEnum onRoundEnd = onRoundEndEnum.None;
        [Tooltip("")]
        public object[] onRoundEndParams = null;

        [Tooltip("")]
        public onBuffHitEnum onHit = onBuffHitEnum.None;
        [Tooltip("")]
        public object[] onHitParams = null;

        [Tooltip("")]
        public onBeHurtEnum onBeHurt = onBeHurtEnum.None;
        [Tooltip("")]
        public object[] onBeHurtParams = null;

        [Tooltip("")]
        public onRollEnum onRoll = onRollEnum.None;
        [Tooltip("")]
        public object[] onRollParams = null;

        [Tooltip("")]
        public onKillEnum onKill = onKillEnum.None;
        [Tooltip("")]
        public object[] onKillParams = null;

        [Tooltip("")]
        public onBeKillEnum onBeKilled = onBeKillEnum.None;
        [Tooltip("")]
        public object[] onBeKilledParams = null;

        [Tooltip("")]
        public onCastEnum onCast = onCastEnum.None;
        [Tooltip("")]
        public object[] onCastParams = null;

        #endregion

        [Tooltip("buff对玩家的状态修改(默认：T，T，F)")]
        public ChaControlState stateMod = ChaControlState.origin;

        [Tooltip("buff对玩家的属性修改")]
        public ChaProperty[] propMod = null;
    }
}
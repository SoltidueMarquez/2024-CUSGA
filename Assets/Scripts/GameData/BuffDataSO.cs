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
        [Tooltip("buff��Ψһid")]
        public string id;

        [Tooltip("buff������")]
        public BuffDataName dataName;

        //[Tooltip("�洢buff��iconͼ��ͼƬ�����֣�Resources�ļ����£�")]
        //public string icon;

        [Tooltip("���ڷ��������tag�����磺���棬�������ж���")]
        public string[] tags;

        [Tooltip("buff����߲���")]
        public int maxStack;

        [Tooltip("buff�ĳ����غ�����")]
        public int duringCount;

        [Tooltip("�Ƿ������õ�buff(��������ȼ����ߣ�")]
        public bool isPermanent;

        [Tooltip("buff���²���ʱ�Ĳ��ԣ�Add�����ӻغϣ�Replace���滻�غ���Ϊָ������Keep��ʲô��������")]
        public BuffUpdateEnum buffUpdateEnum;

        [Tooltip("buff�Ƴ�����ʱ�Ĳ��ԣ�Reduce�����ٲ�����Clear��������в�����")]
        public BuffRemoveStackUpdateEnum removeStackUpdateEnum;

        #region ------�ص���------
        [Tooltip("OnCreate�ص��� ���ص��������������")]
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

        [Tooltip("buff����ҵ�״̬�޸�(Ĭ�ϣ�T��T��F)")]
        public ChaControlState stateMod = ChaControlState.origin;

        [Tooltip("buff����ҵ������޸�")]
        public ChaProperty[] propMod = null;
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static OneLine.Examples.ComplexExample;
using static UnityEditor.Progress;

namespace DesignerScripts
{
    /// <summary>
    /// ��������
    /// </summary>
    public enum paramsType
    {
        intType,
        floatType,
        stringType,
        boolType,
        vector3Type,
        vector2Type
    }
    [Serializable]
    public struct Param
    {
        public paramsType type;
        public string name;
        public string value;
    }
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
        public onRoundStartEnum onRoundStart = onRoundStartEnum.None;
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
        [Tooltip("buffInfo��Ҫ�Ķ������")]
        public List<Param> paramList;
        [Tooltip("buffЧ������")]
        [Multiline(5)]
        public string description;
        [Tooltip("buffSprite")]
        public Sprite sprite;

        public static Dictionary<string,System.Object> GetParamDic(List<Param> paramList)
        {
            Dictionary<string, System.Object> dict = new Dictionary<string, System.Object>();
            foreach (var param in paramList)
            {
                switch (param.type)
                {
                    case paramsType.intType:
                        dynamic temp = int.Parse(param.value);
                        dict.Add(param.name, temp);
                        break;
                    case paramsType.floatType:
                        dynamic temp2 = float.Parse(param.value);
                        dict.Add(param.name, temp2);
                        break;
                    case paramsType.stringType:
                        dynamic temp3 = param.value;
                        dict.Add(param.name, temp3);
                        break;
                    case paramsType.boolType:
                        dynamic temp4 = bool.Parse(param.value);
                        dict.Add(param.name, temp4);
                        break;

                }
            }
            return dict;
        }
    }
}
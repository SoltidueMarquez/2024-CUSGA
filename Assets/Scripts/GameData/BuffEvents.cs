using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 存储buff的各个回调点
/// </summary>
namespace DesignerScripts
{
    public class BuffEvents
    {
        public static Dictionary<string, OnBuffCreate> onCreateFunc = new Dictionary<string, OnBuffCreate>()
        {
            {"test1",TestShow },
            {"test2",TestShow }

        };
        public static Dictionary<string, OnBuffRemove> onRemoveFunc = new Dictionary<string, OnBuffRemove>();
        public static Dictionary<string, OnRoundStart> onRoundStartFunc = new Dictionary<string, OnRoundStart>();
        public static Dictionary<string, OnRoundEnd> onRoundEndFunc = new Dictionary<string, OnRoundEnd>() 
        {
            {"CheckMoneyAddHealth" ,CheckMoneyOnRoundEnd},
        }
            ;
        public static Dictionary<string, BuffOnHit> onBuffHitFunc = new Dictionary<string, BuffOnHit>();
        public static Dictionary<string, BuffOnBeHurt> onBeHurtFuc = new Dictionary<string, BuffOnBeHurt>();
        public static Dictionary<string, BuffOnkill> onKillFunc = new Dictionary<string, BuffOnkill>();
        public static Dictionary<string, BuffOnBeKilled> onBeKillFunc = new Dictionary<string, BuffOnBeKilled>();

        public static Dictionary<string, BuffOnRoll> onRollFunc = new Dictionary<string, BuffOnRoll>();


        public static void TestShow(BuffInfo buffInfo)
        {
            Debug.Log("在回合开始的时候触发的东西触发了");
        }
        public static void TestonRemove(BuffInfo buffInfo)
        {
            Debug.Log("在回合结束的时候触发的东西触发了");
        }
        public static void TestOnRoundStart(BuffInfo buffInfo)
        {
            Debug.Log("回合开始的时候触发的东西触发了");
        }
        public static void TestOnRoundEnd(BuffInfo buffInfo)
        {
            Debug.Log("回合结束的时候触发的东西触发了");
        }

        

        public static void CheckMoneyOnRoundEnd(BuffInfo buffInfo)
        {
            //访问Player（即PlayerController）
            ChaState tempChaState = buffInfo.creator.GetComponent<ChaState>();
            //访问当前的资源
            if (tempChaState.resource.currentMoney > 0)
            {
                tempChaState.ModResources(new ChaResource(-4,0,0,0));
                Debug.Log(tempChaState.resource.currentHp);
            }
        }
    }



}

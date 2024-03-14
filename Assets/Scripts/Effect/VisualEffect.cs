using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public delegate void visualEffectEvent(params object[] args);
public class VisualEffect
{
    public object[] args;
    public GameObject creator;
    public string effectName;
    public visualEffectEvent OnDicePlay;//包含了特效和数字
    //可能需要的属性
    public float duration;

    public VisualEffect(string effectName, GameObject creator, visualEffectEvent OnDicePlay, float duration, params object[] args)
    {
        this.effectName = effectName;
        this.creator = creator;
        this.OnDicePlay = OnDicePlay;
        this.duration = duration;
        this.args = args;
    }
}

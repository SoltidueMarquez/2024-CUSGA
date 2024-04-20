using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SceneTransitionMode
{
    //场景过渡模式
    None,
    Fade,
    CrossFade,
    Circle,
    Radial,
    Wipe,
    Custom
}
[CreateAssetMenu(fileName ="SceneLoadConfig",menuName ="SceneLoader/SceneLoadConfig")]
public class SceneLoadConfig : ScriptableObject
{
    
}

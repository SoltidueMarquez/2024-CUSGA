using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SceneTransitionMode
{
    //��������ģʽ
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

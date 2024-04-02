using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 统一资源读取
/// </summary>
public enum SpriteType
{
    Buff,
    SingleDice,
    Intention,
    Halidom
}
public static class ResourcesManager
{
    //根据名字加载图片资源
    public static Sprite GetSprite(SpriteType type, string name)
    {
        string path = "";
        switch (type)
        {
            case SpriteType.Buff:
                path = "Buff/" + name;
                break;
            case SpriteType.SingleDice:
                path = "SingleDice/" + name;
                break;
            case SpriteType.Intention:
                path = "Intention/" + name;
                break;
            case SpriteType.Halidom:
                path = "Halidom/" + name;
                break;
        }
        return Resources.Load<Sprite>(path);
    }


}

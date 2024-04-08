using UnityEngine;

namespace Frame.Core
{
    /// <summary>
    /// PersistentSingleton为持续泛型单例父类，使脚本挂载的物体不随场景改变而销毁，并且使其他脚本更容易调用本脚本内容。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        public static T instance { get; private set; }
 
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
 
            DontDestroyOnLoad(gameObject);
        }
    }
}

using UnityEngine;

namespace Frame.Core
{
    /// <summary>
    /// PersistentSingleton为持续泛型单例父类，使脚本挂载的物体不随场景改变而销毁，并且使其他脚本更容易调用本脚本内容。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }
 
        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
 
            DontDestroyOnLoad(gameObject);
        }
    }
}

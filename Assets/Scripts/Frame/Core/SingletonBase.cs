using UnityEngine;

namespace Frame.Core
{
    public class SingletonBase<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T m_instance;
        private static object m_instanceLock = new object();

        protected virtual void Awake()
        {
            if (m_instance == null)
            {
                m_instance = this as T;
            }
            else if (m_instance != this)
            {
                Destroy(gameObject);
            }
        }

        public static T Instance
        {
            get
            {
                lock (m_instanceLock)
                {

                    if (m_instance == null)
                    {
                        //单例是否已经存在于场景
                        m_instance = FindObjectOfType<T>();

                        if (FindObjectsOfType<T>().Length > 1)
                        {
                            return m_instance;
                        }

                        //不存在则创建一个
                        if (m_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            m_instance = singleton.AddComponent<T>();
                            singleton.name = "singleton " + typeof(T).ToString();

                            DontDestroyOnLoad(singleton);
                        }
                    }


                    return m_instance;
                }

            }
        }

        protected virtual void OnDestroy()
        {
            m_instance = null;
        }
    }
}

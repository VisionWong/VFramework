using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFramework
{
    /// <summary>
    /// 继承Mono的单例，场景加载时不会销毁
    /// </summary>
    /// <typeparam name="T"></typeparam>
	public abstract class MonoSingleton<T> : MonoBehaviour where T: MonoSingleton<T>
    {
        protected static T _instance = null;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = GameObject.Find("DDOLRoot");
                    if (go == null)
                    {
                        go = new GameObject("DDOLRoot");
                    }
                    _instance = go.AddComponent<T>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        protected MonoSingleton()
        {
            if (_instance != null)
            {
                throw new SingletonException(string.Format("This {0} Singleton Instance is not null!", typeof(T).ToString()));
            }
        }

        private void OnApplicationQuit()
        {
            _instance = null;
        }
    }
}

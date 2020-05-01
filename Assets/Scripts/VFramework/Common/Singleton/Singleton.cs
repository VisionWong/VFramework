using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFramework
{
	/// <summary>
	/// 单例模式基类
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public abstract class Singleton<T> where T : class, new()
	{
	    protected static T _instance = null;
	
	    public static T Instance
	    {
	        get
	        {
	            if (_instance == null)
	            {
	                _instance = new T();
	            }
	            return _instance;
	        }
	    }
	
	    protected Singleton()
	    {
	        if (_instance != null)
            {
                throw new SingletonException(string.Format("This {0} Singleton Instance is not null!", typeof(T).ToString()));
            }
            Init();
	    }

        public virtual void Init() { }
	}
}

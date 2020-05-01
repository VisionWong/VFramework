using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFramework
{
    /// <summary>
    /// GameObject缓存池
    /// </summary>
	public class PoolMgr : MonoSingleton<PoolMgr>
	{
        private Dictionary<string, PoolData> _poolDict = new Dictionary<string, PoolData>();

        private GameObject _poolRoot = null;//缓存池根结点

        /// <summary>
        /// 从池中获取物体，若没有则异步加载，之后执行回调函数
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public void GetObj(string path, Action<GameObject> callback)
        {
            //若池子里有该物体
            if (_poolDict.ContainsKey(path) && _poolDict[path].Count > 0)
            {
                callback(_poolDict[path].Pop());
            }
            else
            {
                ResourceMgr.Instance.LoadAsync<GameObject>(path, obj =>
                {
                    //让对象名字和父结点名字一致
                    obj.name = path;
                    //执行回调
                    callback(obj);
                });
            }
        }
        
        /// <summary>
        /// 将物体返还缓存池
        /// </summary>
        /// <param name="name"></param>
        /// <param name="poolObj"></param>
        public void PushObj(GameObject poolObj)
        {
            string name = poolObj.name;

            if (_poolRoot == null)
            {
                _poolRoot = new GameObject("PoolRoot");
            }
         
            if (_poolDict.ContainsKey(name))
            {
                _poolDict[name].Push(poolObj);
            }
            else
            {
                //若不存在该池,则新建一个
                _poolDict.Add(name, new PoolData(poolObj, _poolRoot));
            }
        }
	}
}

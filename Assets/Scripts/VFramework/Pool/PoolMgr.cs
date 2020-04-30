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
        /// 从池中获取物体，若没有则实例化返回
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameObject GetObj(string name)
        {
            GameObject go = null;

            //若池子里有该物体
            if (_poolDict.ContainsKey(name) && _poolDict[name].Count > 0)
            {
                go = _poolDict[name].Pop();
            }
            else
            {
                go = GameObject.Instantiate(Resources.Load<GameObject>(name));
                //让对象名字和父结点名字一致
                go.name = name;
            }
            return go;
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

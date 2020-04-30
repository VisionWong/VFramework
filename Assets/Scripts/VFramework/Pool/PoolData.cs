using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFramework
{
    /// <summary>
    /// 缓存池数据，包含父结点和同名物体缓存栈
    /// </summary>
	public class PoolData
	{
        private GameObject _parentObj;
        private Stack<GameObject> _poolStack;

        /// <summary>
        /// 缓存池物体的数量
        /// </summary>
        public int Count
        {
            get
            {
                return _poolStack.Count;
            }
        }

        public PoolData(GameObject poolObj, GameObject poolRoot)
        {
            _parentObj = new GameObject(poolObj.name);
            _parentObj.transform.SetParent(poolRoot.transform);

            _poolStack = new Stack<GameObject>();
            Push(poolObj);
        }

        public GameObject Pop()
        {
            //出栈
            GameObject go = _poolStack.Pop();
            //移出父物体
            go.transform.parent = null;
            //显示
            go.SetActive(true);
            return go;
        }

        public void Push(GameObject poolObj)
        {
            //入栈
            _poolStack.Push(poolObj);
            //设置父物体
            poolObj.transform.SetParent(_parentObj.transform);
            //隐藏
            poolObj.SetActive(false);
        }
	}
}

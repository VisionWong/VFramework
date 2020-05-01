using System.Collections;
using UnityEngine;

namespace VFramework
{
    /// <summary>
    /// 资源加载管理器，AB包的尚未完善
    /// </summary>
	public class ResourceMgr : Singleton<ResourceMgr>
	{
        /// <summary>
        /// 同步加载资源，若为GameObject，则实例化后返回
        /// </summary>
        public T Load<T>(string path) where T : Object
        {
            T obj = Resources.Load<T>(path);
            if (obj is GameObject)
            {
                return GameObject.Instantiate(obj);
            }
            else
            {
                return obj;
            }
        }


        /// <summary>
        /// 异步加载，加载完后执行回调函数,如果是GameObject，则会先实例化
        /// </summary>
        public void LoadAsync<T>(string path, System.Action<T> callback = null) where T : Object
        {
            MonoMgr.Instance.StartCoroutine(ReallyLoadAsync(path, callback));
        }

        private IEnumerator ReallyLoadAsync<T>(string path, System.Action<T> callback) where T : Object
        {
            ResourceRequest rr = Resources.LoadAsync<T>(path);
            yield return rr;
            if (rr.asset is GameObject)
            {
                callback?.Invoke(GameObject.Instantiate(rr.asset) as T);
            }
            else
            {
                callback?.Invoke(rr.asset as T);
            }
            
        }
    }
}

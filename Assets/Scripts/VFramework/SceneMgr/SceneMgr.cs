using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace VFramework
{
    /// <summary>
    /// 场景管理器，负责同步异步加载场景
    /// </summary>
	public class SceneMgr : Singleton<SceneMgr>
	{
        /// <summary>
        /// 同步加载场景，加载完后执行fun
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fun"></param>
	    public void LoadScene(string name, Action fun)
        {
            SceneManager.LoadScene(name);
            fun?.Invoke();
        }

        /// <summary>
        /// 异步加载场景，加载完执行回调函数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        public void LoadSceneAsync(string name, Action callback)
        {
            MonoMgr.Instance.StartCoroutine(ReallyLoadSceneAsync(name, callback));          
        }

        private IEnumerator ReallyLoadSceneAsync(string name, Action callback)
        {
            AsyncOperation ao = SceneManager.LoadSceneAsync(name);
            while (!ao.isDone)
            {
                //可通过消息中心发布事件，使得外界能够获取progress
                yield return ao.progress;
            }
            callback?.Invoke();
        }
    }
}

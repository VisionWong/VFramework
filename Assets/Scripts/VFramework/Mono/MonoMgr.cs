using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Internal;

namespace VFramework
{
	/// <summary>
	/// 公共mono管理器，让不继承mono的类也能执行帧更新和协程
	/// </summary>
	public class MonoMgr : MonoSingleton<MonoMgr>
	{
	    private event Action updateEvent = null;

        private void Update()
        {
            if (updateEvent != null)
            {
                updateEvent();
            }
        }

        #region 帧更新
        public void AddUpdateListener(Action update)
        {
            updateEvent += update;
        }

        public void RemoveUpdateListener(Action update)
        {
            updateEvent -= update;
        }
        #endregion

        #region 协程
        public new Coroutine StartCoroutine(string methodName)
        {
            return base.StartCoroutine(methodName);
        }

        public new Coroutine StartCoroutine(IEnumerator routine)
        {
            return base.StartCoroutine(routine);
        }

        public new Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
        {
            return base.StartCoroutine(methodName, value);
        }

        public new void StopAllCoroutines()
        {
            base.StopAllCoroutines();
        }
 
        public new void StopCoroutine(IEnumerator routine)
        {
            base.StopCoroutine(routine);
        }

        public new void StopCoroutine(Coroutine routine)
        {
            base.StopCoroutine(routine);
        }

        public new void StopCoroutine(string methodName)
        {
            base.StopCoroutine(methodName);
        }
        #endregion

        #region 延时方法Invoke
        #endregion
    }  
}

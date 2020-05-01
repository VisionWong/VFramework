using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VFramework
{
    /// <summary>
    /// 输入管理器，统一管理输入的检测
    /// </summary>
	public class InputMgr : Singleton<InputMgr>
	{
        private bool _isStart = false;

        public override void Init()
        {
            MonoMgr.Instance.AddUpdateListener(Update);
        }

        public void StartOrEndCheck(bool isStart)
        {
            _isStart = isStart;
        }

        private void Update()
        {
            if (!_isStart)
                return;
            CheckKey(KeyCode.W);
            CheckKey(KeyCode.S);
            CheckKey(KeyCode.A);
            CheckKey(KeyCode.D);
        }

        private void CheckKey(KeyCode key)
        {
            if (Input.GetKeyDown(key))
            {
                MessageCenter.Instance.Broadcast(MessageType.OnKeyUp, key);
            }
            if (Input.GetKeyUp(key))
            {
                MessageCenter.Instance.Broadcast(MessageType.OnKeyDown, key);
            }
        }
    }
}

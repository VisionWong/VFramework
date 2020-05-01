using System;
using System.Collections.Generic;

namespace VFramework
{
    /// <summary>
    /// 全局事件中心，销毁时记得移除监听
    /// </summary>
    public class MessageCenter : Singleton<MessageCenter>
    {
        /// <summary>
        /// 事件表
        /// </summary>
        private Dictionary<MessageType, Delegate> _eventDict = new Dictionary<MessageType, Delegate>();

        #region 添加监听
        public void AddListener(MessageType eventType, Action callBack)
        {
            //安全校验
            BeforeAddListener(eventType, callBack);
            //注册事件
            _eventDict[eventType] = (Action)_eventDict[eventType] + callBack;
        }

        public void AddListener<T>(MessageType eventType, Action<T> callBack)
        {
            //安全校验
            BeforeAddListener(eventType, callBack);
            //注册事件
            _eventDict[eventType] = (Action<T>)_eventDict[eventType] + callBack;
        }

        public void AddListener<T1, T2>(MessageType eventType, Action<T1, T2> callBack)
        {
            //安全校验
            BeforeAddListener(eventType, callBack);
            //注册事件
            _eventDict[eventType] = (Action<T1, T2>)_eventDict[eventType] + callBack;
        }

        #endregion

        #region 移除监听
        public void RemoveListener(MessageType eventType, Action callBack)
        {
            BeforeRemoveListener(eventType, callBack);
            _eventDict[eventType] = (Action)_eventDict[eventType] - callBack;
            AfterListenerRemoved(eventType);
        }

        public void RemoveListener<T>(MessageType eventType, Action<T> callBack)
        {
            BeforeRemoveListener(eventType, callBack);
            _eventDict[eventType] = (Action<T>)_eventDict[eventType] - callBack;
            AfterListenerRemoved(eventType);
        }

        public void RemoveListener<T1, T2>(MessageType eventType, Action<T1, T2> callBack)
        {
            BeforeRemoveListener(eventType, callBack);
            _eventDict[eventType] = (Action<T1, T2>)_eventDict[eventType] - callBack;
            AfterListenerRemoved(eventType);
        }

        #endregion

        #region 事件广播
        public void Broadcast(MessageType eventType)
        {
            Delegate dlg = null;
            if (_eventDict.TryGetValue(eventType, out dlg))
            {
                Action callBack = dlg as Action;//若强转失败则委托类型不同
                if (callBack != null)
                {
                    callBack();
                }
                else
                {
                    throw new Exception(string.Format("广播错误：事件{0}存在不同委托类型", eventType));
                }
            }
            else
            {
                throw new Exception(string.Format("广播错误：事件表中不存在事件{0}", eventType));
            }
        }

        public void Broadcast<T>(MessageType eventType, T arg)
        {
            Delegate dlg = null;
            if (_eventDict.TryGetValue(eventType, out dlg))
            {
                Action<T> callBack = dlg as Action<T>;//若强转失败则委托类型不同
                if (callBack != null)
                {
                    callBack(arg);
                }
                else
                {
                    throw new Exception(string.Format("广播错误：事件{0}存在不同委托类型", eventType));
                }
            }
            else
            {
                throw new Exception(string.Format("广播错误：事件表中不存在事件{0}", eventType));
            }
        }

        public void Broadcast<T1, T2>(MessageType eventType, T1 arg1, T2 arg2)
        {
            Delegate dlg = null;
            if (_eventDict.TryGetValue(eventType, out dlg))
            {
                Action<T1, T2> callBack = dlg as Action<T1, T2>;//若强转失败则委托类型不同
                if (callBack != null)
                {
                    callBack(arg1, arg2);
                }
                else
                {
                    throw new Exception(string.Format("广播错误：事件{0}存在不同委托类型", eventType));
                }
            }
            else
            {
                throw new Exception(string.Format("广播错误：事件表中不存在事件{0}", eventType));
            }
        }
        #endregion

        #region 安全校验
        /// <summary>
        /// 添加监听前的安全校验
        /// </summary>
        private void BeforeAddListener(MessageType eventType, Delegate callBack)
        {
            //若字典中不存在该事件，则添加
            if (!_eventDict.ContainsKey(eventType))
            {
                _eventDict.Add(eventType, null);
            }
            Delegate dlg = _eventDict[eventType];
            //若返回的委托与参数的委托类型不同，则抛出异常
            if (dlg != null && dlg.GetType() != callBack.GetType())
            {
                throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托为{1}，要添加的委托类型为{2}",
                    eventType, dlg.GetType(), callBack.GetType()));
            }
        }

        /// <summary>
        /// 移除监听前的安全校验
        /// </summary>
        private void BeforeRemoveListener(MessageType eventType, Delegate callBack)
        {
            if (_eventDict.ContainsKey(eventType))
            {
                Delegate dlg = _eventDict[eventType];
                if (dlg == null)
                {
                    throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托", eventType));
                }
                else if (dlg.GetType() != callBack.GetType())
                {
                    throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，要移除的委托类型为{1}，事件对应的委托类型为{2}",
                        eventType, callBack.GetType(), dlg.GetType()));
                }
            }
            else
            {
                throw new Exception(string.Format("移除监听错误：没有事件码{0}", eventType));
            }
        }

        /// <summary>
        /// 移除监听后更新事件表
        /// </summary>
        /// <param name="eventType"></param>
        private void AfterListenerRemoved(MessageType eventType)
        {
            if (_eventDict[eventType] == null)
            {
                _eventDict.Remove(eventType);
            }
        }
        #endregion
    }
}

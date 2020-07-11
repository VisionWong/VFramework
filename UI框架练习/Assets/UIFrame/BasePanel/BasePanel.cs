using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    /// <summary>
    /// 当界面进来
    /// </summary>
    public virtual void OnEnter()
    {
        
    }

    /// <summary>
    /// 当界面退出
    /// </summary>
    public virtual void OnExit()
    {

    }

    /// <summary>
    /// 当界面暂停时
    /// </summary>
    public virtual void OnPause()
    {

    }

    /// <summary>
    /// 当界面重新启用
    /// </summary>
    public virtual void OnResume()
    {

    }
}

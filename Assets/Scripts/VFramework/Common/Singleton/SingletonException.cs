using System;

namespace VFramework
{
	/// <summary>
	/// 单例模式异常类
	/// </summary>
	public class SingletonException : Exception
	{
	    public SingletonException(string msg) : base(msg)
	    {
	
	    }
	}
}

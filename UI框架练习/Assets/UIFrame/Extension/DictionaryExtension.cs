using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 字典拓展方法类
/// </summary>
public static class DictionaryExtension
{
    /// <summary>
    /// 根据key直接从字典返回一个值，若无此值则返回null
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    /// <typeparam name="Tvalue"></typeparam>
    /// <param name="dict"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static Tvalue TryGet<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)
    {
        Tvalue value;
        dict.TryGetValue(key, out value);
        return value;
    }
}

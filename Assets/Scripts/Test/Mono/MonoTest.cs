using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFramework;

public class MonoTest : MonoBehaviour
{
    void Start()
    {
        Test test = new Test();
    }
}

public class Test
{
    public Test()
    {
        MonoMgr.Instance.AddUpdateListener(Update);
        MonoMgr.Instance.StartCoroutine(Fun());
    }

    public void Update()
    {
        Debug.Log(1);
    }

    public IEnumerator Fun()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log(3);
    }
}

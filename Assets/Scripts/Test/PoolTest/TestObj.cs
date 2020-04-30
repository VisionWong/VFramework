using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFramework;

public class TestObj : MonoBehaviour
{
    private float time = 0;

    private void Update()
    {
        time += Time.deltaTime;
        if (time > 1)
        {
            PoolMgr.Instance.PushObj(gameObject);
            time = 0;
        }
    }
}

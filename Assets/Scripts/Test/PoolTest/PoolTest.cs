using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFramework;

public class PoolTest : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PoolMgr.Instance.GetObj("Cube", o => {
                o.transform.localScale = Vector3.one * 2;
            });
        }
        else if (Input.GetMouseButtonDown(1))
        {
            PoolMgr.Instance.GetObj("Ball", o =>
            {
                o.transform.localScale = Vector3.one * 2;
            });
        }
        else if (Input.GetMouseButtonDown(2))
        {
            PoolMgr.Instance.GetObj("asdasd", o =>
            {
                o.transform.localScale = Vector3.one * 2;
            });
        }
    }
}

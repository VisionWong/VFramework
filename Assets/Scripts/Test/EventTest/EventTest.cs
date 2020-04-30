using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFramework;

public class EventTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            MessageCenter.Instance.Broadcast<string>(MessageType.MonsterDead, "怪物死了");
        }
    }
}

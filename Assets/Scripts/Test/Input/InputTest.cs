using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFramework;

public class InputTest : MonoBehaviour
{
    private void Start()
    {
        InputMgr.Instance.StartOrEndCheck(true);

        MessageCenter.Instance.AddListener<KeyCode>(MessageType.OnKeyUp, OnKeyUp);
        MessageCenter.Instance.AddListener<KeyCode>(MessageType.OnKeyDown, OnKeyDown);
    }

    private void OnKeyUp(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.W:
                Debug.Log("前进");
                break;
            case KeyCode.S:
                Debug.Log("后退");
                break;
            case KeyCode.A:
                Debug.Log("向左");
                break;
            case KeyCode.D:
                Debug.Log("向右");
                break;
        }
    }

    private void OnKeyDown(KeyCode key)
    {
        switch (key)
        {
            case KeyCode.W:
                Debug.Log("前进停止");
                break;
            case KeyCode.S:
                Debug.Log("后退停止");
                break;
            case KeyCode.A:
                Debug.Log("向左停止");
                break;
            case KeyCode.D:
                Debug.Log("向右停止");
                break;
        }
    }
}

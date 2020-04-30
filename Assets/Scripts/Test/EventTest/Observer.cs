using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VFramework;

public class Observer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MessageCenter.Instance.AddListener(MessageType.MonsterDead, OnMonsterDead);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        MessageCenter.Instance.RemoveListener(MessageType.MonsterDead, OnMonsterDead);
    }

    private void OnMonsterDead()
    {
        Debug.Log(1);
    }
}

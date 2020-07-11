using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    private void Start()
    {
        UIManager.Instance.PushPanel(UIPanelType.MainMenu);
    }
}

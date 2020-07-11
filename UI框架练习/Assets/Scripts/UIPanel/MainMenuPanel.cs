using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// 呼出一个界面入栈
    /// </summary>
    /// <param name="panelType"></param>
    public void OnPushPanel(string panelTypeStr)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeStr);
        UIManager.Instance.PushPanel(panelType);
    }
}

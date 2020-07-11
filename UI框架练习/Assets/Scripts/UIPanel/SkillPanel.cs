using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanel : BasePanel
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    #region///状态方法
    public override void OnEnter()
    {
        //播放动画等
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public override void OnExit()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }

    public override void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }

    public override void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
    }
    #endregion

    public void OnCloseBtn()
    {
        UIManager.Instance.PopPanel();
    }
}

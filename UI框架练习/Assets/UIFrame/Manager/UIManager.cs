using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

/// <summary>
/// UI框架的核心管理类
/// </summary>
public class UIManager
{
    #region///单例模式
    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                instance = new UIManager();
            return instance;
        }
    }
    private UIManager()
    {
        ParseUIPanelJson();
    }
    #endregion

    private Dictionary<UIPanelType, string> panelPathDict;//储存界面储存路径
    private Dictionary<UIPanelType, BasePanel> panelDict;//储存管理生成的界面
    private Stack<BasePanel> panelStack = new Stack<BasePanel>();//界面管理栈
    private Transform canvasTransform;//画布
    public Transform CanvasTransform
    {
        get
        {
            if(canvasTransform == null)
            {
                canvasTransform = GameObject.Find("Canvas").GetComponent<Transform>();               
            }
            return canvasTransform;
        }
    }

    /// <summary>
    /// 解析UI界面Json文档
    /// </summary>
    private void ParseUIPanelJson()
    {
        panelPathDict = new Dictionary<UIPanelType, string>();
        TextAsset ta = Resources.Load<TextAsset>("UIPanelType");
        //解析Json文档
        List<UIPanelInfo> infoList = JsonConvert.DeserializeObject<List<UIPanelInfo>>(ta.text);
        //存入字典
        foreach (var info in infoList)
        {
            panelPathDict.Add(info.panelType, info.path);           
        }
    }

    /// <summary>
    /// 通过界面类型来获取一个界面对象
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public BasePanel GetPanel(UIPanelType type)
    {
        if(panelDict == null)
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();
        }

        BasePanel panel = panelDict.TryGet(type);
        //若还未生成此界面
        if(panel == null)
        {
            //根据该界面类型从字典里获取实例化的路径
            string path = panelPathDict.TryGet(type);
            GameObject go = GameObject.Instantiate(Resources.Load(path) as GameObject);
            //添加到画布下,不维持世界坐标
            go.transform.SetParent(CanvasTransform, false);
            panel = go.GetComponent<BasePanel>();
            //添加到字典中
            panelDict.Add(type, panel);
        }
        return panel;
    }

    /// <summary>
    /// 将指定界面入栈
    /// </summary>
    public void PushPanel(UIPanelType type)
    {
        //若栈内有其他界面，把栈顶的界面暂停
        if(panelStack.Count > 0)
        {
            BasePanel topPanel = panelStack.Peek();
            topPanel.OnPause();
        }
        //将界面入栈
        BasePanel panel = GetPanel(type);
        panelStack.Push(panel);
        panel.OnEnter();
    }
    /// <summary>
    /// 将栈顶界面出栈
    /// </summary>
    public void PopPanel()
    {
        if (panelStack.Count <= 0) return;
        //将栈顶界面推出
        BasePanel topPanel = panelStack.Pop();      
        topPanel.OnExit();
        //将下一个界面恢复
        if (panelStack.Count <= 0) return;
        BasePanel topPanel2 = panelStack.Peek();
        topPanel2.OnResume();
    }
}

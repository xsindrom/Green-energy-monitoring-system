using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UIMainController : MonoSingleton<UIMainController>
{
    public RectTransform windowsRoot;

    private CanvasScaler scaler;
    public CanvasScaler Scaler
    {
        get
        {
            if (!scaler)
            {
                scaler = GetComponent<CanvasScaler>();
            }
            return scaler;
        }
    }

    [SerializeField]
    private List<UIBaseWIndow> windowsList = new List<UIBaseWIndow>();
    private Dictionary<string, UIBaseWIndow> windowsDict = new Dictionary<string, UIBaseWIndow>();

    public T GetWindow<T>(string windowKey) where T : UIBaseWIndow
    {
        if (windowsDict.ContainsKey(windowKey))
        {
            var window = windowsDict[windowKey];
            return (T)window;
        }
        return null;
    }

    public UIBaseWIndow GetWindow(string windowKey)
    {
        if (windowsDict.ContainsKey(windowKey))
        {
            var window = windowsDict[windowKey];
            return window;
        }
        return null;
    }

    public override void Init()
    {
        base.Init();
        for(int i = 0; i < windowsList.Count; i++)
        {
            var window = Instantiate(windowsList[i], windowsRoot);
            windowsDict.Add(window.windowKey, window);
            window.Init();
        }
    }
}

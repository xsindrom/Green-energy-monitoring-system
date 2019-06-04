using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIBasePopup : UIBaseWIndow
{
    protected UnityAction<PopupCallbackType> callback;

    public virtual void OpenPopup(UnityAction<PopupCallbackType> callback)
    {
        this.callback = callback;
        OpenWindow();
    }

    public virtual void CloseWindow(PopupCallbackType type)
    {
        CloseWindow();
        EventHelper.SafeCall(callback, type);
    }

    public virtual void OnSubmitButtonClick()
    {
        CloseWindow(PopupCallbackType.Success);
    }

    public virtual void OnDenyButtonClick()
    {
        CloseWindow(PopupCallbackType.Fail);
    }
}

public enum PopupCallbackType
{
    Success,
    Fail
}
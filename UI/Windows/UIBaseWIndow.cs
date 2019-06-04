using UnityEngine;

public class UIBaseWIndow : MonoBehaviour
{
    public string windowKey;
    public WindowState windowState;
    public bool preventFromEsc = false;
    public UIWindowTweenAnimation windowAnimation;

    public virtual void OpenWindow()
    {
        if (windowState == WindowState.Close)
        {

            if (windowAnimation)
            {
                windowAnimation.CompletePlaying();
                windowState = WindowState.Open;
                gameObject.SetActive(true);
                windowAnimation.PlayAnimation("open");
            }
            else
            {
                windowState = WindowState.Open;
                gameObject.SetActive(true);
            }
        }
    }

    public virtual void CloseWindow()
    {
        if (windowState == WindowState.Open)
        {

            if (windowAnimation)
            {
                windowAnimation.CompletePlaying();
                windowState = WindowState.Close;
                windowAnimation.PlayAnimation("close", () => gameObject.SetActive(false));
            }
            else
            {
                windowState = WindowState.Close;
                gameObject.SetActive(false);
            }
        }
    }

    public virtual void Init()
    {
        switch (windowState)
        {
            case WindowState.Open: gameObject.SetActive(true); break;
            case WindowState.Close: gameObject.SetActive(false); break;
        }
    }

    public virtual void TryCloseWindowByEscape()
    {
        CloseWindow();
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!preventFromEsc)
            {
                TryCloseWindowByEscape();
            }
        }
    }
}

public enum WindowState { Open, Close }
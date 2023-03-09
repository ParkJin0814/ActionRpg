using UnityEngine;
using UnityEngine.Events;

public class Base_Window : MonoBehaviour
{
    [SerializeField] GameObject base_Window;
    protected UnityAction openAction = null;
    protected UnityAction closeAction = null;

    public void OpenWindow(bool v)
    {
        if (GameManager.Inst != null) GameManager.Inst.ButtonClick();
        if (v) openAction?.Invoke();
        else closeAction?.Invoke();
        base_Window.SetActive(v);
    }
}

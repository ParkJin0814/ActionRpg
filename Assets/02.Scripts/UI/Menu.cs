using UnityEngine;

public class Menu : MonoBehaviour
{
    public static bool menuActivated = false;
    [SerializeField] private GameObject go_menuBase;

    public void OpenMenu()
    {
        GameManager.Inst.ButtonClick();
        menuActivated = true;
        go_menuBase.SetActive(true);
    }

    public void CloseMenu()
    {
        GameManager.Inst.ButtonClick();
        menuActivated = false;
        go_menuBase.SetActive(false);
    }
    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}

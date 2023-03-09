public class Menu : Base_Window
{
    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    public void GameOver()
    {
        SceneLoader.Inst.SceneMove("00.MainTitle");
    }
}

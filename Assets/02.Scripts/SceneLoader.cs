public class SceneLoader : Singleton<SceneLoader>
{

    public void SceneMove(string Scene)
    {
        LodingSceneMover.LoadScene(Scene);
    }
}

using UnityEngine;

public class Title : MonoBehaviour
{
    public AudioClip BGM;
    public AudioClip EffectSound;
    public AudioSource EffectSource;
    public GameObject quitGame;
    private void Start()
    {
        SoundManager.Inst.PlayBGM(BGM);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitGame.SetActive(true);
        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void GameStart()
    {
        SceneLoader.Inst.SceneMove("02.Main");
    }
    public void OnButtonClick()
    {
        SoundManager.Inst.PlayOneShot(EffectSource, EffectSound);
    }
}

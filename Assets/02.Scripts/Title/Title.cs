using UnityEngine;

public class Title : MonoBehaviour
{
    public AudioClip BGM;
    public AudioClip EffectSound;
    public AudioSource EffectSource;
    private void Start()
    {
        SoundManager.Inst.PlayBGM(BGM);
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

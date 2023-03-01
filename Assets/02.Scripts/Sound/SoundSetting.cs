using UnityEngine;
using UnityEngine.UI;

public class SoundSetting : MonoBehaviour
{
    public static bool soundSettingActivated = false;
    [SerializeField] private GameObject go_soundSettingBase;
    [SerializeField] Slider BGMvolume;
    [SerializeField] Slider Effectvolume;

    private void Start()
    {
        BGMvolume.value = SoundManager.Inst.bgmVolume;
        Effectvolume.value = SoundManager.Inst.effectVolume;
    }
    private void Update()
    {
        if (soundSettingActivated)
        {
            SoundManager.Inst.effectVolume = Effectvolume.value;
            SoundManager.Inst.bgmVolume = BGMvolume.value;
        }
    }
    public void OpenSoundSetting()
    {

        soundSettingActivated = true;
        go_soundSettingBase.SetActive(true);
    }

    public void CloseSoundSetting()
    {

        soundSettingActivated = false;
        go_soundSettingBase.SetActive(false);
    }
}

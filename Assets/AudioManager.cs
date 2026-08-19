using UnityEngine;

//ƒQ[ƒ€‘S‘Ì‚ÅBGM,SE‚ğŠÇ—‚·‚é‚â‚Â
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    //BGM‚ÆSE‚Ì‰¹—Êİ’è
    public float bgmVolume = 1f;
    public float ButtonSeVolume = 0.2f;
    public float PlayerSeVolume = 0.6f;
    float baseBGMVolume = 1f;
    float baseButtonSeVolume = 0.2f;
    float basePlayerSeVolume = 0.6f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //‰¹—ÊŠÖŒW
    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
    }

    public void SetBtnSEVolume(float volume)
    {
        ButtonSeVolume = volume;
    }

    public void SetPlayerSEVolume(float volume)
    {
        PlayerSeVolume = volume;
    }

    public void ResetVolumes()
    {
        bgmVolume = baseBGMVolume;
        ButtonSeVolume = baseButtonSeVolume;
        PlayerSeVolume = basePlayerSeVolume;
    }
}
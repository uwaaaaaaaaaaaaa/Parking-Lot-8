using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SettingMenu : MonoBehaviour
{
    public GameObject settingMenuPanel;
    //音量変更用のバー
    public Slider volumeBtnSE;
    public Slider volumePlayerSE;
    public Slider volumeBGM;
    //SEのソース
    AudioSource SeSource;
    //SEのクリップ
    public AudioClip SeClip;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        settingMenuPanel.SetActive(false);
        // 保存済み音量を反映

        volumeBtnSE.value = AudioManager.Instance.ButtonSeVolume;
        volumePlayerSE.value = AudioManager.Instance.PlayerSeVolume;
        volumeBGM.value = AudioManager.Instance.bgmVolume;

        // スクロールバー変更時の処理登録
        volumeBtnSE.onValueChanged.AddListener(ChangeBtnSEVolume);
        volumePlayerSE.onValueChanged.AddListener(ChangePlayerSEVolume);
        volumeBGM.onValueChanged.AddListener(ChangeBGMVolume);

        //SEのソースを取得
        SeSource = GetComponent<AudioSource>();
    }

    //音量を常に取得
    void Update()
    {
        //音量を調整する
        SeSource.volume = AudioManager.Instance.ButtonSeVolume;
    }

    //設定用パネルの表示管理（ボタンから直接呼び出し）
    public void OpenorClose()
    {
        SeSource.volume = AudioManager.Instance.ButtonSeVolume;
        SeSource.PlayOneShot(SeClip);
        settingMenuPanel.SetActive(!settingMenuPanel.activeSelf);
    }

    //音量変更時の処理
    public void ChangeBtnSEVolume(float value)
    {
        AudioManager.Instance.SetBtnSEVolume(value);
    }

    public void ChangePlayerSEVolume(float value)
    {
        AudioManager.Instance.SetPlayerSEVolume(value);
    }

    public void ChangeBGMVolume(float value)
    {
    AudioManager.Instance.SetBGMVolume(value);
    }

    public void ResetVolumes()
    {
        AudioManager.Instance.ResetVolumes();
        // スクロールバーの値をリセット
        volumeBtnSE.value = AudioManager.Instance.ButtonSeVolume;
        volumePlayerSE.value = AudioManager.Instance.PlayerSeVolume;
        volumeBGM.value = AudioManager.Instance.bgmVolume;
    }
}

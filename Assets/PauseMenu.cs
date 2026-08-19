using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuPanel;
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
        pauseMenuPanel.SetActive(false);
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
        pauseMenuPanel.SetActive(!pauseMenuPanel.activeSelf);
        // ポーズメニューが開かれたときにゲームを一時停止する
        if (pauseMenuPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    //タイトルに戻るボタン
    public void ReturnToTitle()
    {
        SeSource.volume = AudioManager.Instance.ButtonSeVolume;
        SeSource.PlayOneShot(SeClip);
        // ゲームを再開
        Time.timeScale = 1f;
        // タイトルシーンに戻る
        SceneManager.LoadScene("TitleScene"); 
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

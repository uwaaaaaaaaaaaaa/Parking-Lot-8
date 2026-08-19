using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class StartDirector : MonoBehaviour
{
    //フェードイン用の黒
    public Image fadeImage;
    //プレイヤーのアニメーション操作用
    public Animator playerAnimator;
    //ボタンとかあるCanvas用
    public GameObject StartCanvas;
    //BGMとSEのクリップ
    public AudioClip BgmSource;
    public AudioClip SeSource;
    //BGMとSEのオーディオソース
    public AudioSource BGMaudioSource;
    public AudioSource SEaudioSource;

    void Start()
    {
        

        // 最初は透明
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        //表示した状態ではじめる
        StartCanvas.SetActive(true);

        //音量を調整し、BGMを再生する
        BGMaudioSource.loop = true;
        BGMaudioSource.clip = BgmSource;
        BGMaudioSource.Play();
    }
    public void Update()
    {
        //音量を調整する
        BGMaudioSource.volume = AudioManager.Instance.bgmVolume;
        SEaudioSource.volume = AudioManager.Instance.ButtonSeVolume;
    }
    //ボタン押下でプレイヤーのアニメーションとフェードアウトが始まる
    public void StartGameBtn()
    {
        //ボタンのSEを鳴らす
        SEaudioSource.PlayOneShot(SeSource);
        StartCanvas.SetActive(false);
        playerAnimator.SetTrigger("start");
        playerAnimator.SetBool("Run", true);
        Invoke("startgameFade", 3f);
    }


public void startgameFade()
    {
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        float duration = 1f;
        float time = 0f;
        float ratio = time / duration;

        Color color = fadeImage.color;

        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = time / duration;

            color.a = alpha;
            fadeImage.color = color;


            yield return null;
        }

        SceneManager.LoadScene("GameScene");
    }
}

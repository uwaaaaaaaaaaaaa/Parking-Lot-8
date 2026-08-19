using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class endingDirector : MonoBehaviour
{
    //フェードイン用の黒
    public Image fadeImage;
    //プレイヤーのアニメーション操作用
    public Animator playerAnimator;
    //ボタンが押された際のSE
    public AudioClip buttonSE;
    AudioSource audioSource;
    //クリックボタンの音量を調整するための変数
    public float buttonSEVolume = 0.2f;
    //BGMとSEのクリップ
    public AudioClip BgmSource;
    public AudioClip SeSource;
    //オーディオソース
    AudioSource audioSourceBGM;


    void Start()
    {
        //プレイヤーは走りっぱなし
        playerAnimator.SetBool("isRunning", true); 

        // 最初は真っ黒にする
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        StartCoroutine(FadeIn());

        //オーディオソースの取得
        audioSourceBGM = GetComponent<AudioSource>();

        //音量を調整し、BGMを再生する
        audioSourceBGM.loop = true;
        audioSourceBGM.volume = AudioManager.Instance.bgmVolume;
        audioSourceBGM.Play();

    }

    //ボタンで関連付ける
    public void BackTitle()
    {
        //音量を調整し、ボタンのSEを再生したあとにタイトルシーンに遷移する
        audioSourceBGM.volume = AudioManager.Instance.ButtonSeVolume;
        audioSourceBGM.PlayOneShot(buttonSE);
        StartCoroutine(WaitAndLoadScene("TitleScene", buttonSE.length));
    }

    public void FadeInAnimation()
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float duration = 1f;
        float time = 0f;

        Color color = fadeImage.color;

        while (time < duration)
        {
            time += Time.deltaTime;

            float alpha = 1f - (time / duration);

            color.a = alpha;
            fadeImage.color = color;

            yield return null;
        }

        // 確実に透明にする
        color.a = 0f;
        fadeImage.color = color;

        //ボタンの判定の邪魔になるのでイメージを非表示にする
        fadeImage.gameObject.SetActive(false);
    }
    
    IEnumerator WaitAndLoadScene(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}

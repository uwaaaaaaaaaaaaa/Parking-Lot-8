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


    void Start()
    {
        //プレイヤーは走りっぱなし
        playerAnimator.SetBool("isRunning", true); 

        // 最初は真っ黒にする
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        StartCoroutine(FadeIn());

    }

    //ボタンで関連付ける
    public void BackTitle()
    {
        SceneManager.LoadScene("TitleScene");
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
    }

}

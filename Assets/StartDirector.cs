using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class StartDirector : MonoBehaviour
{
    //フェードイン用の黒
    public Image fadeImage;
    //プレイヤーのアニメーション操作用
    public Animator playerAnimator;
    //ボタンとかあるCanvas用
    public GameObject StartCanvas;

    
    void Start()
    {
        

        // 最初は透明
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        //表示した状態ではじめる
        StartCanvas.SetActive(true);

        
    }

    //ボタン押下でプレイヤーのアニメーションとフェードアウトが始まる
    public void StartGameBtn()
    {
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

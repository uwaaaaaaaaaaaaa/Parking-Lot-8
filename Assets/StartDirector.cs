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
    //SE,BGM類
    public AudioSource bgm;
    public AudioSource runningSE;
    public AudioSource ClickSE;


    
    void Start()
    {
        

        // 最初は透明
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        //表示した状態ではじめる
        StartCanvas.SetActive(true);

        // BGM再生
        bgm.loop = true;
        
        bgm.Play();


    }

    //ボタン押下でプレイヤーのアニメーションとフェードアウトが始まる
    public void StartGameBtn()
    {
        StartCanvas.SetActive(false);
        ClickSE.Play();
        playerAnimator.SetTrigger("start");
        playerAnimator.SetBool("Run", true);
        Invoke(nameof(StartRunningSE), 1f);
        Invoke("startgameFade", 3f);
    }

    void StartRunningSE()
{
        runningSE.loop = true;
        runningSE.Play();
}

public void startgameFade()
    {
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        float startVolume = runningSE.volume;
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

            runningSE.volume = Mathf.Lerp(startVolume, 0f, ratio);

            yield return null;
        }

        SceneManager.LoadScene("GameScene");
    }
}

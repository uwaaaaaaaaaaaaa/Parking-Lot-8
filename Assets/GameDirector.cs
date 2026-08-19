using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameDirector : MonoBehaviour
{
    //間違いがあるかどうかの変数、間違いが出る確率の変数
    public bool isIncorrect = false;
    public int IncorrectProbability = 67;
    public int RandomParam;
    //現在のステージ数のカウント
    public int StageCount;
    //ステージ数表示用のレンダラー関係
    public SpriteRenderer numberRenderer;
    public Sprite[] numberSprites;
    //異変オブジェクトと異変内容を決定するパラメータ
    public ChangeableObject[] targets;
    int spriteIndex;
    int objIndex;
    //フェードアニメーション用
    public Image fadeImage;
    //出口表示切り替え用
    public GameObject InfBoardNum;
    public GameObject InfBoardExit;
    //BGMのソース
    AudioSource BgmSource;
    public AudioClip BgmClip;

    void Start()
    {
        //ステージ数のリセット
        StageCount = 0;
        //フェードイン用に表示
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;
        FadeInAnimation();
        InfBoardNum.SetActive(true);
        InfBoardExit.SetActive(false);
        //オーディオソースの取得
        BgmSource = GetComponent<AudioSource>();
        //音量の取得
        BgmSource.volume = AudioManager.Instance.bgmVolume;
        //BGMを再生する
        BgmSource.clip = BgmClip;
        BgmSource.loop = true;
        BgmSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

        //現在のステージが間違いかどうか決める
        if (!(StageCount == 0) && RandomParam < IncorrectProbability && !(StageCount >= 9))
        {
            isIncorrect = true;
        }
        else
        {
            isIncorrect = false;
        }
        //StageCountが９以上の時は掲示板の表示を変える
        if(StageCount == 9)
        {
            InfBoardNum.SetActive(false);
            InfBoardExit.SetActive(true);
        }
    }

    //PlayerがGateを通過した時の正誤判定
    public void JudgeForward()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].ResetSprite();
        }

        if (isIncorrect && !(StageCount >= 9))
        { StageCount++; }
        else
        { StageCount = 0; }

        UpdateNumberSprite();
        RandomParam = Random.Range(0, 100);
        //異変を起こす変数だった場合異変のオブジェクトなどを決める
        if (RandomParam < IncorrectProbability)
        {
            objIndex = Random.Range(0, targets.Length);
            spriteIndex = Random.Range(1, targets[objIndex].sprites.Length);
            Debug.Log(targets[objIndex].sprites.Length);
            Debug.Log(spriteIndex);
            targets[objIndex].ChangeSprite(spriteIndex);
        }
        else
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].ResetSprite();
            }
        }
    }

    public void JudgeBack()
    {
        if(StageCount >= 9)
        {
                        FadeOutAnimation();
            Debug.Log("EndingSceneへ遷移");
            return;
        }
        if(StageCount < 9) { 
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].ResetSprite();
        }
        if (!isIncorrect && !(StageCount >= 9))
        { StageCount++; }
        else
        { StageCount = 0; }

        UpdateNumberSprite();
        RandomParam = Random.Range(0, 100);
        if (RandomParam < IncorrectProbability)
        {
            objIndex = Random.Range(0, targets.Length);
            spriteIndex = Random.Range(1, targets[objIndex].sprites.Length);
            Debug.Log(targets[objIndex].sprites.Length);
            Debug.Log(spriteIndex);
            targets[objIndex].ChangeSprite(spriteIndex);
        }
        else
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].ResetSprite();
            }
        }
        }
        

    }

    public void UpdateNumberSprite()
    {
        //ステージ数がスプライト数を越した時のための保険
        if (StageCount >= 0 && StageCount < numberSprites.Length)
        {
            //配列から現在のステージ数にあう画像をピックアップ
            numberRenderer.sprite = numberSprites[StageCount];
        }

    }

    //始まりのフェードイン
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

        //確実に透明にする
        color.a = 0f;
        fadeImage.color = color;
        //邪魔になるので非表示
        fadeImage.gameObject.SetActive(false);
    }

    //終わりのフェードアウト
    public void FadeOutAnimation()
    {
        StartCoroutine(FadeAndLoad());
    }

    IEnumerator FadeAndLoad()
    {
        //再び表示
        fadeImage.gameObject.SetActive(true);

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

        //完全に暗転後にシーン遷移
        SceneManager.LoadScene("EndingScene"); // 次のシーン名に変更
    }

}

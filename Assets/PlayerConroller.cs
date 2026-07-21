using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerConroller : MonoBehaviour
{
    //プレイヤーの移動速度
    public float speed = 1.5f;
    //移動、長時間放置、旋回のアニメーション遷移を制御するbool型の変数
    public bool isRunning = false;
    public bool isLongIdle = true;
    public bool isTurning = false;
    public Animator anim;
    float idleTime = 0f;
    //振り向き方向の指定用の変数
    public int nextfacing = 1; 
    public int defaultScale = 7;
    //瞬間移動するゲートの座標
    float forwardGateX;
    float backGateX;
    //移動した先での無限ループを防ぐ差
    float offset = 0.1f;
    //ルール管理オブジェクト
    GameObject GameDirector;

    void Start()
    {
        //ゲームのフレームレートを指定
        Application.targetFrameRate = 60;
        //子オブジェクトのAnimatorコンポーネントを取得
        anim = GetComponent<Animator>();
        //それぞれののオブジェクトを取得
        this.GameDirector = GameObject.Find("GameDirector");
        forwardGateX = GameObject.Find("forwardGate").transform.position.x;
        backGateX = GameObject.Find("backGate").transform.position.x;
        Debug.Log(forwardGateX);
        Debug.Log(backGateX);
    }

    // Update is called once per frame
    void Update()
    {
        isRunning = false; 
        //左右キーが押されているかどうかを判定する
        bool isKeyPressed = Keyboard.current.leftArrowKey.isPressed || Keyboard.current.rightArrowKey.isPressed;

        //自身の向いている方向と移動方向が逆の場合に旋回アニメーションを再生する

        if (!isTurning)
        {
            if (Keyboard.current.leftArrowKey.isPressed && transform.localScale.x > 0)
            {
                isTurning = true;
                anim.SetTrigger("turn");
                nextfacing = -1;
                
            }
            else if (Keyboard.current.rightArrowKey.isPressed && transform.localScale.x < 0)
            {
                isTurning = true;
                anim.SetTrigger("turn");
                nextfacing = 1;
            }
        }



        //左右キーが押された時にプレイヤーを移動させる

        if (!isTurning)
        {
            if (Keyboard.current.leftArrowKey.isPressed)
            {
                RunningLeft();
            }
            if (Keyboard.current.rightArrowKey.isPressed)
            {
                RunningRight();
            }
        }


        //3秒以上放置している場合にアニメーションを切り替える
        if (isKeyPressed)
        {
            idleTime = 0f;          // リセット
            isLongIdle = false;
        }
        else
        {
            idleTime += Time.deltaTime; // 時間加算

            if (idleTime >= 3f)
            {
                isLongIdle = true;
            }
        }

        //それぞれのGateを通過した時の処理）

        if (transform.position.x <= forwardGateX)
        {
            transform.position = new Vector3(backGateX - offset, transform.position.y, transform.position.z);
            GameDirector.GetComponent<GameDirector>().JudgeForward();
        }
        if (transform.position.x >= backGateX)
        {
            transform.position = new Vector3(forwardGateX + offset, transform.position.y, transform.position.z);
            GameDirector.GetComponent<GameDirector>().JudgeBack();
            Debug.Log("奥通過");
        }


        // Animatorに反映
        anim.SetBool("isLongIdle", isLongIdle);
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isTurning", isTurning);

    }

    public void RunningRight()
    {
        isRunning = true;
        if(Keyboard.current.leftShiftKey.isPressed)
            transform.position += Vector3.right * speed * 1.5f * Time.deltaTime;
        else
                    transform.position += Vector3.right * speed * Time.deltaTime;
    }
    public void RunningLeft()
    {
        isRunning = true;
        if (Keyboard.current.leftShiftKey.isPressed)
            transform.position += Vector3.left * speed * 1.5f * Time.deltaTime;
        else
            transform.position += Vector3.left * speed * Time.deltaTime;
    }


    public void EndTurn()
    {
        isTurning = false;
        transform.localScale = new Vector3(defaultScale * nextfacing, defaultScale, defaultScale);
    }

}

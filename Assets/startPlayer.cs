using UnityEngine;

public class startPlayer : MonoBehaviour
{
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //自身についているアニメーターを読み込む
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //今のアニメーション状態を取得
        AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo(0);

        //走るアニメーションになったタイミングで動き始める
        if (state.IsName("running"))
        {
            transform.Translate(Vector3.right * 1.5f * Time.deltaTime);
        }

    }
}

using UnityEngine;
using UnityEngine.UIElements;

public class flowObj: MonoBehaviour
{
    //動くスピードの設定
    public float movinSpeed = 6f;
    //終点と始点の座標
    public float endPos = 0f;
    public float startPos = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.left * movinSpeed * Time.deltaTime);

        if (transform.position.x <= endPos)
        {
            transform.position = new Vector3(startPos,transform.position.y,transform.position.z); 
        }
    }
}

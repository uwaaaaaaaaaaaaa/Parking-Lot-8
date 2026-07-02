using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //ゲームのフレームレートを指定
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        //playerの名前のオブジェクトを取得
        GameObject player = GameObject.Find("player");
        //playerの位置にカメラを移動させる
        if (player != null)
        {
            transform.position = new Vector3(player.transform.position.x + 3.44f, player.transform.position.y + 3.44f, transform.position.z);
        }
    }
}

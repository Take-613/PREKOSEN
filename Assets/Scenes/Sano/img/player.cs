using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;   // 横に移動する速度
    public float jumpP = 300f; // ジャンプ力

    Rigidbody2D rbody; // リジッドボディを使うための宣言

    // Start is called before the first frame update
    void Start()
    {
        // リジッドボディ2Dをコンポーネントから取得して変数に入れる
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // ジャンプをするためのコード（もしスペースキーが押されて、上方向に速度がない時に）
        if (Input.GetKeyDown(KeyCode.Space) && rbody.linearVelocity.y <= 0.01)
        {
             // リジッドボディに力を加える（上方向にジャンプ力をかける）
            rbody.AddForce(transform.up * jumpP);
        }
    }

    private void FixedUpdate()
    {
        //リジッドボディに一定の速度を入れる（横移動の速度, リジッドボディのyの速度）
        rbody.linearVelocity = new Vector2(speed, rbody.linearVelocity.y);
    }
}
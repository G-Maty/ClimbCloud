using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid2D;
    Animator animetor;
    float jumpforce = 680.0f;
    float walkForce = 30.0f;
    float maxWalkSpeed = 2.0f;
    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        this.animetor = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //ジャンプする
        if (Input.GetKeyDown(KeyCode.Space) && this.rigid2D.velocity.y == 0)//y方向の速度が0の時だけジャンプすることで多段ジャンプ縛る
        {
            this.rigid2D.AddForce(transform.up * this.jumpforce);
        }

        //左右移動
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;

        //プレイヤの速度を測定
        float speedx = Mathf.Abs(this.rigid2D.velocity.x);

        //スピード制限
        if(speedx < this.maxWalkSpeed)
        {
            this.rigid2D.AddForce(transform.right * key * this.walkForce);
        }

        //反転対策
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);
        }

        //画面外に出た場合は最初から
        if(transform.position.y < -10)
        {
            SceneManager.LoadScene("GameScene");
        }

        //プレイヤの速度に応じてアニメーション速度を変える
        this.animetor.speed = speedx / 2.0f;
    }


    //ゴールに到達
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ゴール");
        SceneManager.LoadScene("ClearScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UnityChanController : MonoBehaviour
{
    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private float velocityZ = 16f;

    private float velocityX = 10f;
    private float velocityY = 10f;
    private float movableRange = 3.4f;
    //動きを減速させる定数
    private float coefficient = 0.99f;
    //ゲーム終了の判定；
    private bool isEnd = false;
    //ゲーム終了時に表示するテキスト
    private GameObject stateText;
    //スコアを表示するテキスト
    private GameObject scoreText;
    //スコア
    private int score = 0;
    //左ボタン押下の判定
    private bool isLButtonDown = false;
    //右ボタン押下の判定
    private bool isRButtonDown = false;
    //ジャンプボタン押下の判定
    private bool isJButtonDown = false;




    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = GetComponent<Animator>();

        this.myAnimator.SetFloat("Speed", 1);

        this.myRigidbody = GetComponent<Rigidbody>();

        this.stateText = GameObject.Find("GameResultText");

        this.scoreText = GameObject.Find("ScoreText");


    }

    // Update is called once per frame
    void Update()
    {

        //ゲーム終了ならUnityちゃんを止める
        if (this.isEnd)
        {
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.velocityZ *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;

        }

        float inputVelocityX = 0;
        float inputVelocityY = 0;

        //Unityちゃんを左右のキーコード,ボタンが押されていればそれに応じて移動させる
        if((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            inputVelocityX = -this.velocityX;
        }
        else if((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown)&& this.movableRange > this.transform.position.x)
        {
            inputVelocityX = this.velocityX;
        }

        //ジャンプしていないときにスペースキー,ボタンを押したらジャンプする
        if ((Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            inputVelocityY = this.velocityY;
        }
        else
        {
            //現在のY軸の速度を代入
            inputVelocityY = this.myRigidbody.velocity.y;
        }

        //ジャンプ中はJumpにFalse
        if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //x軸、y軸、z軸の移動
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, this.velocityZ);

    }

    //トリガーモードでほかのオブジェクトと接触した場合の処理；；
    private void OnTriggerEnter(Collider other)
    {
        //障害物に衝突した場合
        if (other.gameObject.tag == TagName.CarTag || other.gameObject.tag == TagName.TrafficConeTag)
        {
            this.isEnd = true;
            //stateTextにGAME OVERを表示
            this.stateText.GetComponent<Text>().text = "GAME OVER";


        }

        //ゴール地点に到達した場合
        if (other.gameObject.tag == TagName.GoalTag)
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "CLEAR!";

        }

        //コインに衝突した場合
        if (other.gameObject.tag == TagName.CoinTag)
        {
            //スコアを加算；；
            this.score += 10;
            ;
            //得点表示
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";



            //パーティクルを再生
            GetComponent<ParticleSystem>().Play();

            //接触したコインのオブジェクトを破棄
            Destroy(other.gameObject);
        }
    }

    //ジャンプボタンを押した場合の処理
    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;

    }
    //ジャンプボタンを離した場合の処理
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;

    }

    //左ボタン処理
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;

    }
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //右ボタン処理
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }
}

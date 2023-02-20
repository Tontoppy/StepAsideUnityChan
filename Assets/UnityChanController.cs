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
    //����������������萔
    private float coefficient = 0.99f;
    //�Q�[���I���̔���G
    private bool isEnd = false;
    //�Q�[���I�����ɕ\������e�L�X�g
    private GameObject stateText;
    //�X�R�A��\������e�L�X�g
    private GameObject scoreText;
    //�X�R�A
    private int score = 0;
    //���{�^�������̔���
    private bool isLButtonDown = false;
    //�E�{�^�������̔���
    private bool isRButtonDown = false;
    //�W�����v�{�^�������̔���
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

        //�Q�[���I���Ȃ�Unity�������~�߂�
        if (this.isEnd)
        {
            this.velocityX *= this.coefficient;
            this.velocityY *= this.coefficient;
            this.velocityZ *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;

        }

        float inputVelocityX = 0;
        float inputVelocityY = 0;

        //Unity���������E�̃L�[�R�[�h,�{�^����������Ă���΂���ɉ����Ĉړ�������
        if((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -this.movableRange < this.transform.position.x)
        {
            inputVelocityX = -this.velocityX;
        }
        else if((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown)&& this.movableRange > this.transform.position.x)
        {
            inputVelocityX = this.velocityX;
        }

        //�W�����v���Ă��Ȃ��Ƃ��ɃX�y�[�X�L�[,�{�^������������W�����v����
        if ((Input.GetKeyDown(KeyCode.Space) || this.isJButtonDown) && this.transform.position.y < 0.5f)
        {
            this.myAnimator.SetBool("Jump", true);
            inputVelocityY = this.velocityY;
        }
        else
        {
            //���݂�Y���̑��x����
            inputVelocityY = this.myRigidbody.velocity.y;
        }

        //�W�����v����Jump��False
        if(this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //x���Ay���Az���̈ړ�
        this.myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, this.velocityZ);

    }

    //�g���K�[���[�h�łق��̃I�u�W�F�N�g�ƐڐG�����ꍇ�̏����G�G
    private void OnTriggerEnter(Collider other)
    {
        //��Q���ɏՓ˂����ꍇ
        if (other.gameObject.tag == TagName.CarTag || other.gameObject.tag == TagName.TrafficConeTag)
        {
            this.isEnd = true;
            //stateText��GAME OVER��\��
            this.stateText.GetComponent<Text>().text = "GAME OVER";


        }

        //�S�[���n�_�ɓ��B�����ꍇ
        if (other.gameObject.tag == TagName.GoalTag)
        {
            this.isEnd = true;
            this.stateText.GetComponent<Text>().text = "CLEAR!";

        }

        //�R�C���ɏՓ˂����ꍇ
        if (other.gameObject.tag == TagName.CoinTag)
        {
            //�X�R�A�����Z�G�G
            this.score += 10;
            ;
            //���_�\��
            this.scoreText.GetComponent<Text>().text = "Score" + this.score + "pt";



            //�p�[�e�B�N�����Đ�
            GetComponent<ParticleSystem>().Play();

            //�ڐG�����R�C���̃I�u�W�F�N�g��j��
            Destroy(other.gameObject);
        }
    }

    //�W�����v�{�^�����������ꍇ�̏���
    public void GetMyJumpButtonDown()
    {
        this.isJButtonDown = true;

    }
    //�W�����v�{�^���𗣂����ꍇ�̏���
    public void GetMyJumpButtonUp()
    {
        this.isJButtonDown = false;

    }

    //���{�^������
    public void GetMyLeftButtonDown()
    {
        this.isLButtonDown = true;

    }
    public void GetMyLeftButtonUp()
    {
        this.isLButtonDown = false;
    }

    //�E�{�^������
    public void GetMyRightButtonDown()
    {
        this.isRButtonDown = true;
    }
    public void GetMyRightButtonUp()
    {
        this.isRButtonDown = false;
    }
}

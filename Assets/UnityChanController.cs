using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityChanController : MonoBehaviour
{
    private Animator myAnimator;
    private Rigidbody myRigidbody;
    private float velocityZ = 16f;

    private float velocityX = 10f;
    private float velocityY = 10f;
    private float movableRange = 3.4f;


    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = GetComponent<Animator>();

        this.myAnimator.SetFloat("Speed", 1);

        this.myRigidbody = GetComponent<Rigidbody>();


    }

    // Update is called once per frame
    void Update()
    {
        float inputVelocityX = 0;
        float inputVelocityY = 0;

        //Unity���������E�̃L�[�R�[�h��������Ă���΂���ɉ����Ĉړ�������
        if(Input.GetKey(KeyCode.LeftArrow) && -this.movableRange < this.transform.position.x)
        {
            inputVelocityX = -this.velocityX;
        }
        else if(Input.GetKey(KeyCode.RightArrow)&& this.movableRange > this.transform.position.x)
        {
            inputVelocityX = this.velocityX;
        }

        //�W�����v���Ă��Ȃ��Ƃ��ɃX�y�[�X�L�[����������W�����v����
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
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
}

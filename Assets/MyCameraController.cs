using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCameraController : MonoBehaviour
{

    //Unity�����I�u�W�F�N�g������
    private GameObject unitychan;
    //Unity�����ƃJ�����̋���
    private float difference;


    // Start is called before the first frame update
    void Start()
    {
        //Unity�������擾
        this.unitychan = GameObject.Find("unitychan");
        //Unity�����ƃJ�����̈ʒu��Z�������߂�
        this.difference = unitychan.transform.position.z - this.transform.position.z;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(0, this.transform.position.y, this.unitychan.transform.position.z - difference);

    }
}

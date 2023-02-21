using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //Unity������transform
    private Transform unitychanTransform;
    //���̃I�u�W�F�N�g��transform
    private Transform myTransform;
    //Unity�����Ƃ̋������E�i����ȏ�Unity�����̌��ɍs���ƃI�u�W�F�N�g��j��j
    private int limitDistanse = 6;

    // Start is called before the first frame update
    void Start()
    {
        this.myTransform = this.transform;
        this.unitychanTransform = GameObject.Find("unitychan").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.myTransform.position.z + limitDistanse <= this.unitychanTransform.position.z)
        {
            Destroy(this.gameObject);
        }

    }
}

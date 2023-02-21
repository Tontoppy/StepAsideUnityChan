using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //Unityちゃんのtransform
    private Transform unitychanTransform;
    //このオブジェクトのtransform
    private Transform myTransform;
    //Unityちゃんとの距離限界（これ以上Unityちゃんの後ろに行くとオブジェクトを破壊）
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

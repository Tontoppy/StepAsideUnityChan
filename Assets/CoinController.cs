using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
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
        this.transform.Rotate(0, Random.Range(0, 360), 0);
        this.myTransform = this.transform;
        this.unitychanTransform = GameObject.Find("unitychan").transform;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 3, 0);

        if(this.myTransform.position.z +limitDistanse <= this.unitychanTransform.position.z)
        {
            Destroy(this.gameObject);
        }

    }
}

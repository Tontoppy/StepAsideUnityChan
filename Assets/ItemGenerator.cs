using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{

    //carPrefabを格納する
    public GameObject carPrefab;
    //coinPrefabを格納する
    public GameObject coinPrefab;
    //cornPrefab
    public GameObject conePrefab;

    //スタート地点
    private int startPos = 80;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    //発展課題：Unityちゃんのtransformを格納する
    private Transform unitychanTransform;
    //発展課題：アイテムの配置間隔
    private int itemSpacing = 15;
    //発展課題：アイテム配置位置のUnityちゃんからの距離
    private int itemDistance = 50;
    //発展課題：ひとつ前にアイテムを生成したZ座標を記録する
    int previousLine = 0;


    // Start is called before the first frame update
    void Start()
    {
        //発展課題：Unityちゃんのtransformを取得
        this.unitychanTransform = GameObject.Find("unitychan").transform;

    }

    // Update is called once per frame
    void Update()
    {
        //発展課題：UnityちゃんのZ座標を取得
        float unitychanZ = this.unitychanTransform.position.z;
        //アイテムを配置したい、Unityちゃんの50m先のZ座標が15mの倍数値を通過するたびにアイテムを配置する(整数値に切り捨てる)
        int itemGenerateLine = ((int)(unitychanZ + itemDistance));
        Debug.Log(itemGenerateLine % itemSpacing == 0);
        if (itemGenerateLine % itemSpacing == 0 
            && startPos <= itemGenerateLine
            && itemGenerateLine <= goalPos
            && previousLine != itemGenerateLine)
        {
            GenerateItem(((int)itemGenerateLine));
            //previousLineを更新
            previousLine = itemGenerateLine;
        }

    }

    //発展課題：アイテム生成メソッドを分離。引数はゼット座標
    void GenerateItem(int positionZ)
    {

        //どのアイテムを出すのかランダムに設定；
        int num = Random.Range(1, 11);
        if (num <= 2)
        {
            //コーンを横一列に生成；
            for (float j = -1; j <= 1; j += 0.4f)
            {
                GameObject cone = Instantiate(conePrefab);
                cone.transform.position = new Vector3(4 * j, cone.transform.position.y, positionZ);

            }
        }
        else
        {
            //レーンごとにアイテム生成；
            for (int j = -1; j <= 1; j++)
            {
                //アイテムの種類
                int item = Random.Range(1, 11);
                //Z座標をランダムにずらす
                int offsetZ = Random.Range(-5, 6);
                //60%コイン、30％車、10％なし
                if (1 <= item && item <= 6)
                {
                    //コイン生成
                    GameObject coin = Instantiate(coinPrefab);
                    coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, positionZ + offsetZ);

                }
                else if (7 <= item && item <= 9)
                {
                    //車を生成
                    GameObject car = Instantiate(carPrefab);
                    car.transform.position = new Vector3(posRange * j, car.transform.position.y, positionZ + offsetZ);

                }
            }

        }
    }
}

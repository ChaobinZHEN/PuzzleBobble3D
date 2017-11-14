using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBobble : MonoBehaviour
{

    public static CreateBobble Instance;
    public GameObject[] shootBobble = new GameObject[2];
    public GameObject[] bobbleStyle = new GameObject[5];
    public int layerMaxBallNum = 5;

    private GameObject bobbleObject;

	// Use this for initialization
	void Start () {
        Instance = this;
        //  Create bobbles ready to shoot
        Vector3 shootPos = GameObject.FindGameObjectWithTag("Muzzle").transform.position;
        bobbleObject = bobbleStyle[Random.Range(0, layerMaxBallNum)];
        shootBobble[0] = Instantiate(bobbleObject, shootPos, Quaternion.identity) as GameObject;
        shootPos = GameObject.Find("Loading").transform.position;
        bobbleObject = bobbleStyle[Random.Range(0, layerMaxBallNum)];
        shootBobble[1] = Instantiate(bobbleObject, shootPos, Quaternion.identity) as GameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

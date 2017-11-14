using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBobble : MonoBehaviour {
    private Transform transform;
    private Transform muzzleForm;
    private Vector3 shootPos;

	// Use this for initialization
	void Start () {
        transform = this.transform;
        muzzleForm = transform.Find("Muzzle");
        shootPos = muzzleForm.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreatShotBall()
    {
        CreateBobble.Instance.shootBobble[0] = CreateBobble.Instance.shootBobble[1];
        CreateBobble.Instance.shootBobble[0].transform.position = shootPos;   //  Move loading bobble to the muzzle
        Vector3 loadingPos = GameObject.Find("Loading").transform.position;
        CreateBobble.Instance.shootBobble[1] = Instantiate(CreateBobble.Instance.bobbleStyle[Random.Range(0, CreateBobble.Instance.layerMaxBallNum)], loadingPos, Quaternion.identity) as GameObject;
        Cannon.Instance.shootable = true;
    }
}

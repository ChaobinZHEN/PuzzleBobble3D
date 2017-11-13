using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public static Cannon Instance;
    public float speed = 2f;
    public bool shooted = false;
    public float rotateSpeed = 50f;

    private Transform cannonForm;
    private Transform muzzleForm;

    private Vector3 shootDir;   //  Shoot direction
    private Vector3 muzzlePos;   //  Muzzle position
    private Vector3 cannonPos;  //  Cannon position

	// Use this for initialization
	void Start () {
        Instance = this;
        cannonForm = this.transform;
        muzzleForm = cannonForm.Find("Muzzle");
        cannonPos = cannonForm.position;
        muzzlePos = muzzleForm.position;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A))
        {
            cannonForm.Rotate(Vector3.down * Time.deltaTime * rotateSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            cannonForm.Rotate(Vector3.up * Time.deltaTime * rotateSpeed);
        }
	}
}

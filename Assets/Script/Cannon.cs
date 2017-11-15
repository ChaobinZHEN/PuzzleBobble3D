using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public static Cannon Instance;
    public bool shootable = false;
    public float rotateSpeed = 50f;
    public float shootSpeed;
    public float minSpeed;
    public bool creatable = false;
    public float shootTime;
    private float timer = 0f;

    private Transform cannonForm;
    private Transform muzzleForm;
    private Transform loadingForm;

    private Vector3 shootDir;   //  Shoot direction
    private Vector3 muzzlePos;   //  Muzzle position
    private Vector3 cannonPos;  //  Cannon position
    private Vector3 loadingPos;    //  Loading position

    //public CreateBobble createBooble;

	// Use this for initialization
	void Start () {
        Instance = this;
        cannonForm = this.transform;
        muzzleForm = cannonForm.Find("Muzzle");
        loadingForm = cannonForm.Find("Loading");
        cannonPos = cannonForm.position;
        muzzlePos = muzzleForm.position;
        loadingPos = loadingForm.position;
        //createBooble = new CreateBobble();
        shootable = true;
		
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

        timer += Time.deltaTime;
        if (timer > shootTime && shootable) {
            Shoot();
        }

        if (Input.GetKey(KeyCode.W) && shootable)
        {   

            Shoot();
        } 

	}

    void Shoot() {
        GameObject bobbleObject = CreateBobble.Instance.shootBobble[0];
        //createBooble.shootBobble[0] = null;
        shootDir = muzzleForm.position - cannonForm.position;
        bobbleObject.GetComponent<Rigidbody>().isKinematic = false;
        bobbleObject.GetComponent<Rigidbody>().velocity = shootDir * shootSpeed;
        bobbleObject.GetComponent<Collider>().isTrigger = false;
        creatable = true;
        /*
        // Keep minimun speed
        if (bobbleObject.GetComponent<Rigidbody>().velocity.magnitude <= minSpeed)
        {
            bobbleObject.GetComponent<Rigidbody>().velocity =  bobbleObject.GetComponent<Rigidbody>().velocity.normalized * minSpeed;
        }
        */
        shootable = false;
        bobbleObject.AddComponent<StopBobble>();
        timer = 0f;
    }


}

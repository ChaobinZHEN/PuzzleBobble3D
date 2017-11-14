using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public static Cannon Instance;
    public bool shootable = false;
    public float rotateSpeed = 50f;
    public float shootSpeed;
    public float minSpeed;

    private Transform cannonForm;
    private Transform muzzleForm;
    private Transform loadingForm;

    private Vector3 shootDir;   //  Shoot direction
    private Vector3 muzzlePos;   //  Muzzle position
    private Vector3 cannonPos;  //  Cannon position
    private Vector3 loadingPos;    //  Loading position

    private CreateBobble createBooble;

	// Use this for initialization
	void Start () {
        Instance = this;
        cannonForm = this.transform;
        muzzleForm = cannonForm.Find("Muzzle");
        loadingForm = cannonForm.Find("Loading");
        cannonPos = cannonForm.position;
        muzzlePos = muzzleForm.position;
        loadingPos = loadingForm.position;
        createBooble = GetComponent<CreateBobble>();
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


	}

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W) && shootable)
        {
            GameObject bobbleObject = createBooble.shootBobble[0];
            shootDir = muzzleForm.position - cannonForm.position;
            bobbleObject.GetComponent<Rigidbody>().isKinematic = false;
            bobbleObject.GetComponent<Rigidbody>().velocity = shootDir * shootSpeed;
            bobbleObject.GetComponent<Collider>().isTrigger = false;
            // Keep minimun speed
            if (bobbleObject.GetComponent<Rigidbody>().velocity.magnitude <= minSpeed)
            {
                bobbleObject.GetComponent<Rigidbody>().velocity =  bobbleObject.GetComponent<Rigidbody>().velocity.normalized * minSpeed;
            }
            shootable = false;
            bobbleObject.AddComponent<StopBobble>();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour {

    public static Cannon Instance;
    public bool shootable = false;
    public float rotateSpeed = 50f;
    public float shootSpeed;
    //public float minSpeed;
    public bool creatable = false;
    public float shootTime;

    public float timer;
    private float movingTimer;
    public float movingTime;

    public bool defeat;
    public bool victory;

    //private bool minVel;

    private int score;

    public Text scoreText;
    //public Text finalScoreText;

    private Transform cannonForm;
    private Transform muzzleForm;
    private Transform loadingForm;

    private Vector3 shootDir;   //  Shoot direction
    private Vector3 muzzlePos;   //  Muzzle position
    private Vector3 cannonPos;  //  Cannon position
    private Vector3 loadingPos;    //  Loading position

    public GameObject bobbleObject;
    public GameObject gameOver;
    public GameObject win;

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
        timer = 0f;
        movingTimer = 0f;

        score = 0;

        defeat = false;
        victory = false;
		
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

        if (Input.GetKey(KeyCode.W) && shootable)
        {   

            Shoot();
        } 

        // Shoot automatically
        timer += Time.deltaTime;
        if (timer > shootTime && shootable) {
            Shoot();
        }

        // Reset the bobble if it can't stop
        if (timer > Config.resetTime && !bobbleObject.GetComponent<BobbleProperty>().stop)
        {

            bobbleObject.GetComponent<Rigidbody>().isKinematic = true;
            //bobbleObject.GetComponent<Collider>().isTrigger = true;
            bobbleObject.transform.position = cannonForm.position;
            shootable = true;
            timer = 0f;
            Debug.Log("Reset bobble!");
        }

        movingTimer += Time.deltaTime;
        if(movingTimer > movingTime) {
            GameObject.Find("Moving Up").transform.Translate(Vector3.left * 0.2f);
            GameObject.Find("Main Camera").transform.Translate(Vector3.up * 0.2f);
            movingTimer = 0f;
        }

        setScoreText();

        // Game over
        if(defeat) {
            defeat = false;
            Instantiate(gameOver, Vector3.zero, Quaternion.identity);
            Time.timeScale = 0f;
        }

        // Victory
        if(victory) {
            victory = false;
            Instantiate(win, Vector3.zero, Quaternion.identity);
            Time.timeScale = 0f;
        }
            

	}

    /*
    void FixedUpdate()
    {
        // Minimum speed
        if (bobbleObject.GetComponent<Rigidbody>().velocity.magnitude < minSpeed && !bobbleObject.GetComponent<BobbleProperty>().stop && minVel)
        {
            minVel = false;
            bobbleObject.GetComponent<Rigidbody>().useGravity = false;

        }
    }
    */




    void Shoot() {
        bobbleObject = CreateBobble.Instance.shootBobble[0];
        //createBooble.shootBobble[0] = null;
        shootDir = muzzleForm.position - cannonForm.position;
        bobbleObject.GetComponent<Rigidbody>().isKinematic = false;
        bobbleObject.GetComponent<Rigidbody>().velocity = shootDir.normalized * shootSpeed;
        //bobbleObject.GetComponent<Collider>().isTrigger = false;
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

    public void setScoreText()
    {
        scoreText.text = "Score: " + score.ToString();

    }
    public void PoppedScore(){
        score += 10;
        if (Config.debug)
        {
            Debug.Log("Pop Score!");
        }
    }

    public void RollingScore(int num)
    {
        score += 10 * (int)Mathf.Pow(2, num);
        if (Config.debug)
        {
            Debug.Log("Rolling Score!");
        }
    }

    public void Level1Button()
    {
        Debug.Log("Level 1 Button click!");
    }
}

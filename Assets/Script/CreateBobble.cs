using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBobble : MonoBehaviour
{

    public static CreateBobble Instance;
    public GameObject[] shootBobble = new GameObject[2];
    public GameObject[] bobbleStyle = new GameObject[5];
    public int layerMaxBallNum = 5;

    private GameObject randBobble;
    private GameObject emptyPoint;
    private Vector3 emptyPointPos;

    public struct bobble
    {
        public GameObject pointObject;
        public GameObject bobbleObject;
    }

    public bobble[,] m_bobble;

    private int row = Config.row;
    private int col = Config.col;


	// Use this for initialization
	void Start () {
        Instance = this;
        m_bobble = new bobble[row, col];
        emptyPoint = GameObject.Find("Empty Point");
        emptyPointPos = emptyPoint.transform.position;

        // Initiate bobbles
        Init(Config.initRow);
        //  Create bobbles ready to shoot
        Vector3 shootPos = GameObject.FindGameObjectWithTag("Cannon").transform.position;
        randBobble = bobbleStyle[Random.Range(0, layerMaxBallNum)];
        shootBobble[0] = Instantiate(randBobble, shootPos, Quaternion.identity) as GameObject;
        shootBobble[0].transform.parent = GameObject.Find("Moving Up").transform;
        Vector3 loadingPos = GameObject.Find("Loading").transform.position;
        randBobble = bobbleStyle[Random.Range(0, layerMaxBallNum)];
        shootBobble[1] = Instantiate(randBobble, loadingPos, Quaternion.identity) as GameObject;
        shootBobble[1].transform.parent = GameObject.Find("Moving Up").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Initiate base
    public void Init(int num) {
        // Clear the arrays
        for (int j = 0; j < col; j++) {
            for (int i = 0; i < row; i++) {
                if (m_bobble[i, j].bobbleObject != null)
                {
                    Destroy(m_bobble[i, j].bobbleObject);
                    m_bobble[i, j].bobbleObject = null;
                }
                if (m_bobble[i, j].pointObject != null)
                {
                    Destroy(m_bobble[i, j].pointObject);
                    m_bobble[i, j].pointObject = null;
                }
            }
        }
        // Create point for bobble stop postion
        for (int i = 0; i < row; i++) {
            for (int j = 0; j < (col - (i % 2)); j++) {
                m_bobble[i, j].pointObject = Instantiate(emptyPoint, new Vector3(emptyPointPos.x + i * 1.732050f * Config.radBobble, emptyPointPos.y - i * Config.offsetHeight, emptyPointPos.z + j * 2 * Config.radBobble + (i % 2) * Config.radBobble), Quaternion.identity) as GameObject;
                m_bobble[i, j].bobbleObject = null;
            }
        }

        // Create random bobbles
        for (int i = 0; i <= num; i++) {
            for (int j = 0; j < (col - (i % 2)); j++)
            {
                randBobble = bobbleStyle[Random.Range(0, layerMaxBallNum)];
                m_bobble[i, j].bobbleObject = Instantiate(randBobble, m_bobble[i, j].pointObject.transform.position, Quaternion.identity) as GameObject;
                m_bobble[i, j].bobbleObject.GetComponent<Rigidbody>().isKinematic = true;
                m_bobble[i, j].bobbleObject.tag = Config.staticBobble;

            }
        }
    }
}

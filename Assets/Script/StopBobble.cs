using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBobble : MonoBehaviour {
    private Transform m_transform;
    private Transform cannonForm;
    private Vector3 shootPos;
    private Vector3 loadingPos;
    private int rollingCount;
    //private Vector3 vel;
    //private float timer;


    struct xy
    {
        public int x;
        public int y;
    }

    private xy m_xy;
    private int m_x = Config.row;
    private int m_y = Config.col;

    private ArrayList listA = new ArrayList();//放入要检查的泡泡
    private ArrayList listB = new ArrayList();//ListB用来保存最后相交的结果
    private Stack stackA = new Stack();//栈A用来作为一个过渡

	// Use this for initialization
	void Start () {
        //m_transform = this.m_transform;
        cannonForm = GameObject.Find("Cannon").transform;
        shootPos = cannonForm.position;
        rollingCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        /*
        vel = GetComponent<Rigidbody>().velocity;
        if(vel.magnitude <= Cannon.Instance.minSpeed){
            vel = vel * Cannon.Instance.minSpeed;
            GetComponent<Rigidbody>().velocity = vel;
        }
        */

	}


    void CreateShootBobble()
    {   

        CreateBobble.Instance.shootBobble[0].transform.parent = null;
        CreateBobble.Instance.shootBobble[0] = null;
        CreateBobble.Instance.shootBobble[0] = CreateBobble.Instance.shootBobble[1];
        CreateBobble.Instance.shootBobble[0].transform.position = shootPos;   //  Move loading bobble to the muzzle
        CreateBobble.Instance.shootBobble[1] = null;
        loadingPos = GameObject.Find("Loading").transform.position;
        CreateBobble.Instance.shootBobble[1] = Instantiate(CreateBobble.Instance.bobbleStyle[Random.Range(0, CreateBobble.Instance.layerMaxBallNum)], loadingPos, Quaternion.identity) as GameObject;
        CreateBobble.Instance.shootBobble[1].transform.parent = GameObject.Find("Moving Up").transform;
        Cannon.Instance.shootable = true;
        Debug.Log("Create Bobble!");
    }

    void OnCollisionEnter(Collision collision)
    {   

        if (collision.collider.tag == Config.staticBobble || collision.collider.tag == Config.topWall)
        {   

            GetComponent<Rigidbody>().velocity = Vector3.zero;
            Destroy(GetComponent<StopBobble>());
            tag = Config.staticBobble;
            m_xy = NearPoint(transform.position);

            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = CreateBobble.Instance.m_bobble[m_xy.x, m_xy.y].pointObject.transform.position; //停在最近点
            //GetComponent<Collider>().isTrigger = true;
            GetComponent<BobbleProperty>().stop = true;

            if (Cannon.Instance.creatable == true)
            {
                CreateShootBobble();
                Cannon.Instance.creatable = false;
            }

            // Put all the same color bobbles into list A
            listA.Clear();
            for (int i = 0; i < m_x; i++)
            {
                for (int j = 0; j < m_y - i % 2; j++)
                {
                    if (CreateBobble.Instance.m_bobble[i, j].bobbleObject != null)
                    {
                        if (CreateBobble.Instance.m_bobble[i, j].bobbleObject.GetComponent<BobbleProperty>().color == GetComponent<BobbleProperty>().color)
                        {
                            xy t_xy;
                            t_xy.x = i;
                            t_xy.y = j;
                            listA.Add(t_xy);
                            //CreateBobble.Instance.m_bobble[i, j].bobbleObject.GetComponent<BobbleProperty>().inListA = true;
                        }
                    }
                }
            }

            CreateBobble.Instance.m_bobble[m_xy.x, m_xy.y].bobbleObject = this.gameObject;

            // Defeat detect
            for (int i = 0; i < m_x; i++)
            {
                for (int j = 0; j < m_y - i % 2; j++)
                {
                    if (CreateBobble.Instance.m_bobble[i, j].bobbleObject != null)
                    {
                        if (CreateBobble.Instance.m_bobble[i, j].bobbleObject.transform.position.x >= (GameObject.Find("Moving Up").transform.position.x - Config.defeatDistance)) 
                        {
                            Cannon.Instance.defeat = true;
                        }
                    }
                }
            }
            //CreateBobble.Instance.m_bobble[m_xy.x, m_xy.y].bobbleObject.transform.parent = GameObject.Find("Top Wall").transform;
            //Debug.Log("Color is " + this.GetComponent<BobbleProperty>().color + " and List A is " + listA.Count);

            // Find the intersect same color bobbles and put them into list B
            all_intersect(m_xy);

            // If there are three same color intersect
            //Debug.Log("Color is " + this.GetComponent<BobbleProperty>().color + " and List B is " + listB.Count);
            if (listB.Count >= 3)
            {
                for (int i = 0; i < listB.Count; i++)
                {
                    xy t_xy = (xy)listB[i];
                    // m_scoretotal1 += CreatBall.Instance.m_Layering; 
                    CreateBobble.Instance.m_bobble[t_xy.x, t_xy.y].bobbleObject.GetComponent<BobbleProperty>().popped = true;
                    Cannon.Instance.PoppedScore();
                    //Destroy(CreateBobble.Instance.m_bobble[t_xy.x, t_xy.y].bobbleObject);
                    CreateBobble.Instance.m_bobble[t_xy.x, t_xy.y].bobbleObject = null;


                }

            }

            // Drop bobbles
            listA.Clear();
            // Put all the bobbles except row 1 into list A
            for (int i = 1; i < m_x; i++)
            {
                for (int j = 0; j < m_y - i % 2; j++)
                {
                    if (CreateBobble.Instance.m_bobble[i, j].bobbleObject != null)
                    {
                        xy t_xy;
                        t_xy.x = i;
                        t_xy.y = j;
                        listA.Add(t_xy);
                        //CreateBobble.Instance.m_bobble[i, j].bobbleObject.GetComponent<BobbleProperty>().inListA = true;
                    }
                }
            }

            // Clean the bobble not intersect with row 1 out of list A
            for (int j = 0; j < m_y; j++)
            {
                if (CreateBobble.Instance.m_bobble[0, j].bobbleObject != null)
                {
                    xy t_xy;
                    t_xy.x = 0;
                    t_xy.y = j;
                    listA.Add(t_xy);
                    all_intersect(t_xy);
                }
            }
            if (Config.debug)
            {
                Debug.Log("Rolling, List A is " + listA.Count);
            }

            if (listA.Count > 0)
            {
                for (int i = 0; i < listA.Count; i++)
                {
                    if (listA[i] != null)
                    {
                        xy t_xy = (xy)listA[i];
                        // m_scoretotal1 += CreatBall.Instance.m_Layering; 
                        CreateBobble.Instance.m_bobble[t_xy.x, t_xy.y].bobbleObject.GetComponent<BobbleProperty>().rolling = true;
                        rollingCount++;
                        //Destroy(CreateBobble.Instance.m_bobble[t_xy.x, t_xy.y].bobbleObject);
                        CreateBobble.Instance.m_bobble[t_xy.x, t_xy.y].bobbleObject = null;
                    }
                }
            }
            if (Config.debug)
            {
                Debug.Log("Rolling count is " + rollingCount);
            }
            if (rollingCount > 0)
            {
                Cannon.Instance.RollingScore(rollingCount);
            }
            rollingCount = 0;

            // Victory detect
            // Defeat detect
            for (int i = 0; i < m_x; i++)
            {
                for (int j = 0; j < m_y - i % 2; j++)
                {
                    if (CreateBobble.Instance.m_bobble[i, j].bobbleObject == null)
                    {
                        Cannon.Instance.victory = true;
                    }
                }
            }
        }

    }

    // Find the near point when the bobble stop
    xy NearPoint(Vector3 point)
    {
        float length = 100f;
        xy nearpoint = new xy();

        for (int i = m_x -1; i >= 0; i--)
        {
            for (int j = 0; j < m_y - (i % 2); j++)
            {
                if (CreateBobble.Instance.m_bobble[i, j].bobbleObject == null)
                {
                    float tempLen = Vector3.Distance(point, CreateBobble.Instance.m_bobble[i, j].pointObject.transform.position);
                    if (tempLen < length)
                    {
                        length = tempLen;

                        nearpoint.x = i;
                        nearpoint.y = j;

                    }
                }
            }
        }
        return nearpoint;
    }


    void all_intersect(xy t_xy)
    {
        stackA.Clear();
        listB.Clear();
        stackA.Push(t_xy);
        xy judgxy;
        xy tempxy;
        while (stackA.Count > 0)
        {
            judgxy = (xy)stackA.Pop();
            for (int i = 0; i < listA.Count; i++)
            {
                if (listA[i] != null)
                {
                    tempxy = (xy)listA[i];
                    if (Vector3.Distance(CreateBobble.Instance.m_bobble[judgxy.x, judgxy.y].bobbleObject.transform.position, CreateBobble.Instance.m_bobble[tempxy.x, tempxy.y].bobbleObject.transform.position) < 2 * Config.radBobble * 1.1f)
                    { 
                        stackA.Push(tempxy);
                        listA[i] = null;
                    }
                }
            }
            listB.Add(judgxy);
            //CreateBobble.Instance.m_bobble[judgxy.x, judgxy.y].bobbleObject.GetComponent<BobbleProperty>().inListB = true;

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBobble : MonoBehaviour {
    private Transform m_transform;
    private Transform muzzleForm;
    private Vector3 shootPos;

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
        m_transform = this.m_transform;
        muzzleForm = GameObject.Find("Muzzle").transform;
        shootPos = muzzleForm.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void CreateShootBobble()
    {
        CreateBobble.Instance.shootBobble[0] = CreateBobble.Instance.shootBobble[1];
        CreateBobble.Instance.shootBobble[0].transform.position = shootPos;   //  Move loading bobble to the muzzle
        Vector3 loadingPos = GameObject.Find("Loading").transform.position;
        CreateBobble.Instance.shootBobble[1] = Instantiate(CreateBobble.Instance.bobbleStyle[Random.Range(0, CreateBobble.Instance.layerMaxBallNum)], loadingPos, Quaternion.identity) as GameObject;
        Cannon.Instance.shootable = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Config.staticBobble || other.tag == Config.topWall)
        {
            Destroy(GetComponent<StopBobble>());
            tag = Config.staticBobble;
            m_xy = NearPoint(transform.position);

            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = CreateBobble.Instance.m_bobble[m_xy.x, m_xy.y].pointObject.transform.position; //停在最近点
            CreateBobble.Instance.m_bobble[m_xy.x, m_xy.y].bobbleObject = this.gameObject;
            //GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Collider>().isTrigger = true;
            //创建要发射的泡泡
            CreateShootBobble();
        }
    }

    xy NearPoint(Vector3 point) //寻找最近点
    {
        float length = 100f;
        xy nearpoint = new xy();

        for (int i = 0; i < m_x; i++)
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
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBobble : MonoBehaviour
{

    public static CreateBobble Instance;
    public GameObject[] shootBobble = new GameObject[2];
    public GameObject[] bobbleStyle = new GameObject[5];
    public int layerMaxBallNum = 5;

    private GameObject randBobble;
    private GameObject leftCorner;
    private Vector3 leftCornerPos;

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
        leftCorner = GameObject.Find("Left Corner");
        leftCornerPos = leftCorner.transform.position;

        // Initiate bobbles
        Init(3);
        //  Create bobbles ready to shoot
        Vector3 shootPos = GameObject.FindGameObjectWithTag("Muzzle").transform.position;
        randBobble = bobbleStyle[Random.Range(0, layerMaxBallNum)];
        shootBobble[0] = Instantiate(randBobble, shootPos, Quaternion.identity) as GameObject;
        shootPos = GameObject.Find("Loading").transform.position;
        randBobble = bobbleStyle[Random.Range(0, layerMaxBallNum)];
        shootBobble[1] = Instantiate(randBobble, shootPos, Quaternion.identity) as GameObject;
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
            if(i % 2 == 0) {
                for (int j = 0; j < col; j++) {
                    m_bobble[i, j].pointObject = Instantiate(leftCorner, new Vector3(leftCornerPos.x + i * 1.732050f * Config.radBobble, leftCornerPos.y - i * Config.offsetHeight, leftCornerPos.z + j * 2 * Config.radBobble), Quaternion.identity);
                    m_bobble[i, j].bobbleObject = null;
                }
            }
            if(i % 2 == 1) {
                for (int j = 0; j < (col - 1); j++) {
                    m_bobble[i, j].pointObject = Instantiate(leftCorner, new Vector3(leftCornerPos.x + i * 1.732050f * Config.radBobble, leftCornerPos.y - i * Config.offsetHeight, leftCornerPos.z + j * 2 * Config.radBobble + Config.radBobble), Quaternion.identity);
                    m_bobble[i, j].bobbleObject = null;
                }
            }

        }
    }
}

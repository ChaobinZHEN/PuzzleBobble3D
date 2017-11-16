using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config : MonoBehaviour {
    public const float rampAngle = 15.0f;
    public const int row = 15;
    public const int col = 8;
    public const float radBobble = 0.30f;
    public const float offsetHeight = 0.15714286f;
    public const string staticBobble = "StaticBobble";
    public const string topWall = "TopWall";
    public const int initRow = 3;

    public const float resetTime = 5.0f;   // Pop the bobble if it can't stop

    public const bool debug = true;

    public const float defeatDistance = 1.26f + radBobble;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

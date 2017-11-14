using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbleProperty : MonoBehaviour {
    public bool dead = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(dead)
        {
            Destroy(this.gameObject);
        }
	}
}

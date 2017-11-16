using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defeat : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == Config.staticBobble)
        {
            GameObject.Find("Cannon").GetComponent<Cannon>().defeat = true;
        }
            
    }
}

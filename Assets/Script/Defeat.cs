using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class Defeat : MonoBehaviour {
    public GameObject canvasPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(Config.debug) {
            Debug.Log("Defeat Plane trigger!");
        }
        if (other.tag == Config.staticBobble)
        {
            GameObject.Find("Cannon").GetComponent<Cannon>().defeat = true;
            /*
            Text text;
            text = GameObject.Find("Game Over/Score Text").GetComponent<Text>();
            text = Cannon.Instance.scoreText;
            */
        }
            
    }
}

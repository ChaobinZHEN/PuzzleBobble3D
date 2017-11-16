using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobbleProperty : MonoBehaviour {
    public bool popped = false;
    public bool rolling = false;
    public bool stop = false;
    // Color Blue = 1, Green = 2, Purple = 3, Red = 4, Yellow = 5
    public int color;

    public bool inListA = false;
    public bool inListB = false;

    private float timer = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if(popped)
        {
            tag = null;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            timer += Time.deltaTime;
            if (timer > 1.0f) {
                timer = 0f;
                Destroy(this.gameObject);
                Debug.Log("Popped!");
            }
        }

        if (rolling)
        {
            tag = null;
            this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            this.gameObject.GetComponent<Collider>().isTrigger = false;
            this.gameObject.GetComponent<Rigidbody>().velocity = (Vector3.up + Vector3.right)*10;
            timer += Time.deltaTime;
            if (timer > 5.0f)
            {
                timer = 0f;
                Destroy(this.gameObject);
                Debug.Log("Rolling!");
            }
        }

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Button : MonoBehaviour {
    [Header("Go to the scene")]
    public string goToTheScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Click() {
        Debug.Log("Level 1 Button click!");
        SceneManager.LoadScene(goToTheScene);
        Time.timeScale = 1f;
        Destroy(GameObject.Find("Game Over"));
    }

}

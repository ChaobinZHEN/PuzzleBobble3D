using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level2Button : MonoBehaviour {
    [Header("Go to the scene")]
    public string goToTheScene;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Click()
    {
        Debug.Log("Level 2 Button click!");
        SceneManager.LoadScene(goToTheScene);
        Time.timeScale = 1f;
    }
}

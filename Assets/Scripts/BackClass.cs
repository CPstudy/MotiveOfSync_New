using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class BackClass : MonoBehaviour {

    public string scene;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape))
        {
            if(scene.Equals("button")) {
                Application.Quit();
            } else if(scene.Equals("stage1"))
            {
                SceneManager.LoadScene("button", LoadSceneMode.Single);
            } else if (scene.Equals("stage2"))
            {
                SceneManager.LoadScene("button", LoadSceneMode.Single);
            }

        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeText : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject text = new GameObject();
        TextMesh t = text.AddComponent<TextMesh>();
        t.text = "new text set";
        t.fontSize = 15;
        t.transform.localEulerAngles += new Vector3(0, 0, 0);
        t.transform.localPosition += new Vector3(0.04f, 1.98f, 16.49f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

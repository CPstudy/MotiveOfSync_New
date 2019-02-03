using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageChanger : MonoBehaviour {
    
    public Material[] materials;

    public void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = materials[2];
    }
}

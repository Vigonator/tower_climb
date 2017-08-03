using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCam : MonoBehaviour {

    public GameObject cam;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(-cam.transform.position);
	}
}

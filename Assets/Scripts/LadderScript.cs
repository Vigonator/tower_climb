using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderScript : MonoBehaviour {

    public GameObject icon;

   

    

	// Use this for initialization
	void Start () {
        icon.SetActive(false);


    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void iconOn(float y)
    {
        icon.SetActive(true);

        icon.transform.position = new Vector3(icon.transform.position.x, y, icon.transform.position.z);
    }

    public void iconOff()
    {
        icon.SetActive(false);
    }
}

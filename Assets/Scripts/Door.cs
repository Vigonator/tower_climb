using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    public GameObject iconOut, iconIn;
    

    public GameObject moveNodeIn, moveNodeOut;

    public GameObject model;

    // Use this for initialization
    void Start () {
        iconOut.SetActive(false);
        iconIn.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        
        
	}

    public void iconOutOn(float y)
    {
        iconOut.SetActive(true);

        iconOut.transform.position = new Vector3(iconOut.transform.position.x, y, iconOut.transform.position.z);
    }

    public void iconInOn(float y)
    {
        iconIn.SetActive(true);

        iconIn.transform.position = new Vector3(iconIn.transform.position.x, y, iconIn.transform.position.z);
    }

    public void iconOutOff()
    {
        iconOut.SetActive(false);

        
    }

    public void iconInOff()
    {
        iconIn.SetActive(false);


    }

    public void openDoor()
    {
        model.SetActive(false);
    }

    public void closeDoor()
    {
        model.SetActive(true);
    }
}

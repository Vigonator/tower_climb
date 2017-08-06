using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEnemyContact : Weapon {

	// Use this for initialization
	void Start () {
        damage = 4;
        knockback = 2;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        onHit(other);
    }

    private void OnTriggerStay(Collider other)
    {
        onHit(other);
    }
}

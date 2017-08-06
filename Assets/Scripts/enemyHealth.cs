using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealth : HealthBehaviour {

	// Use this for initialization
	void Start () {
        damageDealerType = "PlayerWeapon";

        maxHealth = 20;
        health = maxHealth;


        entityMovement = this.GetComponent<EntityMovement>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

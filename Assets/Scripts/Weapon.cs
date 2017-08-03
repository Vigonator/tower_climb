using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    protected float damage, knockback, cycletime;

    protected HealthBehaviour healthBehaviour;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        healthBehaviour = other.GetComponent<HealthBehaviour>();

        if (healthBehaviour != null && this.tag == healthBehaviour.damageDealerType)
        {
            Vector3 knockbackDirection = new Vector3(other.transform.position.x - this.transform.position.x, 0, other.transform.position.z - this.transform.position.z).normalized;
            knockbackDirection.y = 4;

            knockbackDirection *= knockback;

            healthBehaviour.hit(damage, knockbackDirection);

            Debug.Log("hit" + other);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    protected float damage, knockback, cycleTime, fireTime;
    protected int ammo;
    protected bool canFire;
    protected HealthBehaviour healthBehaviour;
    protected Collider cl;

	// Use this for initialization
	void Start () {
        cl = GetComponent<Collider>();

        cl.enabled = false;

        canFire = true;

        lateStart();
	}

    protected virtual void lateStart()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void checkFire()
    {
        

        

        if(canFire == true && ammo > 0)
        {
            canFire = false;
            fire();
        }

        
    }

    protected void fire()
    {
        StartCoroutine("cycle");

        animate();

        StartCoroutine("firing");
    }

    protected void onHit(Collider other)
    {
        healthBehaviour = other.GetComponent<HealthBehaviour>();
        
        if (healthBehaviour != null && this.tag == healthBehaviour.damageDealerType )
            {
                Vector3 knockbackDirection = new Vector3(other.transform.position.x - this.transform.position.x, 0, other.transform.position.z - this.transform.position.z).normalized;
                knockbackDirection.y = 4;

                knockbackDirection *= knockback;

                healthBehaviour.hit(damage, knockbackDirection);
    
                Debug.Log("hit " + other + "with" + this.gameObject);
            }
    }

    protected virtual void animate()
    {

    }

    IEnumerator cycle()
    {
        canFire = false;

        Debug.Log("cycle started");

        yield return new WaitForSeconds(cycleTime);

        Debug.Log("cycle ended");

        canFire = true;
    }

    IEnumerator firing()
    {
        cl.enabled = true;

        yield return new WaitForSeconds(fireTime);

        cl.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
            onHit(other);        
    }




}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBehaviour : MonoBehaviour {

    public float health, maxHealth, iTime, contactDamage;

    public string damageDealerType;

    protected bool executeDamageBehaviour = true;

    Weapon damageDealer;

    protected EntityMovement entityMovement;

	// Use this for initialization
	void Start () {
        health = maxHealth;

        damageDealerType = "playerWeapon";
        entityMovement = this.GetComponent<EntityMovement>();
	}
	
	// Update is called once per frame
	void Update () {
        
        

        healthPercentBehaviour(health / maxHealth);

		if(health <= 0)
        {
            emptyHealth();
        }
	}

    public virtual void hit(float damage)
    {
        if (executeDamageBehaviour)
        {
            


            causeDamage(damage);
        }
    }

    public virtual void hit(float damage, Vector3 knockback)
    {
        if (executeDamageBehaviour)
        {
            entityMovement.moveDirection = transform.TransformDirection(knockback);

            causeDamage(damage);
        }
    }

    protected virtual void causeDamage(float damage)
    {
        health -= damage;

       
    }

    protected virtual void emptyHealth()
    {
        Destroy(this.gameObject);
    }

    protected virtual void healthPercentBehaviour(float percent)
    {

    }

    

    
}

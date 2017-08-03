using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthBehaviour {



	// Use this for initialization
	void Start () {
        damageDealerType = "Enemy";

        maxHealth = 20;
        health = maxHealth;
        iTime = 0.8F;

        entityMovement = this.GetComponent<EntityMovement>();
    }

    public override void hit(float damage)
    {
        if (executeDamageBehaviour)
        {
            StartCoroutine("invincibility");
            causeDamage(damage);
        }
    }

    public override void hit(float damage, Vector3 knockback)
    {

        Debug.Log("Player hit with knockback");
        if (executeDamageBehaviour)
        {
            StartCoroutine("invincibility");
            entityMovement.controller.Move(new Vector3(transform.TransformDirection(knockback).x, 0, transform.TransformDirection(knockback).z) * Time.deltaTime);
            entityMovement.moveDirection.y = knockback.y;
            entityMovement.status.jumping = true;

            causeDamage(damage);
        }
    }

    protected override void causeDamage(float damage)
    {
        health -= damage;

        executeDamageBehaviour = false;
    }

    IEnumerator invincibility()
    {
        executeDamageBehaviour = false;

        StartCoroutine("invincibilityIndicator");

        yield return new WaitForSeconds(iTime);

        executeDamageBehaviour = true;
    }

    IEnumerator invincibilityIndicator()
    {
        Color32 regColor;

        Renderer renderer;
        renderer = this.gameObject.GetComponent<Renderer>();


        regColor = renderer.material.GetColor("_Color");
        

        

        while (!executeDamageBehaviour)
        {

            renderer.material.SetColor("_Color", new Color32(150, 185, 255, 255));

            yield return new WaitForSeconds(0.2F);


            renderer.material.SetColor("_Color", regColor);

            yield return new WaitForSeconds(0.2F);
        }
    }
}

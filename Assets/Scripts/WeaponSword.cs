using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSword : Weapon
{

    // Use this for initialization
    protected override void lateStart()
    {
        damage = 6;
        knockback = 2;

        cycleTime = 1.2F;
        fireTime = 0.6F;
        ammo = 2;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {

            

            checkFire();
        }
    }


}


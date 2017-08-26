using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : EntityMovement {
    float direction = -1;

    
    // Use this for initialization

    protected override void edgeEnterBehaviour()
    {
        direction = -direction;


    }
    

    protected override void getFinalSpeed()
    {
        finalSpeed = 0 * direction;
    }
}

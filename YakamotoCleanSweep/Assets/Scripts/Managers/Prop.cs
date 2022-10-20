using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop : PropManager
{
    // Start is called before the first frame update

    private int HEALTH = 0; //TODO Change this to a Health object

    void Start()
    {
        
    }


    private void TakingDamage(int damage)
    {
        if (HEALTH <= damage)
        {
            HEALTH = 0;
            base.IncreaseCount();
        }
        else
            HEALTH -= damage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int maxHP = 3;

    public int HP {get; private set;}

    public HPDisplay display;

    private void Start()
    {
        HP = maxHP;
        display.createHP(maxHP);
    }

    public void reset()
    {
        HP = maxHP;
        display.createHP(maxHP);
    }

    public void gotHit()
    {
        if(HP > 0)
        {
            display.removeHP(--HP);
        }
    }


}

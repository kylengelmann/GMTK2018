using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public enum AttackType
    {
        up = 0,
        mid = 1,
        down = 2
    }

    public int comboLength = 3;

    AttackType nextAttack;

    Animator anim;

	void Start () {
		nextAttack = randomType();
        anim = GetComponent<Animator>();
        onAttackEnd();
	}
	
	void Update () {
		
	}

    AttackType randomType()
    {
        return (AttackType) Mathf.Min(Mathf.FloorToInt(Random.value*3f), 2);
    }

    void onAttackEnd()
    {
        string trigger = "attack";
        switch(nextAttack)
        {
            case AttackType.up:
                trigger += "Up";
                break;
            case AttackType.mid:
                trigger += "Mid";
                break;
            case AttackType.down:
                trigger += "Down";
                break;
        }
        anim.ResetTrigger(trigger);
        anim.SetTrigger(trigger);
        nextAttack = randomType();
    }

}

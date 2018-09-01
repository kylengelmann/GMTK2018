using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour {

    Animator anim;

	void Start () {
		anim = GetComponent<Animator>();
	}
	
    Attack.AttackType dodging = Attack.AttackType.Invalid;
    
    string trigger;

	void Update () {
        if(dodging != Attack.AttackType.Invalid) return;
		if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            trigger = "dodgeDown";
            dodging = Attack.AttackType.up;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            trigger = "dodgeUp";
            dodging = Attack.AttackType.down;
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            trigger = "dodgeMid";
            dodging = Attack.AttackType.mid;
        }

        if(dodging != Attack.AttackType.Invalid)
        {
            anim.ResetTrigger(trigger);
            anim.SetTrigger(trigger);
        }

	}

    void onFinishDodge()
    {
        dodging = Attack.AttackType.Invalid;
    }
}

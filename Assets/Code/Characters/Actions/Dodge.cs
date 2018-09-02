using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour {

    Animator anim;
    TileWalk walk;

    Health health;

	void Start () {
        health = GetComponent<Health>();
        anim = GetComponent<Animator>();
        AnimationEventReceiver receiver;
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>();
            receiver = GetComponentInChildren<AnimationEventReceiver>();
        }
        else
        {
            receiver = GetComponent<AnimationEventReceiver>();
        }
        receiver.onDodgeEnd += onFinishDodge;
        receiver.startDodging += () => {isDodging = true;};
        receiver.endDodging += () => {isDodging = false;};

        walk = GetComponent<TileWalk>();
	}
	
    Attack.AttackType dodging = Attack.AttackType.Invalid;
    
    string trigger;

	void Update () {
        if(dodging != Attack.AttackType.Invalid) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
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
        if(dodging != Attack.AttackType.Invalid)
        {
            walk.canMove = false;
        }

	}

    void onFinishDodge()
    {
        dodging = Attack.AttackType.Invalid;
        walk.canMove = true;
    }

    bool isDodging;

    public bool checkDodge(Attack.AttackType attackType)
    {
        if(!isDodging || dodging != attackType)
        {
            walk.setMove(Vector2.left);
            health.gotHit();
            if(health.HP == 0)
            {
                GameManager.gameManager.resetLevel();
            }
            return false;
        }
        return true;
    }
}

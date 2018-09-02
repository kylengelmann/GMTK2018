using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour {

    Animator anim;
    TileWalk walk;

    Health health;

    SpriteRenderer sr;

    void Start () {
        sr = GetComponentInChildren<SpriteRenderer>();
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
        receiver.endGotHit += endGotHit;

        walk = GetComponent<TileWalk>();
	}
	
    Attack.AttackType dodging = Attack.AttackType.Invalid;
    
    string trigger;

	void Update () {
        if(dodging != Attack.AttackType.Invalid || !player.freeToAct) {
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
        else if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            trigger = "dodgeMid";
            dodging = Attack.AttackType.mid;
        }

        if(dodging != Attack.AttackType.Invalid)
        {
            anim.ResetTrigger(trigger);
            anim.SetTrigger(trigger);
            player.freeToAct = false;
        }

	}

    void onFinishDodge()
    {
        dodging = Attack.AttackType.Invalid;
        //walk.canMove = true;
        player.freeToAct = true;
    }

    bool isDodging;

    public bool checkDodge(Attack.AttackType attackType, Vector2 direction, bool takeDamage)
    {
        if(!isDodging || dodging != attackType)
        {
            sr.flipX = direction.x > 0f;
            walk.setMove(direction);
            if(takeDamage) {
                health.gotHit();
            }
            player.freeToAct = false;
            if(health.HP == 0)
            {
                anim.ResetTrigger("died");
                anim.SetTrigger("died");
                GetComponent<BoxCollider2D>().enabled = false;
            }
            else
            {
                anim.ResetTrigger("wasHit");
                anim.SetTrigger("wasHit");
                
            }
            return false;
        }
        return true;
    }

    void endGotHit()
    {
        anim.ResetTrigger("dodgeMid");
        anim.ResetTrigger("dodgeDown");
        anim.ResetTrigger("dodgeUp");
        anim.ResetTrigger("move");
        player.freeToAct = true;
        isDodging = false;
        dodging = Attack.AttackType.Invalid;
        if (health.HP == 0)
        {
            GameManager.gameManager.resetLevel();
        }
    }

    void reset()
    {
        isDodging = false;
        dodging = Attack.AttackType.Invalid;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}

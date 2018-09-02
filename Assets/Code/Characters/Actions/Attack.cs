using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public float MaxTimeBetweenAttacks = .5f;

    public float MinTimeBetweenAttacks = 0f;

    float timeBetween;

    float t;

    bool isCheckingHits;

    public bool canAttackUp = true;
    public bool canAttackMid = true;
    public bool canAttackDown = true;

    public enum AttackType
    {
        Invalid = -1,
        up = 0,
        mid = 1,
        down = 2
    }

    public int comboLength = 3;

    AttackType nextAttack;
    AttackType currentAttack;

    Animator anim;

    bool wasDodged;

    Health health;

	void Start () {
        health = GetComponent<Health>();
		nextAttack = randomType();
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

        receiver.onAttackEnd += onAttackEnd;
        receiver.startHitCheck += () => {isCheckingHits = true;};
        receiver.endHitCheck += () => { isCheckingHits = false; };

    }
	
	void Update () {
        if(playerInRange && !isAttacking)
        {
            t += Time.deltaTime;
            if(t >= timeBetween)
            {
                startAttack();
                resetTimer();
            }
        }
        else if(isCheckingHits)
        {
            if (player == null) {
                wasDodged = false;
                return;
            }
            Dodge dodge = player.GetComponent<Dodge>();

            if(!dodge.checkDodge(currentAttack, new Vector2(-transform.localScale.x, 0f), health != null))
            {
                isCheckingHits = false;
                wasDodged = false;
            }
        }
	}

    GameObject player;

    AttackType randomType()
    {
        AttackType result = AttackType.Invalid;
        while(result == AttackType.Invalid)
        {
            result = (AttackType)Mathf.Min(Mathf.FloorToInt(Random.value * 3f), 2);
            if(result == AttackType.up && !canAttackUp)
            {
                result = AttackType.Invalid;
            }
            else if (result == AttackType.down && !canAttackDown)
            {
                result = AttackType.Invalid;
            }
            else if (result == AttackType.mid && !canAttackMid)
            {
                result = AttackType.Invalid;
            }
        }
        return result;
    }

    bool isAttacking;

    void startAttack()
    {
        wasDodged = true;
        isAttacking = true;
        string trigger = "attack";
        switch (nextAttack)
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
        currentAttack = nextAttack;
        nextAttack = randomType();
    }

    void onAttackEnd()
    {
        isAttacking = false;
        if(wasDodged && health != null)
        {
            health.gotHit();
            if(health.HP == 0)
            {
                anim.SetTrigger("died");
                foreach (BoxCollider2D box in GetComponents<BoxCollider2D>())
                {
                    box.enabled = false;
                }
            }
        }
    }

    bool playerInRange;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInRange = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player = null;
            playerInRange = false;
        }

        resetTimer();
    }

    void resetTimer()
    {
        t = 0f;
        timeBetween = Mathf.Lerp(MinTimeBetweenAttacks, MaxTimeBetweenAttacks, Random.value);
    }

    void reset()
    {
        anim.ResetTrigger("resetti");
        anim.SetTrigger("resetti");
        foreach (BoxCollider2D box in GetComponents<BoxCollider2D>())
        {
            box.enabled = true;
        }
    }


}

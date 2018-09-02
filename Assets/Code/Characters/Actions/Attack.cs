using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public float MaxTimeBetweenAttacks = .5f;

    public float MinTimeBetweenAttacks = 0f;

    float timeBetween;

    float t;

    bool isCheckingHits;

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

	void Start () {
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
            if (player == null) return;
            Dodge dodge = player.GetComponent<Dodge>();
            if(dodge == null) return;

            if(!dodge.checkDodge(currentAttack))
            {
                isCheckingHits = false;
            }
        }
	}

    GameObject player;

    AttackType randomType()
    {
        return (AttackType) Mathf.Min(Mathf.FloorToInt(Random.value*3f), 2);
    }

    bool isAttacking;

    void startAttack()
    {
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

}

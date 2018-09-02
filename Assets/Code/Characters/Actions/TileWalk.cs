using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWalk : MonoBehaviour {

    public AnimationCurve movementCurve;
    public float moveTime = .5f;

    //Vector2 position;
    [HideInInspector] public bool canMove = true;
    bool isMoving;
    bool isBumping;
    Vector2 lastPos;
    Vector2 moveDir;

    Vector2 nextMove;

    bool nextSet;

    float t;

    TilePosition tilePos;
    SpriteRenderer sr;
    Animator anim;

    bool isOnLadder;

    private void Start()
    {
        tilePos = GetComponent<TilePosition>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    void Update () {

        if(!nextSet && (player.freeToAct || isMoving)) {
            if (Input.GetKeyDown(KeyCode.D))
            {
                nextSet = true;
                nextMove = Vector2.right;
                sr.flipX = false;
            }
            else if(Input.GetKeyDown(KeyCode.A))
            {
                nextSet = true;
                nextMove = Vector2.left;
                sr.flipX = true;
            }
            else if(Input.GetKeyDown(KeyCode.W))
            {
                nextSet = true;
                nextMove = Vector2.up;
                sr.flipX = false;
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                nextSet = true;
                nextMove = Vector2.down;
                sr.flipX = false;
            }
        }
        if (!isMoving && player.freeToAct && nextSet)
        {
            if(setMove(nextMove))
            {
                player.freeToAct = false;
                anim.SetBool("isOnLadder", isOnLadder);
                anim.ResetTrigger("move");
                anim.SetTrigger("move");
            }
            nextSet = false;
        }
        
        if(isMoving){
            t += Time.deltaTime;

            if(t >= moveTime)
            {
                isMoving = false;
                tilePos.setTransform(tilePos.getPosition());
                player.freeToAct = true;
                return;
            }
            else if(t >= moveTime*.6f)
            {
                player.freeToAct = true;
            }

            float moveDist = movementCurve.Evaluate(t/moveTime);

            tilePos.setTransform(lastPos + moveDir*moveDist);
        }
    }

    public bool setMove(Vector2 moveDir)
    {
        this.moveDir = moveDir;
        t = 0f;
        lastPos = tilePos.getPosition();

        if (isOnLadder && !Mathf.Approximately(moveDir.x, 0))
        {
            return false;
        }

        Collider2D collider = Physics2D.OverlapPoint(lastPos + moveDir + new Vector2(.5f, .5f), ~LayerMask.GetMask("Player"));
        if (collider == null || collider.isTrigger) {


            if(!isOnLadder && !Mathf.Approximately(moveDir.y, 0f)) {
                if (collider != null && collider.gameObject.layer == LayerMask.NameToLayer("Ladder")) { 
                    isOnLadder = true;
                }
                else
                {
                    return false;
                }
            }
            else if(isOnLadder && (collider == null || !(collider.gameObject.layer == LayerMask.NameToLayer("Ladder"))))
            {
                GetComponentInChildren<playerAudio>().scrape();
                isOnLadder = false;
            }

            tilePos.setPosition(lastPos + moveDir, false);
            isMoving = true;
            if(isOnLadder)
            {
                GetComponentInChildren<playerAudio>().scrape();
            }
            return true;
        }
        return false;
    }

    void reset()
    {
        nextSet = false;
        isMoving = false;
    }
}

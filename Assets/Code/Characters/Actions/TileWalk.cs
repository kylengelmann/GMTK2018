﻿using System.Collections;
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

    private void Start()
    {
        tilePos = GetComponent<TilePosition>();
    }

    void Update () {
        if(!nextSet) {
            if (Input.GetKeyDown(KeyCode.D))
            {
                nextSet = true;
                nextMove = Vector2.right;
            }
            else if(Input.GetKeyDown(KeyCode.A))
            {
                nextSet = true;
                nextMove = Vector2.left;
            }
            else if(Input.GetKeyDown(KeyCode.W))
            {
                nextSet = true;
                nextMove = Vector2.up;
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                nextSet = true;
                nextMove = Vector2.down;
            }
        }
        if (!isMoving && canMove && nextSet)
        {
            setMove(nextMove);
            nextSet = false;
        }
        
        if(isMoving){
            t += Time.deltaTime;

            if(t >= moveTime)
            {
                isMoving = false;
                tilePos.setTransform(tilePos.getPosition());
                return;
            }

            float moveDist = movementCurve.Evaluate(t/moveTime);

            tilePos.setTransform(lastPos + moveDir*moveDist);
        }
    }

    public void setMove(Vector2 moveDir)
    {
        this.moveDir = moveDir;
        t = 0f;
        lastPos = tilePos.getPosition();

        Collider2D collider = Physics2D.OverlapPoint(lastPos + moveDir + new Vector2(.5f, .5f), ~LayerMask.GetMask("Player"));
        if (collider == null || collider.isTrigger) {
            tilePos.setPosition(lastPos + moveDir, false);
            isMoving = true;
        }
    }

    void reset()
    {
        nextSet = false;
        isMoving = false;
        canMove = true;
    }
}

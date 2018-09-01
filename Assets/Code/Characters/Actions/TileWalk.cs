using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWalk : MonoBehaviour {

    public AnimationCurve movementCurve;
    public float moveTime = .5f;

    public AnimationCurve bumpCurve;
    public float bumpTime = .1f;
    public float bumpDist = .2f;

    Vector2 position = Vector2.zero;
    bool isMoving;
    bool isBumping;
    Vector2 lastPos;
    Vector2 moveDir;

    Vector2 nextMove;

    bool nextSet;

    float t;
	
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
        if (!isMoving && !isBumping && nextSet)
        {
            setMove(nextMove);
            nextSet = false;
        }
        
        if(isMoving){
            t += Time.deltaTime;

            if(t >= moveTime)
            {
                isMoving = false;
                setTransform(position);
                return;
            }

            float moveDist = movementCurve.Evaluate(t/moveTime);

            setTransform(lastPos + moveDir*moveDist);
        }
        else if(isBumping)
        {
            t += Time.deltaTime;

            if (t >= bumpTime)
            {
                isBumping = false;
                setTransform(position);
                return;
            }

            float moveDist = bumpDist*bumpCurve.Evaluate(t/bumpTime);

            setTransform(position + moveDir * moveDist);
        }
    }

    void setTransform(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    void setMove(Vector2 moveDir)
    {
        this.moveDir = moveDir;
        t = 0f;
        if (Physics2D.OverlapPoint(position + moveDir + new Vector2(.5f, .5f), -1) == null) {
            lastPos = position;
            position += moveDir;
            isMoving = true;
        }
        else if(Mathf.Approximately(moveDir.y, 0))
        {
            isBumping = true;
        }
    }
}

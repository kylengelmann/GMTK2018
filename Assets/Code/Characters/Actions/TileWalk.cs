using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileWalk : MonoBehaviour {

    public AnimationCurve movementCurve;
    public float moveTime = .5f;
    Vector2 _position = Vector2.zero;
    bool isMoving;
    Vector2 lastPos;
    Vector2 moveDir;

    Vector2 nextPos;

    public Vector2 position
    {
        get
        {
            return _position;
        }
        set
        {
            lastPos = _position;
            _position = value;
            moveDir = _position - lastPos;
            t = 0f;
            isMoving = true;
        }
    }

    bool nextSet;

    float t;
	
	void Update () {
        if(!nextSet) {
            if (Input.GetKeyDown(KeyCode.D))
            {
                nextSet = true;
                nextPos = _position + Vector2.right;
            }
            else if(Input.GetKeyDown(KeyCode.A))
            {
                nextSet = true;
                nextPos = _position - Vector2.right;
            }
            else if(Input.GetKeyDown(KeyCode.W))
            {
                nextSet = true;
                nextPos = _position + Vector2.up;
            }
            else if(Input.GetKeyDown(KeyCode.S))
            {
                nextSet = true;
                nextPos = _position - Vector2.up;
            }
        }
        if (!isMoving && nextSet)
        {
            position = nextPos;
            nextSet = false;
        }
        
        if(!isMoving) return;
        t += Time.deltaTime;

        if(t >= moveTime)
        {
            isMoving = false;
            setPos(_position);
        }

        float moveDist = movementCurve.Evaluate(t/moveTime);

        setPos(lastPos + moveDir*moveDist);
    }

    void setPos(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    } 
}

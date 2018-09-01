
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePosition : MonoBehaviour {

    Vector2 position;

    void Start () {
        position = new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y));
        setTransform(position);
    }

    public void setTransform(Vector2 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, transform.position.z);
    }

    public void setPosition(Vector2 newPos)
    {
        position = new Vector2(Mathf.Round(newPos.x), Mathf.Round(newPos.y));
    }

    public Vector2 getPosition()
    {
        return position;
    }
}

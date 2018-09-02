using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {


    public TilePosition start;
    public Level next;

    int numChests;
    int chestsLeft;

	void Start () {
		numChests = GetComponentsInChildren<Chest>().Length;
        chestsLeft = numChests;
	}

    public void openChest()
    {
        if(--chestsLeft == 0)
        {
            GameManager.gameManager.completeLevel();
        }
    }

    public Vector2 getPosition()
    {
        return start.getPosition();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {


    public TilePosition start;
    public Level next;

    int numChests;
    int chestsLeft;

    float openTime = 2f;

	void Start () {
		numChests = GetComponentsInChildren<Chest>().Length;
        chestsLeft = numChests;
	}

    void reset()
    {
        chestsLeft = numChests;
    }

    public void openChest()
    {
        if(--chestsLeft == 0)
        {
            StartCoroutine(startNextLevel());
        }
    }

    public Vector2 getPosition()
    {
        return start.getPosition();
    }

    IEnumerator startNextLevel()
    {
        yield return new WaitForSeconds(openTime);
        GameManager.gameManager.completeLevel();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerReset : characterReset {

    public void Start()
    {
        GameManager.gameManager.player = gameObject;
        transform.SetParent(GameManager.gameManager.currentLevel.transform);
        BroadcastMessage("reset");
    }

    public override void reset()
    {
        GetComponent<TilePosition>().setPosition(GameManager.gameManager.currentLevel.getPosition());
        player.freeToAct = true;
    }
}

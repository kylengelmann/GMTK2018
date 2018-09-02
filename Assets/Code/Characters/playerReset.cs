using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerReset : characterReset {

    public override void reset()
    {
        GetComponent<TilePosition>().setPosition(GameManager.gameManager.levelStart.getPosition());
    }
}

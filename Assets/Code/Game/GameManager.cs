using UnityEngine;

public class GameManager : MonoBehaviour {
   
    public static GameManager gameManager;

    public TilePosition levelStart;

    private void Awake()
    {
        gameManager = this;
    }

    
    public void resetLevel()
    {
        BroadcastMessage("reset", SendMessageOptions.DontRequireReceiver);
    }


}

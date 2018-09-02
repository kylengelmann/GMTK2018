using UnityEngine;

public class GameManager : MonoBehaviour {
   
    public static GameManager gameManager;
    [HideInInspector] public GameObject player;

    public Level level1;
    [HideInInspector]public Level currentLevel;

    private void Awake()
    {
        gameManager = this;
        currentLevel = level1;
    }

    
    public void resetLevel()
    {
        currentLevel.BroadcastMessage("reset", SendMessageOptions.DontRequireReceiver);
    }

    public void completeLevel()
    {
        currentLevel = currentLevel.next;
        player.transform.SetParent(currentLevel.transform);
        resetLevel();
    }


}

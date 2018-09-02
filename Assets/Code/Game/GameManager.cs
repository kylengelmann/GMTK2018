using UnityEngine;

public class GameManager : MonoBehaviour {
   
    public static GameManager gameManager;
    [HideInInspector] public GameObject player;

    [SerializeField] Level level1;
    [HideInInspector]public Level currentLevel;
    public float speedIncrease =.15f;

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
        if(currentLevel.next == null) currentLevel.next = level1;
        if(currentLevel.next == level1)
        {
            Time.timeScale += speedIncrease;
            GetComponent<AudioSource>().pitch = Time.timeScale;
        }
        currentLevel = currentLevel.next;
        player.transform.SetParent(currentLevel.transform);
        resetLevel();
    }


}

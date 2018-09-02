using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudio : MonoBehaviour {

  public AudioSource playerSounds;
    public AudioClip[] ouches;
  public AudioClip dieSound;
  public AudioClip moveSound;
    public AudioClip scrapeSound;
  public AudioClip keyPickUp;

  public void ouch()
  {
    playerSounds.clip = ouches[Mathf.FloorToInt(Random.value*ouches.Length - .001f)];
    playerSounds.Play();
  }

  public void die()
  {
    playerSounds.clip = dieSound;
    playerSounds.Play();
  }

  public void move()
  {
    playerSounds.clip = moveSound;
    playerSounds.Play();
  }

    public void scrape()
    {
        playerSounds.clip = scrapeSound;
        playerSounds.Play();
    }

  public void keySound()
  {
    playerSounds.clip = keyPickUp;
    playerSounds.Play();
  }
  
  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

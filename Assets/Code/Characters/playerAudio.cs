using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudio : MonoBehaviour {

  public AudioSource playerSounds;
  public AudioClip ouch1;
  public AudioClip ouch2;
  public AudioClip ouch3;
  public AudioClip ouch4;
  public AudioClip ouch5;
  public AudioClip dieSound;
  public AudioClip moveSound;
  public AudioClip keyPickUp;

  public void ouch()
  {
    playerSounds.clip = ouch1;
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

  
  // Use this for initialization
  void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

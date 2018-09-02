using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestAudio : MonoBehaviour {

  public AudioSource chestSound;
  public AudioClip chestOpen;

  public void opening()
  {
    chestSound.clip = chestOpen;
    chestSound.Play();
  }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

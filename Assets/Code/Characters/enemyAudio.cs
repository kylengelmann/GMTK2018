using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAudio : MonoBehaviour {

  public AudioSource enemySounds;
  public AudioClip enemySwoosh;
    public AudioClip enemyCharge;

  public void woosh()
  {
    enemySounds.clip = enemySwoosh;
    enemySounds.Play();
  }
   
    public void charge()
    {
        enemySounds.clip = enemyCharge;
        enemySounds.Play();
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

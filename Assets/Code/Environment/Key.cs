using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<keyCollector>().addKey();
            GetComponent<BoxCollider2D>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    void reset()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}

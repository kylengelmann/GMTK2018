using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    Animator anim;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void open()
    {
        anim.ResetTrigger("open");
        anim.SetTrigger("open");
    }

    void reset()
    {
        anim.ResetTrigger("open");
        anim.ResetTrigger("resetti");
        anim.SetTrigger("resetti");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.layer == LayerMask.NameToLayer("Player"))
        {
            keyCollector collector = other.GetComponent<keyCollector>();
            if(collector.numKeys > 0)
            {
                collector.useKey();
                open();
                GameManager.gameManager.currentLevel.openChest();
            }
        }
    }
}

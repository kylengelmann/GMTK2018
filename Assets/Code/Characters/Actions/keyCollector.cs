using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyCollector : MonoBehaviour {

    public int numKeys = 0;
	public KeyDisplay display;

    public void addKey()
    {
        numKeys++;
        display.addKey();
    }

    public void useKey()
    {
        numKeys --;
        display.removeKey();
    }

    private void reset()
    {
        numKeys = 0;
        display.reset();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDisplay : MonoBehaviour {

	public float padding = 50f;

    public GameObject keyTemplate;

    List<GameObject> keys = new List<GameObject>();

    public void addKey()
    {
        GameObject newKey = Instantiate(keyTemplate, transform);
        newKey.GetComponent<RectTransform>().anchoredPosition = new Vector2(keys.Count*padding, 0f);
        keys.Add(newKey);
    }

    public void reset()
    {
        foreach(GameObject key in keys)
        {
            Destroy(key);
        }
        keys.RemoveAll(key => { return true;});
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPDisplay : HPDisplay {

    public GameObject HPTemplate;
    public Sprite HasHP;
    public Sprite LostHP;

    List<RectTransform> hpImgs = new List<RectTransform>();

    public override void createHP(int HP)
    {
        for (; hpImgs.Count > 0; hpImgs.RemoveAt(0))
        {
            Destroy(hpImgs[0].gameObject);
        }
        for (int i = 0; i < HP; i++)
        {
            RectTransform newHP = Instantiate(HPTemplate, transform).GetComponent<RectTransform>();
            hpImgs.Add(newHP);
            newHP.GetComponent<Image>().sprite = HasHP;
        }
    }

    public override void removeHP(int HPLeft)
    {
        hpImgs[HPLeft].GetComponent<Image>().sprite = LostHP;
    }
}

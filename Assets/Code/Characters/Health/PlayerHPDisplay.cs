using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPDisplay : HPDisplay {

    public float padding = 105f;
    public GameObject HPTemplate;
    public Sprite HasHP;
    public Sprite LostHP;

    List<RectTransform> hpImgs = new List<RectTransform>();

    public override void createHP(int HP)
    {
        for(int i = 0; i < HP; i++) {
            RectTransform newHP = Instantiate(HPTemplate, transform).GetComponent<RectTransform>();
            newHP.anchoredPosition = new Vector2(i*padding, 0f);
            hpImgs.Add(newHP);
            newHP.GetComponent<Image>().sprite = HasHP;
        }

    }

    public override void removeHP(int HPLeft)
    {
        hpImgs[HPLeft].GetComponent<Image>().sprite = LostHP;
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HPDisplay : MonoBehaviour {

    public abstract void createHP(int HP);
    public abstract void removeHP(int HPLeft);
}

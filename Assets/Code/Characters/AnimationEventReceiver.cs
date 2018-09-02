using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventReceiver : MonoBehaviour {

    public System.Action onAttackEnd;
    public System.Action startHitCheck;
    public System.Action endHitCheck;

    public System.Action onDodgeEnd;
    public System.Action startDodging;
    public System.Action endDodging;

    public System.Action endGotHit;
    

    void OnAttackEnd()
    {
        if(onAttackEnd != null)
        {
            onAttackEnd.Invoke();
        }
    }

    void StartHitCheck()
    {
        if(startHitCheck != null)
        {
            startHitCheck.Invoke();
        }
    }

    void EndHitCheck()
    {
        if (endHitCheck != null)
        {
            endHitCheck.Invoke();
        }
    }

    void OnDodgeEnd()
    {
        if(onDodgeEnd != null)
        {
            onDodgeEnd.Invoke();
        }
    }

    void StartDodging()
    {
        if (startDodging != null)
        {
            startDodging.Invoke();
        }
    }

    void EndDodging()
    {
        if(endDodging != null)
        {
            endDodging.Invoke();
        }
    }

    void EndGotHit()
    {
        if(endGotHit != null)
        {
            endGotHit.Invoke();
        }
    }



}
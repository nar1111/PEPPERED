using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uni_Event_Trg : MonoBehaviour
{
    public bool SelfDestruct;
    public bool PushAct;
    public string TrgTag;
    public UnityEngine.Events.UnityEvent EventName;
    private bool InReach;


    private void Update()
    {
        if (MySceneManager.Act && PushAct && InReach && MySceneManager.CutscenePlaying == false)
        {
            EventName.Invoke();
            if (SelfDestruct) { Destroy(gameObject); }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        //If you need to just bump into collider
        if (other.CompareTag(TrgTag) && !PushAct)
        {
           EventName.Invoke();
           if (SelfDestruct) { Destroy(gameObject); }
        }

        //If you need to act
        else if (other.CompareTag(TrgTag) && PushAct)
        {
            InReach = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //If you left the act area
        if (other.CompareTag(TrgTag) && PushAct)
        {
            InReach = false;
        }
    }
}

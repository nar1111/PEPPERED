using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Trigger_Bird_Idle : State
{
    [SerializeField] private SpriteRenderer MyRen;
    [SerializeField] private float TrgDist = 0;
    [SerializeField] private GameObject Player;
    [SerializeField] private Animator MyAnim;
    [SerializeField] private State_Trigger_Bird_Activated ActvState;
    private int StateT = 0;

    public override State RunCurrentState()
    {
        if (MyRen.isVisible && StateT == 0) { DistanceCheck(); }

        if (StateT == 1 && MyAnim.GetBool("Fly") == true)
        {
            StateT = 2;
            return ActvState;
        }
        return this;
    }

    private void DistanceCheck()
    {
        if (Vector2.Distance (Player.transform.position, MyRen.gameObject.transform.position) < TrgDist && StateT == 0)
        {
            MyAnim.Play("TriggereBird_WindUp");
            StateT = 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Benji_State_Talk : State
{
    [SerializeField] private Benji_State_Hide HideState;
    [SerializeField] private GameObject Player;
    [SerializeField] private Animator Myanim;
    [SerializeField] private Animator HughAnim;
    [SerializeField] private SpriteRenderer MyRen;
    private int MyStates = 0;
    private float Distance;

    public override State RunCurrentState()
    {
        HowCloseAreThey();

        if (MyStates == 0) { MyStates = 1; Myanim.Play("Benjamin_Talk"); Myanim.SetBool("Talk", true); }
        else if (MyStates == 3) { MyStates = 4; return HideState; }

        return this;
    }

    private void HowCloseAreThey()
    {
        if (MyRen.isVisible)
        {
            Distance = Vector3.Distance(Player.transform.position, Myanim.gameObject.transform.position);
            if (Distance < 2 && MyStates == 1) { MyStates = 2; Myanim.Play("Benjamin_Scared"); Myanim.transform.localScale = new Vector3(1, 1, 1); Myanim.SetBool("Talk", false); }
            else if (Distance < 1 && MyStates == 2) { MyStates = 3; HughAnim.Play("Hugh_Alert"); }
        }
    }
}

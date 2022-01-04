using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Strawbo_Idle : State
{
    #pragma warning disable 649
    [SerializeField] private State_Strawbo_Patrol PatrolState;
    [SerializeField] private Animator SeanAnim;
    [SerializeField] private SpriteRenderer MyRen;
    [SerializeField] private Animator Myanim;
    [SerializeField] private AUDIOMANAGER Audi;
    [SerializeField] private GameObject FL;
    [HideInInspector]public int State = 0;

    public override State RunCurrentState()
    {
        if (State == 1)
        {
            State = 2;
            FL.SetActive(true);
            Myanim.Play("Strawberry Activate");
            Invoke("Activate", 1f);
            SeanAnim.SetBool("Talk", false);
        }
        else if (State == 3)
        {
            return PatrolState;
        } else if (State == 0)
        {
            SeanAnim.SetBool("Talk", true);
        }

        return this;
    }

    public void Activate()
    {
        State = 3;
    }
}

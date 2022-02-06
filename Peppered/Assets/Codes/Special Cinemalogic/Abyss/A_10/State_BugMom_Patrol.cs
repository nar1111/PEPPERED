using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_BugMom_Patrol : State
{
    #pragma warning disable 649
    [Header("GAME OBJ")]
    [SerializeField] private GameObject Pepper;
    [SerializeField] private GameObject MomObj;
    [SerializeField] private GameObject NarkObj;
    [SerializeField] private SpriteRenderer SprRen;
    private Transform StrPoint;

    [Header("MOVEMENT")]
    [SerializeField] private float FlySpeed;
    [SerializeField] private float WaitTime;
    [SerializeField] private Transform[] MovePoints;

    [Header("FUNCTIONALITY")]
    [SerializeField] private State_BugMom_Alerted AlertState;
    [SerializeField] private GameObject Me;
    public FOV FieldOfView;

    private int CurrentTarget = 0;
    private int Stopper = 0;
    private float Timer;

    private void Start()
    {
        Timer = WaitTime;
        StrPoint = MomObj.transform;
        if (WHAT_HAVE_I_DONE.Collectibles.ContainsKey("BugMom")) { Destroy(Me); }
    }

    public override State RunCurrentState()
    {
        if (SprRen.isVisible)
        {
            MoveBitch();
            Stopper = 0;
        } else if (SprRen.isVisible == false && Stopper == 0)
        {
            Stopper = 1;
            MomObj.transform.position = StrPoint.position;
            CurrentTarget = 0;
            MomObj.transform.localScale = new Vector3(-1, 1, 1);
            NarkObj.transform.eulerAngles = Vector3.forward * -180f;
        }
        if (FieldOfView.PepperInSight == 1) { return AlertState; }
        return this;
    }

    private void MoveBitch()
    {
        MomObj.transform.position = Vector2.MoveTowards(MomObj.transform.position, MovePoints[CurrentTarget].position, FlySpeed * Time.deltaTime);

        if (Vector2.Distance(MomObj.transform.position, MovePoints[CurrentTarget].position) < 0.05f)
        {
            SwitchTarget();
        }
    }

    private void SwitchTarget()
    {
        if (Timer > 0) { Timer -= Time.deltaTime; }
        else if (Timer <= 0)
        {
            Timer = WaitTime;
            if (CurrentTarget == 0) { CurrentTarget = 1; MomObj.transform.localScale = new Vector3(1,1,1); NarkObj.transform.eulerAngles = Vector3.forward * 0f; }
            else if (CurrentTarget == 1){ CurrentTarget = 0; MomObj.transform.localScale = new Vector3(-1, 1, 1); NarkObj.transform.eulerAngles = Vector3.forward * -180f; }
        }
    }
}

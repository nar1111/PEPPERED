using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Barista_Idle : State
{
    #pragma warning disable 649
    [Header("STATES")]
    [SerializeField] private State_Barista_Alerted AlertState;
    [SerializeField] private Animator MyAnim;
    public FOV FieldOfView;


    [Header("Alertness speed")]
    [SerializeField] private GameObject Pepper;
    [SerializeField] private float NeededAngle;
    private float SleepTimer;
    private int SleepStates = 0;


    public override State RunCurrentState()
    {
        SleepingFunction();

        if (FieldOfView.PepperInSight == 1){ return AlertState; }
        else { return this; }
    }

    private void SleepingFunction()
    {
        if (SleepStates == 0)
        {
            MyAnim.Play("Barista_Asleep");
            SleepStates = 1;
            FieldOfView.viewAngle = 0.1f;
            FieldOfView.gameObject.SetActive(false);
            SleepTimer = 2f;
        }

        else if (SleepStates == 2)
        {
            MyAnim.Play("Barista_Awake");
            FieldOfView.gameObject.SetActive(true);
            FieldOfView.viewAngle += Time.deltaTime * 300;

            if (FieldOfView.viewAngle >= NeededAngle)
            {
                FieldOfView.viewAngle = NeededAngle;
                SleepTimer = 2f;
                SleepStates = 3;
            }
        }

        else if (SleepStates == 4)
        {
            MyAnim.Play("Barista_Falling");
            FieldOfView.viewAngle -= Time.deltaTime * 45;
            if (FieldOfView.viewAngle <= 1f)
            {
                FieldOfView.gameObject.SetActive(false);
                SleepStates = 0;
            }
        }

        //Sleeping stages
        if (SleepTimer > 0)
        {
            SleepTimer -= Time.deltaTime;

        }
        else if (SleepTimer < 0)
        {
            SleepTimer = 0;
            SleepStates++;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_BugMom_Alerted : State
{
    #pragma warning disable 649
    [Header("STATES")]
    public FOV FieldOfView;
    [SerializeField] private State_BugMom_Patrol PatrolState;
    [SerializeField] private State_BugMom_Freakout FreakoutState;

    [Header("Alertness speed")]
    [SerializeField] private PLAYER_CONTROLS Pepper;
    [SerializeField] private float TimeToProcess;
    [SerializeField] private float MinDist;
    [SerializeField] private float MaxDist;
    private float Modifier;
    private float Timer = 0.1f;
    private int MyStage = 0;

    [Header("UI")]
    [SerializeField] private Image StealthUI;
    [SerializeField] private GameObject LoadingBar;

    public override State RunCurrentState()
    {
        FOVFunction();

        if (FieldOfView.PepperInSight == 600) { return FreakoutState; }

        if (FieldOfView.PepperInSight == 0 && Timer > 0f)
        {
            if (LoadingBar.activeInHierarchy == true) { LoadingBar.SetActive(false); }
            Timer -= Time.deltaTime;
            StealthUI.fillAmount = Timer / TimeToProcess;
        }
        else if (FieldOfView.PepperInSight == 0 && Timer < 0f)
        {
            Timer = 0f;
            return PatrolState;
        }

        return this;
    }

    private void FOVFunction()
    {
        if (FieldOfView.PepperInSight == 1)
        {
            if (LoadingBar.activeInHierarchy == false) { LoadingBar.SetActive(true); }
            Modifier = 3f + 0.5f;
            TimerAdd();
        }
    }

    private void TimerAdd()
    {
        if (Timer <= TimeToProcess)
        {
            Timer += Time.deltaTime * Modifier;
        }
        else if (Timer >= TimeToProcess)
        {
            Timer = TimeToProcess;
            LoadingBar.SetActive(false);
            FieldOfView.gameObject.SetActive(false);
            FieldOfView.PepperInSight = 600;
        }
        StealthUI.fillAmount = Timer / TimeToProcess;
    }
}
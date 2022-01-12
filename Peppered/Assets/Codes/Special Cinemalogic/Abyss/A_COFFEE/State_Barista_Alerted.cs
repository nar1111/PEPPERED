using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_Barista_Alerted : State
{
    #pragma warning disable 649
    [Header("STATES")]
    public FOV FieldOfView;
    [SerializeField] private State_Barista_Idle IdleState;
    [SerializeField] private State_Barista_Freakout FreakOut;


    [Header("Alertness speed")]
    [SerializeField] private PLAYER_CONTROLS Pepper;
    [SerializeField] private float TimeToProcess;
    [SerializeField] private float MinDist;
    [SerializeField] private float MaxDist;
    private float Modifier;
    private float Distance;
    private float Timer = 0.1f;

    [Header("UI")]
    [SerializeField] private Image StealthUI;
    [SerializeField] private GameObject LoadingBar;


    public override State RunCurrentState()
    {
        FOVFunction();

        if (FieldOfView.PepperInSight == 0 && Timer > 0f)
        {
            if (LoadingBar.activeInHierarchy == true) { LoadingBar.SetActive(false); }
            Timer -= Time.deltaTime;
            StealthUI.fillAmount = Timer / TimeToProcess;
        }
        else if (FieldOfView.PepperInSight == 0 && Timer < 0f)
        {
            Timer = 0f;
            return IdleState;
        } 

        if (FreakOut.CutsceneNumber != 0) { return FreakOut; }

        return this;
    }

    private void FOVFunction()
    {
        if (FieldOfView.PepperInSight == 1)
        {
            //How far the player is
            Distance = Vector3.Distance(Pepper.transform.position, transform.position);
            if (LoadingBar.activeInHierarchy == false) { LoadingBar.SetActive(true); }

            //If Pepper faces the NPC
            if (Pepper.transform.localScale.x != transform.parent.localScale.x)
            {
                if (Distance > MaxDist) { Modifier = 0.2f + 0.5f; }
                else if (Distance < MaxDist && Distance > MinDist) { Modifier = 1f + 0.5f; }
                else if (Distance < MinDist) { Modifier = 3f + 0.5f; }
            }
            else
            {
                if (Distance > MaxDist) { Modifier = 0.2f; }
                else if (Distance < MaxDist && Distance > MinDist) { Modifier = 1f; }
                else if (Distance < MinDist) { Modifier = 3f; }
            }
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
            if (!Pepper.MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Invisible"))
            {
                LoadingBar.SetActive(false);
                FieldOfView.gameObject.SetActive(false);
                FreakOut.CutsceneNumber = 1;
            } else if (Pepper.MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Invisible") && Input.GetAxisRaw("Horizontal") != 0)
            {
                LoadingBar.SetActive(false);
                FieldOfView.gameObject.SetActive(false);
                FreakOut.CutsceneNumber = 2;
            }
        }
        StealthUI.fillAmount = Timer / TimeToProcess;
    }
}

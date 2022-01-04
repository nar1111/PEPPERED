using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_GiftShop_Alerted : State
{
    [Header("Alertness speed")]
    public FOV FieldOfView;
    [SerializeField] private GameObject NarkObj;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float TimeToProcess;
    [SerializeField] private float MinDist;
    [SerializeField] private float MaxDist;
    private float Modifier;
    private float Distance;
    private float Timer = 0.1f;

    [Header ("STATES")]
    [SerializeField] private State_GiftShop_Idle IdleState;
    [SerializeField] private State_GiftShop_FreakOut FreakOut;

    [Header("UI")]
    [SerializeField] private Image StealthUI;
    [SerializeField] private GameObject LoadingBar;
    [SerializeField] private PLAYER_CONTROLS Pepper;


    public override State RunCurrentState()
    {
        FOVFunction();
        Rotate();

        if (MaxDist == 600)
        {
            MaxDist = 7;
            return FreakOut;
        }

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
            LoadingBar.SetActive(false);
            FieldOfView.gameObject.SetActive(false);
            MaxDist = 600;
        }

        StealthUI.fillAmount = Timer / TimeToProcess;
    }

    private void Rotate()
    {
        NarkObj.transform.Rotate(Vector3.forward * MoveSpeed * Time.deltaTime);
    }
}

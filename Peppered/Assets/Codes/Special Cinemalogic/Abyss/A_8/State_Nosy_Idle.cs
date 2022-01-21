using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class State_Nosy_Idle : State
{
    #pragma warning disable 649
    [Header("Everything")]
    public FOV FieldOfView;
    [SerializeField] private PLAYER_CONTROLS Pepper;
    [SerializeField] private GameObject UIThing;
    [SerializeField] private float TimeToProcess;
    [SerializeField] private PlayableDirector[] Cutscene;
    private float Modifier;
    private float Timer = 0.1f;
    private int GuiltState = 0;

    [Header("UI")]
    [SerializeField] private Image StealthUI;
    [SerializeField] private GameObject LoadingBar;
    [SerializeField] private State_Nosy_Explode Explode;


    public override State RunCurrentState()
    {
        FOVFunction();

        if (FieldOfView.PepperInSight == 600)
        {
            Pepper.CanMove = false;
            MySceneManager.CutscenePlaying = true;
            Pepper.MyRigidBody.velocity = Vector2.zero;
            FieldOfView.gameObject.SetActive(false);
            UIThing.SetActive(false);
            return Explode;
        }

        return this;
    }


    private void FOVFunction()
    {
        if (FieldOfView.PepperInSight == 1 && !Pepper.MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Invisible"))
        {
            if (LoadingBar.activeInHierarchy == false) { LoadingBar.SetActive(true); }
            Modifier = 2f;
            TimerAdd();
        }
        else if (FieldOfView.PepperInSight == 1 && Pepper.MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Invisible"))
        {
            if (GuiltState == 0)
            {
                GuiltState = 1;
                Cutscene[0].Play();
            }
        }
        else
        {
            if (Timer > 0f)
            {
                if (LoadingBar.activeInHierarchy == true) { LoadingBar.SetActive(false); }
                Timer -= Time.deltaTime;
                StealthUI.fillAmount = Timer / TimeToProcess;
            }
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

    public void PauseCutscene()
    {
        Cutscene[0].Pause();
        GuiltState = 0;
    }
}

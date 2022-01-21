using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_Strawbo_Patrol : State
{
    #pragma warning disable 649
    [SerializeField] private float Speed;
    [SerializeField] private Transform[] FlyPoint;
    [SerializeField] private GameObject Strawbo;
    private int CurrentTarget = 0;

    [Header("Alertness speed")]
    public FOV FieldOfView;
    [SerializeField] private PLAYER_CONTROLS Pepper;
    [SerializeField] private GameObject UIThing;
    [SerializeField] private float TimeToProcess;
    [SerializeField] private float MinDist;
    [SerializeField] private float MaxDist;
    private float Modifier;
    private float Timer = 0.1f;

    [Header("UI")]
    [SerializeField] private State_Strawbo_Flee Flee;
    [SerializeField] private Image StealthUI;
    [SerializeField] private GameObject LoadingBar;

    public override State RunCurrentState()
    {
        FOVFunction();
        Patrol();

        if (FieldOfView.PepperInSight == 0 || Pepper.MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Invisible"))
        {
            if (Timer > 0f)
            {
                if (LoadingBar.activeInHierarchy == true) { LoadingBar.SetActive(false); }
                Timer -= Time.deltaTime;
                StealthUI.fillAmount = Timer / TimeToProcess;
            }
        }

        if (FieldOfView.PepperInSight == 600)
        {
            UIThing.SetActive(false);
            return Flee;
        }

        return this;
    }

    private void Patrol()
    {
        Strawbo.transform.position = Vector2.MoveTowards(Strawbo.transform.position, FlyPoint[CurrentTarget].position, Speed * Time.deltaTime);

        //ROTATE THIS BITCH
        Vector3 dir = FlyPoint[CurrentTarget].position - Strawbo.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
        Strawbo.transform.rotation = Quaternion.Slerp(Strawbo.transform.rotation, rotation, Speed * Time.deltaTime);


        if (Vector2.Distance(Strawbo.transform.position, FlyPoint[CurrentTarget].position) < 0.05f)
        {
            if (CurrentTarget < FlyPoint.Length - 1) { CurrentTarget++; } else { Strawbo.SetActive(false); }
        }
    }

    private void FOVFunction()
    {
        if (FieldOfView.PepperInSight == 1 && !Pepper.MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Invisible"))
        {
            if (LoadingBar.activeInHierarchy == false) { LoadingBar.SetActive(true); }
            Modifier = 4f;
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
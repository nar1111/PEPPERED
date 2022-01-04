using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PG_Activate : State
{
    [Header("STATES")]
    [SerializeField] private State_PG_Idle IdleState;

    [Header("EVERYTHING ELSE")]
    [SerializeField] private Transform[] EntrancePoints;
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private Animator PlayerDecoy;
    [SerializeField] private int CurrentEntrance = 0;
    [SerializeField] private GameObject OutEffect;

    private int AnimState = 0;
    private int Limiter;
    private float ChangeTubeTimer = 0;

    public override State RunCurrentState()
    {
        Player.transform.position = new Vector2(EntrancePoints[CurrentEntrance].position.x, EntrancePoints[CurrentEntrance].position.y + 0.4f);

        Timer();

        SwitchTube();

        if (Input.GetButtonDown("Jump") && AnimState == 0)
        {
            IdleState.JumpOut(CurrentEntrance);
            PlayerDecoy.Play("Invisible");
            return IdleState;
        }

        return this;
    }

    private void SwitchTube()
    {
        if (Input.GetAxisRaw("Horizontal") == 1 && AnimState == 0 && Limiter == 0)
        {
            Limiter = 1;
            if (CurrentEntrance != EntrancePoints.Length - 1)
            {
                AnimState = 1;
                AnimState = 1;
                CurrentEntrance++;
                ChangeTubeTimer = 0.5f;
                PlayerDecoy.SetBool("Downbutton", true);
            }
        }
        else if (Input.GetAxisRaw("Horizontal") == -1 && AnimState == 0 && Limiter == 0)
        {
            Limiter = 1;
            if (CurrentEntrance != 0)
            {
                AnimState = 1;
                CurrentEntrance--;
                ChangeTubeTimer = 0.5f;
                PlayerDecoy.SetBool("Downbutton", true);
                AnimState = 1;
            }
        } else if (Input.GetAxisRaw("Horizontal") == 0 && Limiter == 1)
        {
            Limiter = 0;
        }
    }

    void Timer()
    {
        if (ChangeTubeTimer > 0) { ChangeTubeTimer -= Time.deltaTime; }
        else if (ChangeTubeTimer < 0)
        {
            ChangeTubeTimer = 0;
            AnimState = 0;
            //Player.transform.position = EntrancePoints[CurrentEntrance].position;
            PlayerDecoy.transform.position = EntrancePoints[CurrentEntrance].position;
            PlayerDecoy.SetBool("Downbutton", false);
            Instantiate(OutEffect, new Vector2(EntrancePoints[CurrentEntrance].position.x, EntrancePoints[CurrentEntrance].position.y + 0.4f), Quaternion.identity);
            PlayerDecoy.Play("Crouch 3");
        }
    }

    public void EnterTube(Transform EnteredHere)
    {
        AnimState = 1;
        ChangeTubeTimer = 0.5f;
        for (int i = 0; i < EntrancePoints.Length; i++)
        {
            if (EnteredHere.position == EntrancePoints[i].position)
            {
                CurrentEntrance = i;
            }
        }
    }
}
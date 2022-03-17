using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Ballpit_Active : State
{
    [Header("PLAYER")]
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private Animator PlayerDecoy;
    [SerializeField] private Rigidbody2D MyRigidBody;

    [Header("MISC.")]
    [SerializeField] private float JumpForce;
    [SerializeField] private GameObject JupmOutEffect;
    [SerializeField] private AUDIOMANAGER Audiman;

    [Header("TECH STUFF")]
    [SerializeField] private State_Ballpit_Idle IdleState;
    private float fHorizontalDampingWhenStopping = 0.01f;
    private float fHorizontalDampingWhenTurning = 0.01f;
    private float fHorizontalVelocity;

    private bool Move = false;

    public override State RunCurrentState()
    {
       MoveThisGuy();
       TurnSprite();

        if (Input.GetButtonDown("Jump") && MySceneManager.CutscenePlaying == false)
        {
            Player.Wind = true;
            Player.MyAnim.Play("Take Off");
            Player.CanMove = true;
            Player.Bounce(JumpForce, 2);
            PlayerDecoy.Play("Invisible");
            Audiman.StopIt("Ballpit Move");
            Audiman.Play("Ballpit Jump");
            Instantiate(JupmOutEffect, Player.gameObject.transform.position, Quaternion.identity);
            return IdleState;
        }

        if (Player.Dead == 1)
        {
            Player.CanMove = true;
            Player.Wind = true;
            PlayerDecoy.Play("Invisible");
            Audiman.StopIt("Ballpit Move");
            return IdleState;
        }
        return this;
    }


    private void MoveThisGuy()
    {
        Player.gameObject.transform.position = new Vector2(PlayerDecoy.transform.position.x, PlayerDecoy.transform.position.y + 0.3f);
        fHorizontalVelocity = MyRigidBody.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f && MySceneManager.CutscenePlaying == false)
        {
            //PlayerDecoy.Play("Invisible");
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.fixedDeltaTime * 5f);
        }

        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity) || MySceneManager.CutscenePlaying == true)
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.fixedDeltaTime * 5f);
            // PlayerDecoy.Play("Landing");
        }

        if (MySceneManager.CutscenePlaying == false)
        {
            MyRigidBody.velocity = new Vector2(fHorizontalVelocity, MyRigidBody.velocity.y);
        }

        if (Input.GetAxisRaw("Horizontal") != 0 && Move == false)
        {
            Move = true;
            Audiman.Play("Ballpit Move");
        } else if (Input.GetAxisRaw("Horizontal") == 0 && Move == true)
        {
            Move = false;
            Audiman.StopIt("Ballpit Move");
        }
    }

    private void TurnSprite()
    {
        //Turn Sprite Around
        if (Input.GetAxisRaw("Horizontal") > 0 && MySceneManager.CutscenePlaying == false) { PlayerDecoy.gameObject.transform.localScale = new Vector3(1, 1, 1); }
        else if (Input.GetAxisRaw("Horizontal") < 0 && MySceneManager.CutscenePlaying == false) { PlayerDecoy.gameObject.transform.localScale = new Vector3(-1, 1, 1); }
    }
}

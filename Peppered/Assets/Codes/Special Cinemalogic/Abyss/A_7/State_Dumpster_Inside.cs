using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Dumpster_Inside : State
{
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private GameObject Dumpster;
    [SerializeField] private State_Dumpster_Idle IdleState;
    [SerializeField] private Animator Myanim;
    [SerializeField] private float BouncePower;
    private bool Moved;

    public override State RunCurrentState()
    {
        MoveInside();

        if (Input.GetButtonDown("Jump") && MySceneManager.CutscenePlaying == false)
        {
            Player.gameObject.transform.position = new Vector2(Dumpster.transform.position.x, Dumpster.transform.position.y + 1f);
            Player.Wind = true;
            Player.CanMove = true;
            Player.MyRigidBody.velocity = Vector2.zero;
            Player.Bounce(BouncePower, 2);
            Player.MyRigidBody.gravityScale = 1.6f;
            Player.MyAnim.Play("HighJump1");
            return IdleState;
        }

        return this;
    }


    private void MoveInside()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f && Moved == false)
        {
            Moved = true;
            Myanim.Play("Dumpster_Shake");
            //MoveSound.Play();
        }

        if (Input.GetAxisRaw("Horizontal") == 0) { Moved = false; }
    }
}

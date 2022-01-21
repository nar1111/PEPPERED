using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Dumpster_Idle : State
{
    #pragma warning disable 649
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private GameObject Dumpster;
    [SerializeField] private GameObject DiveEffect;
    [SerializeField] private Animator Myanim;
    [SerializeField] private State_Dumpster_Inside Inside_State;
    [SerializeField] private bool Deadly = false;
    [SerializeField] private AUDIOMANAGER AudiMan;
    private float Used = 0;


    public override State RunCurrentState()
    {
        RelaunchUse();

        if (Vector2.Distance(Player.gameObject.transform.position, Dumpster.transform.position) < 0.6f && Player.MyRigidBody.velocity.y < -0.1f && Used == 0)
        {
            if (!Deadly)
            {
                Player.MyAnim.Play("Invisible");
                Instantiate(DiveEffect, new Vector2(Dumpster.transform.position.x, Dumpster.transform.position.y + 0.2f), Quaternion.identity);
                Player.CanMove = false;
                Player.MyRigidBody.velocity = Vector2.zero;
                Player.MyRigidBody.gravityScale = 0f;
                Used = 1f;
                Player.Wind = false;
                Myanim.Play("Dumpster_Shake");
                AudiMan.Play("Dumpster Move");
                return Inside_State;
            }
            else
            {
                Player.gameObject.transform.position = new Vector2(Dumpster.transform.position.x, Dumpster.transform.position.y + 0.5f);
                Player.MyRigidBody.velocity = Vector2.zero;
                Player.Death(5,2);
            }
        }

        return this;
    }

    private void RelaunchUse()
    {
        if (Used > 0)
        {
            Used -= Time.deltaTime;
        }
        else if (Used < 0)
        {
            Used = 0;
        }
    }
}

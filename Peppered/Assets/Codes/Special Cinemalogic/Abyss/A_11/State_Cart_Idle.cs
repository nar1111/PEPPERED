using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Cart_Idle : State
{
    [Header("States")]
    [SerializeField] private State_Cart_Ride RideState;
    [SerializeField] private float TriggerRaidus;
    private float Used = 0;

    [Header ("Player")]
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private Animator MyAnim;


    public override State RunCurrentState()
    {
        RelaunchUse();

        if (Vector2.Distance(Player.gameObject.transform.position, transform.position) < TriggerRaidus && Player.MyRigidBody.velocity.y < -0.1f && Used == 0 && Player.CanMove == true)
        {
            Player.MyAnim.Play("Invisible");
            Player.CanMove = false;
            Player.transform.position = transform.position;
            Player.MyRigidBody.velocity = Vector2.zero;
            Player.transform.parent = MyAnim.gameObject.transform;
            MyAnim.Play("Cart_Land");
            Used = 1f;
            Player.Wind = false;
            RideState.DirectMeBitch();
            return RideState;
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

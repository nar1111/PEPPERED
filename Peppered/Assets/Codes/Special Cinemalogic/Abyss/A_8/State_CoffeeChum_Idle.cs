using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_CoffeeChum_Idle : State
{
    #pragma warning disable 649
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private GameObject CoffeeChum;
    [SerializeField] private State_CoffeChum_Ride RideState;
    [SerializeField] private Animator MyAnim;
    private float Used = 0;

    public override State RunCurrentState()
    {
        RelaunchUse();

        if (Vector2.Distance(Player.gameObject.transform.position, CoffeeChum.transform.position) < 0.6f && Player.MyRigidBody.velocity.y < -0.1f && Used == 0)
        {
            MyAnim.Play("Coffee_Jumped");
            Player.MyAnim.Play("Invisible");
            Player.CanMove = false;
            Player.transform.position = CoffeeChum.transform.position;
            Player.MyRigidBody.velocity = Vector2.zero;
            Used = 1f;
            Player.Wind = false;
            return RideState;
        }
        else { return this; }
    }

    private void RelaunchUse()
    {
        if (Used > 0)
        {
            Used -= Time.deltaTime;
        } else if (Used < 0)
        {
            Used = 0;
        }
    }
}

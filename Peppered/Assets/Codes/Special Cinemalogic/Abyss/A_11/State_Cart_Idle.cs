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
    [SerializeField] private GameObject CartPlayer;


    public override State RunCurrentState()
    {
        RelaunchUse();

        if (Vector2.Distance(Player.gameObject.transform.position, transform.position) < TriggerRaidus && Player.MyRigidBody.velocity.y < -0.1f && Used == 0)
        {
            Player.MyAnim.Play("Invisible");
            Player.CanMove = false;
            Player.transform.position = transform.position;
            Player.MyRigidBody.velocity = Vector2.zero;
            CartPlayer.SetActive(true);
            Used = 1f;
            Player.Wind = false;
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

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
    [SerializeField] private string AnimName;
    [SerializeField] private GameObject AdditionalThing;
    [SerializeField] private float TriggerRaidus;
    private float Used = 0;

    public override State RunCurrentState()
    {
        RelaunchUse();

        if (Vector2.Distance(Player.gameObject.transform.position, CoffeeChum.transform.position) < TriggerRaidus && Player.MyRigidBody.velocity.y < -0.1f && Used == 0)
        {
            MyAnim.Play(AnimName);
            Player.MyAnim.Play("Invisible");
            WHAT_HAVE_I_DONE.Collectibles.Add("CoffeeChum", 1);
            Player.CanMove = false;
            Player.transform.position = CoffeeChum.transform.position;
            Player.MyRigidBody.velocity = Vector2.zero;
            Used = 1f;
            Player.Wind = false;
            if (AdditionalThing != null) { AdditionalThing.SetActive(true); }
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

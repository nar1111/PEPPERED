using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Ballpit_Idle : State
{
    [Header("STATES")]
    [SerializeField] private State_Ballpit_Active ActiveState;

    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private Animator PlayerDecoy;
    [SerializeField] private GameObject JumpInEffect;
    [SerializeField] private Transform[] EdgePoints;
    private int StateChanger = 0;
    


    public override State RunCurrentState()
    {
        Checker();
        if (StateChanger == 1)
        {
            StateChanger = 0;
            if (Player.transform.localScale.x == 1) { PlayerDecoy.transform.localScale = new Vector3(1, 1, 1); }
            else { PlayerDecoy.transform.localScale = new Vector3(-1,1,1); }
            return ActiveState;
        }

        return this;
    }


    private void Checker()
    {
        //INSIDE THE BORDER
        if (
            Player.transform.position.x > EdgePoints[0].position.x &&
            Player.transform.position.x < EdgePoints[1].position.x &&
            Player.MyRigidBody.velocity.y < -0.1f &&
            Player.transform.position.y - EdgePoints[0].position.y <= 0f &&
            Player.transform.position.y - EdgePoints[0].position.y >= -0.5f &&
            StateChanger == 0
           )
        {
            Instantiate(JumpInEffect, new Vector2 (Player.transform.position.x, Player.transform.position.y + 0.2f), Quaternion.identity);
            Player.MyAnim.Play("Invisible");
            Player.CanMove = false;
            Player.Wind = false;
            Player.MyRigidBody.velocity = Vector2.zero;
           // PlayerDecoy.Play("Landing");
            PlayerDecoy.Play("Invisible");
            PlayerDecoy.transform.position = new Vector2(Player.transform.position.x, PlayerDecoy.transform.position.y);
            StateChanger = 1;
        }
    }
}

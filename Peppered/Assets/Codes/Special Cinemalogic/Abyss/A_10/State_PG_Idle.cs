using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_PG_Idle : State
{
    [Header("STATES")]
    [SerializeField] private State_PG_Activate ActState;

    [Header ("EVERYTHING ELSE")]
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private Transform[] Entrace;
    [SerializeField] private GameObject Dust;
    [SerializeField] private GameObject PunchBox;
    [SerializeField] private float[] JumpPower;
    private Transform UsedEntrace;
    private int StateChanger = 0;

    public override State RunCurrentState()
    {
        Checker();
        if (StateChanger == 1)
        {
            StateChanger = 0;
            return ActState;
        }

        return this;
    }

    private void Checker()
    {
        foreach (Transform Entraces in Entrace)
        {
            if (Vector2.Distance(Player.gameObject.transform.position, Entraces.transform.position) < 0.6f && Player.MyRigidBody.velocity.y < -0.1f && StateChanger == 0)
            {
                UsedEntrace = Entraces;
                ActState.EnterTube(UsedEntrace);
                Player.MyAnim.Play("Invisible");
                PunchBox.SetActive(false);
                Instantiate(Dust, new Vector2 (UsedEntrace.position.x, UsedEntrace.position.y + 0.9f) , Quaternion.identity);
                Player.CanMove = false;
                Player.Wind = false;
                Player.MyRigidBody.velocity = Vector2.zero;
                Player.MyRigidBody.gravityScale = 0f;
                StateChanger = 1;
            }
        }
    }

    public void JumpOut(int EntrenceNum)
    {
        Player.MyRigidBody.gravityScale = 1.6f;
        Player.Wind = true;
        Player.MyAnim.Play("Take Off");
        Player.transform.localScale = new Vector3(1, 1, 1);
        Player.CanMove = true;
        Player.Bounce(JumpPower[EntrenceNum], 2);
    }
}
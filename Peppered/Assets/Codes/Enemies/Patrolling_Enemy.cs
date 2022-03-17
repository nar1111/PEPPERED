using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Patrolling_Enemy : MonoBehaviour
{
    #pragma warning disable 649
    public PLAYER_CONTROLS Player;
    [SerializeField]
    private float Speed;
    private float WaitTime;
    public float StartWaitTime;
    public Transform[] MoveSpots;
    private int CurrentTarget = 0;
    private bool GoForward = true;

	// Use this for initialization
	void Start () 
	{
        WaitTime = StartWaitTime;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Player != null)
        {
          //  if (Player.CanMove == true)
           // {
                transform.position = Vector2.MoveTowards(transform.position, MoveSpots[CurrentTarget].position, Speed * Time.deltaTime);
          //  }
        }

        else { transform.position = Vector2.MoveTowards(transform.position, MoveSpots[CurrentTarget].position, Speed * Time.deltaTime); }



        if (Vector2.Distance (transform.position, MoveSpots[CurrentTarget].position) < 0.2f)
        {
            //You're at the point.

            //Change target
            if (WaitTime <= 0)
            {
                // Go forward
                if (GoForward)
                {
                    if (CurrentTarget < MoveSpots.Length - 1) { CurrentTarget++; }
                    else { CurrentTarget--; GoForward = false; }
                }
                // Go backward
                else
                {
                    if (CurrentTarget > 0) { CurrentTarget--; }
                    else { CurrentTarget++; GoForward = true; }
                }
                WaitTime = StartWaitTime;
            }

            //Wait a bit
            else { WaitTime -= Time.deltaTime; }
        }
	}


}

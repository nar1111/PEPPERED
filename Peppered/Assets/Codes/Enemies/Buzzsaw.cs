using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buzzsaw : MonoBehaviour
{
    [SerializeField]private StopLight SL;
    [SerializeField]private Transform[] MovePoints;
    [HideInInspector]public bool CanMove;
    [SerializeField]private int CurrentTarget = 0;
    private float Speed;
    //1 = forward 2 = Backward
    private int Direction;

    private void Update()
    {
        MoveBitch();
    }

    private void MoveBitch()
    {
        if (CanMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, MovePoints[CurrentTarget].position, Speed * Time.deltaTime);


            if (Vector2.Distance(transform.position, MovePoints[CurrentTarget].position) < 0.05f)
            {
                // Go forward
                if (Direction == 1)
                {
                    if (CurrentTarget < MovePoints.Length - 1) { CurrentTarget++; }
                    else { CanMove = false; }
                }

                // Go backward
                else if (Direction == 2)
                {
                    if (CurrentTarget > 0) { CurrentTarget--; }
                    else { CanMove = false; if (SL != null) { SL.StartPoint(); } }
                }
            }
        }
    }

    public void StartTheEngine(float SawSpeed)
    {
        if (CurrentTarget == 0) { Direction = 1; }
        else { Direction = 2; }
        Speed = SawSpeed;
        CanMove = true;
    }

}
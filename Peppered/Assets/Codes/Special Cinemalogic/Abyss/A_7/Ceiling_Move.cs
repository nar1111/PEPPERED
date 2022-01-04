using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceiling_Move : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private PLAYER_CONTROLS Playa;
    [SerializeField] private Transform[] MovePoints;
    [SerializeField] private GameObject WarningLight;
    [SerializeField] private int CurrentTarget = 0;
    [SerializeField] private float Speed;

    private void Update()
    {
        MoveBitch();
    }

    private void MoveBitch()
    {
        if (Playa.Dead == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(MovePoints[CurrentTarget].position.x, transform.position.y), Speed * Time.deltaTime);
            WarningLight.transform.position = Vector2.MoveTowards(WarningLight.transform.position, new Vector2(MovePoints[CurrentTarget].position.x, WarningLight.transform.position.y), Speed * Time.deltaTime);
        }

            if (Vector2.Distance(transform.position, new Vector2(MovePoints[CurrentTarget].position.x, transform.position.y)) < 0.05f)
            {
                if (CurrentTarget == 0) { CurrentTarget = 1; }
                else if (CurrentTarget == 1) { CurrentTarget = 0; }
            }
    }

}

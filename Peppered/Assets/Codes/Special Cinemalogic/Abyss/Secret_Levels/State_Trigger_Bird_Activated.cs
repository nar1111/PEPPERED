using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Trigger_Bird_Activated : State
{
    [SerializeField] private GameObject WholeThing;
    [SerializeField] private Transform FlyPoint;
    [SerializeField] private float Speed;
    [SerializeField] private GameObject BirdObj;

    private int FlyChecker = 0;

    public override State RunCurrentState()
    {
        if (FlyChecker == 0) { StartFlying(); }

        if (Vector2.Distance(BirdObj.transform.position, FlyPoint.position) > 0.05f)
        {
            BirdObj.transform.position = Vector2.MoveTowards(BirdObj.transform.position, FlyPoint.position, Speed * Time.deltaTime);
        }
        else { Destroy(WholeThing); }
        return this;
    }

    private void StartFlying()
    {
        FlyChecker = -1;

        if (BirdObj.transform.position.x > FlyPoint.position.x)
        {
            BirdObj.transform.localScale = new Vector3(-1, 1, 1);
        }
        else { BirdObj.transform.localScale = new Vector3(1, 1, 1); }
    }
}

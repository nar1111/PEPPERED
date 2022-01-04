using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_GiftShop_Idle : State
{
    #pragma warning disable 649
    [Header("GAME OBJ")]
    [SerializeField] private GameObject NarkObj;
    [SerializeField] private SpriteRenderer MyRen;

    [Header("MOVEMENT")]
    [SerializeField] private float MoveSpeed;

    [Header("FUNCTIONALITY")]
    [SerializeField] private State_GiftShop_Alerted AlertState;
    public FOV FieldOfView;


    public override State RunCurrentState()
    {
        if (MyRen.isVisible)
        {
            Rotate();
            if (FieldOfView.PepperInSight == 1) { return AlertState; }
        }
        return this;
    }

    void Rotate()
    {
        NarkObj.transform.Rotate(Vector3.forward * MoveSpeed * Time.deltaTime);
    }
}

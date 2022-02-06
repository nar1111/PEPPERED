using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Cart_Jump : State
{
    [Header("Physics")]
    [SerializeField] private Rigidbody2D MyRigidbody;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private State_Cart_Ride CartRide;


    [Header("Navigation")]
    [SerializeField] private Transform GroundChecker;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private bool Grounded;


    [Header("Cosmetics")]
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private GameObject LandingDust;
    [SerializeField] private GameObject Cart;
    private float HangTime = 0f;

    public override State RunCurrentState()
    {
        Player.gameObject.transform.position = new Vector2(transform.parent.position.x, transform.parent.position.y + 0.5f);

        ISeeGround();

        SpeedLimit();

        HangTimeTimer();

        if (Grounded)
        {
            if (HangTime > 0.5f) { Instantiate(LandingDust, Cart.transform.position, Quaternion.identity); }
            MyRigidbody.velocity = new Vector2(MyRigidbody.velocity.x / 2, MyRigidbody.velocity.y);
            return CartRide;
        }

        return this;
    }

    private void ISeeGround()
    {
        Grounded = Physics2D.OverlapCircle(GroundChecker.position, .02f, Ground);
    }

    public void JumpNow(bool MaxBoost, float Direction)
    {
        HangTime = 0f;
        if (MaxBoost)
        {
            MyRigidbody.AddForce(MyRigidbody.transform.up * 18, ForceMode2D.Impulse);
            if (Direction > 0)
            {
                MyRigidbody.AddForce(MyRigidbody.transform.right * 12, ForceMode2D.Impulse);
            }
            else
            {
                MyRigidbody.AddForce(-MyRigidbody.transform.right * 12, ForceMode2D.Impulse);
            }
        }
        else
        {
            MyRigidbody.velocity = Vector2.zero;
            MyRigidbody.AddForce(MyRigidbody.transform.up * 8, ForceMode2D.Impulse);
            if (Direction > 0)
            {
                MyRigidbody.AddForce(MyRigidbody.transform.right *  3, ForceMode2D.Impulse);
            }
            else
            {
                MyRigidbody.AddForce(-MyRigidbody.transform.right * 3, ForceMode2D.Impulse);
            }
        }
    }

    private void HangTimeTimer()
    {
        HangTime += Time.deltaTime;
    }

    private void SpeedLimit()
    {
        //Limit Top speed
        if (MyRigidbody.velocity.magnitude > MaxSpeed)
        {
            MyRigidbody.velocity = MyRigidbody.velocity.normalized * MaxSpeed;
        }
    }

}
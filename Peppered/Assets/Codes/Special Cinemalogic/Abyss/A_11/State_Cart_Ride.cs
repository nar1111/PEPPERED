using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Cart_Ride : State
{
    [Header("Physics")]
    [SerializeField] private Rigidbody2D MyRigidbody;
    [SerializeField] private float MaxBoost;
    [SerializeField] private float MinBoost;
    [SerializeField] private float BoostTime;

    private float CurrentBoost;
    private float MaxBoostTimer = 0f;
    private bool Boosting = false;

    [Header("Navigation")]
    [SerializeField] private Transform GroundChecker;
    [SerializeField] private LayerMask Ground;
    private bool Grounded;

    [Header("States")]
    [SerializeField] private State_Cart_Jump CartJump;
    [SerializeField] private State_Cart_Idle IdleState;

    [Header("Cosmetics")]
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private Animator Myanim;


    private float Direction;
    private bool Press = false;

    public override State RunCurrentState()
    {
        Player.gameObject.transform.position = new Vector2(transform.parent.position.x, transform.parent.position.y + 0.5f);

        SpeedLimit();

        ISeeGround();

        #region Get Out
        if (Input.GetButtonDown("Jump") && MySceneManager.CutscenePlaying == false)
        {
            Player.gameObject.transform.localScale = transform.localScale;
            Player.Wind = true;
            Player.gameObject.transform.parent = null;
            Player.MyAnim.Play("Take Off");
            Myanim.Play("Cart_Exit");
            Player.CanMove = true;
            Player.Bounce(12f, 2);
            MaxBoostTimer = -0.1f;
            Boosting = false;
            Press = false;
            MyRigidbody.velocity = new Vector2(MyRigidbody.velocity.x / 2f, MyRigidbody.velocity.y);
            return IdleState;
        }
        #endregion

        SmallPush();

        BigPush();

        #region Jumping
        if (!Grounded)
        {
            CartJump.JumpNow(Boosting, Direction);
            if (Boosting)
            {
                MaxBoostTimer = -0.1f;
                Boosting = false;
            }
            Myanim.Play("Cart_Jump");
            return CartJump;
        }
        #endregion

        return this;
    }

    private void SmallPush()
    {
        //Pressed
        if (Input.GetAxisRaw("Horizontal") != 0 && Press == false && !Boosting)
        {
            Press = true;
            CurrentBoost = MinBoost;
            Direction = Input.GetAxisRaw("Horizontal");

            //Slow Down
            if (MyRigidbody.velocity.magnitude > 2f && MyRigidbody.gameObject.transform.localScale.x != Direction)
            {
                MyRigidbody.gameObject.transform.localScale = new Vector3(Direction, 1, 1);
                MyRigidbody.velocity = new Vector2(MyRigidbody.velocity.x / 3f, MyRigidbody.velocity.y);
                Myanim.Play("Cart_Land");
            } else
            {
                Myanim.Play("Cart_Push", 0, 0);
                MyRigidbody.gameObject.transform.localScale = new Vector3(Direction, 1, 1);

                //Push
                if (Direction > 0)
                {
                    MyRigidbody.AddForce(MyRigidbody.transform.right * CurrentBoost, ForceMode2D.Impulse);
                }
                else
                {
                    MyRigidbody.AddForce(-MyRigidbody.transform.right * CurrentBoost, ForceMode2D.Impulse);
                }
            }
        }
        //Depressed (lol)
        if (Input.GetAxisRaw("Horizontal") == 0f && Press == true)
        {
            Press = false;
        }

    }

    private void BigPush()
    {
        if (Input.GetButtonDown("Skip") && !Boosting)
        {
            Boosting = true;
            MaxBoostTimer = 1f;
            Myanim.Play("Cart_Max_Push");
        }

        if (MaxBoostTimer > 0f && Boosting && MyRigidbody.velocity.magnitude > 0.1f)
        {
            MaxBoostTimer -= Time.deltaTime;
            if (Direction > 0)
            {
                MyRigidbody.velocity = new Vector2(MaxBoost, MyRigidbody.velocity.y);
            }
            else
            {
                MyRigidbody.velocity = new Vector2(-MaxBoost, MyRigidbody.velocity.y);
            }
        } else if (MaxBoostTimer <= 0f && Boosting || MyRigidbody.velocity.magnitude < 0.1f && Boosting)
        {
            Boosting = false;
            MaxBoostTimer = 0;
            Myanim.Play("Cart_Land");
        }
    }

    private void SpeedLimit()
    {
        //Limit Top speed
        if (MyRigidbody.velocity.magnitude > MaxBoost)
        {
            MyRigidbody.velocity = MyRigidbody.velocity.normalized * MaxBoost;
        }
    }

    private void ISeeGround()
    {
        Grounded = Physics2D.OverlapCircle(GroundChecker.position, .02f, Ground);
    }

    public void DirectMeBitch()
    {
        Direction = MyRigidbody.gameObject.transform.localScale.x;
    }
}
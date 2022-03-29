using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class State_Cart_Ride : State
{
    #pragma warning disable 649
    [Header("Physics")]
    [SerializeField] private Rigidbody2D MyRigidbody;
    [SerializeField] private float MaxBoost;
    [SerializeField] private float MinBoost;
    private float MaxSpeedReg;
    private float MaxSpeedSuper;


    private float CurrentBoost;
    private float MaxBoostTimer = 0f;
    [HideInInspector] public bool Boosting = false;


    [Header("Navigation")]
    [SerializeField] private Transform GroundChecker;
    [SerializeField] private LayerMask Ground;
    private bool Grounded;


    [Header("States")]
    [SerializeField] private State_Cart_Jump CartJump;
    [SerializeField] private State_Cart_Idle IdleState;


    [Header("Cosmetics")]
    [SerializeField] private GameObject Warning;
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private Animator Myanim;
    [SerializeField] private GameObject PushEffect;
    [SerializeField] private AUDIOMANAGER Audiman;
    [SerializeField] private AudioSource RideSfx;


    [Header("OVER HEATING")]
    [SerializeField] private Light2D HeatLight;
    [SerializeField] private Light2D HeatLightExtra;
    [SerializeField] private ParticleSystem FireEffect;
    [SerializeField] private Cart_Crash CrashIt;
    private float HeatLvl;
    private float HeatTimer;
    private float StartHeatTimer = 1.5f;


    private float ChillTimer;
    private float Direction;
    private bool Press = false;


    public override State RunCurrentState()
    {
        Player.gameObject.transform.position = new Vector2(transform.parent.position.x, transform.parent.position.y + 0.5f);

        if (Grounded)
        {
            RideSfx.volume = MyRigidbody.velocity.magnitude / 12;
        }

        SpeedLimit();

        ISeeGround();

        Overheat();

        if (ChillTimer < 0)
        {
            #region Get Out
            if (Input.GetButtonDown("Jump") && MySceneManager.CutscenePlaying == false)
            {
                MySceneManager.DontKillMe = 0;
                Player.gameObject.transform.localScale = transform.localScale;
                Player.Wind = true;
                Player.gameObject.transform.parent = null;
                Player.MyAnim.Play("Take Off");
                RideSfx.Stop();
                Myanim.Play("Cart_Exit");
                Player.CanMove = true;
                Player.Bounce(12f, 2);
                MaxBoostTimer = -0.1f;
                MaxBoost = MaxSpeedReg;
                Boosting = false;
                Press = false;
                Warning.SetActive(false);
                FireEffect.emissionRate = 0;
                HeatLvl = 0;
                HeatLight.intensity = 0;
                HeatLightExtra.intensity = 0;
                MyRigidbody.velocity = new Vector2(MyRigidbody.velocity.x / 2f, MyRigidbody.velocity.y);
                return IdleState;
            }
            #endregion

            if (MySceneManager.CutscenePlaying == false)
            {
                SmallPush();

                BigPush();
            }
            else
            {
                MyRigidbody.velocity = new Vector2(0, MyRigidbody.velocity.y);
                MaxBoostTimer = -0.1f;
                Boosting = false;
                Press = false;
            }
        } else
        {
            ChillTimer -= Time.deltaTime;
        }

        #region Jumping
        if (!Grounded)
        {
            RideSfx.volume = 0f;
            CartJump.JumpNow(Boosting, Direction);
            if (Boosting)
            {
                MaxBoostTimer = -0.1f;
                //Boosting = false;
            }
            FireEffect.emissionRate = 0;
            HeatLvl = 0;
            HeatLight.intensity = 0;
            HeatLightExtra.intensity = 0;
            MaxBoost = MaxSpeedReg;
            Myanim.Play("Cart_Jump");
            Audiman.Play("Cart Jump");
            Warning.SetActive(false);
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
            Audiman.Play("Cart Ride");

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
            Direction = MyRigidbody.gameObject.transform.localScale.x;
            Audiman.Play("Cart Push");

            if (Direction > 0)
            {
                MyRigidbody.AddForce(MyRigidbody.transform.right * 0.1f, ForceMode2D.Impulse);
            }
            else
            {
                MyRigidbody.AddForce(-MyRigidbody.transform.right * 0.1f, ForceMode2D.Impulse);
            }
            Boosting = true;
            MaxBoostTimer = 1f;
            Myanim.Play("Cart_Max_Push");
            Instantiate(PushEffect, GroundChecker.position, Quaternion.identity);
            MaxBoost = MaxSpeedSuper;
            HeatTimer = StartHeatTimer;
            HeatLvl += 1;
        }

        if (MaxBoostTimer > 0f && Boosting && MyRigidbody.velocity.magnitude > 0)
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
        }

        else if (MaxBoostTimer < 0f && Boosting || Boosting && MyRigidbody.velocity.magnitude <= 0)
        {
            MaxBoost = MaxSpeedReg;
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
        RideSfx.Play();
        Direction = MyRigidbody.gameObject.transform.localScale.x;
        MaxSpeedReg = MaxBoost;
        MaxSpeedSuper = MaxSpeedReg + 5;
    }

    public void JumpSFX() { Audiman.Play("Cart Land"); }

    public void Chill(float HowLong)
    {
        MaxBoostTimer = 0;
        Myanim.Play("Cart_Land");
        MyRigidbody.velocity = new Vector2(MyRigidbody.velocity.x / 4, MyRigidbody.velocity.y);
        MaxBoostTimer = -0.1f;
        Boosting = false;
        Press = false;
        ChillTimer = HowLong;
    }

    private void Overheat()
    {
        if (HeatTimer > 0) { HeatTimer -= Time.deltaTime; }
        else if (HeatTimer <= 0 && HeatLvl > 0)
        {
            HeatLvl -= Time.deltaTime;
        }


        //Light Up
        if (HeatLight.intensity < HeatLvl)
        {
            HeatLight.intensity += 10 * Time.deltaTime;
        } else if (HeatLight.intensity > HeatLvl && HeatLight.intensity > 0)
        {
            HeatLight.intensity -= 10 * Time.deltaTime;
        }

        //Too Much
        if (HeatLvl >= 3)
        {
            if (!Warning.activeInHierarchy)
            {
                FireEffect.emissionRate = 100;
                Warning.SetActive(true);
                Audiman.Play("Error");
            }
            if (HeatLightExtra.intensity < 4) { HeatLightExtra.intensity += 2 * Time.deltaTime; }
            
            if (HeatLvl >= 4)
            {
                CrashIt.Shit();
            }
        } else
        {
            FireEffect.emissionRate = 0;
            Warning.SetActive(false);
            if (HeatLightExtra.intensity > 0) { HeatLightExtra.intensity -= 20 * Time.deltaTime; }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_Thing : MonoBehaviour
{
    [Header("MOVEMENT")]
    public PLAYER_CONTROLS Player;
    public float Speed;
    public Transform[] MoveSpots;
    [Header("Cosmetics")]
    public Animator MyAnim;
    public GameObject SplatSprite;
    public GameObject SplatEffect;
    public AUDIOMANAGER AudiMan;
    //public A_Playtest_Level Count;


    private int CurrentTarget = 0;
    private bool GoForward = true;
    private int Grounded = 0;
    private Rigidbody2D MyRigid;
    private SpriteRenderer MyRen;
    private float StartSpeed;

    //GROUNDED 0 = I WALKING HERE
    //GROUNDED 1 AND 3 = LAUNCHED
    //GROUNDED 2 = FREEZED BECAUSE HEARD A DIVE KICK
    //GROUNDED 4 = PUNCHED 


    private void Start()
    {
        StartSpeed = Speed;
        MyRigid = GetComponent<Rigidbody2D>();
        MyRen = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (MyRen.isVisible)
        {
            #region MOVE
        if (Player != null)
        {
            if (Player.CanMove == true && Grounded == 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3 (MoveSpots[CurrentTarget].position.x, transform.position.y, 0), Speed * Time.deltaTime);
            }
        }
        #endregion

            #region Turn Around
        if (MoveSpots[CurrentTarget].transform.position.x > transform.position.x && transform.localScale.x == -1) { transform.localScale = new Vector3(1,1,1); }
        else if (MoveSpots[CurrentTarget].transform.position.x < transform.position.x && transform.localScale.x == 1) { transform.localScale = new Vector3(-1, 1, 1); }
        #endregion

            #region Switch Move Point
        if(Mathf.Abs (transform.position.x - MoveSpots[CurrentTarget].position.x) < 0.2f)
                //(Vector2.Distance(transform.position.x, MoveSpots[CurrentTarget].position.x) < 0.2f)
        {
             //You're at the point.
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
        }
        #endregion

            #region Dive kick
        //DIVE KICK
        if (Player.MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Divekick3") && Grounded == 0)
        {  
            if (Vector2.Distance (transform.position, Player.gameObject.transform.position) < 5f){Grounded = 1;}
            else { Grounded = 2; }
        }

        //PROCESS DODGED DIVE KICK
        if (!Player.MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Divekick3") && Grounded == 2) { Grounded = 0; }

        //LAUNCH
        if (Grounded == 1)
        {
            Grounded = 3;
            MyRen.flipY = true;
            MyRigid.velocity = Vector2.zero;
            MyRigid.AddForce(transform.up * 8f, ForceMode2D.Impulse);
        }
            #endregion
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        //Landed back
        if (other.gameObject.CompareTag("Ground") && Grounded != 4)
        {
            if (Grounded == 3) { Grounded = 0; MyRen.flipY = false; }
        }

        //Crushed by tiles
        if (Grounded == 4)
        {
            if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
            {
                Grounded = 8;
                AudiMan.Play("Crush");
                Instantiate(SplatEffect, gameObject.transform.position, Quaternion.identity);
                //Count.YellowCounting();
                Destroy(gameObject);
            }
        }

        //Bonked into player
         if (other.gameObject.CompareTag("Player") && Grounded == 0)
         {
            if(other.gameObject.transform.position.y > transform.position.y + 0.6f) { MyRigid.AddForce(transform.up * 2f, ForceMode2D.Impulse); Player.Bounce(12f, 1); }
            Speed = 0;
         }

        //Punched on ground
        if (other.gameObject.CompareTag("Attack") && Grounded != 4)
        {
            //Divekick
            if (other.gameObject.transform.position.y > transform.position.y + 0.6f)
            {
                AudiMan.Play("Crush");
                Player.Bounce(12f, 2);
                Instantiate(SplatEffect, gameObject.transform.position, Quaternion.identity);
                Instantiate(SplatSprite, gameObject.transform.position, Quaternion.identity);
                // Count.YellowCounting();
                Destroy(gameObject);
            }

            //Everything else
            else
            {
                AudiMan.Play("Crush");
                if (other.gameObject.transform.position.x > transform.position.x)
                {
                    Instantiate(SplatSprite, gameObject.transform.position, Quaternion.identity);
                    Grounded = 4; MyAnim.Play("Pookachu Pucnhed");
                    MyRigid.velocity = Vector2.zero;
                    MyRigid.velocity = new Vector2(-15f, 3f);
                }

                else if (other.gameObject.transform.position.x < transform.position.x)
                {
                    Instantiate(SplatSprite, gameObject.transform.position, Quaternion.identity);
                    Grounded = 4; MyAnim.Play("Pookachu Pucnhed");
                    MyRigid.velocity = Vector2.zero;
                    MyRigid.velocity = new Vector2(15f, 0f);
                }
            }
        }
    }


    private void OnCollisionExit2D(Collision2D other)
    {
        //Player isn't near
        if (other.gameObject.CompareTag("Player") && Grounded == 0 && Speed == 0) { Speed = StartSpeed; }
    }



}

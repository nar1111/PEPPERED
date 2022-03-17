using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull_Stage_3 : State
{
    [Header("STUFF")]
    [SerializeField] private GameObject[] MoreStuff;
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private GameObject[] Bulls;
    [SerializeField] private Transform[] DropPoints;
    [SerializeField] private Bull_Stage_4 Stage4;

    [Header("Navigation")]
    [SerializeField] private A_11 A11;
    [SerializeField] private LayerMask Ground;
    [SerializeField] private Transform[] GroundChecker;
    [SerializeField] private Transform[] EndPoints;
    private bool Grounded1;
    private bool Grounded2;

    //Handicap
    private float StartTimer = 1.5f;
    private float Speed;
    private float MinSpeed = 7f;
    private float MaxSpeed = 12f;
    private float MyDistance = 4f;
    private bool DistanceSwitch = true;

    [Header("COSMETICS")]
    [SerializeField] private AUDIOMANAGER Audiman;
    [SerializeField] private GameObject[] CeilingBreak;
    [SerializeField] private Animator[] MyAnims;
    [SerializeField] private GameObject ShakeIt;
    private int MyStage = 0;

    public override State RunCurrentState()
    {
        if (MyStage != 0)
        {
            FlipBulls();
        }

        if (Bulls[0].activeInHierarchy)
        {
            ISeeGround1();

            AnimateShit();
        }

        if (Bulls[1].activeInHierarchy && MyStage >= 3)
        {
            ISeeGround2();

            Handicap2();
        }

        Handicap();

        if (MyStage >= 3)
        {
            Move();

            AdjustSpeed();
        }

        if (MyStage == 6) { return Stage4; }

        return this;
    }

    public void StartTheChase()
    {
        MyStage = 1;
        MyAnims[0].Play("Bull Bro Idle");
        Bulls[0].SetActive(true);
        Bulls[0].transform.position = DropPoints[0].position;
        Audiman.Play("Rock Impact");
        Audiman.Play("Battle Start");
        Instantiate(ShakeIt, new Vector3 (DropPoints[0].position.x, DropPoints[0].position.y + 3f), Quaternion.identity);
        CeilingBreak[0].SetActive(true);

        if (Player.transform.parent != null)
        {
            State_Cart_Ride MyRide = Player.transform.parent.GetComponentInChildren<State_Cart_Ride>();
            MyRide.Chill(1.5f);
        } else
        {
            Player.MyRigidBody.velocity = Vector2.zero;
            Player.CanMove = false;
            Player.MyAnim.Play("Stop Run");
            Player.MyAnim.SetFloat("Speed", -1f);
            Invoke("UnfreezePlayer", 1f);
        }
    }

    public void SecondBullChase()
    {
         MyStage = 3;
         StartTimer = 2f;
         MyAnims[1].Play("Bro 2 Idle 1");
         Audiman.Play("Rock Impact");
         Audiman.Play("Battle Start");
         Bulls[1].SetActive(true);
         Bulls[1].transform.position = DropPoints[1].position;
         Instantiate(ShakeIt, new Vector3(DropPoints[1].position.x, DropPoints[1].position.y + 3f), Quaternion.identity);
         CeilingBreak[1].SetActive(true);

        if (Player.transform.parent != null)
        {
            State_Cart_Ride MyRide = Player.transform.parent.GetComponentInChildren<State_Cart_Ride>();
            MyRide.Chill(1.5f);
        }
        else
        {
            Player.MyRigidBody.velocity = Vector2.zero;
            Player.CanMove = false;
            Player.MyAnim.Play("Stop Run");
            Player.MyAnim.SetFloat("Speed", -1f);
            Invoke("UnfreezePlayer", 1f);
        }
    }

    private void UnfreezePlayer() { Player.CanMove = true; }

    private void Handicap()
    {
        //Time Ran Out
        if (MyStage == 2)
        {
            StartTimer -= Time.deltaTime;
            if (StartTimer < 0)
            {
                MyAnims[0].Play("Bull Bro Ride");
                MyStage = 3; 
            }
        }

        //Distance is Big Enough
        else if (MyStage == 2 && StartTimer >= 0 && Vector2.Distance(Bulls[0].transform.position, Player.transform.position) > 10)
        {
            MyAnims[0].Play("Bull Bro Ride");
            MyStage = 3;
        }

    }

    private void Handicap2()
    {
        if (MyStage == 3)
        {
            StartTimer -= Time.deltaTime;
            if (StartTimer < 0)
            {
                MyAnims[0].Play("Bull Bro Ride");
                MyStage = 4;
            }
        }
    }

    private void FlipBulls()
    {
        if (Player.transform.position.x > Bulls[0].transform.position.x)
        {
            Bulls[0].transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            Bulls[0].transform.localScale = new Vector3(-1, 1, 1);
        }


        if (Player.transform.position.x > Bulls[1].transform.position.x)
        {
            Bulls[1].transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            Bulls[1].transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void Move()
    {
        if (Vector2.Distance(Bulls[0].transform.position, EndPoints[0].position) > 0.4f && A11.Handicapped == false)
        {
            Bulls[0].transform.position = Vector2.MoveTowards(Bulls[0].transform.position, new Vector2(Player.transform.position.x, Bulls[0].transform.position.y), Speed * Time.deltaTime);
            MyAnims[0].Play("Bull Bro Ride");
        } else
        {
            MyAnims[0].Play("Bull Bro Idle");
        }

        if (MyStage >= 4 && Vector2.Distance(Bulls[1].transform.position, EndPoints[1].position) > 0.4f)
        {
            Bulls[1].transform.position = Vector2.MoveTowards(Bulls[1].transform.position, new Vector2(Player.transform.position.x, Bulls[1].transform.position.y), 3 * Time.deltaTime);
            MyAnims[1].Play("Bro 2 Ride 1");
        } else if (MyStage >= 4 && Vector2.Distance(Bulls[1].transform.position, EndPoints[1].position) < 0.4f)
        {
            MyAnims[1].Play("Bro 2 Idle 1");
        }
    }

    private void AdjustSpeed()
    {
        if (MyDistance > 2f && DistanceSwitch == true)
        {
            MyDistance -= Time.deltaTime;
        }
        else { DistanceSwitch = false; }

        if (MyDistance < 5f && DistanceSwitch == false)
        {
            MyDistance += Time.deltaTime;
        }
        else { DistanceSwitch = true; }

        if (Vector2.Distance(Bulls[0].transform.position, Player.transform.position) < MyDistance)
        {
            Speed = MinSpeed;
        }
        else
        {
            Speed = MaxSpeed;
        }
    }

    public void ChangeState()
    {
        MyStage = 6;
    }

    public void WallsNow()
    {
        MoreStuff[0].SetActive(true);
        MoreStuff[1].SetActive(true);
    }

    #region BullsAnimate
    private void ISeeGround1()
    {
        Grounded1 = Physics2D.OverlapCircle(GroundChecker[0].position, .02f, Ground);
        if (MyStage == 1 && Grounded1)
        {
            Audiman.Play("Dive Kick Land");
            MyStage = 2;
            Instantiate(ShakeIt, GroundChecker[0].position, Quaternion.identity);
        }
    }

    private void ISeeGround2()
    {
        Grounded2 = Physics2D.OverlapCircle(GroundChecker[1].position, .02f, Ground);
    }

    private void AnimateShit()
    {
        if (Grounded1) { MyAnims[0].SetBool("Jump", false); }
        else { MyAnims[0].SetBool("Jump", true); }

        if (Grounded2) { MyAnims[1].SetBool("Jump", false); }
        else { MyAnims[1].SetBool("Jump", true); }
    }
    #endregion
}
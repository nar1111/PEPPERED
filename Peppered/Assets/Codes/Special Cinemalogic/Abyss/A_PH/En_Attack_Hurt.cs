using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class En_Attack_Hurt : MonoBehaviour
{
    #pragma warning disable 649
    public Animator MyAnim;
    [Header ("UI")]
    public Slider HealthBar;
    private int Health = 6;

    [Header ("Fight")]
    [SerializeField]
    private float StartTimer;
    [SerializeField]
    private float Timer;
    [SerializeField]
    private GameObject Bullet;
    [SerializeField]
    private PLAYER_CONTROLS Player;
    [SerializeField]
    private PlayableDirector[] Cutscene;
    [SerializeField]
    private GameObject DeathEffect;


    [Header ("Movement")]
    [SerializeField]
    private float MoveSpeed;
    [SerializeField]
    private Transform[] RightMoveSpots;
    [SerializeField]
    private Transform[] LeftMoveSpots;
    [SerializeField]
    private Transform[] ChangeSpot;
    private int RightSide;
    private int CurrentTarget = 0;
    private int BossState;


    // Start is called before the first frame update
    void Start() { Timer = StartTimer; RightSide = 0; }

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = Health;

        if (Health == 0 && BossState != -10)
        {
            BossState = -10;
            HealthBar.gameObject.SetActive(false);
            //Cutscene[1].Play();
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            MySceneManager.Abyss_State = -2;
            Destroy(gameObject);
        }

        #region Shooting
        if (Timer <= 0 && MySceneManager.DeadState == 0 && BossState == 0)
        {
            //Shoot
            BossState = 1;
            MyAnim.Play("Envelope_Charge");
        }
        else { Timer -= Time.deltaTime; }

        if (MySceneManager.DeadState > 0) { Timer = StartTimer; }
        #endregion

        #region Movement
        //Float at the right side RS = 1 
        if (RightSide == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, RightMoveSpots[CurrentTarget].position, MoveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, RightMoveSpots[CurrentTarget].position) < 0.2f)
            {
                 if (CurrentTarget < RightMoveSpots.Length - 1) { CurrentTarget++; }
                 else { CurrentTarget = 0; }
            }
        }

        //RIGHT SIDE
        else if (RightSide == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, ChangeSpot[0].position, 10 * Time.deltaTime);

            if (Vector2.Distance(transform.position, ChangeSpot[0].position) < 0.2f)
            {
                RightSide = 1;
                MyAnim.Play("Envelope_Idle");
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
                BossState = 0;
            }
        }

        //LEFT SIDE
        else if (RightSide == 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, ChangeSpot[1].position, 10 * Time.deltaTime);

            if (Vector2.Distance(transform.position, ChangeSpot[1].position) < 0.2f)
            {
                RightSide = 4;
                MyAnim.Play("Envelope_Idle");
                gameObject.transform.localScale = new Vector3(1, 1, 1);
                BossState = 0;
            }
        }

        //Float at the left side RS = 4 
        if (RightSide == 4)
        {
            transform.position = Vector2.MoveTowards(transform.position, LeftMoveSpots[CurrentTarget].position, MoveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, LeftMoveSpots[CurrentTarget].position) < 0.2f)
            {
                if (CurrentTarget < LeftMoveSpots.Length - 1) { CurrentTarget++; }
                else { CurrentTarget = 0; }
            }
        }
        #endregion
    }


    void DEARGODFIRE()
    {
        Instantiate(Bullet, transform.position, Quaternion.identity);
        MyAnim.Play("Envelope_Attack");
        Timer = StartTimer;
        BossState = 0;
    }


    #region Getting Hurt
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && Player.MyRigidBody.velocity.y < 0f && BossState != 2)
        {
            BossState = 2;
            Health--;
            MyAnim.Play("Envelope_Hurt");
            Player.ResetAll();
            Player.MyRigidBody.velocity = Vector3.zero;
            Player.MyRigidBody.AddForce(transform.up * 3f, ForceMode2D.Impulse);
            Player.MyAnim.Play("Take Off");
            Cutscene[0].Stop();
            Cutscene[0].Play();

            if (RightSide == 1)
            {
                RightSide = 3;
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }

            if (RightSide == 4)
            {
                RightSide = 0;
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        else if (other.tag == "Attack" && BossState != 2)
        {
            BossState = 2;
            Health --;
            MyAnim.Play("Envelope_Hurt");
 
            Player.ResetAll();
            Player.MyRigidBody.velocity = Vector3.zero;
            Player.MyRigidBody.AddForce(transform.up * 3f, ForceMode2D.Impulse);
            Player.MyAnim.Play("Take Off");
            Cutscene[0].Stop();
            Cutscene[0].Play();

            if (RightSide == 1)
            {
                RightSide = 3;
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }


            if (RightSide == 4)
            {
                RightSide = 0;
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    #endregion
}

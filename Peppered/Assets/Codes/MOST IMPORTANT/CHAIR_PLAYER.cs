using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CHAIR_PLAYER : MonoBehaviour
{
    #pragma warning disable 649
    public float PushPower;
    [SerializeField]private float JumpPower;
    [SerializeField]private AudioSource Roll;
    public Transform GroundChecker;
    public Object JumpDust;
    public LayerMask Ground;
    public bool CanMove = true;
    private SpriteRenderer MyRen;
    [SerializeField]private bool Old;
    [HideInInspector]public bool FIGHT = false;
    [SerializeField]private GameObject TimerBar;
    [SerializeField]private ParticleSystem DashEffect;
    [SerializeField]private ParticleSystem.EmissionModule Dash;
    [SerializeField]private Sprite MainSpr;
    [SerializeField]private Sprite FightSpr;
    [SerializeField]private GameObject AttackCol;
    [SerializeField]private AUDIOMANAGER AudiMan;
    private float CoolingTimer = 0;
    private int Direction;

    #region Private Stuff
    [HideInInspector]
    public Rigidbody2D MyRigidBody;
    private float FallMultiplier = 2.7f;
    private float LowJumpMultiplier = 1.8f;
    private float GroundCheckerRadius = 0.12f;
    private bool RequestJump;
    private bool RequestPush;
    [HideInInspector]
    public bool Grounded;
    private bool Land;
    private bool AxisInUse = false;
    private float Cooldown;
    private bool ClickedUp;
    #endregion

    void Awake()
    {
        MyRigidBody = GetComponent<Rigidbody2D>();
        MyRen = GetComponent<SpriteRenderer>(); 
        if (DashEffect != null) { Dash = DashEffect.emission; }
        CanMove = true;
    }

    void Update()
    {
        MOVING();

        BATTLEFIGHT();

        MOVE();

        ACTING();
    }

    private void ACTING()
    {
        if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") == 0f && CanMove)
        {
            if (!ClickedUp && Grounded) { ClickedUp = true; MySceneManager.Act = true; }
        }
    }

    private void MOVE()
    {
        if (Input.GetAxisRaw("Horizontal") == 1 && MySceneManager.CutscenePlaying == false && CanMove)
        {
            if (AxisInUse == false) //&& Cooldown == 0
            {
                AxisInUse = true;
                if (Old) { MyRen.flipX = false; }
                else { transform.localScale = new Vector3(1f, 1f, 1f); }
                RequestPush = true;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") == -1 && MySceneManager.CutscenePlaying == false && CanMove)
        {
            if (AxisInUse == false) //&& Cooldown == 0 
            {
                AxisInUse = true;
                if (Old) { MyRen.flipX = true; }
                else { transform.localScale = new Vector3(-1f, 1f, 1f); }
                RequestPush = true;
            }
        }
        else if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if (AxisInUse == true)
            {
                AxisInUse = false;
            }
        }
    }

    private void BATTLEFIGHT()
    {
        if (Input.GetButtonDown("Skip") && CoolingTimer == 0 && FIGHT && Direction == 0)
        {
            CanMove = false;
            CoolingTimer = 2f;
            Dash.rateOverDistance = 60f;
            Direction = 1;
            MyRen.sprite = FightSpr;
            AttackCol.SetActive(true);
            TimerBar.SetActive(true);
            AudiMan.Play("Punch1");
        }

        if (CoolingTimer > 0)
        {
            //Direction. 0 = none. 1 = Dash. 2 = Knock left. 3 = Knock right. 4 = Stop.
            //STOP DASHING
            if (CoolingTimer < 1.7f && CanMove == false && Direction == 1)
            {
                Direction = 4;
                MyRigidBody.velocity = Vector3.zero;
            }

            //DASH
            else if (Direction == 1)
            {
                if (transform.localScale == new Vector3(1f, 1f, 1f))
                {
                    MyRigidBody.velocity = new Vector2(11, MyRigidBody.velocity.y);
                }
                else if (transform.localScale == new Vector3(-1f, 1f, 1f))
                {
                    MyRigidBody.velocity = new Vector2(-11, MyRigidBody.velocity.y);
                }
            }

            //KNOCKBACK LEFT
            else if (Direction == 2) { MyRigidBody.velocity = new Vector2(-6, MyRigidBody.velocity.y); }
            else if (Direction == 3) { MyRigidBody.velocity = new Vector2(6, MyRigidBody.velocity.y); }


            //STOP
            else if (Direction == 4)
            {
                AttackCol.SetActive(false);
                Dash.rateOverDistance = 0f;
                MyRen.sprite = MainSpr;
                CanMove = true;
            }

            if (TimerBar.activeInHierarchy) { TimerBar.transform.localScale = new Vector3(CoolingTimer / 6f, TimerBar.transform.localScale.y); }
            CoolingTimer -= Time.deltaTime;
        }

        //Timer stopped.
        else if (CoolingTimer < 0 && Direction != 0)
        {
            CoolingTimer = 0;
            Direction = 0;
            TimerBar.SetActive(false);
            CanMove = true;
        }
    }

    private void MOVING()
    {
        Grounded = Physics2D.OverlapCircle(GroundChecker.position, GroundCheckerRadius, Ground);
        if (Grounded && Land && MyRigidBody.velocity.y < 0) { MyRigidBody.velocity = new Vector3(MyRigidBody.velocity.x, 0f); Land = false; }
        if (Cooldown > 0) { Cooldown -= Time.deltaTime; }
        else if (Cooldown <= 0) { Cooldown = 0; if (Grounded) { MyRigidBody.velocity = new Vector3(0f, MyRigidBody.velocity.y); } }

        if (Input.GetButtonDown("Jump") && Grounded && MySceneManager.CutscenePlaying == false && CanMove) { RequestJump = true; }
    }

    private void FixedUpdate()
    {
        BETTERJUMPCHECK();

        ROLLCHECK();

        MOVEFIXEDCHECK();

        // Даём еще раз нажать Act
        if (Input.GetAxisRaw("Vertical") == 0) { ClickedUp = false; MySceneManager.Act = false; }
    }

    private void MOVEFIXEDCHECK()
    {
        if (RequestJump && MySceneManager.CutscenePlaying == false)
        {
            MyRigidBody.AddForce(transform.up * JumpPower, ForceMode2D.Impulse);
            if (JumpDust != null)
            {
                Instantiate(JumpDust, gameObject.transform.position - new Vector3(0f, 0.34f, 0f), Quaternion.identity);
            }

            Land = true;
            RequestJump = false;
        }
        if (RequestPush)
        {
            if (Grounded)
            {
                MyRigidBody.velocity = Vector3.zero;
                if (!Old)
                {
                    if (transform.localScale == new Vector3(1f, 1f, 1f)) { MyRigidBody.AddForce(transform.right * PushPower, ForceMode2D.Impulse); RequestPush = false; if (Grounded) { Roll.Play(); } Cooldown = 0.5f; }
                    else if (transform.localScale == new Vector3(-1f, 1f, 1f)) { MyRigidBody.AddForce(transform.right * -PushPower, ForceMode2D.Impulse); RequestPush = false; if (Grounded) { Roll.Play(); } Cooldown = 0.5f; }
                }
                else
                {
                    if (MyRen.flipX == false) { MyRigidBody.AddForce(transform.right * PushPower, ForceMode2D.Impulse); RequestPush = false; if (Grounded) { Roll.Play(); } Cooldown = 0.5f; }
                    else if (MyRen.flipX == true) { MyRigidBody.AddForce(transform.right * -PushPower, ForceMode2D.Impulse); RequestPush = false; if (Grounded) { Roll.Play(); } Cooldown = 0.5f; }
                }
            }
            else if (!Grounded)
            {
                if (!Old)
                {
                    MyRigidBody.velocity = new Vector3(0f, MyRigidBody.velocity.y);
                    if (transform.localScale == new Vector3(1f, 1f, 1f)) { MyRigidBody.AddForce(transform.right * PushPower / 2, ForceMode2D.Impulse); RequestPush = false; if (Grounded) { Roll.Play(); } Cooldown = 0.5f; }
                    else if (transform.localScale == new Vector3(-1f, 1f, 1f)) { MyRigidBody.AddForce(transform.right * -PushPower / 2, ForceMode2D.Impulse); RequestPush = false; if (Grounded) { Roll.Play(); } Cooldown = 0.5f; }
                }
                else
                {
                    MyRigidBody.velocity = new Vector3(0f, MyRigidBody.velocity.y);
                    if (MyRen.flipX == false) { MyRigidBody.AddForce(transform.right * PushPower / 2, ForceMode2D.Impulse); RequestPush = false; if (Grounded) { Roll.Play(); } Cooldown = 0.5f; }
                    else if (MyRen.flipX == true) { MyRigidBody.AddForce(transform.right * -PushPower / 2, ForceMode2D.Impulse); RequestPush = false; if (Grounded) { Roll.Play(); } Cooldown = 0.5f; }
                }
            }
        }
    }

    private void ROLLCHECK()
    {
        if (MyRigidBody.velocity.x > 0 && Input.GetAxisRaw("Horizontal") == 0 && CoolingTimer < 1.7f && MySceneManager.CutscenePlaying == false)
        {
            MyRigidBody.velocity = new Vector3(MyRigidBody.velocity.x - 0.8f, MyRigidBody.velocity.y);
        }
        else if (MyRigidBody.velocity.x < 0 && Input.GetAxisRaw("Horizontal") == 0 && CoolingTimer < 1.7f && MySceneManager.CutscenePlaying == false)
        {
            MyRigidBody.velocity = new Vector3(MyRigidBody.velocity.x + 0.8f, MyRigidBody.velocity.y);
        }
    }

    private void BETTERJUMPCHECK()
    {
        if (MyRigidBody.velocity.y < 0)
        {
            MyRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1f) * Time.deltaTime;
        }
        else if (MyRigidBody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            MyRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1f) * Time.deltaTime;
        }
    }

    public void StopMove(){ Direction = 4; }

    #region Collision
    private void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.name.Equals ("MAIN")) 
        {
            transform.parent = other.gameObject.transform;
        }
    }

    private void OnCollisionExit2D (Collision2D other)
    {
        if (other.gameObject.name.Equals("MAIN")) 
        {
            transform.parent = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Star" && FIGHT)
        {
            AttackCol.SetActive(false);
            TimerBar.SetActive(false);
            AudiMan.Play("Rock Impact");
            Dash.rateOverDistance = 0f;
            MyRen.sprite = MainSpr;
            MyRigidBody.velocity = Vector2.zero;
            CoolingTimer = 0.5f;
            if (other.gameObject.transform.position.x > transform.position.x) { Direction = 2; }
            else if (other.gameObject.transform.position.x < transform.position.x){ Direction = 3; }
        }
    }
    #endregion
}
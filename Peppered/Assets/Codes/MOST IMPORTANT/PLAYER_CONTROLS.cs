using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PLAYER_CONTROLS : MonoBehaviour
{
    #pragma warning disable 649
    #region public Stuff
    [Header ("MOVEMENT")]
    [SerializeField][Range(0, 1)] float fHorizontalDampingBasic = 0.5f;
    [SerializeField][Range(0, 1)] float fHorizontalDampingWhenStopping = 0.5f;
    [SerializeField][Range(0, 1)] float fHorizontalDampingWhenTurning = 0.5f;
    private float fHorizontalVelocity;


    [Header("JUMPING")]
    [SerializeField]private float JumpForce = 10;
    [SerializeField]private float FallMultiplier = 3.4f;
    [SerializeField]private float LowJumpMultiplier = 2f;
    [SerializeField]private float JumpBuffer = 0.5f;
    [HideInInspector]public bool JumpPermission = true;
    private float JumpBufferCount;
    private float CoyoteTime;


    [Header("SLIDING")]
    [SerializeField][Range (0.1f, 10)]private float SlideSpeed;
    [SerializeField][Range (0.1f, 3)] private float SlideTime;
    private float SlideTimeTimer;
    private int RequestSlide;

    [Header("DIVE KICK")]
    [SerializeField] private GameObject PunchBox;
    [SerializeField][Range(10, 60)]private float DiveKickForce;
    [SerializeField][Range (0, 5)]private float DiveKickTimer;
    private float StartDiveKickTimer;
    [HideInInspector]public int DiveKick;

    [Header ("COSMETICS")]
    public bool Wind = true;
    [SerializeField]private AudioSource WindSound;
    [SerializeField]private Object HighJumpDust;
    [SerializeField]private Object LandDust;
    [SerializeField]private Object DiveKickDust;
    [SerializeField]private Object DeathEffect;
    [SerializeField]private ParticleSystem FootSteps;
    [SerializeField]private ParticleSystem.EmissionModule FootEmission;

    [Header("TECH STUFF")]
    [SerializeField]private DeathAnim_Manager DeathAnimator;
    [SerializeField]private Transform GroundChecker1;
    [SerializeField]private Transform GroundChecker2;
    [SerializeField]private Transform CeilingChecker;
    [SerializeField]private Transform WallChecker;
    // Dead 0 = alive. Dead 1 = dead. 
    public int Dead;
    

    [Header("Navigation")]
    [SerializeField]private LayerMask Ground;
    [SerializeField]private LayerMask WallsHigh;
    [SerializeField]private LayerMask WallsLow;
    [HideInInspector]public Rigidbody2D MyRigidBody;
    [HideInInspector]public bool Grounded;
    public bool CanMove = true;
    [HideInInspector]public Vector3 RespawnPos;
    [HideInInspector]public Animator MyAnim;
    #endregion

    #region private (SHH)
    private bool RequestJump;
    private bool Walled;
    private bool Ceiling;
    private float CrawlSpeed;
    private int JumpPressLimiter = 0;
	private AUDIOMANAGER AudioMan;
	#endregion

	void Awake()
	{
        #region Y know da drill
        MyAnim = GetComponent <Animator>();
        MyRigidBody = GetComponent <Rigidbody2D>();
	    AudioMan = FindObjectOfType <AUDIOMANAGER>();
        FootEmission = FootSteps.emission;
        SlideTimeTimer = SlideTime;
        StartDiveKickTimer = DiveKickTimer;
        if (Wind){MyAnim.SetBool ("Wind", true);}
        else {MyAnim.SetBool ("Wind", false);}
	    if (MySceneManager.DeadState == 3){transform.position = MySceneManager.CheckPointPos; Dead = 1;}
	    #endregion
	}

    void Update ()
	{
        if (Dead == 0)
        {
            CRAWL();

            GROUND();

            JUMP();

            MOVE();

            RANDOM();

            SLIDE();
        }
        else if (Dead == 1)
        {
            MyRigidBody.velocity = Vector2.zero;
            MyRigidBody.gravityScale = 0f;
        }
    }

    private void SLIDE()
    {
        if (RequestSlide == 1)
        {
            //You ran out of time
            if (SlideTimeTimer <= 0 || CoyoteTime < 0f)
            {
                RequestSlide = 2;
                SlideTimeTimer = SlideTime;
                PunchBox.SetActive(false);
                FootEmission.rateOverDistance = 0f;
                MyRigidBody.velocity = new Vector2(0, MyRigidBody.velocity.y);
                if (Ceiling && Grounded || Input.GetAxisRaw("Vertical") < 0f && Grounded) { CrawlSpeed = 0.14f; WallChecker.gameObject.SetActive(false); }
                else { WallChecker.gameObject.SetActive(true); }
                MyAnim.SetBool("Downbutton", true);
                CanMove = true;
            }
            else
            {
                if (WallChecker.gameObject.activeInHierarchy) { WallChecker.gameObject.SetActive(false); }
                SlideTimeTimer -= Time.deltaTime;
            }
        }
        else if (RequestSlide == 2)
        {
            if (Input.GetAxisRaw("Vertical") == 0f) { RequestSlide = 0; }
        }
    }

    private void RANDOM()
    {
        if (Wind)
        {
            if (Grounded)
            {
                WindSound.volume = MyRigidBody.velocity.magnitude / 20;
            }
            else if (!Grounded)
            {
                WindSound.volume = MyRigidBody.velocity.magnitude / 12;
            }
            //Wind pitch
            if (WindSound.volume > 0.5f && WindSound.volume < 0.8f) { WindSound.pitch = 1.2f; }
            else if (WindSound.volume >= 0.8f) { WindSound.pitch = 1.4f; }
            else if (WindSound.volume < 0.5f) { WindSound.pitch = 1f; }
        } else if (Wind == false && WindSound.volume != 0) { WindSound.volume = 0; }

        //Waiting for the end of animation
        if (DiveKick > 1)
        {
            if (DiveKickTimer <= 0) { DiveKick = 0; CanMove = true; DiveKickTimer = 0; PunchBox.SetActive(false); }
            else { DiveKickTimer -= Time.deltaTime; if (DiveKick == 2 && DiveKickTimer < 0.30f) { DiveKick = 3; JumpPressLimiter = 0; } }
        }
    }

    private void MOVE()
    {
        fHorizontalVelocity = MyRigidBody.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f || MySceneManager.CutscenePlaying == true)
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.fixedDeltaTime * 3f);
            MyAnim.SetFloat("Speed", 0f);
        }

        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity) || MySceneManager.CutscenePlaying == true)
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.fixedDeltaTime * 3f);
            MyAnim.SetFloat("Speed", 0f);
        }
        else
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingBasic, Time.fixedDeltaTime * 3f + CrawlSpeed);
        }
    }

    private void JUMP()
    {
        //Jump Buffer
        if (Input.GetButtonDown("Jump") && MySceneManager.CutscenePlaying == false && Ceiling == false && DiveKick != 1 && JumpPermission)
        {
            if (CanMove && RequestSlide == 0 || !CanMove && RequestSlide == 1 || !CanMove && DiveKick == 3 || CanMove && RequestSlide == 2)
            {
                JumpBufferCount = JumpBuffer;
            }
        }
        else
        {
            JumpBufferCount -= Time.deltaTime;
        }

        //Dive
        if (!Grounded && Input.GetButtonDown("Jump") && Input.GetAxisRaw("Vertical") < 0f && DiveKick == 0 && CoyoteTime < 0f)
        {
            if (MySceneManager.CutscenePlaying == false && CanMove)
            {
                CanMove = false;
                MyRigidBody.velocity = Vector3.zero;
                DiveKick = 1;
                RequestSlide = 2;
                PunchBox.SetActive(true);
                AudioMan.Play("Dive Kick");
                MyAnim.Play("Divekick1");
            }
        }

        //Jump
        if (JumpBufferCount >= 0 && CoyoteTime > 0f && DiveKick != 1 && JumpPressLimiter == 0 && JumpPermission)
        {
            if (MySceneManager.CutscenePlaying == false)
            {
                JumpPressLimiter = 1;
                JumpBufferCount = 0;
                CoyoteTime = -2;
                RequestJump = true;
            }
        }
    }

    private void GROUND()
    {

        //Checking for walls while standing
        if (WallChecker.gameObject.activeInHierarchy)
        {
            Walled = Physics2D.OverlapCircle(WallChecker.position, 0.05f, WallsHigh);
            if (Ceiling) { Ceiling = false; }
        }
        else
        {
            //Checking for walls and ceiling while crouching
            Walled = Physics2D.OverlapCircle(GroundChecker2.position, 0.05f, WallsLow);
            Ceiling = Physics2D.OverlapCircle(CeilingChecker.position, 0.05f, WallsHigh);
        }

        //Checking for ground
        Grounded = Physics2D.OverlapCircle(GroundChecker1.position, .01f, Ground) || Physics2D.OverlapCircle(GroundChecker2.position, .1f, Ground);

        if (Grounded)
        {
            if (MyAnim.GetBool("Ground") == false && MyRigidBody.velocity.y < 0.1f)
            {
                JumpPressLimiter = 0;
                CoyoteTime = .2f;
                MyAnim.SetBool("Ground", true);
                MyAnim.SetBool("Falling", false);
                if (!MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Invinsible"))
                {
                    if (MySceneManager.DeadState != 2) { AudioMan.Play("Land"); }
                    Instantiate(LandDust, gameObject.transform.position - new Vector3(0f, 0.38f, 0f), Quaternion.identity);
                }
            }
            else if (CoyoteTime != .2f && Mathf.Abs(MyRigidBody.velocity.y) <= 0) { CoyoteTime = .2f; }

            if (DiveKick == 1)
            {
                JumpPressLimiter = 0;
                DiveKickTimer = StartDiveKickTimer;
                DiveKick = 2;
                AudioMan.Play("Dive Kick Land");
                Instantiate(DiveKickDust, gameObject.transform.position, Quaternion.identity);
            }
        }

        if (!Grounded)
        {
            CoyoteTime -= Time.deltaTime;
            if (MyAnim.GetBool("Ground") == true) { MyAnim.SetBool("Ground", false); }
            if (MyRigidBody.velocity.y < 0) { MyAnim.SetBool("Falling", true); }
            //Fallen while dive kick
            if (DiveKick > 1) { DiveKick = 0; CanMove = true; CoyoteTime = .2f; PunchBox.SetActive(false); }
        }
    }

    private void CRAWL()
    {
        if (Input.GetAxisRaw("Vertical") < 0f && Grounded && MySceneManager.CutscenePlaying == false && CanMove == true)
        {
            //Trigger Crawl
            if (MyAnim.GetBool("Downbutton") == false && Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0 || MyAnim.GetBool("Downbutton") == false && Walled) //&& Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 0
            { MyAnim.SetBool("Downbutton", true); CrawlSpeed = 0.14f; WallChecker.gameObject.SetActive(false); }

            //Trigger Slide. 1 = Slide in action. 2 = Slide waiting for a reset. 0 = Slide ready.
            else if (MyAnim.GetBool("Downbutton") == false && Mathf.Abs(fHorizontalVelocity) > 4f && RequestSlide == 0 && !Walled)
            {
                AudioMan.Play("Slide");
                WallChecker.gameObject.SetActive(false);
                MyAnim.SetBool("Downbutton", true);
                MyAnim.SetTrigger("Slide");
                PunchBox.SetActive(true);
                FootEmission.rateOverDistance = 10f;
                RequestSlide = 1;
                CanMove = false;
            }
        }

        else if (Input.GetAxisRaw("Vertical") == 0f || MySceneManager.CutscenePlaying == false || CanMove == false)
        {
            if (MyAnim.GetBool("Downbutton") == true)
            {
                if (!Ceiling) { MyAnim.SetBool("Downbutton", false); CrawlSpeed = 0; WallChecker.gameObject.SetActive(true); }
            } 
        }
    }

    private void FixedUpdate()
	{
        if (Dead == 0)
        {
            BETTERJUMPCHECK();

            JUMPING();

            MOVING();
        }
    }

    private void MOVING()
    {
        //Simple moving
        if (CanMove && MySceneManager.CutscenePlaying == false)
        {
            MyRigidBody.velocity = new Vector2(fHorizontalVelocity, MyRigidBody.velocity.y);
            if (!Walled) { MyAnim.SetFloat("Speed", 2f); }
            else { MyAnim.SetFloat("Speed", 0f); }

            //Turn Sprite Around
            if (Input.GetAxisRaw("Horizontal") > 0) { transform.localScale = new Vector3(1, 1, 1); }
            else if (Input.GetAxisRaw("Horizontal") < 0) { transform.localScale = new Vector3(-1, 1, 1); }
        }

        //Slide moving
        else if (!CanMove && MySceneManager.CutscenePlaying == false && RequestSlide == 1)
        {
            if (transform.localScale.x == 1)
            {
                MyRigidBody.velocity = new Vector2(SlideSpeed, MyRigidBody.velocity.y);
            }
            else if (transform.localScale.x == -1)
            {
                MyRigidBody.velocity = new Vector2(-SlideSpeed, MyRigidBody.velocity.y);
            }
        }

        //Dive Kicking
        if (DiveKick == 1) { MyRigidBody.velocity = new Vector2(0, -DiveKickForce); }
    }

    private void JUMPING()
    {
        if (RequestJump && MySceneManager.CutscenePlaying == false)
        {
            //High Jump
            if (MyAnim.GetBool("Downbutton") == true || RequestSlide == 1 || DiveKick == 3)
            {
                AudioMan.Play("High Jump");
                MyAnim.Play("HighJump1");
                RequestSlide = 2;
                MyRigidBody.velocity = new Vector2(MyRigidBody.velocity.x, JumpForce + 2f);
                Instantiate(HighJumpDust, new Vector3(transform.position.x, transform.position.y - 0.37f), Quaternion.identity);
            }
            //Simple Jump
            else
            {
                if (CanMove)
                {
                    AudioMan.Play("Jump");
                    MyAnim.Play("Take Off");
                    MyRigidBody.velocity = new Vector2(MyRigidBody.velocity.x, JumpForce);
                }
            }
            RequestJump = false;
        }
    }

    private void BETTERJUMPCHECK()
    {
        if (MyRigidBody.velocity.y < 0 && !Grounded)
        {
            MyRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1f) * Time.deltaTime;
        }

        else if (MyRigidBody.velocity.y > 0 && !Grounded && !Input.GetButton("Jump") && MySceneManager.DeadState == 0)
        {
            MyRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1f) * Time.deltaTime;
        }
    }

    #region Misc
    #region Death
    public void Death(int AnimNumber, float Time)
    {
        if (MySceneManager.DeadState == 0 && MySceneManager.CutscenePlaying == false && Dead == 0)
        {
            PunchBox.SetActive(false);
            Dead = 1;
            WindSound.volume = 0;
            MyAnim.Play("Invisible");
            FootEmission.rateOverDistance = 0f;
            DeathAnimator.CreateDeathAnim(AnimNumber, Time);
        }
    }
    #endregion

    #region Reset 
    public void ResetAll()
    {
        MyAnim.SetFloat("Speed", 0f);
        Dead = 0;
        JumpPressLimiter = 0;
        PunchBox.SetActive(false);
        if (!Ceiling)
        {
            MyAnim.SetBool("Downbutton", false);
            WallChecker.gameObject.SetActive(true);
            CrawlSpeed = 0;
        }
        RequestJump = false;
        SlideTimeTimer = SlideTime;
        RequestSlide = 0;
        DiveKick = 0;
        MyRigidBody.gravityScale = 1.6f;
        FootEmission.rateOverDistance = 0f;
        fHorizontalVelocity = 0;
        CanMove = true;
        MyRigidBody.velocity = Vector2.zero;
    }
    #endregion

    #region Sounds
    public void RunL_Play() { AudioMan.Play("RunL"); }
    public void RunR_Play() { AudioMan.Play("RunR"); }
    public void Respawned() { AudioMan.Play("Respawn"); AudioMan.Play("Come Back"); }
    #endregion

    #region Bounce
    public void Bounce(float BounceForce, int JumpMode)
    {
       RequestJump = false;
       SlideTimeTimer = SlideTime;
       FootEmission.rateOverDistance = 0f;
       PunchBox.SetActive(false);
       RequestSlide = 0;
       CanMove = true;
       DiveKick = 0;
       CrawlSpeed = 0;
       fHorizontalVelocity = 0;
       MyRigidBody.velocity = Vector3.zero;
       MyRigidBody.velocity = new Vector2(MyRigidBody.velocity.x, BounceForce);
       if (JumpMode == 1) { MyAnim.Play("Take Off"); }
       else if (JumpMode == 2) { MyAnim.Play("HighJump1"); }
    }
    #endregion

    #endregion
}
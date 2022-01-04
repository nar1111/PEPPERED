using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_CONTROLS : MonoBehaviour {

	#region public Stuff
	[Header ("MOVEMENT")]	
	public float MaxSpeed;
	public float TimeZeroToMax;
	private float AccelRatePerSec;
	private float ForwardVelocity;

	[Header ("COSMETICS")]
	public bool Wind = true;
	public GameObject StarNumber;
	public Object DustJump;
	public Object DustDiveKick;
	public Object DeathEffect;
	public Object DashEffect;
    public Object RunDust;
    public Object Ghost;
    public SpriteRenderer SprRender;

	[Header ("FIGHT BOX")]
	public GameObject DivekickBox;
	public GameObject PunchBox;

	[HideInInspector]
	public Rigidbody2D MyRigidBody;
	[HideInInspector]
	public bool Grounded;
	[HideInInspector]
	public bool CanMove = true;
	[HideInInspector]
	public Vector3 RespawnPos;

    [Header("TECH STUFF")]
    private Collider2D MyCol;
	public Transform GroundChecker;
    public Transform WallChecker;
    public LayerMask Ground;
    public LayerMask Walls;
	[HideInInspector]
	public Animator MyAnim;

	#endregion

	#region private (SHH)
	private CAMERA Cam;
	private float FallMultiplier = 2.7f;
	private float LowJumpMultiplier = 1.8f;
    private float Runtimer = 0;

    [HideInInspector]
	public int TimesKicked = 0;
    [HideInInspector]
    public bool Staydown;
	private float CoolDown;
	private IEnumerator Fight;
	private Transform NumberTransform;
	private bool Dead;
	private float StarIntr;
    //Сохраняем изначальную макс скорость
    [HideInInspector]
    public float StrtMaxspeed;
	private bool RequestSlideStop;
	private bool RequestSlide;
	private bool RequestJump;
	private bool RequestMove;
    [HideInInspector]
    public int DashDirection;
    private bool Walled;
    private int Wall = 0;
	private The_Void TheVoid;
	private float GroundCheckerRadius = 0.19f;
    [HideInInspector]
    public bool Stunned;
	private AUDIOMANAGER AudioMan;
	#endregion

	void Awake()
	{

        #region Y know da drill
        MyCol = GetComponent<Collider2D>();
        TheVoid = FindObjectOfType<The_Void>();
        MyAnim = GetComponent <Animator>();
	    Cam = FindObjectOfType<CAMERA>();
	    MyRigidBody = GetComponent <Rigidbody2D>();
	    AudioMan = FindObjectOfType <AUDIOMANAGER>();
        StrtMaxspeed = MaxSpeed;
	
        if (Wind){MyAnim.SetBool ("Wind", true);}
        else {MyAnim.SetBool ("Wind", false);}
	    AccelRatePerSec = MaxSpeed / TimeZeroToMax;
	    if (MySceneManager.DeathTransition == 3)
        {
        gameObject.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, 0); 
        StartCoroutine(YoDead()); 
        transform.position = MySceneManager.CheckPointPos; 
        MySceneManager.InTheVoid = false;
        }
	#endregion

	}

	void Update ()
	{

            #region Pink floyd DA Wall
            Walled = Physics2D.OverlapCircle(WallChecker.position, 0.05f, Walls);

            if (Walled && Wall == 0)
            {
                if (transform.localScale == new Vector3(1f, 1f, 1f)) { Wall = 1; }
                else if (transform.localScale == new Vector3(-1f, 1f, 1f)) { Wall = 2; }
            }

            else if (Walled == false && Wall != 0) { Wall = 0; }
            #endregion

            #region CRAWL
            if (Input.GetAxisRaw("Vertical") < 0f && Grounded && MySceneManager.CutscenePlaying == false)
            {
                if (MyAnim.GetBool("Downbutton") == false) { MyAnim.SetBool("Downbutton", true); }
            }
            else
            {
                if (Staydown == true)
                {
                    MyAnim.SetBool("Downbutton", true);
                }
                else
                {
                    MyAnim.SetBool("Downbutton", false);
                }
            }

            #endregion

            #region I see GROUND y'all
            Grounded = Physics2D.OverlapCircle(GroundChecker.position, GroundCheckerRadius, Ground);

            if (Grounded && MyAnim.GetBool("Ground") == false && MyRigidBody.velocity.y < 0)
            { MyAnim.SetBool("Ground", true); MyAnim.SetBool("Falling", false); TimesKicked = 0; DashDirection = 0; }

            else if (!Grounded && MyAnim.GetBool("Ground") == true) { MyAnim.SetBool("Ground", false); }

            #endregion

            #region JUMP button MC
            if (Input.GetButtonDown("Jump") && Grounded && CanMove && MySceneManager.CutscenePlaying == false && !Staydown) { RequestJump = true; AudioMan.Play("Jump"); }

            //прыжок после подката
            else if (Input.GetButtonDown("Jump") && Grounded && !CanMove && !Staydown && RequestSlide && MySceneManager.CutscenePlaying == false) { RequestJump = true; AudioMan.Play("Jump"); }
            #endregion

            #region FIGHT
            if (Input.GetButtonDown("Jump") && !Grounded && CanMove && DashDirection == 0 && TimesKicked < 3
            && Input.GetAxisRaw("Vertical") == 0f && MySceneManager.CutscenePlaying == false && CoolDown == 0)
            {
                if (Fight != null)
                { StopCoroutine(Fight); }
                DashDirection = 1;
                CanMove = false;
                MyRigidBody.gravityScale = 0;
                MyRigidBody.velocity = new Vector3(0f, 0f);
                MyAnim.Play("Punch", -1, 0f);
                Fight = Unfreeze();
                StartCoroutine(Unfreeze());
            }

            else if (Input.GetButtonDown("Jump") && !Grounded && CanMove && DashDirection == 0 && TimesKicked < 3
            && Input.GetAxisRaw("Vertical") > 0f && MySceneManager.CutscenePlaying == false && CoolDown == 0)
            {
                if (Fight != null)
                { StopCoroutine(Fight); }
                DashDirection = 2;
                CanMove = false;
                MyRigidBody.gravityScale = 0;
                MyRigidBody.velocity = new Vector3(0f, 0f);
                MyAnim.Play("Uppercut", -1, 0f);
                Fight = Unfreeze();
                StartCoroutine(Unfreeze());
            }

            else if (Input.GetButtonDown("Jump") && !Grounded & CanMove && DashDirection == 0 && CoolDown == 0
            && Input.GetAxisRaw("Vertical") < 0f && MySceneManager.CutscenePlaying == false && DashDirection != 3)
            {
                if (Fight != null)
                { StopCoroutine(Fight); }
                DashDirection = 3;
                Fight = Unfreeze();
                StartCoroutine(Unfreeze());
            }

            if (CoolDown > 0) { CoolDown -= Time.deltaTime; }
            if (CoolDown < 0) { CoolDown = 0; }

            #endregion

            #region SLIIIDE
            if (Input.GetAxisRaw("Vertical") < 0f && Grounded && CanMove && MySceneManager.CutscenePlaying == false && !RequestSlide && ForwardVelocity == MaxSpeed)
            { MyRigidBody.velocity = new Vector3(MyRigidBody.velocity.x, MyRigidBody.velocity.y); RequestSlide = true; PunchBox.SetActive(true); AudioMan.Play("Slide"); StartCoroutine(Unfreeze()); }

            //Тормозим
            if (RequestSlide && MyRigidBody.velocity.x > 0 && Input.GetAxisRaw("Horizontal") < 0) { RequestSlideStop = true; }
            else if (RequestSlide && MyRigidBody.velocity.x < 0 && Input.GetAxisRaw("Horizontal") > 0) { RequestSlideStop = true; }

            else if (RequestSlide && Input.GetButtonDown("Jump")) { RequestSlideStop = true; }
            #endregion

            #region STA–A–A–A–A–ARS
            if (Input.GetButtonDown("Stars") && Grounded && !MySceneManager.CutscenePlaying && !RequestSlide && Input.GetAxisRaw("Vertical") == 0f && Input.GetAxisRaw("Horizontal") == 0f)
            {

                if (StarIntr == 0)
                {
                    MyAnim.Play("Star 1");
                    var Go = Instantiate(StarNumber, gameObject.transform.position + new Vector3(0f, 1.4f, 0f), Quaternion.identity);
                    Go.GetComponent<TextMesh>().text = WHAT_HAVE_I_DONE.StarNum.ToString() + "/" + WHAT_HAVE_I_DONE.MaxStars.ToString() + " STARS";
                    var Go2 = Instantiate(StarNumber, gameObject.transform.position + new Vector3(0f, 1.1f, 0f), Quaternion.identity);
                    Go2.GetComponent<TextMesh>().text = WHAT_HAVE_I_DONE.Lives.ToString() + " LIVES";
                    var Go3 = Instantiate(StarNumber, gameObject.transform.position + new Vector3(0f, 0.8f, 0f), Quaternion.identity);
                    Go3.GetComponent<TextMesh>().text = "1 DEAD BODY";
                    AudioMan.Play("Amount1");
                    StarIntr = 1;
                }

                else if (StarIntr == 1 && WHAT_HAVE_I_DONE.StarNum == 0 || WHAT_HAVE_I_DONE.Immortality == 1) { StarIntr = 0; MyAnim.Play("Idle"); AudioMan.Play("Amount2"); }
                else if (StarIntr == 1 && WHAT_HAVE_I_DONE.Immortality == 0 && WHAT_HAVE_I_DONE.StarNum > 0) { MyAnim.Play("Star 3"); StarIntr++; AudioMan.Play("Star pull"); }
                else if (StarIntr == 2) { StarIntr++; }
            }

            if (Input.GetButton("Stars") && Grounded && !MySceneManager.CutscenePlaying && StarIntr >= 3)
            {
                MyAnim.Play("Star 4");
                StarIntr += Time.deltaTime;
            }

            else if (StarIntr >= 3 && Grounded && !MySceneManager.CutscenePlaying && Input.GetButtonUp("Stars"))
            { StarIntr = 2; MyAnim.Play("Star 5"); }

            #region Cancel
            if (StarIntr > 0 && (Input.GetAxisRaw("Vertical") != 0f || Input.GetAxisRaw("Horizontal") != 0f || Input.GetButtonDown("Jump") || !Grounded))
            {
                if (StarIntr == 1) { AudioMan.Play("Amount2"); }
                StarIntr = 0;
            }

            if (MySceneManager.Act) { StarIntr = 0; if (!Wind) { MyAnim.Play("Idle"); } else { MyAnim.Play("Idle_Wind"); } }
            #endregion

            #endregion

 

        #region MOVE, b**ch, GET OUTTA WAY GET OUTTA etc. etc.
        // НАЛЕВО
        if (Input.GetAxisRaw("Horizontal") < 0f && MySceneManager.CutscenePlaying == false && Wall != 2)
        {
            if (!RequestSlide) { transform.localScale = new Vector3(-1f, 1f, 1f); }

            //Я не ползу
            if (!MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Crawl") && !Dead)
            {
                ForwardVelocity += AccelRatePerSec * Time.deltaTime;
                ForwardVelocity = Mathf.Min(ForwardVelocity, MaxSpeed);
                if (Runtimer < 665f) { Runtimer += Time.deltaTime; }
                if (Runtimer >= 3f && Runtimer != 666f)
                {
                    Runtimer = 666f; MaxSpeed = StrtMaxspeed + 2f; MyAnim.speed = 1.3f;

                    if (Grounded)
                    {
                        GameObject Newobj = Instantiate(RunDust, gameObject.transform.position - new Vector3(0f, 0.42f, 0f), Quaternion.identity) as GameObject;
                        Newobj.transform.localScale = new Vector3(-1f, 1f, 1f);
                    }
                    GameObject NewGhost = Instantiate(Ghost, transform.position, Quaternion.identity) as GameObject;
                    NewGhost.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    NewGhost.transform.localScale = new Vector3(-1f, 1f, 1f);
                    Instantiate(NewGhost, (transform.position + new Vector3(0.2f, 0f, 0f)), Quaternion.identity);
                    Instantiate(NewGhost, (transform.position + new Vector3(0.5f, 0f, 0f)), Quaternion.identity);
                }
            }

            //Я ползу
            else { ForwardVelocity = 2; }
            if (CanMove) { RequestMove = true; }
        }

        //НАПРАВО
        else if (Input.GetAxisRaw("Horizontal") > 0f && MySceneManager.CutscenePlaying == false && Wall != 1)
        {
            if (!RequestSlide) { transform.localScale = new Vector3(1f, 1f, 1f); }

            //Я не ползу
            if (!MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Crawl") && !Dead)
            {
                ForwardVelocity += AccelRatePerSec * Time.deltaTime;
                ForwardVelocity = Mathf.Min(ForwardVelocity, MaxSpeed);
                if (Runtimer < 665f) { Runtimer += Time.deltaTime; }
                if (Runtimer >= 3f && Runtimer != 666f)
                {
                    Runtimer = 666f;
                    MaxSpeed = StrtMaxspeed + 2f; MyAnim.speed = 1.3f;
                    if (Grounded)
                    {
                        Instantiate(RunDust, gameObject.transform.position - new Vector3(0f, 0.42f, 0f), Quaternion.identity);
                    }
                    GameObject NewGhost = Instantiate(Ghost, transform.position, Quaternion.identity) as GameObject;
                    NewGhost.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
                    Instantiate(NewGhost, (transform.position - new Vector3(0.2f, 0f, 0f)), Quaternion.identity);
                    Instantiate(NewGhost, (transform.position - new Vector3(0.5f, 0f, 0f)), Quaternion.identity);
                }
            }

            //Я ползу
            else { ForwardVelocity = 2; }
            if (CanMove) { RequestMove = true; }
        }

        //Стой
        else
        {
            ForwardVelocity = 0;
            MyAnim.SetFloat("Speed", 0f);
            RequestMove = false;
            Runtimer = 0;
            MaxSpeed = StrtMaxspeed;
            MyAnim.speed = 1f;
        }
        #endregion

        #region I see GROUND y'all
        Grounded = Physics2D.OverlapCircle(GroundChecker.position, GroundCheckerRadius, Ground);

        if (Grounded == true && MyCol.sharedMaterial.friction == 0.0f && MyRigidBody.velocity.y <= 0)

        {
            MyAnim.SetBool("Ground", true);
            MyAnim.SetBool("Falling", false);
            MyCol.sharedMaterial.friction = 4.5f;
            TimesKicked = 0;
            DashDirection = 0;
            if (!RequestSlide)
            {
                Instantiate(DustJump, gameObject.transform.position - new Vector3(0f, 0.42f, 0f), Quaternion.identity);
                AudioMan.Play("Land");
            }
        }

        else if (!Grounded && MyCol.sharedMaterial.friction == 4.5f) { MyAnim.SetBool("Ground", false); MyCol.sharedMaterial.friction = 0.0f; }

        #endregion


    }


    void FixedUpdate()
	{
		#region Better Jump Check

		if (MyRigidBody.velocity.y < 0 && CanMove && DashDirection == 0) {
			MyAnim.SetBool ("Falling", true);
			MyRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (FallMultiplier - 1f) * Time.deltaTime;
		}   
		else if (MyRigidBody.velocity.y > 0 && !Input.GetButton ("Jump") && CanMove && DashDirection == 0) {
			MyRigidBody.velocity += Vector2.up * Physics2D.gravity.y * (LowJumpMultiplier - 1f) * Time.deltaTime;
	}  


		#endregion

		#region Jumping
		if (RequestJump && MySceneManager.CutscenePlaying == false)
		{
			if     (!RequestSlide)
			{ MyRigidBody.AddForce (transform.up * 2.3f, ForceMode2D.Impulse);}
			else if (RequestSlide)
			{
			StopCoroutine (Unfreeze()); 
			MyRigidBody.AddForce (transform.up * 2.6f, ForceMode2D.Impulse);
			 
			}
			MyAnim.SetTrigger ("JumpButton");
			RequestJump = false;
		}
		#endregion

		if (CanMove && MySceneManager.CutscenePlaying == false)
		{

		#region Moving
		if (RequestMove)
		{
			MyAnim.SetFloat ("Speed", 2f);

			if 		(transform.localScale == new Vector3 (-1f, 1f, 1f))
			        {MyRigidBody.velocity =  new Vector3 (-ForwardVelocity, MyRigidBody.velocity.y);}

			else if (transform.localScale == new Vector3 (1f, 1f, 1f))
			        {MyRigidBody.velocity =  new Vector3 (ForwardVelocity, MyRigidBody.velocity.y);}
		}  

			else {if (!Grounded)
				{MyRigidBody.velocity = new Vector3 (MyRigidBody.velocity.x * 0.9f, MyRigidBody.velocity.y);}}
		#endregion

		}

		#region Sliding

		if (RequestSlide && !RequestSlideStop && MySceneManager.CutscenePlaying == false)
		{
		if (transform.localScale == new Vector3 (-1f, 1f, 1f))		{MyRigidBody.velocity = new Vector3 (-8f, MyRigidBody.velocity.y);}
		else if (transform.localScale == new Vector3 (1f, 1f, 1f))	{MyRigidBody.velocity = new Vector3 ( 8f, MyRigidBody.velocity.y);}  
		}

		if (RequestSlide && RequestSlideStop){MyRigidBody.velocity = new Vector3 (MyRigidBody.velocity.x * 0.9f, MyRigidBody.velocity.y);}

		else if (!RequestSlide && RequestSlideStop){CanMove = true; MyRigidBody.AddForce (transform.up * 2.6f, ForceMode2D.Impulse); MyAnim.Play ("Take Off"); PunchBox.SetActive(false); }
		#endregion

	}


        #region Exit
    private void OnCollisionExit2D(Collision2D other)
	{
        if (other.gameObject.CompareTag ("Wall")) {Wall = 0;}
	}
    #endregion

        #region Trigger
    void OnTriggerEnter2D (Collider2D other)
		{

		if (other.tag == "KillPlane" && !Dead && MySceneManager.CutscenePlaying == false)
		{
		Instantiate (DeathEffect, other.gameObject.transform.position, Quaternion.identity);
		TheVoid.Death(); 
		Dead = true;
        Cam.ScreenShake(0.5f);
		MySceneManager.DeathTransition = 1;
		StartCoroutine(YoDead());
		}

        if (other.tag == "Bullet" && !Stunned) { Stunned = true; StartCoroutine(Shocked()); }
  
         }
		#endregion

		#region Misc


	public void Bounce(float BounceForce)
	{
        if (DashDirection == 3)
		{
		DivekickBox.SetActive(false);
		MyAnim.SetTrigger ("JumpButton");
		Cam.ScreenShake (0.2f);
		Time.timeScale = 0.1f;
		MyRigidBody.velocity = new Vector3 (MyRigidBody.velocity.x, 0f);
		MyRigidBody.AddForce(transform.up * BounceForce, ForceMode2D.Impulse);
		CanMove = true;
		Time.timeScale = 1f;
		MyRigidBody.gravityScale = 1;
		TimesKicked = 0;
		DashDirection = 0;

		StopAllCoroutines();
		}  
        else if (DashDirection == 10) 
        {
            CanMove = true;
            DivekickBox.SetActive(false);
            MyRigidBody.velocity = new Vector3(MyRigidBody.velocity.x, 0f);
            MyRigidBody.AddForce(transform.up * BounceForce / 2, ForceMode2D.Impulse);
            TimesKicked = 0;
            DashDirection = 0;
            Time.timeScale = 1f;
            MyRigidBody.gravityScale = 1;
            //StopAllCoroutines();
        }

        else if (MyRigidBody.velocity.y < 0f && DashDirection < 10)
		{ 
        MyRigidBody.velocity = new Vector3 (MyRigidBody.velocity.x, 0f);
		MyRigidBody.AddForce(transform.up * BounceForce / 1, ForceMode2D.Impulse);
		MyAnim.SetTrigger ("JumpButton");
		}
	}

		#endregion

	public IEnumerator Unfreeze ()
		{

		#region Punch
		if (DashDirection == 1)
		{
		PunchBox.SetActive (true);
		Cam.ScreenShake (0.1f);
		CoolDown = 0.2f;
		if (transform.localScale == new Vector3 (1f,1f,1f))
		{
		MyRigidBody.AddForce (transform.right * 7f, ForceMode2D.Impulse);
		Instantiate (DashEffect, gameObject.transform.position - new Vector3 (0, 0.2f, 0), Quaternion.identity);
		}

		else if (transform.localScale == new Vector3 (-1f,1f,1f))
		{
		MyRigidBody.AddForce (-transform.right * 7f, ForceMode2D.Impulse);
		Instantiate (DashEffect, gameObject.transform.position - new Vector3 (0, 0.5f, 0), Quaternion.Euler (new Vector3 (0, 0 ,180)));
		}
		if (TimesKicked == 0){AudioMan.Play("Punch1");}
		else if (TimesKicked == 1){AudioMan.Play("Punch2");}
		else if (TimesKicked == 2){AudioMan.Play("Punch3");}
		MyRigidBody.gravityScale = 0;
		TimesKicked++;

		yield return new WaitForSeconds (0.1f);

		MyRigidBody.velocity = new Vector3 (MyRigidBody.velocity.x / 3, 0f);
		CanMove = true;
		DashDirection = 0;

		yield return new WaitForSeconds (0.2f);
		if (DashDirection == 0){MyRigidBody.gravityScale = 1;}
		PunchBox.SetActive (false);
		}
		#endregion

		#region Uppercut
		else if (DashDirection == 2)
		{
		PunchBox.SetActive (true);
		Cam.ScreenShake (0.1f);
		CoolDown = 0.2f; 
		Instantiate (DashEffect, gameObject.transform.position + new Vector3 (0, 0.2f, 0), Quaternion.Euler (new Vector3 (0, 0 ,90)));
		MyRigidBody.AddForce (transform.up * 6.5f, ForceMode2D.Impulse);
		MyRigidBody.gravityScale = 0;
		if (TimesKicked == 0){AudioMan.Play("Punch1");}
		else if (TimesKicked == 1){AudioMan.Play("Punch2");}
		else if (TimesKicked == 2){AudioMan.Play("Punch3");}
		TimesKicked++;

		yield return new WaitForSeconds (0.1f);
		MyRigidBody.velocity = new Vector3 (0f, 0f);
		CanMove = true;
		DashDirection = 0;

		yield return new WaitForSeconds (0.2f);
		if (DashDirection == 0){MyRigidBody.gravityScale = 1;}
		PunchBox.SetActive (false);
		}
		#endregion

		#region Divekick
		if (DashDirection == 3)
		{
        AudioMan.Play("Dive Kick");
        DivekickBox.SetActive(true); 
		CanMove = false;
        MyAnim.SetTrigger("DiveKicktrg");
		MyAnim.Play ("Divekick2", -1, 0f);
		CoolDown = 0.2f;
		MyRigidBody.velocity = new Vector3 (MyRigidBody.velocity.x / 2, 0f);
		MyRigidBody.AddForce (-transform.up * 6.8f, ForceMode2D.Impulse);

		while (!Grounded) {yield return null;}
        MyRigidBody.gravityScale = 1f;
        AudioMan.Play("Dive Kick Land");
		MyRigidBody.velocity = new Vector3 (0f, 0f);
        Instantiate (DustDiveKick, gameObject.transform.position, Quaternion.identity);
		Dead = true;
		Cam.ScreenShake (0.5f); 
		TimesKicked = 0;

		yield return new WaitForSeconds (0.6f);
        DivekickBox.SetActive(false);
        DashDirection = 0;
		CanMove = true;
		
		Dead = false;
		}
		#endregion

		#region Unfreeze Slide
		else if (RequestSlide)
		{
		CanMove = false;
		MyAnim.Play ("Slide");

		for (int i = 0; i < 4; i++)
		{
		if 	(RequestSlideStop && Grounded){yield return new WaitForSeconds (0.1f); MyAnim.Play ("Crouch"); CanMove = true; RequestSlide = false; RequestSlideStop = false; break;}
		else if (!Grounded)
        {
            CanMove = true; 
            RequestSlide = false;
            RequestSlideStop = false;
            PunchBox.SetActive(false);
            break;
        } 

        else if (MySceneManager.CutscenePlaying == true){MyRigidBody.velocity = Vector3.zero; DivekickBox.SetActive(false);}
        yield return new WaitForSeconds (0.1f);
		}

		CanMove = true;
		yield return new WaitForSeconds (0.2f);
		if (Input.GetAxisRaw ("Horizontal") == 0f){MyRigidBody.velocity  = new Vector3 (0f, MyRigidBody.velocity.y);}
		RequestSlide = false;
		RequestSlideStop = false;
        PunchBox.SetActive(false);
        }
		#endregion

		}

 	
	IEnumerator YoDead ()
		{

		#region Deactivate Player
		if (MySceneManager.DeathTransition != 3){AudioMan.Play("Death");}
		TimesKicked = 0;
		DashDirection = 0;
		StarIntr = 0;
		RequestJump = false;
		RequestMove = false;
		RequestSlide = false;
        DivekickBox.SetActive(false);
        PunchBox.SetActive(false);
		MyAnim.SetFloat ("Speed", 0f);
		MyRigidBody.gravityScale = 0f;
		MyRigidBody.velocity = new Vector3 (0f, 0f);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        CanMove = false;
		MySceneManager.CutscenePlaying = true;
        yield return new WaitForSeconds(0.1f);
        MyRigidBody.velocity = Vector3.zero;
		yield return new WaitForSeconds (0.9f);
		#endregion

		#region Wait for it
		if (MySceneManager.DeathTransition != 3)

		{
                transform.position = MySceneManager.CheckPointPos;
            Cam.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, -10f);

		while (MySceneManager.InTheVoid)
		{yield return null;}
		}

		#endregion
	
		#region Bring player back
		MyAnim.SetTrigger ("Respawn");
		AudioMan.Play("Respawn");
		gameObject.GetComponent<SpriteRenderer>().color = new Color (255, 255, 255, 255);
		MyRigidBody.gravityScale = 1;
		yield return new WaitForSeconds (0.99f);
		MySceneManager.CutscenePlaying = false;
		TimesKicked = 0;
		MySceneManager.DeathTransition = 0;
		CanMove = true;
		Dead = false;
		#endregion

		}

    #region Shocked
    public IEnumerator Shocked() 
    {
        Cam.ScreenShake(0.4f);
        MyAnim.Play("Shocked");
        MyRigidBody.gravityScale = 0f;
        MyRigidBody.velocity = new Vector3(0f, 0f);
        CanMove = false;
        yield return new WaitForSeconds(0.5f);
        MyRigidBody.gravityScale = 1;
        CanMove = true;
        MySceneManager.CutscenePlaying = false;
        for (int i = 0; i < 12; i++) 
        { 
            SprRender.color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(0.05f);
            SprRender.color = new Color(255, 255, 255, 255);
            yield return new WaitForSeconds(0.05f);
        }

        TimesKicked = 0;
        DashDirection = 0;
        Stunned = false;
    }
    #endregion

    #region Used a Star
    public void UsedIt ()
		{
			StarIntr = 0;
            WHAT_HAVE_I_DONE.Lives = WHAT_HAVE_I_DONE.Lives + 5; 
			TheVoid.Lives();
			MyAnim.Play("Idle");
            WHAT_HAVE_I_DONE.MaxStars--;
            WHAT_HAVE_I_DONE.StarNum--;
		}
		#endregion

	#region Moving Sounds
		public void RunL_Play()
		{AudioMan.Play("RunL");}

		public void RunR_Play()
		{AudioMan.Play ("RunR");}
		#endregion


}



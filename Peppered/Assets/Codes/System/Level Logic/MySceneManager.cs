using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MySceneManager : MonoBehaviour
{

	#region Stuff
	public static MySceneManager instance = null; 
	public static bool CutscenePlaying;
    public static int Regret = 0;
    public static int Abyss_State = 0;
    public static int Cynthia = 0;
    public static int Merdeka = 0;

    #region Death stuff
    [SerializeField]
    private SaveSystem SS;
    [HideInInspector]
	public static Vector3 CheckPointPos;
    [HideInInspector]
    public float CheckPointX;
    [HideInInspector]
    public float CheckPointY;
    [HideInInspector]
    public float CheckPointZ;
	[HideInInspector]
	public static string CheckPointLvlName;
	private Scene CurrentScene;
	[HideInInspector]
	public static int DeadState;
    #endregion

    #region Music
    public float BGVol;
    public int CurrentSongNum;
    public AudioSource Music;
    public AudioSource Void;
    public AudioClip[] Track;
    private int NewTrack;
    [HideInInspector]
    public int RespawnLocationTrack;
    private float CBHVol;
    #endregion

    #region Function
    public static bool Act;
    [HideInInspector]
    public static int EnterNum;
    private string EnterAnim;
    private GameObject[] LevelExits;
    private Animator LevelLoaderAnim;
    private Text Inevitability;
    private PLAYER_CONTROLS Player;
	private float Cooldown;
	private bool ClickedUp;
    #endregion

    private float Musicvol; 

    #endregion

    #region Tech Stuff
    private void OnEnable()
    {
        //The very first Scene Manager
        if (instance == null)
        {
            instance = this; DontDestroyOnLoad(gameObject);
            Music.clip = Track[CurrentSongNum];
            Music.volume = BGVol;
            Music.Play();

        }
        else if (instance != null) { Destroy(gameObject); return; }
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode sceneMode)
    {
        //Find player and transition stuff after level loaded
        Player = FindObjectOfType<PLAYER_CONTROLS>();
        LevelLoaderAnim = GameObject.Find("Transition").GetComponent<Animator>();
        Inevitability = GameObject.Find("Inevitability").GetComponent<Text>();
   

        //Simple transition
        if (DeadState != 3)
        {
            //Search for all exits in the level
            LevelExits = GameObject.FindGameObjectsWithTag("Exit");
            CutscenePlaying = false;

            if (Player != null)
            {
                //Find the right one
                for (int i = 0; i < LevelExits.Length; i++)
                {
                    if (LevelExits[i].GetComponent<LevelLoader>().EntranceNumber == EnterNum)
                    {
                        Player.gameObject.transform.position = LevelExits[i].transform.position;
                        if (LevelExits[i].transform.localScale.x == 1) { Player.gameObject.transform.localScale = new Vector3(1, 1, 1); }
                        else if (LevelExits[i].transform.localScale.x == -1) { Player.gameObject.transform.localScale = new Vector3(-1, 1, 1); }
                    }
                }

                Player.CanMove = true;
            }
            LevelLoaderAnim.Play(EnterAnim);

            //Switch track
            if (CurrentSongNum != NewTrack)
            {
                Music.clip = Track[NewTrack];
                CurrentSongNum = NewTrack;
                StartCoroutine(FadeIn());
            }
         
        }

        //Trigger death transition
        else if (DeadState == 3)
        {
            LevelLoaderAnim.Play("Death_Start");
            Player.gameObject.SetActive(false);
            Player.transform.position = CheckPointPos;
            

            //Should I wait?
            LevelLoaderAnim.Play("Death_End");
            Void.Pause();
            Player.gameObject.SetActive(true);
            Player.MyAnim.SetTrigger("Respawn");


            //Song is the same
            if (CurrentSongNum == RespawnLocationTrack)
            {
                Music.volume = CBHVol;
                Music.Play();
            }

            //Switch track
            else if (CurrentSongNum != RespawnLocationTrack)
            {
                Music.Stop();
                CurrentSongNum = RespawnLocationTrack;
                Music.clip = Track[RespawnLocationTrack];
                Music.volume = CBHVol;
                Music.Play();
            }


            DeadState = 0;
        }

        //Jumpcut
        else if (DeadState == 42)
        {
            DeadState = 0;
            LevelLoaderAnim.Play("Death_End");
        }

    }

    void Start ()
	{
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        //Get rid of that cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        
        if (Player == null) { Player = FindObjectOfType<PLAYER_CONTROLS>(); }
        if (LevelLoaderAnim == null) { LevelLoaderAnim = GameObject.Find("Transition").GetComponent<Animator>(); }
        if (Inevitability == null) { Inevitability = GameObject.Find("Inevitability").GetComponent<Text>(); }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }
    #endregion

    void Update()
	{
        //DS 1-2 = Died and I should respawn
        //DS 3 = Died and I should respawn on different level
        //DS 5-6 = I came across a checkpoint

      //  if (Input.GetKeyDown(KeyCode.M) && Music.volume != 0)
      //  {
      //      Musicvol = Music.volume;
      //      Music.volume = 0;
      //  } else if (Input.GetKeyDown(KeyCode.M) && Music.volume == 0)
      //  {
      //      Music.volume = Musicvol;
      //  }

        if (DeadState == 1) { DeadState = 2; StartCoroutine(Died()); }

        if (DeadState == 5) { DeadState = 6; Checkpoint(); }

        if (Player != null)
        {
            //Check if you pressed up and ONLY up
            if (Input.GetAxisRaw ("Vertical") > 0f && Input.GetAxisRaw ("Horizontal") == 0f)
            {
                if (!ClickedUp && Player.Grounded && CutscenePlaying == false && Player.CanMove)
                {
                    ClickedUp = true; Act = true; Cooldown = 0.01f;
                }
            }

            //Reset the ability to press act
            if (Input.GetAxisRaw("Vertical") == 0) { ClickedUp = false; }
            if (Cooldown < 0) { Cooldown = 0f; Act = false; }
            if (Cooldown > 0) { Cooldown -= Time.deltaTime; }
        }

        else if (Player == null)
        {
            //Check if you pressed up and ONLY up
            if (Input.GetAxisRaw("Vertical") > 0f && Input.GetAxisRaw("Horizontal") == 0f)
            {
                if (!ClickedUp && CutscenePlaying == false)
                {
                    ClickedUp = true; Act = true; Cooldown = 0.01f;
                }
            }

            //Reset the ability to press act
            if (Input.GetAxisRaw("Vertical") == 0) { ClickedUp = false; }
            if (Cooldown < 0) { Cooldown = 0f; Act = false; }
            if (Cooldown > 0) { Cooldown -= Time.deltaTime; }

        }
	}

	#region Checkpoint
	public void Checkpoint ()
	{
	    CurrentScene = SceneManager.GetActiveScene();

        if (CheckPointLvlName != CurrentScene.name)
        {
            CheckPointLvlName = CurrentScene.name;
            RespawnLocationTrack = CurrentSongNum;
            //BGVol = Music.volume;
            CBHVol = Music.volume;
            CheckPointX = CheckPointPos.x;
            CheckPointY = CheckPointPos.y;
            CheckPointZ = CheckPointPos.z;
            SS.Save();
        }
        DeadState = 0;
    }
	#endregion

	#region Death
    IEnumerator Died ()
    {
        CurrentScene = SceneManager.GetActiveScene();
        Music.Pause();
        Void.Play();

        //Respawn on different level
        if (CheckPointLvlName != CurrentScene.name && CheckPointLvlName != null)
        {   
            DeadState = 3;
            LevelLoaderAnim.Play("Death_Start");

            if (WHAT_HAVE_I_DONE.Immortality == 0)
            {
                #region How close is it?
                HowCloseIsIt();
                Inevitability.text = WHAT_HAVE_I_DONE.Lives.ToString();
                yield return new WaitForSeconds(1f);
                WHAT_HAVE_I_DONE.Lives--;
                HowCloseIsIt();
                Inevitability.text = WHAT_HAVE_I_DONE.Lives.ToString();
                #endregion
            }
            else { Inevitability.text = (""); }
            yield return new WaitForSeconds(1.5f);

            Inevitability.text = ("");
            SceneManager.LoadScene(CheckPointLvlName);
        }
        //Respawn on this level
        else if (CheckPointLvlName == CurrentScene.name)
        {
                LevelLoaderAnim.Play("Death_Start");
                Player.transform.position = CheckPointPos;
     

                if (WHAT_HAVE_I_DONE.Immortality == 0)
                {
                   #region How close is it?
                HowCloseIsIt();
                Inevitability.text = WHAT_HAVE_I_DONE.Lives.ToString();
                yield return new WaitForSeconds(1f);
                WHAT_HAVE_I_DONE.Lives--;
                HowCloseIsIt();
                Inevitability.text = WHAT_HAVE_I_DONE.Lives.ToString();
                #endregion
                }
                else { Inevitability.text = (""); }


                Player.CanMove = false;
                yield return new WaitForSeconds(1.5f);

                
                LevelLoaderAnim.Play("Death_End");
                Inevitability.text = ("");
                Player.MyAnim.SetTrigger("Respawn");
                DeadState = 0;
                Void.Pause();
                Music.Play();
        }

        #region NO CHECKPOINT TEST LEVEL
        else if (CheckPointLvlName == null)
        {
            LevelLoaderAnim.Play("Death_Start");
            Player.transform.position = new Vector3 (0,0,0);
            if (WHAT_HAVE_I_DONE.Immortality == 0)
            {
                #region How close is it?
                HowCloseIsIt();
                Inevitability.text = WHAT_HAVE_I_DONE.Lives.ToString();
                yield return new WaitForSeconds(1f);
                WHAT_HAVE_I_DONE.Lives--;
                HowCloseIsIt();
                Inevitability.text = WHAT_HAVE_I_DONE.Lives.ToString();
                #endregion
            }

            else { Inevitability.text = (""); }

            yield return new WaitForSeconds(1.5f);
            LevelLoaderAnim.Play("Death_End");
            Inevitability.text = ("");
            Player.gameObject.SetActive(true);
            Player.MyAnim.SetTrigger("Respawn");
            DeadState = 0;
            Void.Pause();
            Music.Play();
        }
        #endregion
    }

    void HowCloseIsIt()
    {
        if (WHAT_HAVE_I_DONE.Lives > 10) { Inevitability.fontSize = 50; }
        else
        {
            if (WHAT_HAVE_I_DONE.Lives == 10) { Inevitability.fontSize = 60; }
            else if (WHAT_HAVE_I_DONE.Lives == 9) { Inevitability.fontSize = 70; }
            else if (WHAT_HAVE_I_DONE.Lives == 8) { Inevitability.fontSize = 80; }
            else if (WHAT_HAVE_I_DONE.Lives == 7) { Inevitability.fontSize = 90; }
            else if (WHAT_HAVE_I_DONE.Lives == 6) { Inevitability.fontSize = 110; }
            else if (WHAT_HAVE_I_DONE.Lives == 5) { Inevitability.fontSize = 130; }
            else if (WHAT_HAVE_I_DONE.Lives == 4) { Inevitability.fontSize = 150; }
            else if (WHAT_HAVE_I_DONE.Lives == 3) { Inevitability.fontSize = 200; }
            else if (WHAT_HAVE_I_DONE.Lives == 2) { Inevitability.fontSize = 250; }
            else if (WHAT_HAVE_I_DONE.Lives == 1) { Inevitability.fontSize = 300; }
        }
    }
    #endregion

    #region Normal Transition
    public void NormalSceneTransition(int EntranceNumber, string ExitAnim, string EnterAnimation, float Time, string LevelName, int NewSong, float Volume)
    {
        CutscenePlaying = true;
        NewTrack = NewSong;
        BGVol = Volume;

        //What's the exit number?
        EnterNum = EntranceNumber;

        //What's the entrance animation?
        EnterAnim = EnterAnimation;

        //What animation should I play?
        LevelLoaderAnim.Play(ExitAnim);

        //Wait
        DeadState = 0;
        StartCoroutine(Load(Time, LevelName));
    }

    public void JumpCut(string LevelName)
    {
        DeadState = 42;
        SceneManager.LoadScene(LevelName);
    }

    public void LoadJumpCut()
    {
        RespawnLocationTrack = CurrentSongNum;
        CurrentSongNum = -100000;
        CBHVol = BGVol;
        DeadState = 3;
        SceneManager.LoadScene(CheckPointLvlName);
    }

    IEnumerator Load(float TrnTime, string LvlName)
    {
        //Change Song
        if (CurrentSongNum != NewTrack)
        {
            while (Music.volume != 0)
            {
                Music.volume = Music.volume - 0.03f;
                yield return new WaitForSeconds(0.01f);
            }

            Music.Stop();
        }
       
        //How long do I wait?
        yield return new WaitForSeconds(TrnTime);

        //LoadLevel
        SceneManager.LoadScene(LvlName);
    }
    #endregion

    #region Music
    public IEnumerator FadeIn()
    {
        Music.Play();
        while (Music.volume < BGVol)
        {
            Music.volume = Music.volume + 0.05f;
            yield return new WaitForSeconds(0.01f);
        }

        Music.volume = BGVol;
    }

    private IEnumerator FadeOut(float FadeSpeed, bool Pause)
    {


        while (Music.volume != 0)
        {
            Music.volume = Music.volume - FadeSpeed;
            yield return new WaitForSeconds(0.01f);
        }

        if (Pause) { Music.Pause(); }
        else { Music.Stop(); }
    }


    public void MFadeOut(float FadeSpeed, bool Pause) { StartCoroutine(FadeOut(FadeSpeed, Pause)); }
    public void MChange (int MNum, float vol)
    {
        CurrentSongNum = MNum;
        BGVol = vol;
        Music.clip = Track[CurrentSongNum];
        Music.volume = BGVol;
    }

    public void MPause() { Music.Pause();}
    public void MStop() { Music.Stop(); }
    public void MPlay() { Music.volume = BGVol; Music.Play(); }
    #endregion

}
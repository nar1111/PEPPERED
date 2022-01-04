using UnityEngine;
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.UI;
public class Office_1 : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]private Text[] Description;
    [SerializeField]private SpriteRenderer SlideRen;
    [SerializeField]private Sprite[] Slides;
    public PlayableDirector[] Cutscene;
    public GameObject[] Actors;
    [SerializeField]
    private Rigidbody2D[] Rigids;
    public AudioSource[] SFX;
    public GameObject[] ThingsToOff;
    private float StopWatch = 0;
    private bool Done = false;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private CHAIR_PLAYER Player;
    [SerializeField]
    private Text AdditionalText;
    [SerializeField]
    private DialogueManager DLMan;
    [SerializeField]
    private MySceneManager SceneMan;
    [SerializeField]
    private Button_Choose BChose;
    [SerializeField]
    private Black_lines BLines;
    public ParticleSystem Rain;
    [SerializeField]
    private AUDIOMANAGER AudioMan;

    #region Private DL Stuff
    [HideInInspector]
    public string[] Line;
    private bool[] IsThereAface;
    private float[] Speed;
    private AudioSource[] Voice;
    private AudioSource[] Noise;
    private Animator[] TalkAnim;
    private int LineNum;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        MySceneManager.CutscenePlaying = true;
        MySceneManager.Regret = -3;
        MySceneManager.Abyss_State = 0;
        Player.CanMove = false;
        Done = false;
        if (MySceneManager.Merdeka == 0)
        {
            StartCoroutine(PauseIt());
        } else { StartCoroutine(PauseItToo()); }
    }

    private void Update()
    {
        //Cynthia Look at you
        if (MySceneManager.Regret == -4)
        {
            if (Player.gameObject.transform.position.x > Actors[1].transform.position.x)
            { Actors[1].transform.localScale = new Vector3(1,1,1); }
            else if (Player.gameObject.transform.position.x < Actors[1].transform.position.x)
            { Actors[1].transform.localScale = new Vector3(-1, 1, 1); }
        }

        if (MySceneManager.Regret == -3)
        { StopWatch += Time.deltaTime; }

        //Why that's not coffee at all!
        if (MySceneManager.Abyss_State == 1 && Player.gameObject.transform.position.x > -4.3f && Player.gameObject.transform.position.x < 14.6f && DLMan.Playing == false && Player.CanMove && MySceneManager.CutscenePlaying == false)
        {
            StopWatch += Time.deltaTime;
            if (StopWatch > 10f)
            {
                MySceneManager.Abyss_State = 2;
                MySceneManager.Cynthia = 2;
                CharAnim[5].Play("Coffee Cup Full");
                AudioMan.Play("Ting");
            }
        }

        if (SceneMan == null) { SceneMan = FindObjectOfType<MySceneManager>(); }
    }

    public void Explosion()
    {
        Player.MyRigidBody.velocity = Vector3.zero;
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        ThingsToOff[2].SetActive(true);
        ThingsToOff[1].SetActive(false);
        StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        //First line
        SceneMan.MFadeOut(0.005f, true);
        MySceneManager.Regret = 0;

        //Oops
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Guarding Life Stars is a task of utmost importance.";
        Line[1] = "- So we demand a high level of\nprofessionalism from every employee.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }


        //Dude on fire
        SceneMan.MChange(3, 0.3f);
        SceneMan.MPlay();
        Done = false;
        Cutscene[2].Play();
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.01f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "- THEODORE IS STILL MISSING!\nWE'RE ALL GONNA DIE!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion  
        while (Done == false) { yield return null; }
        Done = false;
        while (DLMan.Playing) { yield return null; }


        //Second line
        SceneMan.MStop();
        Actors[1].transform.localScale = new Vector3(1, 1, 1);
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- The position of ‘Coffee boy’ is absolutely VITAL.";
        Line[1] = "- And therefore requires at least 30 years of experience as well as a highly ambitious mentality.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }


        //Boom and ending
        Cutscene[3].Play();
        while (Done == false) { yield return null; }
        Done = false;
        CharAnim[1].Play("Shocked_Close");


        ThingsToOff[5].SetActive(false);

        //lines
        ThingsToOff[12].SetActive(true);
        BLines.Show(180f, 0.2f);
        ThingsToOff[16].SetActive(true);

        Description[0].text = "We must do something!";
        Description[1].text = "Not bringing this up\nuntil you do.";
        DLMan.LeftString = "[IMPLORE]";
        DLMan.RightString = "[DISMISS]";
        DLMan.ChoiceStarter();
        while (BChose.ButtonChoice == 0) { yield return null; }
        BLines.Hide(0.4f);
        MySceneManager.Abyss_State = 3;
        ThingsToOff[3].SetActive(false);
        ThingsToOff[20].SetActive(false);
        CharAnim[6].Play("TV_Broken");

        #region First Dialogue Route
        //FIRST DIALOGUE ROUTE
        if (BChose.ButtonChoice == 1)
        {

            //Final Line
            CharAnim[1].Play("Idle");
            LineNum = 3;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[1];
                Speed[i] = 0.04f;
            }
            #endregion
            Line[0] = "- Being Mr.Glagolev isn't in my job description.";
            Line[1] = "- And it won't be in yours either.";
            Line[2] = "- We’ve prepared the star for him on the balcony.\nThat’s as much as we can do.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            if (DLMan.Playing == false) { DLMan.DIStarter(); }
            #endregion
            while (DLMan.Playing) { if (DLMan.Currentline == 2 && ThingsToOff[6].activeInHierarchy == false) { ThingsToOff[6].SetActive(true); } yield return null; }

        }
        #endregion

        #region Second dialogue route
        //SECOND DIALOGUE ROUTE
        else if (BChose.ButtonChoice == 2)
        {
            CharAnim[1].Play("Idle");
            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[1];
                Speed[i] = 0.03f;
            }
            #endregion
            Line[0] = "- Fabulous. I won't.";
            Line[1] = "- Here. Take this coffee cup.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            if (DLMan.Playing == false) { DLMan.DIStarter(); }
            #endregion
            while (DLMan.Playing) { if (DLMan.Currentline == 1 && ThingsToOff[6].activeInHierarchy == false) { ThingsToOff[6].SetActive(true); } yield return null; }
        }
        #endregion


        //Go back to gameplay
        Player.CanMove = true;
        MySceneManager.CutscenePlaying = false;
        MySceneManager.Regret = -4;
        ThingsToOff[1].SetActive(true);
        ThingsToOff[2].SetActive(false);
        ThingsToOff[4].SetActive(true);
        ThingsToOff[12].SetActive(false);
    }

    private IEnumerator PauseIt()
    {
        Cutscene[1].Play();
        Player.CanMove = false;
        while (Done == false) { yield return null; }
        Done = false;
        SFX[2].Play();

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Ladies and gentlemen!\nHas everyone got a good view?";
        Line[1] = "- Because it's the moment\nwe've all been waiting for!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        yield return new WaitForSeconds(1f);
        Actors[5].SetActive(true);
        AudioMan.Play("Dramatic Impact");
        yield return new WaitForSeconds(1f);

        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- It's the 100th anniversary Immortality day.";
        Line[1] = "- Once again, our hero will bring a new Life Star.";
        Line[2] = "— And with it, the extended imprisonment...";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        while (Done == false) { yield return null; }
        Done = false;

        Actors[5].SetActive(false);
        ThingsToOff[21].SetActive(true);
        AudioMan.Play("Dramatic Impact");
        LineNum = 1;
        DeclareLines();

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Of the God of Death.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        yield return new WaitForSeconds(1f);
        Cutscene[10].Play();
        yield return new WaitForSeconds(1.5f);
        SlideRen.sprite = Slides[1];

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Please welcome the hero of the evening";
        Line[1] = "— and every other waking moment.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        while (SFX[2].volume > 0)
        {
            SFX[2].volume -= 0.006f;
            yield return new WaitForSeconds(0.01f);
        }


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Theodore Glagolev!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        AudioMan.Play("Victory Real");
        yield return new WaitForSeconds(3f);
        SlideRen.sprite = Slides[2];
        AudioMan.Play("Dramatic Impact");
        yield return new WaitForSeconds(2f);

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "[silence]";
        Line[1] = "- THEODORE GLAGOLEV!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        AudioMan.Play("Victory Real");
        yield return new WaitForSeconds(3.5f);


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Theodore..?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        yield return new WaitForSeconds(2f);


        Done = false;
        Cutscene[11].Play();
        while (Done == false) { yield return null; }
        Done = false;

        //GAME
        AudioMan.Play("Ting");
       
        Done = false;
        Cutscene[0].Play();
        SFX[6].Play();
        while (Done == false) { yield return null; }
        Done = false;

        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[8];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Disaster on Immortality Day:";
        Line[1] = "- Theodore Glagolev vanishes into thin\nair an hour away from catastrophe.";
        Line[2] = "- WHERE did he go?";
        Line[3] = "- And WHO will prevent\nthe return of the God of Death now?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        SFX[6].Stop();
        SFX[7].Play();
        yield return new WaitForSeconds(1.5f);
        ThingsToOff[22].SetActive(false);
        yield return new WaitForSeconds(3f);


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Excuse me. Are you ready\nfor your job interview?";
        Line[1] = "- Follow me.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        BChose.ButtonChoice = 0;
        Actors[0].transform.localScale = new Vector3(1, 1, 1);

        //Cynthia leaves
        Rigids[0].velocity = new Vector2(2f, 0f);
        while (Rigids[0].transform.position.x < -4f) { yield return null; }
        Actors[0].SetActive(false);
        MySceneManager.CutscenePlaying = false;
        ThingsToOff[0].SetActive(true);
        MySceneManager.Regret = 2;
        SceneMan.MStop();
        SceneMan.MChange(4, 0.3f);
        SceneMan.MPlay();
        Player.CanMove = true;
        ThingsToOff[24].SetActive(false);
    }

    private IEnumerator PauseItToo()
    {
        yield return new WaitForSeconds(1f);
        ThingsToOff[22].SetActive(false);
        yield return new WaitForSeconds(3f);


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "— Excuse me? Could you stop watching the end of the world stream?";
        Line[1] = "- It is time for your job interview test.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        BChose.ButtonChoice = 0;
        Actors[0].transform.localScale = new Vector3(1, 1, 1);

        //Cynthia leaves
        Rigids[0].velocity = new Vector2(2f, 0f);
        while (Rigids[0].transform.position.x < -4f) { yield return null; }
        Actors[0].SetActive(false);
        MySceneManager.CutscenePlaying = false;
        ThingsToOff[0].SetActive(true);
        MySceneManager.Regret = 2;
        SceneMan.MStop();
        SceneMan.MChange(4, 0.3f);
        SceneMan.MPlay();
        Player.CanMove = true;
        ThingsToOff[24].SetActive(false);

    }

    public void TakeCoffee()
    {
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        Player.gameObject.transform.localScale = new Vector3(1,1,1);
        StartCoroutine(YouTakeThatCoffeeRightNowMisterOrMissOrWhateverYouAre());
        MySceneManager.Cynthia = 1;
    }

    private IEnumerator YouTakeThatCoffeeRightNowMisterOrMissOrWhateverYouAre()
    {
        ThingsToOff[6].SetActive(false);
        ThingsToOff[4].SetActive(false);
        ThingsToOff[8].SetActive(true);
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Fabulous. Now put it in the coffee machine.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false){DLMan.DIStarter();}
        #endregion
        while (DLMan.Playing){ yield return null; }

        ThingsToOff[7].SetActive(true); 
        Player.CanMove = true;
        MySceneManager.CutscenePlaying = false;
        ThingsToOff[10].SetActive(true);

        //I took it!
        StopWatch = 0;
        MySceneManager.Abyss_State = 1;
    }

    public void FineHappy()
    {
            Player.CanMove = false;
            MySceneManager.CutscenePlaying = true;
            MySceneManager.Regret = -1;

            if (MySceneManager.Abyss_State != 2)
            {
                MySceneManager.Abyss_State = 0;
            }

            StartCoroutine(FineHereHappy());
    }

    private IEnumerator FineHereHappy()
    {
        //lines
        ThingsToOff[1].SetActive(false);
        ThingsToOff[14].SetActive(true);
        ThingsToOff[12].SetActive(true);

        SceneMan.MStop();
        SceneMan.MChange(2, 1f);

        Player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        Done = false;
        Cutscene[5].Play();
        while (Done == false) { yield return null; }
        Done = false;
        Player.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        ThingsToOff[8].SetActive(false);
        CharAnim[2].Play("CoffeeIdle");
        MySceneManager.Cynthia = 0;

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.03f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- We are ready to begin.";
        Line[1] = "- Oh and just a heads up.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        //Uh-oh.
        CharAnim[1].Play("Angry");
        ThingsToOff[14].SetActive(false);
        BLines.Show(280f, 0f);
        ThingsToOff[17].SetActive(true);
        AudioMan.Play("Dramatic Impact");
        ThingsToOff[13].SetActive(false);


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- You have ONE-SHOT at this.";
        Line[1] = "- Either you pass this test flawlessly. Or you lose this opportunity forever.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        //...Okay...
        ThingsToOff[13].SetActive(true);
        CharAnim[1].Play("Idle");
        ThingsToOff[17].SetActive(false);
        ThingsToOff[14].SetActive(true);
        BLines.Show(160f, 0f);


        yield return new WaitForSeconds(1f);
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.03f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Now.";
        Line[1] = "- SHOW ME WHAT YOU’VE GOT";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { if (DLMan.Currentline == 1 && MySceneManager.Regret != 8) { MySceneManager.Regret = 8; CharAnim[1].Play("Angry"); } yield return null; }
        MySceneManager.Regret = -1;


        //START
        //Cutscene[8].Play();
        ThingsToOff[11].SetActive(true);
        ThingsToOff[7].SetActive(false);
        ThingsToOff[9].SetActive(true);
        ThingsToOff[13].SetActive(false);
        SceneMan.MPlay();
        BLines.Hide(0.2f);

        while (MySceneManager.Regret == -1) { yield return null; }
        ThingsToOff[11].SetActive(false);
        SceneMan.MStop();


        if (MySceneManager.Regret == -3)
        {
            Actors[4].SetActive(true);
            Rain.emissionRate = 0;
            Done = false;
            Cutscene[6].Play();
            while (Done == false) { yield return null; }
            Done = false;
            CharAnim[1].Play("Shocked");
            yield return new WaitForSeconds(1f);


            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.06f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[1];
            }
            #endregion
            Line[0] = "- I... I can't...";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
        }
        else if (MySceneManager.Regret == -4)
        {
            CharAnim[1].Play("Idle");
            yield return new WaitForSeconds(1f);

            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.06f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[1];
            }
            #endregion
            Line[0] = "- Are... Are you serious?";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
        }


        //Boss room
        CharAnim[3].Play("Death_Start");
        AudioMan.Play("Dramatic Impact");
        yield return new WaitForSeconds(0.1f);
        ThingsToOff[8].SetActive(true);
        ThingsToOff[8].transform.parent = null;
        ThingsToOff[8].transform.position = new Vector3(22.594f, 0.215f, 0f);
        Player.gameObject.transform.position = new Vector3(20, 1, 0);
        ThingsToOff[14].SetActive(false);
        ThingsToOff[15].SetActive(true);
        ThingsToOff[25].SetActive(true);
        ThingsToOff[26].SetActive(false);
        yield return new WaitForSeconds(2.5f);
        Cutscene[7].Play();
        if (MySceneManager.Abyss_State == 2) { CharAnim[5].Play("Coffee Cup Full"); }
        AudioMan.Play("Come Back");
        CharAnim[3].Play("Death_End");
        SceneMan.MStop();
        SceneMan.MChange(1, 0.5f);
        SceneMan.MPlay();


        #region Boss reaction to coffee
        //If you made coffee
        if (MySceneManager.Regret == -3)
        {
            if (MySceneManager.Abyss_State == 2) { LineNum = 5; }
            else if (MySceneManager.Abyss_State != 2) { LineNum = 4; }
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.05f;
                Voice[i] = SFX[4];
                TalkAnim[i] = CharAnim[4];
            }
            #endregion
            Line[0] = "- You have surpassed my expectations.";
            Line[1] = "- We are but a humble storage company\nand yet you still wish to join us.";
            Line[2] = "- It seems that we truly share similar values.";
            if (MySceneManager.Abyss_State == 2) { Line[3] = "- Like courage. Honour. Loyalty."; Line[4] = "- And cappuccino that is 90% water from the fire extinguisher system."; }
            else if (MySceneManager.Abyss_State != 2){ Line[3] = "- Like courage. Honour. And loyalty."; }
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
         

            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.05f;
                Voice[i] = SFX[4];
                TalkAnim[i] = CharAnim[4];
            }
            #endregion
            Line[0] = "- This can only mean one thing.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (Done == false) { yield return null; }
            Done = false;
            while (DLMan.Playing) { yield return null; }
            Actors[3].transform.localScale = new Vector3(-1, 1, 1);

            yield return new WaitForSeconds(0.1f);
            SceneMan.MStop();

            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.05f;
                Voice[i] = SFX[4];
                TalkAnim[i] = CharAnim[4];
            }
            #endregion
            Line[0] = "- Cynthia, you're fired.\nWe’ve found a replacement.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
        }

        //If you didn't
        else if (MySceneManager.Regret == -4)
        {

            //First
            if (MySceneManager.Abyss_State != 2)
            {
                LineNum = 2;
                DeclareLines();
                #region Put things in
                for (int i = 0; i < IsThereAface.Length; i++)
                {
                    IsThereAface[i] = false;
                    Speed[i] = 0.09f;
                    Voice[i] = SFX[4];
                    TalkAnim[i] = CharAnim[4];
                }
                #endregion
                Line[0] = "- I am going to speak my mind, if I may.";
                Line[1] = "- Your deliberate and utter failure of our test...";
                #region Go
                DLMan.DILines = Line;
                DLMan.DITextSpeed = Speed;
                DLMan.DIVoice = Voice;
                DLMan.Noise = Noise;
                DLMan.WhoTalks = TalkAnim;
                DLMan.DIStarter();
                #endregion
                while (DLMan.Playing) { yield return null; }
            }
            else if (MySceneManager.Abyss_State == 2)
            {
                LineNum = 4;
                DeclareLines();
                #region Put things in
                for (int i = 0; i < IsThereAface.Length; i++)
                {
                    IsThereAface[i] = false;
                    Speed[i] = 0.09f;
                    Voice[i] = SFX[4];
                    TalkAnim[i] = CharAnim[4];
                }
                #endregion
                Line[0] = "- I am going to speak my mind, if I may.";
                Line[1] = "- Your task was pretty clear.";
                Line[2] = "- Make me a cappuccino.";
                Line[3] = "- Instead you brought me water from the fire extinguisher system.";
                #region Go
                DLMan.DILines = Line;
                DLMan.DITextSpeed = Speed;
                DLMan.DIVoice = Voice;
                DLMan.Noise = Noise;
                DLMan.WhoTalks = TalkAnim;
                DLMan.DIStarter();
                #endregion
                while (DLMan.Playing) { yield return null; }
            }

            while (Done == false) { yield return null; }
            Done = false;
            Actors[3].transform.localScale = new Vector3(-1, 1, 1);


            //Second
            if (MySceneManager.Abyss_State != 2)
            {
                SceneMan.MStop();
                LineNum = 3;
                DeclareLines();
                #region Put things in
                for (int i = 0; i < IsThereAface.Length; i++)
                {
                    IsThereAface[i] = false;
                    Speed[i] = 0.03f;
                    Voice[i] = SFX[4];
                    TalkAnim[i] = CharAnim[4];
                }
                #endregion
                Speed[0] = 0.03f;
                Speed[1] = 0.04f;
                Speed[2] = 0.06f;
                Line[0] = "- Was very inspiring!";
                Line[1] = "- Such creative problem solving skills are EXACTLY what we welcome here!";
                Line[2] = "- Ah, and speaking of things that AREN'T welcome.";
                #region Go
                DLMan.DILines = Line;
                DLMan.DITextSpeed = Speed;
                DLMan.DIVoice = Voice;
                DLMan.Noise = Noise;
                DLMan.WhoTalks = TalkAnim;
                DLMan.DIStarter();
                #endregion
                CharAnim[1].Play("Shocked");
                while (DLMan.Playing) { yield return null; }
            }

            else if (MySceneManager.Abyss_State == 2)
            {
                SceneMan.MStop();
                LineNum = 3;
                DeclareLines();
                #region Put things in
                for (int i = 0; i < IsThereAface.Length; i++)
                {
                    IsThereAface[i] = false;
                    Speed[i] = 0.03f;
                    Voice[i] = SFX[4];
                    TalkAnim[i] = CharAnim[4];
                }
                #endregion
                Speed[0] = 0.04f;
                Speed[1] = 0.04f;
                Speed[2] = 0.06f;
                Line[0] = "- You are clearly saying that I should cut down on caffeine.";
                Line[1] = "- And I agree! I admire your courage, Godsdamn it!";
                Line[2] = "- Ah, speaking of things I don't admire.";
                #region Go
                DLMan.DILines = Line;
                DLMan.DITextSpeed = Speed;
                DLMan.DIVoice = Voice;
                DLMan.Noise = Noise;
                DLMan.WhoTalks = TalkAnim;
                DLMan.DIStarter();
                #endregion
                CharAnim[1].Play("Shocked");
                while (DLMan.Playing) { yield return null; }
                MySceneManager.Abyss_State = 3;
            }

            
            Actors[3].transform.localScale = new Vector3(1, 1, 1);
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.05f;
                Voice[i] = SFX[4];
                TalkAnim[i] = CharAnim[4];
            }
            #endregion
            Line[0] = "- Cynthia, you're fired.\nWe’ve found a replacement.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
        }
        #endregion


        Cutscene[4].Play();
        yield return new WaitForSeconds(1.5f);
        SceneMan.MStop();

        Actors[3].transform.localScale = new Vector3(-1, 1, 1);
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.05f;
            Voice[i] = SFX[4];
            TalkAnim[i] = CharAnim[4];
            Noise[i] = null;
        }
        #endregion
        Line[0] = "- As for you, my faithful cohort.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        AudioMan.Play("Victory");
        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.05f;
            Voice[i] = SFX[4];
            TalkAnim[i] = CharAnim[4];
            Noise[i] = null;
        }
        #endregion
        Line[0] = "- I hereby bestow unto you, the power of a steady income!";
        Line[1] = "- And fear not the God of Death, my warrior.";
        Line[2] = "- His return shan’t affect us or the company at all!";
        Line[3] = "- At a-a-all.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        CharAnim[3].Play("Death_Start");
        AdditionalText.text = "5 YEARS LATER";
        SFX[5].Play();
        yield return new WaitForSeconds(5f);
        SceneMan.NormalSceneTransition(0, "Death_Start", "Death_End", 0f,"Office_2", 0, 0);
    }

    void DeclareLines()
    {
        Line = new string[LineNum];
        IsThereAface = new bool[LineNum];
        Speed = new float[LineNum];
        TalkAnim = new Animator[LineNum];
        Voice = new AudioSource[LineNum];
        Noise = new AudioSource[LineNum];
    }

    public void CutsceneDone() { Done = true; }
}
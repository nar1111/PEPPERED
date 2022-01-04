using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using Cinemachine;
public class Office_2 : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private CinemachineVirtualCamera Cam2;
    private CinemachineBasicMultiChannelPerlin CamNoise;
    [SerializeField]
    private Sprite[] PepsSpr;
    [SerializeField]
    private SpriteRenderer PepsRern;
    [SerializeField]
    private PlayableDirector[] Cutscene;
    [SerializeField]
    private GameObject[] Actors;
    [SerializeField]
    private AudioSource[] SFX;
    [SerializeField]
    private GameObject[] ThingsToOff;
    [SerializeField]
    private Simon_Says Simon;
    [SerializeField]
    private YourBriliantPitch Strategy;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private Sprite[] FaceSprites;
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
    [SerializeField]
    private AUDIOMANAGER AudioMan;
    private bool Event = true;
    private int StartingReg;
    private bool Done = false;

    #region Private DL Stuff
    [HideInInspector]
    public string[] Line;
    private bool[] IsThereAface;
    private Sprite[] Face;
    private float[] Speed;
    private AudioSource[] Voice;
    private AudioSource[] Noise;
    private Animator[] TalkAnim;
    private int LineNum;
    #endregion

    private void Start()
    {
        CharAnim[4].Play("Death_End");
        AudioMan.Play("Dramatic Impact");
        if (SceneMan == null) { SceneMan = FindObjectOfType<MySceneManager>(); }
        if (MySceneManager.Abyss_State == 3) { ThingsToOff[0].SetActive(true); }
        else if (MySceneManager.Abyss_State != 3) { ThingsToOff[0].SetActive(false); }
        BLines.gameObject.SetActive(true);
        BLines.Show(200f, 0f);
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        CamNoise = Cam2.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        StartCoroutine(StartScene());

    }

    private void Update()
    {

        if (Player.gameObject.transform.position.x < Actors[1].transform.position.x && Actors[1].transform.localScale.x == 1) { Actors[1].transform.localScale = new Vector3(-1, 1, 1);}
        else if (Player.gameObject.transform.position.x > Actors[1].transform.position.x && Actors[1].transform.localScale.x == -1) { Actors[1].transform.localScale = new Vector3(1, 1, 1); }

        if (DLMan.Playing == false && Event == true) { MySceneManager.CutscenePlaying = false; Event = false; }

    }

    #region First Cutscene
    public void CutsceneDone() { Done = true; }

    IEnumerator StartScene()
    {
        yield return new WaitForSeconds(1f);
        if (SceneMan == null) { SceneMan = FindObjectOfType<MySceneManager>(); }

        Done = false;
        Cutscene[8].Play();
        while (Done == false) { yield return null; }
        Done = false;


        Player.gameObject.transform.localScale = new Vector3(1,1,1);
        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "- BOSS WANTS TO SEE YOU.";
        Line[1] = "- WHAT DO YOU MEAN 'WHY'?";
        Line[2] = "- DON'T YOU HEAR THE ALARM?";
        Line[3] = "— MOVE!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        Player.CanMove = false;

        CamNoise.m_AmplitudeGain = 0f;
        CamNoise.m_FrequencyGain = 0f;

        Cutscene[9].Play();
        while (Done == false) { yield return null; }
        Done = false;

        BLines.Hide(.5f);
        ThingsToOff[15].SetActive(true);
        yield return new WaitForSeconds(.5f);
        MySceneManager.CutscenePlaying = false;
        Player.CanMove = true;
        BLines.gameObject.SetActive(false);

        ThingsToOff[17].SetActive(true);
        SceneMan.Music.pitch = 0.8f;
        SceneMan.MStop();
        SceneMan.MChange(6, 0.5f);
        SceneMan.MPlay();
    }
    #endregion

    #region Random Talk
    public void Water()
    {
        Event = true;
        MySceneManager.CutscenePlaying = true;
            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Face[i] = null;
                Speed[i] = 0.03f;
                Voice[i] = SFX[1];
                TalkAnim[i] = null;
            }
            #endregion
            Line[0] = "It's a water cooler.";
            Line[1] = "There’s no need for it now.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
    }

    public void Coffee()
    {
        Event = true;
        MySceneManager.CutscenePlaying = true;

        if (MySceneManager.Abyss_State == 3)
        {
            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Face[i] = null;
                Speed[i] = 0.03f;
                Voice[i] = SFX[1];
                TalkAnim[i] = null;
            }
            #endregion
            Line[0] = "A piece of paper blocks the dispenser of the coffee machine.";
            Line[1] = "'Coffee is forbidden, my brave combatants. Water is the only approved beverage in our fine establishment.'";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        }
        else if (MySceneManager.Abyss_State != 3)
        {
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Face[i] = null;
                Speed[i] = 0.03f;
                Voice[i] = SFX[1];
                TalkAnim[i] = null;
            }
            #endregion
            Line[0] = "You don’t need coffee. Adrenaline is energizing enough.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        }
    }

    public void Cunt()
    {
        Event = true;
        MySceneManager.CutscenePlaying = true;
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.03f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "- THE BOSS IS IN HIS OFFICE ON THE RIGHT";
        Line[1] = "- YOU SHOULD KNOW IT BY NOW\nYOU'VE BEEN HERE FOR 5 YEARS.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
    }
    #endregion

    #region Window
    public void Window() { StartCoroutine(WindowView()); }
    IEnumerator WindowView()
    {
        MySceneManager.CutscenePlaying = true;
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.03f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "You take a look outside.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        SceneMan.Music.volume = SceneMan.Music.volume - 0.3f;
        ThingsToOff[4].SetActive(false);
        ThingsToOff[5].SetActive(true);
        Done = false;
        Cutscene[4].Play();
        SFX[3].Pause();
        SFX[2].Stop();
        SFX[2].Play();
        while (Done == false) { yield return null; }
        Done = false;


        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.03f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "Yep. Nothing particularly new to look at.";
        Line[1] = "Just a swarm of souls being dragged down\nto the bottom of the abyss by our lord God of Death.";
        Line[2] = "Lots of folks emigrated from this town over the past five years.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        ThingsToOff[4].SetActive(true);
        ThingsToOff[5].SetActive(false);
        SceneMan.Music.volume = SceneMan.Music.volume + 0.3f;
        MySceneManager.CutscenePlaying = false;
        SFX[3].Play();
        SFX[2].Stop();
    }
    #endregion

    #region Suzie
    public void SuzieCake(){ StartCoroutine(SuzieGift()); }

    IEnumerator SuzieGift()
    {
        MySceneManager.CutscenePlaying = true;
        if (PepsRern.sprite != PepsSpr[0])
        {
            MySceneManager.CutscenePlaying = true;
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Face[i] = null;
                Speed[i] = 0.05f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- Hey, I know it’s crazy awkward to bring up right now, but…";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            ThingsToOff[3].SetActive(false);
            PepsRern.sprite = PepsSpr[0];
            AudioMan.Play("Ting");


            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Face[i] = null;
                Speed[i] = 0.05f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- Happy birthday!";
            Line[1] = "— I wish you happiness AND the knowledge\nthat the 'Z' button skips dialogue! ";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            MySceneManager.CutscenePlaying = false;
        }

        else if (PepsRern.sprite == PepsSpr[0])
        {
            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Face[i] = null;
                Speed[i] = 0.05f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- Also I wish you stopped being so distant.";
            Line[1] = "- It isn't healthy.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            MySceneManager.CutscenePlaying = false;
        }
    }
    #endregion

    #region Boss Interact
    public void BossRoom(){ Player.MyRigidBody.velocity = Vector3.zero; StartCoroutine(BossMeeting()); }

    IEnumerator BossMeeting()
    {
        //I2 localization
        MySceneManager.CutscenePlaying = true;
        StartingReg = MySceneManager.Regret;
        MySceneManager.Regret = -2;
        Player.CanMove = false;
        SceneMan.MFadeOut(0.01f, false); 
        yield return new WaitForSeconds(1f);


        Actors[0].transform.localScale = new Vector3(-1, 1, 1);
        BLines.gameObject.SetActive(true);
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Ah, my favourite warrior.\nCome, join me!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        Done = false;
        Cutscene[0].Play();
        while (Done == false) { yield return null; }
        Done = false;


        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- We’ve reached trying times, my friend.";
        Line[1] = "- No clients. Bad publicity. Debts.";
        Line[2] = "- But worry not! I have a plan.\nHere, take a look!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        BLines.Show(180f, 0.5f);
        Done = false;
        Cutscene[1].Play();
        while (Done == false) { yield return null; }
        Done = false;


        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Our investors are coming! From the God of Death himself! They'll be here for a meeting in 15 minutes.";
        Line[1] = "- See this empty document?";
        Line[2] = "- You now have the privilege of writing a presentation about why they shouldn’t cancel our budget.";
        Line[3] = "- The fate of this company\nis in your hands now, my warrior.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        Done = false;
        Cutscene[2].Play();
        while (Done == false) { yield return null; }
        Done = false;


        yield return new WaitForSeconds(1f);
        BLines.Hide(0.2f);
        ThingsToOff[1].SetActive(true);
        ThingsToOff[2].SetActive(true);
        SFX[3].Stop();
        SceneMan = FindObjectOfType<MySceneManager>();
        SceneMan.Music.pitch = 1f;
        SceneMan.MChange(2, 1f);
        SceneMan.MPlay();


        while (Simon.StartShake != 1) { yield return null; }

        Done = false;
        Cutscene[3].Play();
        while (Done == false) { yield return null; }
        Done = false;


        SceneMan.MStop();
        MySceneManager.Regret = StartingReg;
        ThingsToOff[7].SetActive(false);
        ThingsToOff[8].SetActive(true);
        ThingsToOff[10].SetActive(false);
        ThingsToOff[2].SetActive(false);
        ThingsToOff[11].SetActive(false);

        //did you take the cake or not
        if (PepsRern.sprite == PepsSpr[0]){ PepsRern.sprite = PepsSpr[2]; ThingsToOff[9].SetActive(true); }
        yield return new WaitForSeconds(1f);


        Cutscene[5].Play();
        while (Done == false) { yield return null; }
        Done = false;
        SceneMan.Music.pitch = 1f;
        SceneMan.MChange(5, 0.7f);
        SceneMan.MPlay();


        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "You did it.";
        Line[1] = "You wrote the perfect pitch!\nThat investor money is as good as yours!";
        Line[2] = "Here's what your presentation says:";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        PepsRern.sprite = PepsSpr[1];
        yield return new WaitForSeconds(1f);


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "It sa-a-ays...";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        ThingsToOff[11].SetActive(false);
        ThingsToOff[8].SetActive(false);
        ThingsToOff[7].SetActive(true);

        SceneMan.MStop();
        AudioMan.Play("Dramatic Impact");
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        if (Simon.PerfectPitchPresantation == "")
        {
            Line[0] = "Oh, the page is still blank, actually.";
        }
        else
        {
            Line[0] = Simon.PerfectPitchPresantation;
        }
        Line[1] = "[silence]";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        Strategy.Pitch = Simon.PerfectPitchPresantation;


        yield return new WaitForSeconds(1f);
        PepsRern.sprite = PepsSpr[3];
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "You have no idea what you're doing.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        ThingsToOff[12].SetActive(true);
        BLines.Show(180f, 2f);
        Cutscene[6].Play();
        while (Done == false) { yield return null; }
        Done = false;


        yield return new WaitForSeconds(1f);
        PepsRern.sprite = PepsSpr[2];
        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "- JUDGING BY THE LOOK ON YOUR FACE,\nIT SEEMS YOU'RE IN CHARGE NOW.";
        Line[1] = "- WELL, GRAB YOUR PRESENTATION AND LET'S GO!";
        Line[2] = "- THE INVESTORS ARE HERE!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        ThingsToOff[13].SetActive(true);
        Cutscene[7].Play();
        while (Done == false) { yield return null; }
        Done = false;
        ThingsToOff[16].SetActive(true);
        BLines.Hide(1f);
        MySceneManager.CutscenePlaying = false;
        Player.CanMove = true;
        SFX[5].Stop();

    }
    #endregion

    #region Investros
    public void InvestorTrg() { Player.CanMove = false; MySceneManager.CutscenePlaying = true; StartCoroutine(Investors()); }

    public void SlowDownSailor() { Player.MyRigidBody.velocity = Vector3.zero; }

    IEnumerator Investors()
    {
        Player.gameObject.transform.localScale = new Vector3 (-1,1,1);
        Player.MyRigidBody.velocity = Vector3.zero;
        Player.MyRigidBody.AddForce(-transform.right * 1.9f, ForceMode2D.Impulse);
        AudioMan.Play("Dramatic Impact");

        BLines.Show(180f, 1f);
        SceneMan.MStop();
        SceneMan.MChange(7, 1);
        SceneMan.MPlay();
        yield return new WaitForSeconds(2f);

        LineNum = 11;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
        }
        #endregion
        Voice[0] = SFX[1];
        TalkAnim[0] = null;
        Speed[0] = 0.07f;
        Line[0] = "- SO DO YOU GUYS WANT SOME COFFEE OR ...? ";

        //Cultist
        Voice[1] = SFX[1];
        Voice[2] = SFX[4];
        TalkAnim[1] = null;
        TalkAnim[2] = CharAnim[3];
        Speed[1] = 0.03f;
        Speed[2] = 0.07f;
        Line[1] = "[silence]";
        Line[2] = "- Yes. Coffee would be\nmost pleasing.";

        //Coffee diff
        TalkAnim[3] = null;
        Speed[3] = 0.03f;
        Voice[3] = SFX[1];
        if (MySceneManager.Abyss_State == 3) { Line[3] = "- OH, RIGHT. ACTUALLY, COFFEE WAS FORBIDDEN HERE."; }
        else if (MySceneManager.Abyss_State != 3) { Line[3] = "- GOOD! THE COFFEE MACHINE IS RIGHT BEHIND YOU.\nFEEL FREE TO HELP YOURSELF."; }

        //Cultist
        Voice[4] = SFX[1];
        TalkAnim[4] = null;
        Speed[4] = 0.03f;
        Line[4] = "[silence]";

        //Collegue
        Voice[5] = SFX[1];
        Voice[6] = SFX[1];
        TalkAnim[5] = null;
        TalkAnim[6] = null;
        Speed[5] = 0.03f;
        Speed[6] = 0.03f;
        Line[5] = "- SO LET'S GET STARTED!\nI WON'T WASTE YOUR TIME OR MINE.";
        Line[6] = "- I KNOW YOU'RE VERY BUSY WITH\nWHATEVER IT IS THAT YOU DO.";

        //Collegue
        Voice[7] = SFX[4];
        Voice[8] = SFX[1];
        Voice[9] = SFX[1];
        Voice[10] = SFX[1];
        Speed[7] = 0.07f;
        Speed[8] = 0.03f;
        Speed[9] = 0.03f;
        Speed[10] = 0.03f;
        TalkAnim[7] = CharAnim[3];
        TalkAnim[8] = null;
        TalkAnim[9] = null;
        TalkAnim[10] = null;

        Line[7] = "- We execute the infidels. We manage our lord's investments. We watch PowerPoint presentations.";
        Line[8] = "- AWESOME! WELL, WE’VE GOT ONE FOR YOU!";
        Line[9] = "- I GIVE YOU: THE CEO OF THIS COMPANY!";
        Line[10] = "- THE ONE WHO WILL CONVINCE YOU\nTO GIVE US YOUR MONEY! LET'S HEAR IT!";

        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { if (DLMan.Currentline == 9 && SFX[5].gameObject.transform.localScale == new Vector3(1, 1, 1)) { SFX[5].gameObject.transform.localScale = new Vector3(-1, 1, 1); } yield return null; }

        
        yield return new WaitForSeconds(1f);
        ThingsToOff[4].SetActive(false);
        ThingsToOff[14].SetActive(true);
        AudioMan.Play("Dramatic Impact");

        yield return new WaitForSeconds(2f);
        BChose.ButtonChoice = 0;
        DLMan.LeftString = "[GIVE IT A SHOT]";
        DLMan.RightString = "[RUN.]";
        DLMan.ChoiceStarter();
        while (BChose.ButtonChoice == 0) { yield return null; }

        SceneMan.MStop();
        SceneMan.Music.pitch = 1f;
        CharAnim[4].Play("Death_Start");
        AudioMan.Play("Dramatic Impact");
        //SFX[6].Play();
        AdditionalText.text = "50 YEARS LATER";
        yield return new WaitForSeconds(4f);
        if (BChose.ButtonChoice == 1) { SceneMan.NormalSceneTransition(0, "Death_Start", "Death_End", 0f, "Office_3", 0, 0); }
        else if(BChose.ButtonChoice == 2) { SceneMan.NormalSceneTransition(0, "Death_Start", "Death_End", 0f, "Office_4", 0, 0); }
    }
    #endregion

    public void StrangeDoor() { StartCoroutine(DoorSt()); }

    public void PitchShift()
    {
        SceneMan.Music.pitch = SceneMan.Music.pitch + 0.1f;
    }

    IEnumerator DoorSt()
    {
        AudioMan.Play("Dramatic Impact");
        Actors[2].SetActive(true);
        SceneMan.Music.volume = SceneMan.Music.volume - 0.2f;
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "It’s closed.";
        Line[1] = "The days when it was open\nare long gone.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        SceneMan.Music.volume = SceneMan.Music.volume + 0.2f;
        Actors[2].SetActive(false);
    }

    #region Lines
    void DeclareLines()
    {
        Line = new string[LineNum];
        IsThereAface = new bool[LineNum];
        Face = new Sprite[LineNum];
        Speed = new float[LineNum];
        TalkAnim = new Animator[LineNum];
        Voice = new AudioSource[LineNum];
        Noise = new AudioSource[LineNum];
    }
    #endregion
}
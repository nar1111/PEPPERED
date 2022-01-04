using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class Office_3 : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private Text[] Description;
    [SerializeField]
    private PlayableDirector[] Cutscene;
    [SerializeField]
    private AudioSource[] SFX;
    [SerializeField]
    private GameObject[] Things;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private Sprite[] FaceSprites;
    [SerializeField]
    private CHAIR_PLAYER Player;
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
    private bool Event = false;
    private YourBriliantPitch Strategy;
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


    // Start is called before the first frame update
    void Start()
    {
        SceneMan = FindObjectOfType<MySceneManager>();
        Strategy = FindObjectOfType<YourBriliantPitch>();
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        BLines.Show(220f, 0f);
        SceneMan.MStop();
        AudioMan.Play("Dramatic Impact");
        CharAnim[2].Play("Death_End");
        StartCoroutine(Begining());
    }

    // Update is called once per frame
    void Update()
    {
        if (DLMan.Playing == false && Event == true) { MySceneManager.CutscenePlaying = false; Event = false; }
    }

    public void CutsceneDone() { Done = true; }

    #region Start
    IEnumerator Begining()
    {
     
        yield return new WaitForSeconds(1f);
        if (SceneMan == null) { SceneMan = FindObjectOfType<MySceneManager>(); }
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        SpriteRenderer BRnr = CharAnim[0].GetComponent<SpriteRenderer>();
        LineNum = 2;
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
        Line[0] = "- HERE'S TO ANOTHER\nSUCCESSFUL YEAR, MY FRIEND!";
        Line[1] = "- ANOTHER YEAR OF LIVING ON\nTHE SWEET MONEY OF THE GOD OF DEATH!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        //Confetti
        SceneMan.MChange(5, 0.8f);
        SceneMan.Music.pitch = 0.9f;
        AudioMan.Play("Victory");
        Things[0].SetActive(true);
        Cutscene[2].Play();
        yield return new WaitForSeconds(1f);

        //Accountant Culltist
        Done = false;
        Cutscene[0].Play();
        while (Done == false) { yield return null; }
        Done = false;


        Player.gameObject.transform.localScale = new Vector3(1,1,1);
        Cutscene[2].Stop();
        BRnr.flipX = false;

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Hey. Can we talk for a minute?";
        Line[1] = "- In my office?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        //Accountant Culltist leaves
        Cutscene[1].Play();
        while (Done == false) { yield return null; }
        Done = false;


        LineNum = 2;
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
        Line[0] = "- UGH. DON'T LET HIM\nGET TO YOUR HEAD.";
        Line[1] = "- WE’VE STILL GOT\nA WHOLE NIGHT TO PARTY!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        Things[12].SetActive(true);
        SceneMan.MPlay();
        MySceneManager.CutscenePlaying = false;
        Player.CanMove = true;
        BLines.Hide(1f);
    }
    #endregion

    #region Meeting
    public void MeetingTrg() { MySceneManager.CutscenePlaying = true; Player.CanMove = false; StartCoroutine(Meeting()); }

    IEnumerator Meeting()
    {

        Player.MyRigidBody.velocity = Vector2.zero;
        Player.gameObject.transform.localScale = new Vector3(1,1,1);
        Player.MyRigidBody.AddForce(transform.right * 2f, ForceMode2D.Impulse);
        AudioMan.Play("Dramatic Impact");
        SceneMan.MStop();
        SceneMan.Music.pitch = 1f;
        yield return new WaitForSeconds(1.5f);


        LineNum = 3;
        DeclareLines();

        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- So listen. You know I like you.";
        Line[1] = "- BUT.";
        Line[2] = "- As your accountant, I insist that we talk about the financial situation.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null;}

        Description[0].text = "We'll think\nof something later.";
        Description[1].text = "So what do we do?";
        DLMan.LeftString = "[DISMISS]";
        DLMan.RightString = "[IMPLORE]";
        DLMan.ChoiceStarter();
        while (BChose.ButtonChoice == 0) { yield return null; }

        Things[6].SetActive(true);
        Things[1].SetActive(false);
        Things[2].SetActive(true);
        SceneMan.MChange(1, 0.5f);
        SceneMan.Music.pitch = 1.4f;
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
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        if (BChose.ButtonChoice == 1) { Line[0] = "- No! There is no “later”! We're in a complete hellhole of debt!"; }
        else if (BChose.ButtonChoice == 2) { Line[0] = "- There's nothing to do!"; }
        Line[1] = "- Your business strategy literary says, and I quote:";
        if (Strategy != null) { Line[2] = Strategy.Pitch; }
        else { Line[2] = "- «OUR MISSION». And that's it. That's as far as our plans go."; }
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        Things[2].SetActive(false);
        Things[1].SetActive(true);
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
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- How is that even a strategy? W-who accepted this? I didn't!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        Things[3].SetActive(false);
        Things[4].SetActive(true);


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            
        }
        #endregion
        Voice[0] = SFX[2];
        Voice[1] = SFX[1];
        TalkAnim[0] = CharAnim[3];
        TalkAnim[1] = CharAnim[1];
        Line[0] = "- I thought it was convincing.";
        Line[1] = "- Shut it. You operate guillotines for a living, Chad.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        yield return new WaitForSeconds(0.5f);
        Things[4].SetActive(false);
        Things[3].SetActive(true);
        Things[4].transform.localScale = new Vector3(-1,1,1);


        CharAnim[1].gameObject.transform.localScale = new Vector3(1,1,1);
        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Actually, I tried filing for bankruptcy.";
        Line[1] = "- BUT.";
        Line[2] = "- Our business practice is so abysmally stupid that the only forgiveness for debt that God of Death will give is, well.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { if (DLMan.Currentline == 2 && Things[3].activeInHierarchy) {Things[3].SetActive(false); Things[4].SetActive(true);} yield return null; }
        CharAnim[1].gameObject.transform.localScale = new Vector3(-1, 1, 1);

        SceneMan.MStop();
        SceneMan.Music.pitch = 1f;
        BLines.Show(200f, 0.4f);
        CharAnim[4].Play("Guillotine_Ready");
        Done = false;
        Cutscene[3].Play();
        while (Done == false) { yield return null; }
        Done = false;

        yield return new WaitForSeconds(1.5f);

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- None of us are comfortable with this. But we have no choice.";
        Line[1] = "- I hope you understand.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        BLines.Hide(1f);
        Player.CanMove = true;
        MySceneManager.CutscenePlaying = false;
        Things[5].SetActive(true);
        Things[10].SetActive(true);
     
    }
    #endregion

    #region Ending
    public void EndingTrg() { MySceneManager.Regret = -2; StartCoroutine(Ending()); }

    IEnumerator Ending()
    {
        MySceneManager.CutscenePlaying = true;
        SceneMan.MPause();
        CharAnim[2].Play("Death_Start");
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
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- All hail God of Death.";
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

        AudioMan.Play("Crush");
        yield return new WaitForSeconds(2f);
        SceneMan.NormalSceneTransition(0, "Death_Start", "Death_End", 0f, "[TRANSITION SCENE]", 0, 0);
    }
    #endregion

    #region Run
    public void RunTrg() { StartCoroutine(Run()); }

    IEnumerator Run()
    {
        MySceneManager.CutscenePlaying = true;
        AudioMan.Play("Dramatic Impact");
        yield return new WaitForSeconds(2f);
        Player.MyRigidBody.velocity = Vector2.zero;
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.02f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "You should've run a long time ago.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        MySceneManager.Regret = -3;
        StartCoroutine(Ending());
        AudioMan.Play("Dramatic Impact");

    }
    #endregion

    #region Misc
    public void TalkHim() { StartCoroutine(Accountant()); }

    public void SlowDownSailor() { Player.MyRigidBody.velocity = Vector3.zero; Player.gameObject.transform.localScale = new Vector3(1,1,1); }

    IEnumerator Accountant()
    {
        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
        }
        #endregion
        Voice[0] = SFX[1];
        Voice[1] = SFX[1];
        Voice[2] = SFX[2];
        Voice[3] = SFX[1];
        TalkAnim[0] = CharAnim[1];
        TalkAnim[1] = CharAnim[1];
        TalkAnim[2] = CharAnim[3];
        TalkAnim[3] = CharAnim[1];
        Line[0] = "- Don't you make puppy eyes at me!";
        Line[1] = "- I'm the one who has to clean this mess!";
        Line[2] = "- Really? I thought I had to.";
        Line[3] = "- I meant the paperwork, Chad.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { if (Things[4].transform.localScale.x == -1f && DLMan.Currentline == 2) { Things[4].transform.localScale = new Vector3(1,1,1); } yield return null; }
        Things[4].transform.localScale = new Vector3(-1, 1, 1);
    }
    #endregion

    public void StrangeDoor() { StartCoroutine(DoorSt()); }

    IEnumerator DoorSt()
    {
        AudioMan.Play("Dramatic Impact");
        Things[11].SetActive(true);
        SceneMan.Music.volume = SceneMan.Music.volume - 0.2f;
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "This door is closed.";
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
        Things[11].SetActive(false);
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
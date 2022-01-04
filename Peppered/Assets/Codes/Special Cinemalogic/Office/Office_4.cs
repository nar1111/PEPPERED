using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Events;
public class Office_4 : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private PlayableDirector[] Cutscene;
    [SerializeField]
    private GameObject[] Actors;
    [SerializeField]
    private AudioSource[] SFX;
    [SerializeField]
    private GameObject[] Things;
    [SerializeField]
    private Rigidbody2D ChainsRigid;
    [SerializeField]
    private SpriteRenderer PepRen;

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
    private Button_Choose BChose;
    [SerializeField]
    private Black_lines BLines;
    [SerializeField]
    private AUDIOMANAGER AudioMan;
    private bool Event = false;
    [SerializeField]
    private MySceneManager SceneMan;
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
        SceneMan = FindObjectOfType<MySceneManager>();
        MySceneManager.CutscenePlaying = true;
        if (MySceneManager.Abyss_State == 3) { Things[3].SetActive(true); }
        else if (MySceneManager.Abyss_State != 3) { Things[3].SetActive(false); }
        Player.CanMove = false;
        BLines.Show(180f, 0f);
        CharAnim[1].Play("Death_End");
        AudioMan.Play("Dramatic Impact");
        StartCoroutine(Begining());
    }

    private void Update()
    {
        if (DLMan.Playing == false && Event == true) { MySceneManager.CutscenePlaying = false; Event = false; }
    }

    #region Coffee Machine
    public void CoffeeStart() { Event = true; StartCoroutine(Coffee()); }

    public void CutsceneDone() { Done = true; }

    IEnumerator Coffee()
    {

        MySceneManager.CutscenePlaying = true;
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "Life’s a funny thing. Every choice impacts it hugely.";
        if (MySceneManager.Regret == -3) { Line[1] = "Imagine where’d you be now if you HADN’T poured the coffee all those years ago?"; }
        else if (MySceneManager.Regret == -4){ Line[1] = "Imagine where’d you be now if you HAD poured the coffee all those years ago?"; }
        else { Line[1] = "Imagine where'd you be now if didn't even try to pass the test all those years ago?"; }
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
    #endregion

    #region Cutscenes
    IEnumerator Begining()
    {

        yield return new WaitForSeconds(1f);
        if (SceneMan == null) { SceneMan = FindObjectOfType<MySceneManager>(); }
        Done = false;
        Cutscene[4].Play();
        while (Done == false) { yield return null; }
        Done = false;

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.07f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Follow me, sinner.";
        Line[1] = "- The time has come.";
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
        Cutscene[5].Play();
        while (Done == false) { yield return null; }
        Done = false;

        Things[5].SetActive(true);
        MySceneManager.CutscenePlaying = false;
        Player.CanMove = true;

    }

    public void SurpriseStart() { Player.gameObject.transform.localScale = new Vector3(1, 1, 1);  MySceneManager.CutscenePlaying = true; Player.MyRigidBody.velocity = Vector3.zero; StartCoroutine(Ending()); }

    public void Ohno() { Player.MyRigidBody.velocity = Vector3.zero; Player.PushPower = 0.3f; Things[0].SetActive(true); SceneMan.MFadeOut(0.01f, false); }

    public void Shit(){ StartCoroutine(ShitCut()); MySceneManager.CutscenePlaying = true; Player.CanMove = false; SceneMan.MStop(); }

    IEnumerator ShitCut()
    {
        Cutscene[6].Play();
        while (Done == false) { yield return null; }
        Done = false;
        CharAnim[2].Play("There Goes our relative");
        yield return new WaitForSeconds(2f);
        CharAnim[1].Play("Death_Start");
        AudioMan.Play("Dramatic Impact");
        MySceneManager.Regret = -1;
        yield return new WaitForSeconds(2f);
        SceneMan.NormalSceneTransition(0, "Death_Start", "Death_End", 0f, "[TRANSITION SCENE]", 0, 0);
    }

    IEnumerator Ending()
    {
        SceneMan.MChange(5, 0.6f);
        SceneMan.MPlay();
        Cutscene[0].Play();
        while (Done == false) { yield return null; }
        Done = false;
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
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- You have repaid your debts.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        //Let him go
        Cutscene[1].Play();
        while (Done == false) { yield return null; }
        Done = false;
        ChainsRigid.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(1f);


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.05f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- You can leave with your family now.";
        Line[1] = "- I pray that you've learned your lesson.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        Things[1].SetActive(false);
        PepRen.color = new Color(255, 255, 255, 0);
        Things[2].SetActive(true);
        Cutscene[2].Play();


        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Face[i] = null;
            Speed[i] = 0.07f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- It is never too late to change your life.";
        Line[1] = "- And embrace the righteous way of living.";
        Line[2] = "- Never.";
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
        PepRen.color = new Color(255, 255, 255, 255);
        Things[1].SetActive(true);
        BLines.Show(120f, 0f);
        Cutscene[2].Stop();
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
            TalkAnim[i] = CharAnim[0];
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


        Cutscene[3].Play();
        while (Done == false) { yield return null; }
        Done = false;
        MySceneManager.CutscenePlaying = false;
        Player.CanMove = true;
        BLines.Hide(1.5f);
        yield return new WaitForSeconds(1.5f);
        Player.PushPower = 0.55f;
    }
    #endregion

    public void StrangeDoor() { StartCoroutine(DoorSt()); }

    IEnumerator DoorSt()
    {
        AudioMan.Play("Dramatic Impact");
        Things[4].SetActive(true);
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
        Line[0] = "It’s closed.";
        Line[1] = "The days when it was open are long gone.";
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
        Things[4].SetActive(false);
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
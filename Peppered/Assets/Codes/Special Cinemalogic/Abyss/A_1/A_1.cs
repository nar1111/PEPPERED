using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
//using UnityEngine.UI;
//using Cinemachine;
public class A_1 : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private Drone[] Drones;
    [SerializeField]
    private GameObject[] Cams;
    [SerializeField]
    private PlayableDirector[] Cutscene;
    [SerializeField]
    private GameObject[] Actors;
    [SerializeField]
    private AudioSource[] SFX;
    [SerializeField]
    private GameObject[] Things;
    [SerializeField]
    private Dumpster_Moveset Dump;
    [SerializeField]
    private GameObject[] TurnThingsOff;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private PLAYER_CONTROLS Player;
    [SerializeField]
    private DialogueManager DLMan;
    [SerializeField]
    private Black_lines BLines;
    [SerializeField]
    private AUDIOMANAGER AudioMan;
    [SerializeField]
    private MySceneManager SceneMan;
    private bool Done = false;
    private int SobbingCunt = 0;


    #region Private DL Stuff
    [HideInInspector]
    public string[] Line;
    private float[] Speed;
    private AudioSource[] Voice;
    private AudioSource[] Noise;
    private Animator[] TalkAnim;
    private int LineNum;
    #endregion

    #region Intro
    private void Start()
    {
        if (MySceneManager.Abyss_State == 0)
        {
            Player.MyAnim.Play("Invisible");
            Player.gameObject.transform.position = new Vector3(2.9f, 0f, 0f);
            MySceneManager.CutscenePlaying = true;
            Player.CanMove = false;
            StartCoroutine(GarbageDay());
        }
        else
        {
            //I've been here before
            SobbingCunt = 2;
            Things[7].SetActive(false);
            Things[4].SetActive(false);
            Actors[0].SetActive(false);
            TurnThingsOff[1].SetActive(false);
            Alarm();
        }

        if (MySceneManager.Cynthia == 3)
        {
            TurnThingsOff[0].SetActive(false);
        }
    }

    IEnumerator GarbageDay()
    {
        SceneMan = FindObjectOfType<MySceneManager>();
        BLines.Show(120f, 2f);
        yield return new WaitForSeconds(2f);
        Done = false;
        Cutscene[0].Play();
        while (Done == false) { yield return null; }
        Dump.enabled = true;
        while (!Input.GetButtonDown("Jump")) { yield return null; }
        Dump.enabled = false;
        Player.CanMove = true;
        MySceneManager.CutscenePlaying = false;
        Player.Bounce(12f, 2);
        while (Player.Grounded == true) { yield return null; }
        while (Player.Grounded == false) { yield return null; }
        AudioMan.Play("Battle Start");
        BLines.Hide(.1f);
        Things[0].SetActive(true);
        SceneMan.MStop();
        SceneMan.MChange(12, 0.5f);
        SceneMan.MPlay();
    }
    #endregion

    #region Police
    public void Police(){ StartCoroutine(PoliceAreHere()); }

    IEnumerator PoliceAreHere()
    {
        TurnThingsOff[3].SetActive(false);
        SobbingCunt = 2;
        MySceneManager.Abyss_State = 1;
        SceneMan.MFadeOut(0.005f, false);
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        Player.MyRigidBody.velocity = Vector2.zero;
        BLines.Show(220f, 1.5f);
        yield return new WaitForSeconds(3f);
        //Cutscene[2].Stop();



        Cutscene[1].Play();
        Done = false;
        while (Done == false) { yield return null; }
        SceneMan.MStop();
        SceneMan.MChange(8, 0.85f);
        SceneMan.MPlay();
        Done = false;
        while (Done == false) { yield return null; }
        CharAnim[0].Play("Strawberry Active Idle");
        CharAnim[1].Play("Idle1");
        yield return new WaitForSeconds(1f);
        CharAnim[1].Play("TakeRadio");
        yield return new WaitForSeconds(1f);

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
        }
        #endregion
        Voice[0] = SFX[2];
        Voice[1] = SFX[0];
        TalkAnim[0] = CharAnim[1];
        TalkAnim[1] = null;
        Line[0] = "- Chief. Suspect located.";
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
        SceneMan.MPause();


        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Now what?";
        Line[1] = "- Follow procedure?";
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
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.15f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "- Y E S";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }



        Cams[0].SetActive(true);
        AudioMan.Play("Dramatic Impact");
        SceneMan.MPlay();
        CharAnim[1].Play("Idle1");
        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Evening, suspect. Officer Merdeka.\nAbyss patrol.";
        Line[1] = "- You match the description\nof the star thief.";
        Line[2] = "- Have to detain you. For a search.\nGive your consent.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        CharAnim[1].Play("Talk4");
        yield return new WaitForSeconds(1f);


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- You're refusing?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        SceneMan.Music.pitch = 1.3f;
        Cams[0].SetActive(false);
        Cams[1].SetActive(true);
        BLines.Show(300f, 5f);
        CharAnim[1].Play("Idle3");
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.07f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "[Inhales angrily]";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        SceneMan.MStop();
        CharAnim[1].Play("TakeRadioIdle");
        BLines.Show(155f, 0f);
        Cams[1].SetActive(false);
        Cutscene[3].Play();
        SceneMan.Music.pitch = 1f;
        yield return new WaitForSeconds(2f);


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Chief. Is an arrest possible without proof or a warrant?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.15f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "- N O";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        CharAnim[1].Play("Idle1");
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Rules are rules.\nYou're free to go.";
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
        Cutscene[4].Play();
        while (Done == false) { yield return null; }


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- But.";
        Line[1] = "- I know you stole that star.\nAnd once I find evidence.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        Cams[1].SetActive(true);
        Player.gameObject.transform.localScale = new Vector3(1,1,1);
        AudioMan.Play("Dramatic Impact");
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "-  I will be legally obligated\nto kick your ass.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        Cams[1].SetActive(false);
        yield return new WaitForSeconds(1f);


        Done = false;
        Cutscene[5].Play();
        while (Done == false) { yield return null; }


        MySceneManager.CutscenePlaying = false;
        Player.CanMove = true;
        BLines.Hide(2f);
        SceneMan.MChange(12, 0.5f);
        SceneMan.MPlay();
        //SceneMan.MFadeOut(0.005f, false);

    }
    #endregion

    public void Alarm()
    {
        TurnThingsOff[2].SetActive(false);
        Things[1].SetActive(false);
        Things[3].SetActive(false);
        Things[4].SetActive(false);
        Things[5].SetActive(true);
        Things[6].SetActive(true);
        AudioMan.Play("Dramatic Impact"); 
        for (int i = 0; i < Drones.Length; i++)
        {
            Drones[i].ChangeStage();
            Drones[i].SafeDistance = 4f;
            Drones[i].SafeDistance = 15f;
        }
    }

    public void TV(){ if (SobbingCunt == 0) {SobbingCunt = 1; } }

    public void SobSob()
    {
        if (SobbingCunt == 0)
        { 
             LineNum = 3;
             DeclareLines();
             #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
             Line[0] = "[Sobbing deeply]";
             Line[1] = "- Dear gods! Dear gods!\nHave you heard the news?";
             Line[2] = "[Sobbing continues]";
             #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        }
        else if (SobbingCunt == 1)
        {
            LineNum = 4;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                Voice[i] = SFX[0];
                TalkAnim[i] = null;
            }
            #endregion
            Line[0] = "[Sobbing deeply]";
            Line[1] = "- Dear gods! Dear gods!";
            Line[2] = "- Someone should definitely do\nsomething about this.";
            Line[3] = "[Sobbing continues]";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        }
        else if (SobbingCunt == 2)
        {
            LineNum = 4;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                Voice[i] = SFX[0];
                TalkAnim[i] = null;
            }
            #endregion
            Line[0] = "[Sobbing deeply]";
            Line[1] = "- That police woman said\neverything is under control.";
            Line[2] = "- Dear gods! Dear gods!\nI'm so happy!";
            Line[3] = "[Sobbing intensifies]";
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

    public void Strawberry()
    {
        if (TurnThingsOff[2].activeInHierarchy)
        {
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                Voice[i] = SFX[0];
                TalkAnim[i] = null;
            }
            #endregion
            Line[0] = "Strawberries. These things grow\neverywhere in the Abyss.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        }
        else
        {
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                Voice[i] = SFX[0];
                TalkAnim[i] = null;
            }
            #endregion
            Line[0] = "Looks like someone ate all of them.";
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

    #region Misc
    public void ChangeLayer() { SpriteRenderer SpRen = Player.GetComponent<SpriteRenderer>(); SpRen.sortingLayerName = "Player"; }
    public void LightOn() { Things[1].SetActive(true); }
    public void LightOff() { Things[1].SetActive(false); }
    #endregion

    #region It's donskie
    public void CutsceneDone() { Done = true; }
    #endregion

    #region Lines
    void DeclareLines()
    {
        Line = new string[LineNum];
        Speed = new float[LineNum];
        TalkAnim = new Animator[LineNum];
        Voice = new AudioSource[LineNum];
        Noise = new AudioSource[LineNum];
    }
    #endregion
}
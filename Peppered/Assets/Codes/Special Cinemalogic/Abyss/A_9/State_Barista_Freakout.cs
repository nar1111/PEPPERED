using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Barista_Freakout : State
{
    #pragma warning disable 649
    //1 = Pepper
    //2 = Coffee Chum
    [HideInInspector] public int CutsceneNumber;

    [Header("WINDOWS CUTSCENE")]
    [SerializeField] private Animator CoffeeWindow;
    [SerializeField] private AUDIOMANAGER AudiMan;
    [SerializeField] private Black_lines BLines;
    [SerializeField] private GameObject PunchEffect;
    [SerializeField] private GameObject StealthUI;
    [SerializeField] private GameObject ClosedWindowDialogue;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField] private Animator MyAnim;
    [SerializeField] private Animator[] CharAnim;
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private DialogueManager DLMan;
    [SerializeField] private AudioSource[] SFX;

    #region Private DL Stuff
    [HideInInspector]
    public string[] Line;
    private float[] Speed;
    private AudioSource[] Voice;
    private AudioSource[] Noise;
    private Animator[] TalkAnim;
    private int LineNum;
    #endregion

    public override State RunCurrentState()
    {
        if (CutsceneNumber == 2)
        {
            CutsceneNumber = 0;
            ClosedWindowDialogue.SetActive(true);
            WHAT_HAVE_I_DONE.Collectibles.Add("Barista", 2);
            StartCoroutine(CoffeeChumReact());
        }
        else if (CutsceneNumber == 1)
        {
            CutsceneNumber = 0;
            ClosedWindowDialogue.SetActive(true);
            WHAT_HAVE_I_DONE.Collectibles.Add("Barista", 1);
            StartCoroutine(PlayerReact());
        }
        return this;
    }

    IEnumerator CoffeeChumReact()
    {
        BLines.Show(220f, 0.5f);
        StealthUI.SetActive(false);
        MySceneManager.CutscenePlaying = true;
        yield return new WaitForSeconds(1f);
        MyAnim.Play("Barista_Shoked");
        //Window_Closed
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "— I KNEW IT! I KNEW IT!";
        Line[1] = "— You ARE alive! Stay away! STAY AWAY!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        CoffeeWindow.Play("Window_Filler_Close");
  

        while (!CoffeeWindow.GetCurrentAnimatorStateInfo(0).IsName("Window_Closed")){   yield return null;   }

        AudiMan.Play("Dramatic Impact");
        PunchEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PunchEffect.SetActive(false);
        yield return new WaitForSeconds(1f);
        BLines.Hide(1.5f);
        MySceneManager.CutscenePlaying = false;
    }

    IEnumerator PlayerReact()
    {
        BLines.Show(220f, 0.5f);
        StealthUI.SetActive(false);
        Player.MyRigidBody.velocity = Vector2.zero;
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        yield return new WaitForSeconds(1f);
        MyAnim.Play("Barista_Shoked");
        if (Player.gameObject.transform.localScale.x == 1) { Player.gameObject.transform.localScale = new Vector3(-1,1,1); }

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
        Line[0] = "— NO. WAY.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        MyAnim.Play("Barista_Call");


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "— Hey, girl! I totally found that Star Thief you're looking for!";
        Line[1] = "— Oh my gods, babe, do\nI have things to tell on 'em!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        CoffeeWindow.Play("Window_Filler_Close");


        while (!CoffeeWindow.GetCurrentAnimatorStateInfo(0).IsName("Window_Closed")) { yield return null; }

        AudiMan.Play("Dramatic Impact");
        PunchEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        PunchEffect.SetActive(false);
        yield return new WaitForSeconds(1f);
        AudiMan.Play("Busted");
        BLines.Hide(1.5f);
        MySceneManager.CutscenePlaying = false;
        Player.CanMove = true;
    }

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
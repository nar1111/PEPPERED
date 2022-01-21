using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Book_Look : State
{
    [SerializeField]
    private Button_Choose BChose;
    [SerializeField]
    private PLAYER_CONTROLS Player;
    [SerializeField]
    private DialogueManager DLMan;
    [SerializeField]
    private AudioSource[] SFX;
    [SerializeField]
    private State_Book_Choose ChooseState;
    [SerializeField]
    private GameObject Cam;


    private int MyStage = 0;

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
        if (MyStage == 1)
        {
            MyStage = 0;
            ChooseState.StartLooking();
            Player.CanMove = false;
            return ChooseState;
        }
        return this;
    }

    public void Check()
    {
        if (MyStage != 2)
        {
            MyStage = 2;
            StartCoroutine(CheckityCheck());
        }
    }

    IEnumerator CheckityCheck()
    {
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.05f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "There's a variety of books on display with a few interesting ones.";
        Line[1] = "Care to take a look?";
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
        DLMan.LeftString = "[SURE]";
        DLMan.RightString = "[REFUSE]";
        DLMan.ChoiceStarter();
        while (BChose.ButtonChoice == 0) { yield return null; }

        if (BChose.ButtonChoice == 1)
        {
            MyStage = 1;
            Cam.SetActive(true);
        } else
        {
            MyStage = 0;
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.05f;
                Voice[i] = SFX[0];
                TalkAnim[i] = null;
            }
            #endregion
            Line[0] = "Very well.";
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
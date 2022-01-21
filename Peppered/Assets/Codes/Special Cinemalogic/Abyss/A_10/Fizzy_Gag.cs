using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;
using UnityEngine.UI;

public class Fizzy_Gag : MonoBehaviour
{
    [Header("--CUTSCENE STUFF")]
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private PlayableDirector[] Cutscene;
    [SerializeField] private Animator TheoAnim;
    [SerializeField] private Black_lines BLines;
    [SerializeField] private AudioSource Harp;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField] private Text[] Description;
    [SerializeField] private Button_Choose BChose;
    [SerializeField] private DialogueManager DLMan;
    [SerializeField] private AudioSource[] SFX;
    private bool Done = false;
    private bool Tried = false;

    #region Private DL Stuff
    [HideInInspector]
    public string[] Line;
    private float[] Speed;
    private AudioSource[] Voice;
    private AudioSource[] Noise;
    private Animator[] TalkAnim;
    private int LineNum;
    #endregion

    public void WannaDrink()
    {
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        Player.MyRigidBody.velocity = Vector2.zero;
        StartCoroutine(Drink());
    }

    IEnumerator Drink()
    {
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "It’s “Teo G,” the world's most popular hard soda.";
        Line[1] = "Do you want to try it?";
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
        BLines.gameObject.SetActive(true);
        BLines.Show(180f, 0.2f);
        if (!Tried)
        {
            DLMan.LeftString = "[TAKE ONE]";
            Description[0].text = "";
            DLMan.RightString = "[TAKE NONE]";
            Description[1].text = "";

            DLMan.ChoiceStarter();
            while (BChose.ButtonChoice == 0) { yield return null; }
            BLines.Hide(0.5f);

            if (BChose.ButtonChoice == 1)
            {
                //Drink
                yield return new WaitForSeconds(1f);
                TheoAnim.Play("Teo_G_Open");
                yield return new WaitForSeconds(3f);
                TheoAnim.Play("Teo_G_Empty");
                Player.MyAnim.Play("Drink 1");
                yield return new WaitForSeconds(1f);
                Player.MyAnim.Play("Drink 2");
                Done = false;
                Harp.Play();
                Cutscene[0].Play();
                while (Done == false) { yield return null; }
                Cutscene[0].Pause();
                yield return new WaitForSeconds(1f);

                LineNum = 2;
                DeclareLines();
                #region Put things in
                for (int i = 0; i < LineNum; i++)
                {
                    Speed[i] = 0.06f;
                    Voice[i] = SFX[0];
                    TalkAnim[i] = null;
                }
                #endregion
                Line[0] = "It tastes like a mix of strawberry syrup and fermented butt sweat.";
                Line[1] = "You regret everything.";
                #region Go
                DLMan.DILines = Line;
                DLMan.DITextSpeed = Speed;
                DLMan.DIVoice = Voice;
                DLMan.Noise = Noise;
                DLMan.WhoTalks = TalkAnim;
                DLMan.DIStarter();
                #endregion
                while (DLMan.Playing) { yield return null; }
                Cutscene[0].Play();
                Player.MyAnim.Play("Idle");
                Player.CanMove = true;
                MySceneManager.CutscenePlaying = false;
                TheoAnim.Play("Teo_G_Idle");
                Tried = true;
            }
            else
            {
                LineNum = 1;
                DeclareLines();
                #region Put things in
                for (int i = 0; i < LineNum; i++)
                {
                    Speed[i] = 0.03f;
                    Voice[i] = SFX[0];
                    TalkAnim[i] = null;
                }
                #endregion
                Line[0] = "It's probably overrated anyway.";
                #region Go
                DLMan.DILines = Line;
                DLMan.DITextSpeed = Speed;
                DLMan.DIVoice = Voice;
                DLMan.Noise = Noise;
                DLMan.WhoTalks = TalkAnim;
                DLMan.DIStarter();
                #endregion
                while (DLMan.Playing) { yield return null; }
                Player.CanMove = true;
                MySceneManager.CutscenePlaying = false;
            }
        }
        else
        {
            DLMan.LeftString = "[NO, THANK YOU]";
            Description[0].text = "";
            DLMan.RightString = "[NO, THANK YOU]";
            Description[1].text = "";
            DLMan.ChoiceStarter();
            while (BChose.ButtonChoice == 0) { yield return null; }
            BLines.Hide(0.5f);

            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.03f;
                Voice[i] = SFX[0];
                TalkAnim[i] = null;
            }
            #endregion
            Line[0] = "Good call. Good call.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            Player.CanMove = true;
            MySceneManager.CutscenePlaying = false;
        }
    }

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
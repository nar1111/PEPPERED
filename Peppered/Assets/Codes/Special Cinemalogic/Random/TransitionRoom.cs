using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TransitionRoom : MonoBehaviour
{
    #pragma warning disable 649
    public SaveSystem SS;
    [SerializeField]
    private MySceneManager Sceneman;
    [SerializeField]
    private DialogueManager_SPECIAL DLMan;
    [SerializeField]
    private AudioSource[] Narrator;
    [SerializeField]
    private Image WhiteTransition;
    private MySceneManager SceneMan;

    private Color WhtClr;
    private string[] Line;
    private float[] Speed;
    private AudioSource[] Voice;
    private int LineNum;

    // Start is called before the first frame update
    void Start()
    {
        SceneMan = FindObjectOfType<MySceneManager>();
        //Office Old Wheelchair
        if ( MySceneManager.Regret == -1 ) { StartCoroutine(OfficeEnd1()); }

        //Office guillotine
        else if ( MySceneManager.Regret == -2 ) { StartCoroutine(OfficeEnd2()); }
        else if ( MySceneManager.Regret == -3 ) { StartCoroutine(OfficeEnd3()); }
    }

    IEnumerator OfficeEnd1()
    {
        if (MySceneManager.Merdeka == 0)
        {
            MySceneManager.Merdeka = -1;
            MySceneManager.Regret = 0;
            MySceneManager.Abyss_State = 0;
            MySceneManager.CheckPointLvlName = null;
            WHAT_HAVE_I_DONE.Lives = 1;
            WHAT_HAVE_I_DONE.StarNum = 0;
            WHAT_HAVE_I_DONE.MaxStars = 100;
            WHAT_HAVE_I_DONE.Collectibles.Clear();
            WHAT_HAVE_I_DONE.Coins = 0;
            SS.Save();
        }

        SceneMan.MChange(5, 0.8f);
        SceneMan.MPlay();
        yield return new WaitForSeconds(1f);

        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = Narrator[0];
        }
        #endregion
        Line[0] = "At the end of your 50 year-long enslavement, you couldn't help but wonder.";
        Line[1] = "If life could have been different.";
        Line[2] = "Would things have been better if somebody had stopped the God of Death all those years ago?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        yield return new WaitForSeconds(1f);

        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = Narrator[0];
        }
        #endregion
        Line[0] = "You didn't wonder for long though.";
        Line[1] = "Because as you lay there with your broken neck,";
        Line[2] = "you knew";
        Line[3] = "at the very least, that there\nwas nothing you could have done.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        yield return new WaitForSeconds(2f);
        SceneMan.MStop();

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.06f;
            Voice[i] = Narrator[0];
        }
        #endregion
        Line[0] = "And also because you were dead.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        SceneMan.JumpCut("[Credits]");
    }

    IEnumerator OfficeEnd2()
    {
        if (MySceneManager.Merdeka == 0)
        {
            MySceneManager.Merdeka = -1;
            MySceneManager.Regret = 0;
            MySceneManager.Abyss_State = 0;
            MySceneManager.CheckPointLvlName = null;
            WHAT_HAVE_I_DONE.Lives = 1;
            WHAT_HAVE_I_DONE.StarNum = 0;
            WHAT_HAVE_I_DONE.MaxStars = 100;
            WHAT_HAVE_I_DONE.Collectibles.Clear();
            SS.Save();
        }

        SceneMan.MChange(5, 0.8f);
        SceneMan.MPlay();
        yield return new WaitForSeconds(1f);

        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = Narrator[0];
        }
        #endregion
        Line[0] = "While putting your head in the guillotine, you couldn't help but wonder.";
        Line[1] = "If life could life have been different.";
        Line[2] = "Would things have been better if somebody had stopped the God of Death all those years ago?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        yield return new WaitForSeconds(1f);

        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = Narrator[0];
        }
        #endregion
        Line[0] = "You didn't wonder for long though.";
        Line[1] = "Because at the end of the day,";
        Line[2] = "you knew";
        Line[3] = "at the very least, that there was nothing you could have done.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        yield return new WaitForSeconds(1f);

        SceneMan.MStop();
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.06f;
            Voice[i] = Narrator[0];
        }
        #endregion
        Line[0] = "And also because you were dead.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        SceneMan.JumpCut("[Credits]");
    }

    IEnumerator OfficeEnd3()
    {
        if (MySceneManager.Merdeka == 0)
        {
            MySceneManager.Merdeka = -1;
            MySceneManager.Regret = 0;
            MySceneManager.Abyss_State = 0;
            MySceneManager.CheckPointLvlName = null;
            WHAT_HAVE_I_DONE.Lives = 1;
            WHAT_HAVE_I_DONE.StarNum = 0;
            WHAT_HAVE_I_DONE.MaxStars = 100;
            WHAT_HAVE_I_DONE.Collectibles.Clear();
            SS.Save();
        }

        SceneMan.MChange(5, 0.8f);
        SceneMan.MPlay();
        yield return new WaitForSeconds(1f);

        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = Narrator[0];
        }
        #endregion
        Line[0] = "While the crowd of cultists dragged you to the execution, you couldn't help but wonder.";
        Line[1] = "Could your life have been different?";
        Line[2] = "Would things have been better if somebody had stopped the God of Death all those years ago?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        yield return new WaitForSeconds(1f);

        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = Narrator[0];
        }
        #endregion
        Line[0] = "You didn't wonder for long.";
        Line[1] = "Because at the end of the day,";
        Line[2] = "you knew";
        Line[3] = "at the very least, that there was\nnothing you could have done.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        yield return new WaitForSeconds(1f);

        SceneMan.MStop();
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.06f;
            Voice[i] = Narrator[0];
        }
        #endregion
        Line[0] = "And also because you were dead.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        SceneMan.JumpCut("[Credits]");
    }

    void DeclareLines()
    {
        Line = new string[LineNum];
        Speed = new float[LineNum];
        Voice = new AudioSource[LineNum];
    }
}
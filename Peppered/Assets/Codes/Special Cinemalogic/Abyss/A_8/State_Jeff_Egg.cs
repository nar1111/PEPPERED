using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;

public class State_Jeff_Egg : State
{
#pragma warning disable 649
    //1 = Pepper
    //2 = Coffee Chum
    private int CurrentState = 0;

    [Header("WINDOWS CUTSCENE")]
    [SerializeField] private AUDIOMANAGER AudiMan;
    [SerializeField] private Black_lines BLines;
    [SerializeField] private PlayableDirector[] Cutscene;
    [SerializeField] private GameObject[] Stuff;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField] private Animator[] CharAnim;
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private DialogueManager DLMan;
    [SerializeField] private AudioSource[] SFX;
    private MySceneManager SceneMan;
    private bool Done = false;

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
        if (CurrentState == 0) { CurrentState = 1; WHAT_HAVE_I_DONE.Collectibles.Add("Jeff", 2);  StartCoroutine(GiveBirth()); }

        return this;
    }

    IEnumerator GiveBirth()
    {
        if (SceneMan == null) {SceneMan = FindObjectOfType<MySceneManager>(); }
        AudiMan.ChangePitch("Scream", 0.9f);
        AudiMan.Play("Scream");
        yield return new WaitForSeconds(2f);
        SceneMan.MPause();
        AudiMan.StopIt("Scream");
        AudiMan.ChangePitch("Scream", 1f);
        AudiMan.Play("Dramatic Impact");
        AudiMan.Play("Boop");
        CharAnim[0].Play("Jeff_Shocked");
        CharAnim[1].Play("Shaker_Idle");
        CharAnim[2].Play("Death_End");
        yield return new WaitForSeconds(2f);
        Done = false;
        Cutscene[0].Play();
        while(Done == false) { yield return null; }

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.07f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "— Oh my gods. It's a baby.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        CharAnim[0].Play("Jeff_Happy");
        SceneMan.MPlay();

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.07f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "— I gave birth! It’s a miracle!";
        Line[1] = "— Don’t worry, little guy.\nI'll take good care of you!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        Stuff[0].SetActive(false);
        Stuff[1].SetActive(true);
        AudiMan.Play("Put Down");
        SceneMan.MPause();
        yield return new WaitForSeconds(1f);

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.07f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "— In fact, I’ll check how much you\nhave to pay for rent this instant!";
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
        Cutscene[1].Play();
        while (Done == false) { yield return null; }

        Stuff[2].SetActive(false);
        BLines.Hide(1f);
        Player.CanMove = true;
        MySceneManager.CutscenePlaying = false;
        SceneMan.MPlay();
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine;
public class State_Jeff_Detect : State
{
    [HideInInspector]public int CurrentState;

    [Header("WINDOWS CUTSCENE")]
    [SerializeField] private AUDIOMANAGER AudiMan;
    [SerializeField] private Black_lines BLines;
    [SerializeField] private PlayableDirector[] Cutscene;
    [SerializeField] private GameObject Cam;


    [Header("--DIALOGUE STUFF--")]
    [SerializeField] private Animator[] CharAnim;
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private DialogueManager DLMan;
    [SerializeField] private AudioSource[] SFX;

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
        if (CurrentState == 0) { CurrentState = 1; WHAT_HAVE_I_DONE.Collectibles.Add("Jeff", 1); StartCoroutine(Detected()); }
        return this;
    }

    IEnumerator Detected()
    {
        yield return new WaitForSeconds(1.5f);
        Player.gameObject.transform.localScale = new Vector3(-1,1,1);

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "—  I HAVE!!! PUT up a WARNING!!!";
        Line[1] = "— You!!! Didn't Pay!!!\nATTENTION!!!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        CharAnim[0].Play("Jeff_Idle");
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.06f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "— Unfortunately, I must inform you that national television will hear of this breach of regulations.";
        Line[1] = "— Any inconvenience caused is regretted.\nThank you for your time.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        yield return new WaitForSeconds(0.2f);

        CharAnim[0].Play("Jeff_Annoyed_1");
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "— ASSHOLE!!!!!";
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

        Done = false;
        Cutscene[0].Play();
        while (Done == false) { yield return null; }

        AudiMan.Play("Busted");
        Cam.SetActive(false);
        BLines.Hide(1f);
        Player.CanMove = true;
        MySceneManager.CutscenePlaying = false;
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
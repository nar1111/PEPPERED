using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_BugMom_Freakout : State
{
    [Header("--DIALOGUE STUFF--")]
    [SerializeField] private Animator[] CharAnim;
    [SerializeField] private AudioSource[] SFX;
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private DialogueManager DLMan;

    [Header("--CINEMATIC--")]
    [SerializeField] private GameObject BUGMOM;
    [SerializeField] private GameObject Cam;
    [SerializeField] private Transform FlyPoint;
    [SerializeField] private Black_lines BLines;
    [SerializeField] private AUDIOMANAGER AudioMan;
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
        if (MyStage == 0)
        {
            MyStage = 1;
            StartCoroutine(FreakOut());
        } else if (MyStage == 3 && Vector2.Distance(BUGMOM.transform.position, FlyPoint.transform.position) > 0.2f)
        {
            BUGMOM.transform.position = Vector2.MoveTowards(BUGMOM.transform.position, FlyPoint.transform.position, 3f * Time.deltaTime);
        }
        return this;
    }

    IEnumerator FreakOut()
    {
        WHAT_HAVE_I_DONE.Collectibles.Add("BugMom", 1);
        BLines.Show(220f, 1.5f);
        MySceneManager.CutscenePlaying = true;
        Cam.SetActive(true);
        yield return new WaitForSeconds(1.5f);

        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "— You stay away from my kids, you hear?";
        Line[1] = "— They ain't some Life Stars for you to steal!";
        Line[2] = "— They're MY Life Stars to tell bullshit about vaccinations!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        BLines.Hide(.5f);
        Cam.SetActive(false);
        MyStage = 3;
        AudioMan.Play("Busted");

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "— Imma take this to the national television, you hear?";
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

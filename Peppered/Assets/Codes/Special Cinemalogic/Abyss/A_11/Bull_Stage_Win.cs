using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bull_Stage_Win : State
{

    private int MyStage = 0;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField] private Animator[] CharAnim;
    [SerializeField]
    private PLAYER_CONTROLS Player;
    [SerializeField]
    private DialogueManager DLMan;
    [SerializeField]
    private Black_lines BLines;
    [SerializeField]
    private AudioSource[] SFX;
    [SerializeField]
    private AUDIOMANAGER AudioMan;
    [SerializeField]
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
        if (MyStage == 0)
        {
            MyStage = 1;
            Player.CanMove = false;
            MySceneManager.CutscenePlaying = true;
            Player.MyRigidBody.velocity = Vector2.zero;
            Player.transform.localScale = new Vector3(1,1,1);
            BLines.Show(180f, 2f);
            StartCoroutine(YouWon());
        }

        return this;
    }

    IEnumerator YouWon()
    {
        yield return new WaitForSeconds(3f);
        CharAnim[0].Play("Bull Bro 4 Salute");
        yield return new WaitForSeconds(1.8f);
        CharAnim[0].Play("Bull Bro 4 Salute 2");
        yield return new WaitForSeconds(2.2f);
        CharAnim[1].Play("Bull Bro 3 Stand");
        CharAnim[1].transform.localScale = new Vector3(-1,1,1);


        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.05f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "— Alright, ST, you got skill. We respect that.";
        Line[1] = "— We won't beat you up for stealing. 'cause we respect you.";
        Line[2] = "— So instead...";
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

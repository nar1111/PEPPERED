using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class The_Jeff_Egg : MonoBehaviour
{
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private SpriteRenderer MyRen;
    [SerializeField] private Sprite Cracked;
    [HideInInspector]public int EggStage = 0;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField] private AUDIOMANAGER AudiMan;
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

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(Player.gameObject.transform.position, transform.position) < 0.6f && Player.MyRigidBody.velocity.y < -0.1f && EggStage == 0)
        {
            EggStage = 1;
            Player.MyRigidBody.velocity = Vector2.zero;
            Player.Bounce(8f, 1);
            AudiMan.Play("Crush");
            MyRen.sprite = Cracked;
        }

        if (Vector2.Distance(Player.gameObject.transform.position, transform.position) < 0.6f && MySceneManager.Act == true)
        {
            if (EggStage == 0) { DialogueEgg(); } else { DialogueCracked();}
        }
    }

    void DialogueEgg()
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
        Line[0] = "The egg naturally incubates in the warmth\nof not paying the security deposit.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
    }

    void DialogueCracked()
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
        Line[0] = "Dude.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
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

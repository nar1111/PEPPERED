using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_10 : MonoBehaviour
{
    [Header("--DIALOGUE STUFF--")]
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private PLAYER_CONTROLS Player;
    [SerializeField]
    private DialogueManager DLMan;
    [SerializeField]
    private AUDIOMANAGER AudioMan;
    [SerializeField]
    private AudioSource[] SFX;
    [SerializeField]
    private Black_lines Blines;

    #region Private DL Stuff
    [HideInInspector]
    public string[] Line;
    private float[] Speed;
    private AudioSource[] Voice;
    private AudioSource[] Noise;
    private Animator[] TalkAnim;
    private int LineNum;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (MySceneManager.Abyss_State < 11) { MySceneManager.Abyss_State = 11; }
    }

    public void Bug1()
    {
        if (WHAT_HAVE_I_DONE.Collectibles.ContainsKey("BugMom"))
        {
            LineNum = 3;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "— She gone?";
            Line[1] = "— No, Imma stop hiding when it's safe.";
            Line[2] = "— Like, when I'm 18 or something.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        } else
        {
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.08f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
;
            }
            #endregion
            Line[0] = "— Yo, don't tell her I'm here.";
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

    public void Bug2()
    {
        if (WHAT_HAVE_I_DONE.Collectibles.ContainsKey("BugKids") == false)
        {
            MySceneManager.CutscenePlaying = true;
            Blines.Show(150f, 1.5f);
            StartCoroutine(Bugsted());
        }
    }

    IEnumerator Bugsted()
    {
        yield return new WaitForSeconds(3.5f);
        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "— Aight, star thief - I'm gonna cut you a deal.";
        Line[1] = "— You walk away, and don't rat us out.";
        Line[2] = "— And I'll put a good word for you on TikTok, okay?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        Blines.Hide(1f);
        MySceneManager.CutscenePlaying = false;
        WHAT_HAVE_I_DONE.Collectibles.Add("BugKids", 1);
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

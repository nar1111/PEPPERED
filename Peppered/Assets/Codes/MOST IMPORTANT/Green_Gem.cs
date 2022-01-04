using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green_Gem : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private AudioSource[] SFX;
    [SerializeField] private string MyName;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private PLAYER_CONTROLS Player;
    [SerializeField]
    private DialogueManager DLMan;
    //[SerializeField]
    //private Black_lines BLines;

    [SerializeField] private GameObject Changes;

    #region Private DL Stuff
    [HideInInspector]
    public string[] Line;
    private float[] Speed;
    private AudioSource[] Voice;
    private AudioSource[] Noise;
    private Animator[] TalkAnim;
    private int LineNum;
    #endregion

    void Start()
    {
        if (WHAT_HAVE_I_DONE.GreenGem.ContainsKey(MyName)) { Changes.SetActive(true); }
    }

    public void GreenGemStart()
    {
        if (WHAT_HAVE_I_DONE.GreenGem.Count == 0)
        {
            StartCoroutine(GreenGem0());
        }
        else if (WHAT_HAVE_I_DONE.GreenGem.Count == 1)
        {
            StartCoroutine(GreenGem1());
        }
        WHAT_HAVE_I_DONE.GreenGem.Add(MyName, 1);
    }

    IEnumerator GreenGem0()
    {
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < Speed.Length; i++)
        {
            Speed[i] = 0.05f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- You will never understand\nmy pain and suffering.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing == true) { yield return null; }

        CharAnim[0].Play("Green Gem Dissolve");
    }

    IEnumerator GreenGem1()
    {
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < Speed.Length; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- For my mind has undoubtedly perceived and seen through the monumental deceit.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing == true) { yield return null; }

        CharAnim[0].Play("Green Gem Dissolve");
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
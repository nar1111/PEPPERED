using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bottom_of_the_abyss : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private AUDIOMANAGER Audiman;
    [SerializeField] private Animator TransitionAnim;
    [SerializeField] private GameObject Dial;
    //[SerializeField] private Text[] Description;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private DialogueManager DLMan;
    [SerializeField]
    private Button_Choose BChose;
    [SerializeField]
    private AudioSource[] SFX;

    #region Private DL Stuff
    [HideInInspector]
    public string[] Line;
    private float[] Speed;
    private AudioSource[] Voice;
    private AudioSource[] Noise;
    private Animator[] TalkAnim;
    private int LineNum;
    #endregion

    public void Landed()
    {
        Audiman.Play("Dramatic Impact");
        Player.MyRigidBody.gravityScale = 1.6f;
        TransitionAnim.Play("Death_End", 0, 0);
    }

    public void Hand(){ StartCoroutine(GodHand());  }

    public void Gemmed() { Dial.SetActive(true); }

    IEnumerator GodHand()
    {
        MySceneManager.CutscenePlaying = true;
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.07f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "You take a hard look at the wrist.";
        Line[1] = "It has a pulse.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        //Description[0].text = "";
       // Description[1].text = "";
        DLMan.LeftString = "[TOUCH THE HAND]";
        DLMan.RightString = "[RETURN]";
        DLMan.ChoiceStarter();
        while (BChose.ButtonChoice == 0) { yield return null; }

        if (BChose.ButtonChoice == 1)
        {
           MySceneManager.CutscenePlaying = false;
           Player.Death(2, 2);
        } else { MySceneManager.CutscenePlaying = false; }
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

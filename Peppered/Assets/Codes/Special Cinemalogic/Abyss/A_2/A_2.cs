using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_2 : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private AudioSource[] SFX;
    [SerializeField]
    private GameObject[] Things;
    [SerializeField]
    private Merdeka_Follows MerF;
    [SerializeField]
    private GameObject TeleportEffect;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private PLAYER_CONTROLS Player;
    [SerializeField]
    private DialogueManager DLMan;
    [SerializeField]
    private Black_lines BLines;
    private MySceneManager SceneMan;

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
        SceneMan = FindObjectOfType<MySceneManager>();
        if (MySceneManager.Abyss_State < 1) { MySceneManager.Abyss_State = 1; }
        else if (MySceneManager.Abyss_State > 1)
        {
            for (int i = 0; i < 8; i++)
            {
                Things[i].SetActive(false);
            }
        }

        if (MySceneManager.Merdeka == 1)
        {
            Things[0].transform.position = MerF.MoveSpots[0].transform.position;
            Things[1].SetActive(false);
            Things[4].SetActive(false);
            Things[5].SetActive(false);
            Things[6].SetActive(false);
            Things[7].SetActive(false);
        }
    }

    private void Update()
    {
        if (MySceneManager.Merdeka == 0 && Player.Dead > 0)
        {
            //Check if Merdeka is on first place
            if (Things[0].transform.position != MerF.MoveSpots[0].transform.position)
            {
                MySceneManager.Merdeka = 1;
                Things[1].SetActive(true);
            }
        }
    }

    public void MerdekaReaction(){ StartCoroutine(MerReact()); }

    IEnumerator MerReact()
    {
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        Player.MyRigidBody.velocity = Vector2.zero;
        if (Things[2] != null)
        {
            Things[2].SetActive(true);
            yield return new WaitForSeconds(1f);
        }

        MySceneManager.Merdeka = 2;
        BLines.Show(150f, 1f);

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
        Line[0] = "- I have all day, suspect.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        CharAnim[0].Play("TakeRadio");
        yield return new WaitForSeconds(1.4f);

        LineNum = 2;
        DeclareLines();
        Speed[0] = 0.04f;
        Speed[1] = 0.19f;

        Voice[0] = SFX[1];
        Voice[1] = SFX[0];

        TalkAnim[0] = CharAnim[0];
        TalkAnim[1] = null;

        Line[0] = "- Chief? I DO have all day, right?\nFor this case?";
        Line[1] = "- Y E S";

        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        CharAnim[0].Play("Idle1");

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- See?";
        if (WHAT_HAVE_I_DONE.GreenGem.ContainsKey("Gem3")) { Line[1] = "— Spontaneously combust into the ether all you like."; }
        else { Line[1] = "— Die as much as you like."; }
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        BLines.Hide(1f);
        if (Things[2] != null)
        {
            Things[2].SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        MySceneManager.CutscenePlaying = false;
        Player.CanMove = true;
    }

    public void SecretLocationTrg(){ StartCoroutine(SecretLoc()); }

    IEnumerator SecretLoc()
    {
        SceneMan = FindObjectOfType<MySceneManager>();
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        Player.MyRigidBody.velocity = Vector2.zero;

        while (Player.Grounded == false)
        {
            yield return null;
        }

        SceneMan.MPause();
        Instantiate(TeleportEffect, Player.gameObject.transform.position, Quaternion.identity);
        Player.MyAnim.Play("Invisible");
        CharAnim[0].Play("Starteled");

        yield return new WaitForSeconds(4f);
        Things[8].SetActive(true);
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
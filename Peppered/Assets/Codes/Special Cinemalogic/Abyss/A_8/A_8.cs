using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_8 : MonoBehaviour
{
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private GameObject[] TurnThingsOff;
    [SerializeField] private GameObject Nosy;
    [SerializeField] private GameObject Jeff;
    [SerializeField] private Animator[] WindowAnims;
    [SerializeField] private GameObject[] Cam;
    [SerializeField] private AUDIOMANAGER Audiman;
    [SerializeField] private DialogueManager DLMan;
    [SerializeField] private AudioSource[] SFX;
    [SerializeField] private GameObject[] Stuff;
    [SerializeField] private Animator HostAnim;
    [SerializeField] private Transform[] EnterExit;
    private MySceneManager Sceneman;
    private int WindowChecker = 0;

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
        if (WHAT_HAVE_I_DONE.Collectibles.ContainsKey("Nosy")) { Destroy(Nosy); TurnThingsOff[0].SetActive(true); }
        if (WHAT_HAVE_I_DONE.Collectibles.ContainsKey("Jeff")) { Destroy(Jeff); }

        if (MySceneManager.Abyss_State < 9) { MySceneManager.Abyss_State = 9; }
        else if (MySceneManager.Abyss_State > 9)
        {
            WindowChecker = 1;
            for (int i = 0; i < WindowAnims.Length; i++)
            {
                WindowAnims[i].Play("Window_Closed");
            }

            for (int i = 1; i < 7; i++)
            {
                Destroy(TurnThingsOff[i]);
            }
        }
    }

    public void WindowCheck()
    {
        if (WindowChecker == 0)
        {
            WindowChecker = 1;
            StartCoroutine(LookInside());
        }
    }

    IEnumerator LookInside()
    {
        MySceneManager.CutscenePlaying = true;
        if (Sceneman == null) { Sceneman = FindObjectOfType<MySceneManager>(); }

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
        Line[0] = "You peek inside a window.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        Cam[0].SetActive(true);
        yield return new WaitForSeconds(1f);

        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = HostAnim;
        }
        #endregion
        Line[0] = "— Joining us at the studio is a special guest with an important message. Officer?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        Audiman.Play("Dramatic Impact");
        HostAnim.Play("TV_Host_M");
        yield return new WaitForSeconds(1f);

        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.05f;
            Voice[i] = SFX[1];
            TalkAnim[i] = HostAnim;
        }
        #endregion
        Line[0] = "— Happy Immortality day, citizens.";
        Line[1] = "— Stopping the star thief is a responsibility.";
        Line[2] = "— Everyone's responsibility.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        Cam[1].SetActive(true);
        Audiman.ChangePitch("Dramatic Impact", 0.8f);
        Audiman.Play("Dramatic Impact");
        Sceneman.Music.pitch = 0.8f;


        Line[0] = "— It is our duty. Report ANY information about this criminal.";
        Line[1] = "— Leave no place for them to go.";
        Line[2] = "— Until they are forced to give up.";
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
        yield return new WaitForSeconds(1f);
        Sceneman.Music.pitch = 1f;
        Cam[1].SetActive(false);
        yield return new WaitForSeconds(1f);
        //HostAnim.Play("TV_Host_Idle");

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = HostAnim;
        }
        #endregion
        Line[0] = "— Keep your eyes peeled. Thank you for your cooperation.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        Cam[0].SetActive(false);
        Audiman.ChangePitch("Dramatic Impact", 1f);
        MySceneManager.CutscenePlaying = false;
    }

    public void EnterApartment()
    {
        if (Sceneman == null) { Sceneman = FindObjectOfType<MySceneManager>(); }

        Sceneman.MPause();
        Audiman.ChangePitch("Wind Long", 0.7f);
        Audiman.Play("Wind Long");
        Player.transform.position = EnterExit[0].position;
        Player.transform.localScale = new Vector3(-1, 1, 1);
        Player.MyAnim.SetBool("Wind", false);
        Player.Wind = false;
    }

    public void ExitApartment()
    {
        Sceneman.MPlay();
        Audiman.StopIt("Wind Long");
        Audiman.ChangePitch("Wind Long", 1f);
        Player.transform.position = EnterExit[1].position;
        Sceneman.Music.volume = Sceneman.Music.volume + 0.4f;
        Player.MyAnim.SetBool("Wind", true);
        Player.Wind = true;
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
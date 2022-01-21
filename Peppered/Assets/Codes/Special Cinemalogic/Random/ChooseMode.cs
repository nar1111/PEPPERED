using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseMode : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private GameObject MainText;
    [SerializeField]
    private GameObject LeftSide;
    [SerializeField]
    private GameObject RightSide;
    [SerializeField]
    private GameObject LeftText;
    [SerializeField]
    private GameObject RightText;
    [SerializeField]
    private GameObject LeftButton;
    [SerializeField]
    private GameObject RightButton;
    [SerializeField]
    private GameObject WhiteLine;

    public Button_Choose BChos;

    [Header ("Audio 'n' shit")]
    private int what = 3;
    private float Timer = 0f;

    [Header("Put Dialogue stuff in here")]
    [SerializeField]
    private Text Ltxt;
    [SerializeField]
    private Text Rtxt;
    public AudioSource VoiceClip;
    public DialogueManager_SPECIAL DLMan;
    public MySceneManager SceneMan;

    private int PressNum;
    private string[] Line;
    private float[] Speed;
    private AudioSource[] Voice;


    private void Start()
    {
        Timer = 3f;

        //ES3.DeleteFile("SaveFile.es3");
    }

    void Update()
    {
      
       // if (Timer > 0) { Timer -= Time.deltaTime; }
       // if (Timer <= 0 && what == 3) { what = 4; Greeting(); }
        if (PressNum == 15)
        {
            Ltxt.text = "Take your time.";
            Rtxt.text = "Take your time.";
        }
        else if (PressNum == 30)
        {
            Ltxt.text = "...";
            Rtxt.text = "...";
        }

        if (what == 4 && DLMan.Playing == false) 
        {
            what = 0;
            LeftButton.SetActive(true);
            RightButton.SetActive(true);
            WhiteLine.SetActive(true);
        }

        #region Choose
        if (what > 0)
        {
            if (Input.GetAxisRaw("Horizontal") < 0f && what == 2)
            {

                PressNum++;
                LeftSide.SetActive(true);
                RightSide.SetActive(false);
                what = 1;
                LeftText.SetActive(true);
                RightText.SetActive(false);
                //HappyMusic.Play();
                //Wind.Pause();
            }

            else if (Input.GetAxisRaw("Horizontal") > 0f && what == 1)
            {

                PressNum++;
                LeftSide.SetActive(false);
                RightSide.SetActive(true);
                what = 2;
                LeftText.SetActive(false);
                RightText.SetActive(true);
               // HappyMusic.Pause();
               // Wind.Play();

            }
        } else if (what == 0) 
        {
            if (Input.GetAxisRaw("Horizontal") < 0f)
            {
                BChos.ButtonChoice = 0;
                LeftSide.SetActive(true);
                RightSide.SetActive(false);
                what = 1;
                LeftText.SetActive(true);
                RightText.SetActive(false);
               // HappyMusic.Play();
               // Wind.Pause();
            }

            else if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                BChos.ButtonChoice = 0;
                LeftSide.SetActive(false);
                RightSide.SetActive(true);
                what = 2;
                LeftText.SetActive(false);
                RightText.SetActive(true);
              //  HappyMusic.Pause();
              // Wind.Play();

            }

        }
        #endregion

        if (BChos.ButtonChoice > 0 && what > 0 && what < 3)
        {
            what = 6;
            LeftButton.SetActive(false);
            RightButton.SetActive(false);
            WhiteLine.SetActive(false);
            LeftSide.SetActive(false);
            RightSide.SetActive(false);
            LeftText.SetActive(false);
            RightText.SetActive(false);

           // Wind.Stop();
           // HappyMusic.Stop();
           // Press.Play();
            Okay();
        }

        if (what == 6 && DLMan.Currentline == 1)
        {
            DLMan.ControlOverDialogue = false;
            StartCoroutine(EndCustcene());

        }
    }

    #region Lines
    void Greeting() 
    {
        float[] Speed = new float[3];
        AudioSource[] Voice = new AudioSource[3];
        string[] Line = new string[3];

        for (int i = 0; i < Speed.Length; i++) { Speed[i] = 0.06f; }
        for (int i = 0; i < Voice.Length; i++) { Voice[i] = VoiceClip; }

        Line[0] = "Greetings";
        Line[1] = "Before starting to play";
        Line[2] = "Please, choose a game mode";

        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.DILines = Line;
        DLMan.DIStarter();
    }

    void Okay()
    {
        float[] Speed = new float[2];
        AudioSource[] Voice = new AudioSource[2];
        string[] Line = new string[2];

        Speed[0] = 0.06f;
        Speed[1] = 0.06f;

        for (int i = 0; i < Voice.Length; i++) { Voice[i] = VoiceClip; }

        Line[0] = "Very well";
        Line[1] = "This and all the next choices are on you";

        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.DILines = Line;
        DLMan.DIStarter();
        MySceneManager.Regret = 1;
    }
    #endregion

    #region FADE, BRAH
    public IEnumerator EndCustcene()
    {
        yield return new WaitForSeconds(5f);
        MainText.SetActive(false);
        yield return new WaitForSeconds(1f);
        SceneMan.JumpCut("A_TB2");
    }
    #endregion


}

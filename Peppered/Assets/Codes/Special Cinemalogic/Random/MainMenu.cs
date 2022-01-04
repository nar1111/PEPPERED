using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private GameObject Menu;
    [SerializeField]
    private LoadSystem LS;
    [SerializeField]
    private Text Lefttxt;
    [SerializeField]
    private Text Righttxt;
    [SerializeField]
    private AudioSource Impact;
    public Button_Choose BChos;

    [Header("Put Dialogue stuff in here")]
    public AudioSource VoiceClip;
    public DialogueManager_SPECIAL DLMan;
    public MySceneManager SceneMan;
    private string[] Line;
    private float[] Speed;
    private AudioSource[] Voice;

    private void Start()
    {
        if (ES3.FileExists("SaveFile.es3"))
        {
            LS.Load();
            if (LS.OldSave == 0)
            {
                if (MySceneManager.Merdeka == -1 && MySceneManager.Regret == 0) { StartCoroutine(StartGameSecondTime()); }
                else { SceneMan.LoadJumpCut(); }
            }
            else
            {
                //Outdated save file. Delete start over.
                LS.OldSave = 0;
                MySceneManager.Merdeka = 0;
                MySceneManager.Regret = 0;
                MySceneManager.Abyss_State = 0;
                MySceneManager.CheckPointLvlName = null;
                WHAT_HAVE_I_DONE.Lives = 1;
                WHAT_HAVE_I_DONE.StarNum = 0;
                WHAT_HAVE_I_DONE.MaxStars = 100;
                WHAT_HAVE_I_DONE.Collectibles.Clear();
                SceneMan.JumpCut("Office_1");
            }
        }

        else { SceneMan.JumpCut("Office_1"); }
    }

    IEnumerator StartGameSecondTime()
    {
        yield return new WaitForSeconds( 1.5f );
        SecondTime();
        while (DLMan.Playing == true) { yield return null; }
        yield return new WaitForSeconds( 1f );
        SceneMan.JumpCut( "Office_1" );
    }

    void SecondTime()
    {
        float[] Speed = new float[1];
        AudioSource[] Voice = new AudioSource[1];
        string[] Line = new string[1];

        for (int i = 0; i < Speed.Length; i++) { Speed[i] = 0.06f; }
        for (int i = 0; i < Voice.Length; i++) { Voice[i] = VoiceClip; }

        Line[0] = "If only things could \nhave been different, huh?";

        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.DILines = Line;
        DLMan.DIStarter();
    }

    #region Load Delete
    IEnumerator LoadDelete()
    {
        Menu.SetActive(true);
        while (BChos.ButtonChoice == 0) { yield return null; }

        Menu.SetActive(false);
        if (BChos.ButtonChoice == 1)
        {
            float[] Speed = new float[1];
            AudioSource[] Voice = new AudioSource[1];
            string[] Line = new string[1];

            for (int i = 0; i < Speed.Length; i++) { Speed[i] = 0.06f; }
            for (int i = 0; i < Voice.Length; i++) { Voice[i] = VoiceClip; }

            Line[0] = "Very well.";

            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.DILines = Line;
            DLMan.DIStarter();

            while (DLMan.Playing == true) { yield return null; }

            SceneMan.LoadJumpCut();
        }

        else if (BChos.ButtonChoice == 2)
        {
            BChos.ButtonChoice = 0;
            float[] Speed = new float[2];
            AudioSource[] Voice = new AudioSource[2];
            string[] Line = new string[2];
            for (int i = 0; i < Speed.Length; i++) { Speed[i] = 0.06f; }
            for (int i = 0; i < Voice.Length; i++) { Voice[i] = VoiceClip; }

            Line[0] = "Deleted save data can't be restored.";
            Line[1] = "Are you sure you want this?";

            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.DILines = Line;
            DLMan.DIStarter();

            while (DLMan.Playing == true) { yield return null; }


            Menu.SetActive(true);
            Lefttxt.text = "YES. I WANT\nANOTHER SHOT";
            Righttxt.text = "CANCEL";

            while (BChos.ButtonChoice == 0) { yield return null; }
            Menu.SetActive(false);

            //DELETE EVERYTHING
            if (BChos.ButtonChoice == 1)
            {

                Line[0] = "...";
                Line[1] = "Very well.";

                DLMan.DITextSpeed = Speed;
                DLMan.DIVoice = Voice;
                DLMan.DILines = Line;
                DLMan.DIStarter();

                while (DLMan.Playing == true) { yield return null; }

                ES3.DeleteFile("SaveFile.es3");
                MySceneManager.Merdeka = 0;
                MySceneManager.Regret = 0;
                MySceneManager.Abyss_State = 0;
                MySceneManager.CheckPointLvlName = null;
                WHAT_HAVE_I_DONE.Lives = 1;
                WHAT_HAVE_I_DONE.StarNum = 0;
                WHAT_HAVE_I_DONE.MaxStars = 100;
                WHAT_HAVE_I_DONE.Collectibles.Clear();
                yield return new WaitForSeconds(1f);

                SceneMan.JumpCut("[INTRO Mode]");
            }

            else if (BChos.ButtonChoice == 2)
            {
                for (int i = 0; i < Speed.Length; i++) { Speed[i] = 0.06f; }
                for (int i = 0; i < Voice.Length; i++) { Voice[i] = VoiceClip; }

                Line[0] = "Very well";
                Line[1] = "Where were we?";

                DLMan.DITextSpeed = Speed;
                DLMan.DIVoice = Voice;
                DLMan.DILines = Line;
                DLMan.DIStarter();

                while (DLMan.Playing == true) { yield return null; }

                SceneMan.LoadJumpCut();
            }
        }
    }
    #endregion

    #region Greetings text
    IEnumerator LetsGetThisPartyGoing()
    {
        yield return new WaitForSeconds(1f);
        //We already been here
        if (MySceneManager.Regret != 0)
        {
            Greeting();
            while (DLMan.Playing == true) { yield return null; }
            StartCoroutine(LoadDelete());

        }
        else if (MySceneManager.Regret == 0)
        {
            SceneMan.JumpCut("[INTRO Mode]");
        }
    }

    void Greeting()
    {
        float[] Speed = new float[3];
        AudioSource[] Voice = new AudioSource[3];
        string[] Line = new string[3];

        for (int i = 0; i < Speed.Length; i++) { Speed[i] = 0.06f; }
        for (int i = 0; i < Voice.Length; i++) { Voice[i] = VoiceClip; }

        Line[0] = "Welcome back.";
        Line[1] = "Where were we?";
        Line[2] = "Oh yeah. Test levels.";

        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.DILines = Line;
        DLMan.DIStarter();
    }
    #endregion
}
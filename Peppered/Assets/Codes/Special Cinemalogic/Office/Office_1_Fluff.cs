using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Office_1_Fluff : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private CHAIR_PLAYER Player;
    [SerializeField]
    private DialogueManager DLMan;
    [SerializeField]
    private AudioSource[] SFX;
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private PlayableDirector[] Cutscene;
    [SerializeField]
    private GameObject[] Things;
    [SerializeField]
    private Black_lines BLines;
    private int TVINT;
    private MySceneManager SceneMan;
    [SerializeField]
    private AUDIOMANAGER Audiman;
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

    public void Plant()
    {
        if  (SceneMan == null) { SceneMan = FindObjectOfType<MySceneManager>(); }

        if (Player.CanMove == true)
        {
            if (MySceneManager.Abyss_State == 0)
            {
                LineNum = 2;
                DeclareLines();
                #region Put things in
                for (int i = 0; i < Speed.Length; i++)
                {
                    Speed[i] = 0.03f;
                    Voice[i] = SFX[0];
                    TalkAnim[i] = null;
                }
                #endregion
                Line[0] = "It’s a zamioculcas; A flowering plant.";
                Line[1] = "Legend says that if your wife learns its name, she’ll make it your nickname for at least a few weeks.";
                #region Go
                DLMan.DILines = Line;
                DLMan.DITextSpeed = Speed;
                DLMan.DIVoice = Voice;
                DLMan.Noise = Noise;
                DLMan.WhoTalks = TalkAnim;
                DLMan.DIStarter();
                #endregion
            }

            else if (MySceneManager.Abyss_State > 0)
            {
                LineNum = 2;
                DeclareLines();
                #region Put things in
                for (int i = 0; i < Speed.Length; i++)
                {
                    Speed[i] = 0.03f;
                    Voice[i] = SFX[0];
                    TalkAnim[i] = null;
                }
                #endregion
                Line[0] = "Uh oh. This much water can’t be good for a plant.";
                Line[1] = "Ah, nevermind. It’s plastic.";
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
    }

    public void TheDudes()
    {
        if (SceneMan == null) { SceneMan = FindObjectOfType<MySceneManager>(); }

        if (Player.CanMove == true && MySceneManager.Abyss_State == 0)
        {
            if (TVINT > 0)
            {
                LineNum = 1;
                DeclareLines();
                #region Put things in
                for (int i = 0; i < Speed.Length; i++)
                {
                    Speed[i] = 0.03f;
                    Voice[i] = SFX[0];
                    TalkAnim[i] = null;
                }
                #endregion
                Line[0] = "She turned off the TV.";
                #region Go
                DLMan.DILines = Line;
                DLMan.DITextSpeed = Speed;
                DLMan.DIVoice = Voice;
                DLMan.Noise = Noise;
                DLMan.WhoTalks = TalkAnim;
                DLMan.DIStarter();
                #endregion
            }

            else if (TVINT == 0 && MySceneManager.Abyss_State == 0){StartCoroutine(TV());}

        }

        else if (Player.CanMove == true && MySceneManager.Abyss_State > 0)
        {
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < Speed.Length; i++)
            {
                Speed[i] = 0.03f;
                Voice[i] = SFX[0];
                TalkAnim[i] = null;
            }
            #endregion
            Line[0] = "No response.";
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


    IEnumerator TV()
    {
        if (SceneMan == null) { SceneMan = FindObjectOfType<MySceneManager>(); }
        MySceneManager.CutscenePlaying = true;
        SceneMan.Music.volume = SceneMan.Music.volume - 0.2f;
        Things[0].SetActive(false);
        Things[1].SetActive(true);
        Audiman.Play("News Flash");
        BLines.gameObject.SetActive(true);
        BLines.Show(220, 1f);

        TVINT = 1;

        SFX[1].pitch = 0.7f;
        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < Speed.Length; i++)
        {
            Speed[i] = 0.03f;
            TalkAnim[i] = null;
        }
        #endregion
        Voice[0] = SFX[0];
        Voice[1] = SFX[0];
        Voice[2] = SFX[0];
        Voice[3] = SFX[0];
        Line[0] = "One hour until the end of the world.";
        Line[1] = "Will our missing hero appear with the Life Star?";
        Line[2] = "Or will God of Death be free after 100 years of imprisonment?";
        Line[3] = "Find out right now why we hope he won’t.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing == true) { yield return null; }
        Things[2].SetActive(true);
        yield return new WaitForSeconds(1f);
        Cutscene[0].Play();
        //BLines.Show(150f, 1f);
        SceneMan.MPause();
        Done = false;
        Cutscene[1].Play();
        while (Done == false) { yield return null; }
        Done = false;
        yield return new WaitForSeconds(1f);
        SFX[1].pitch = 1f;

        Player.gameObject.transform.position = new Vector3(3.45f, 0.12f, 0f);
        Player.gameObject.transform.localScale = new Vector3(1,1,1);
        CharAnim[1].Play("Cynthia_TV");
        CharAnim[1].gameObject.transform.position = new Vector3 (5.5f, 0.081f, 0f);
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < Speed.Length; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "Excuse me.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing == true) { yield return null; }


        Things[1].SetActive(false);
        Things[0].SetActive(true);


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < Speed.Length; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "Could you stop wasting my time\nand get on with the test?";
        Line[1] = "Fabulous.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing == true) { yield return null; }


        CharAnim[1].Play("Idle");
        Done = false;
        Cutscene[2].Play();
        while (Done == false) { yield return null; }
        Done = false;

        SceneMan.Music.volume = SceneMan.Music.volume + 0.2f;
        BLines.Hide(0.5f);
        SceneMan.MPlay();
        CharAnim[1].gameObject.transform.position = new Vector3(10.714f, 0.081f, 0f);
        CharAnim[1].gameObject.transform.localScale = new Vector3(-1,1,1);

        yield return new WaitForSeconds(0.5f);
        MySceneManager.CutscenePlaying = false;
        Player.CanMove = true;
        MySceneManager.CutscenePlaying = false;

    }

    void DeclareLines()
     {
         Line = new string[LineNum];
         Speed = new float[LineNum];
         TalkAnim = new Animator[LineNum];
         Voice = new AudioSource[LineNum];
         Noise = new AudioSource[LineNum];
     }

    public void CutsceneDone() { Done = true; }
}
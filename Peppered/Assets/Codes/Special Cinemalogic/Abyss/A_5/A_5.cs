using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class A_5 : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private GameObject[] Cams;
    [SerializeField]
    private AudioSource[] SFX;
    [SerializeField]
    private GameObject[] Things;
    [SerializeField]
    private GameObject[] TurnThingsOff;
    [SerializeField]
    private GameObject Firework;
    [SerializeField]
    private Boss_Door FirstDoor;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private PLAYER_CONTROLS Player;
    [SerializeField]
    private DialogueManager DLMan;
    [SerializeField]
    private Black_lines BLines;
    [SerializeField]
    private AUDIOMANAGER AudioMan;
    [SerializeField]
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

    //Regret 1 = you wrote the letter
    //Regret 2 = You didn't
    //Start is called before the first frame update
    void Start()
    {
        if (MySceneManager.Abyss_State < 6) { MySceneManager.Abyss_State = 6; }
        FirstDoor.Open(true);
    }

    public void StartThePuzzle() { StartCoroutine(PuzzleSt()); }

    IEnumerator PuzzleSt()
    {
        BLines.Show(220f, 3f);
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        Player.MyRigidBody.velocity = new Vector2(0,0);
        SceneMan = FindObjectOfType<MySceneManager>();
        SceneMan.MFadeOut(0.008f, false);
        AudioMan.Play("News Flash");

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- More developments in the search\nfor the mysterious star thief.";
        Line[1] = "- We have received new information\nabout the person in question:";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        CharAnim[0].Play("Joey Talk 0");
        yield return new WaitForSeconds(1f);
        FirstDoor.Closed();
        SceneMan.MChange(1, 0.8f);
        SceneMan.MPlay();


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Where you goin'?\nWe're on lockdown.";
        Line[1] = "- And until they find that star thief,\nnobody’s leavin’ this place.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        yield return new WaitForSeconds(1f);
        SceneMan.MStop();

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- ...without one of these.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        CharAnim[0].Play("Joey Idle");
        BLines.Hide(1f);
        Cams[0].SetActive(true);
        Things[0].SetActive(true);
        MySceneManager.Regret = 0;
        SceneMan.MStop();
        SceneMan.MChange(9, 0.8f);
        SceneMan.MPlay();
        Firework.SetActive(false);
        while (MySceneManager.Regret == 0) { yield return null; }

        //Puzzle Complete
        if (MySceneManager.Regret == 1)
        {
            SceneMan.MFadeOut(0.008f, false);
            Cams[0].SetActive(false);
            BLines.Show(220f, 1f);
            CharAnim[0].Play("Joey Reads 0");
            yield return new WaitForSeconds(1f);

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
            Line[0] = "- 'I would like to request early leave\nfor the following reasons:'";
            Line[1] = "- 'First off, I don't work here.'";
            Line[2] = "- 'Second, I stole the star, and I'm going to stop the God of Death because no one else will.'";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            CharAnim[0].Play("Joey Nonplussed");

            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.08f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "[silence]";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }

            yield return new WaitForSeconds(0.8f);
            AudioMan.Play("Victory");
            CharAnim[0].Play("Joey Amazed");
            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- YOO. YO, HOLY CRAP.\nRESPECT, DOG.";
            Line[1] = "- Here!";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            BLines.Hide(2f);
            MySceneManager.CutscenePlaying = false;
            Player.CanMove = true;
            Things[1].SetActive(true);
        }
        //Puzzle fail
        else if (MySceneManager.Regret == 2)
        {
            SceneMan.MPause();
            SceneMan.Music.pitch = 0.8f;
            CharAnim[0].Play("Joey Talk 0");
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Look, sunshine, gimme that paper.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            CharAnim[0].Play("Joey Writes 0");


            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                Voice[i] = SFX[1];
                TalkAnim[i] = null;
            }
            #endregion
            Line[0] = "[mumbles angrily]";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }

            CharAnim[0].Play("Joey Talk 0");
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Here. Just sign it.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            Things[0].SetActive(true);
            SceneMan.MPlay();

            while (MySceneManager.Regret == 2) { yield return null; }
            SceneMan.MStop();
            SceneMan.Music.pitch = 1f;

            if (MySceneManager.Regret == 3)
            {
                MySceneManager.Regret = 2;
                AudioMan.Play("Dramatic Impact");
                AudioMan.Play("Error");
                yield return new WaitForSeconds(3f);
                LineNum = 1;
                DeclareLines();
                #region Put things in
                for (int i = 0; i < LineNum; i++)
                {
                    Speed[i] = 0.04f;
                    Voice[i] = SFX[0];
                    TalkAnim[i] = CharAnim[0];
                }
                #endregion
                Line[0] = "- Geniuses like you are the reason why\nI think some people don't deserve to vote.";
                #region Go
                DLMan.DILines = Line;
                DLMan.DITextSpeed = Speed;
                DLMan.DIVoice = Voice;
                DLMan.Noise = Noise;
                DLMan.WhoTalks = TalkAnim;
                DLMan.DIStarter();
                #endregion
                while (DLMan.Playing) { yield return null; }
                CharAnim[0].Play("Joey Idle");
                MySceneManager.CutscenePlaying = false;
                Player.CanMove = true;
                Cams[0].SetActive(false);
                Things[1].SetActive(true);
            }
            else if (MySceneManager.Regret == 4)
            {
                LineNum = 1;
                DeclareLines();
                #region Put things in
                for (int i = 0; i < LineNum; i++)
                {
                    Speed[i] = 0.04f;
                    Voice[i] = SFX[0];
                    TalkAnim[i] = CharAnim[0];
                }
                #endregion
                Line[0] = "- Beautiful.";
                #region Go
                DLMan.DILines = Line;
                DLMan.DITextSpeed = Speed;
                DLMan.DIVoice = Voice;
                DLMan.Noise = Noise;
                DLMan.WhoTalks = TalkAnim;
                DLMan.DIStarter();
                #endregion
                while (DLMan.Playing) { yield return null; }
                MySceneManager.CutscenePlaying = false;
                Player.CanMove = true;
                Cams[0].SetActive(false);
                Things[1].SetActive(true);
            }
        }
    }

    public void GetThePass() { StartCoroutine(Door()); }

    IEnumerator Door()
    {
        if (MySceneManager.Regret == 1)
        {
            BLines.Show(220f, 0.5f);
            CharAnim[0].Play("Joey Impressed 0");
            MySceneManager.CutscenePlaying = true;
            Player.CanMove = false;
            Player.gameObject.transform.localScale = new Vector3(1,1,1);
            while (Player.Grounded == false) { yield return null; }


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
            Line[0] = "- You know what you want outta life.";
            Line[1] = "- And listen. Don't let anyone\ntell you shit, brother.";
            Line[2] = "- I'm rooting for you.\nTo the end, dog.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            BLines.Hide(.1f);
            MySceneManager.CutscenePlaying = false;
            Player.CanMove = true;
            CharAnim[0].Play("Joey Idle");
        }
        else if (MySceneManager.Regret == 4)
        {
            Player.gameObject.transform.localScale = new Vector3(1, 1, 1);
            MySceneManager.Regret = 2;
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Now, scram. I'm watching\nthe news here.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            CharAnim[0].Play("Joey Idle");
        }
    }

    public void EndScene()
    {
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        StartCoroutine(End());
    }

    IEnumerator End()
    {
        SceneMan.MFadeOut(0.01f, false);
        yield return new WaitForSeconds(1f);
        if (MySceneManager.Regret == 1)
        {
            SceneMan.MStop();
            SceneMan.MChange(13, 0.5f);
            SceneMan.MPlay();
            Things[2].SetActive(true);
            BLines.Show(220f, 6f);
            yield return new WaitForSeconds(3f);
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.08f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- The police are offering a $1,000 reward\nfor the identity of the star thief.";
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
        else if (MySceneManager.Regret == 2)
        {
            LineNum = 4;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.03f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- The police are offering a $1,000 reward\nfor the identity of the star thief.";
            Line[1] = "- Officials stress that this\ncould be anyone.";
            Line[2] = "- Including anyone trying to leave the 'Life Star Storage Company.’";
            Line[3] = "- Wearing a bright yellow raincoat.";
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


        CharAnim[0].Play("Joey Nonplussed Ipad");


        if (MySceneManager.Regret == 1)
        {
            yield return new WaitForSeconds(4f);
            CharAnim[0].Play("Joey Sad 0");

            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.07f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Well. Mama was right.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }

            CharAnim[0].Play("Joey Phone 0");
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.07f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- I really don’t have a soul.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
        } else
        {
            BLines.Show(220f, 7.5f);
            SceneMan.MStop();
            SceneMan.MChange(10, 0.8f);
            SceneMan.MPlay();
            Things[2].SetActive(true);
            yield return new WaitForSeconds(7.5f);
            PlayableDirector CamDir = Things[2].GetComponent<PlayableDirector>();
            CamDir.Stop();
            SceneMan.MStop();
            CharAnim[0].Play("Joey Talk 0");
            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.07f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- No clue who that might be.";
            Line[1] = "- Well. Back to porn browsing.";
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

        Things[3].gameObject.transform.position = new Vector3(0, -2, 0);
    }

    public void Talk()
    {
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
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
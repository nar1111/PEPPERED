using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class A_10_2 : MonoBehaviour
{

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
    private AudioSource[] SFX;
    [SerializeField]
    private AUDIOMANAGER AudioMan;
    [SerializeField]
    private GameObject[] Stuff;
    [SerializeField]
    private Sprite[] MerFace;
    

    private MySceneManager SceneMan;
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

    public void StartTheCutscene()
    {
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        Player.MyRigidBody.velocity = Vector2.zero;
        StartCoroutine(CutsceneFlashback());
    }

    IEnumerator CutsceneFlashback()
    {
        SpriteRenderer MerRen = Stuff[14].GetComponent<SpriteRenderer>();
        BLines.Show(220f, 1.5f);
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
        Line[0] = "— Well, police officer, I'm sure you are a huge inspiration for our viewers!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        MerRen.sprite = MerFace[1];


        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "— Do you mind telling us a bit about yourself?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        MerRen.sprite = MerFace[3];


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.06f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "— I, um...";
        Line[1] = "— Questioning requires special permission.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        MerRen.sprite = MerFace[2];


        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "— Please, it won't take long!";
        Line[1] = "— Tell us, why did you choose to become a police officer?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        MerRen.sprite = MerFace[3];
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.06f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "— C...Choose?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        MerRen.sprite = MerFace[2];

        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "— Yes! It's an unpopular profession these days. Why did you choose it?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        Stuff[15].SetActive(true);
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.07f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "[Childhood flashback intesify]";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        Stuff[0].SetActive(true);
        Stuff[15].SetActive(false);
        BLines.Hide(0f);
        BLines.Show(100f, 0f);
        Stuff[11].SetActive(true);
        Done = false;
        while (Done == false) { yield return null; }
        Stuff[0].SetActive(false);
        Stuff[1].SetActive(true);
        Stuff[2].SetActive(true);
        Stuff[3].SetActive(true);


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "— Here's to us!";
        Line[1] = "— Objectively the most celebrated people in the world!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "— Police Officers!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        CharAnim[0].Play("Mer Mom Bend Over");
        yield return new WaitForSeconds(1f);

        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "— What about you, Merdeka?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        Stuff[3].SetActive(false);
        Stuff[4].SetActive(true);
        Stuff[5].SetActive(false);


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "— Going to follow in daddy's and mommy's footsteps?";
        Line[1] = "— Going to grow up to be a useful member of society?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        CharAnim[2].Play("Little Merdeka Idle");
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[2];
        }
        #endregion
        Line[0] = "— No, mommy! I gonna be an artist!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        CharAnim[0].Play("Mer Mom Bend Over Angry");
        CharAnim[1].Play("Mer Dad Weird");
        CharAnim[3].gameObject.SetActive(true);
        CharAnim[3].Play("Young Chief Weird");
        Stuff[4].SetActive(false);
        Stuff[3].SetActive(true);
        yield return new WaitForSeconds(3.5f);


        Stuff[9].transform.position = Stuff[10].transform.position;
        Stuff[9].transform.localScale = new Vector3(-1,1,1);
        CharAnim[2].Play("Merdeka Cry");
        Stuff[3].SetActive(false);
        Stuff[6].SetActive(true);
        yield return new WaitForSeconds(2f);


        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[2];
        }
        #endregion
        Line[0] = "— Mom..?";
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


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[2];
        }
        #endregion
        Line[0] = "— Mom, I thought about what I said.";
        Line[1] = " — Can I come out now?";
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
        CharAnim[2].Play("Merdeka Scared 1");


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.1f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[4];
        }
        #endregion
        Line[0] = "— L I T T L E  G I R L.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        Stuff[6].SetActive(false);
        Stuff[7].SetActive(true);
        yield return new WaitForSeconds(1.5f);


        Stuff[12].SetActive(true);
        Done = false;
        while(Done == false) { yield return null; }
        Done = false;
        CharAnim[2].Play("Merdeka Scared 2");


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.1f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[4];
        }
        #endregion
        Line[0] = "— D O  Y O U  W A N N A  S E E  M Y \n H U M A N S O N A  O C?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        while (Done == false) { yield return null; }
        yield return new WaitForSeconds(1f);
        CharAnim[4].Play("Prison Inmate Drawing");
        yield return new WaitForSeconds(1f);

        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[4];
        }
        #endregion
        Line[0] = "— I drew it myself.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        SpriteRenderer DoorRen = Stuff[13].GetComponent<SpriteRenderer>();
        DoorRen.sortingOrder = -2;
        Stuff[7].SetActive(false);
        Stuff[8].SetActive(true);
        CharAnim[2].Play("Merdeka Scream");
        yield return new WaitForSeconds(2f);
        Stuff[11].SetActive(false);
        Stuff[8].SetActive(false);
        CharAnim[5].Play("White_End");
        yield return new WaitForSeconds(1f);


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "— Officer?";
        Line[1] = "— Officer, are you okay?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        MerRen.sprite = MerFace[3];
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.06f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = " — Apologies. I didn't understand the question.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        MerRen.sprite = MerFace[2];


        yield return new WaitForSeconds(2f);

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
        Line[0] = " — And we'll be right back, folks! Right after this break!";
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
        Player.CanMove = true;
        MySceneManager.CutscenePlaying = false;
    }

    #region It's donskie
    public void CutsceneDone() { Done = true; }
    #endregion

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
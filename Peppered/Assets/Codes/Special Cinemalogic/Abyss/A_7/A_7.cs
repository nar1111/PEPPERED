using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
//I'll just quickly add a word BUM here, to check if GitHub thing works

public class A_7 : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private GameObject[] Cams;
    [SerializeField]
    private PlayableDirector[] Cutscene;
    [SerializeField]
    private AudioSource[] SFX;
    [SerializeField]
    private GameObject[] Things;
    [SerializeField]
    private GameObject[] TurnThingsOff;
    [SerializeField]
    private Looking_At_Target[] LookScrp;
    [SerializeField]
    private Elevator_Trap ElevatorAct;
    [SerializeField]
    private Text[] Description;
    [SerializeField]
    private Button_Choose BChose;
    [SerializeField]
    private State_Strawbo_Idle StrawboState;
    private MySceneManager Sceneman;
    private int SeanTalked = 0;

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
    private bool Done = false;
    [HideInInspector] public int Angeri = 0;

    #region Private DL Stuff
    [HideInInspector]
    public string[] Line;
    private float[] Speed;
    private AudioSource[] Voice;
    private AudioSource[] Noise;
    private Animator[] TalkAnim;
    private int LineNum;
    #endregion

    private void Start()
    {
        if (MySceneManager.Abyss_State < 7) { MySceneManager.Abyss_State = 7; }
        else if (MySceneManager.Abyss_State == 8)
        {
            TurnThingsOff[0].SetActive(false);
            TurnThingsOff[1].SetActive(false);
            TurnThingsOff[2].SetActive(false);
        } else if (MySceneManager.Abyss_State >= 9)
        {
            TurnThingsOff[0].SetActive(false);
            TurnThingsOff[1].SetActive(false);
            TurnThingsOff[2].SetActive(false);
            TurnThingsOff[3].SetActive(false);
            TurnThingsOff[4].SetActive(false);
            TurnThingsOff[5].SetActive(false);
            TurnThingsOff[6].SetActive(false);
            TurnThingsOff[7].SetActive(false);
        }
    }

    public void BubblestonAlert()
    {
        CharAnim[0].Play("Benjamin_Scared");
        AudioMan.Play("Amount1");
        Things[0].SetActive(true);
    }

    public void BubbleStonRun()
    {
        StartCoroutine(BubblestonRunEnum());
        MySceneManager.Abyss_State = 8;
        Things[0].SetActive(false);
    }

    IEnumerator BubblestonRunEnum()
    {
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        Player.MyRigidBody.velocity = Vector2.zero;
        Cams[7].SetActive(true);
        BLines.Show(220f, 0.5f);

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
        Line[0] = "[Terrified Glub-glub noises]";
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
        Cams[7].SetActive(false);
        BLines.Hide(0.5f);

        Cutscene[0].Play();
        AudioMan.Play("Fishy Run");
        Done = false;
        while (Done == false) { yield return null; }
        ElevatorAct.NPCActivator();
    }

    public void TurnTheLightOn0()
    {
        Things[1].SetActive(true);
        AudioMan.Play("Spotlight");
    }

    public void TurnTheLightOn1()
    {
        Things[2].SetActive(true);
        AudioMan.Play("Spotlight");
    }

    public void TurnTheLightOn2()
    {
        Things[3].SetActive(true);
        AudioMan.Play("Spotlight");
    }

    public void ExitBlocked()
    {
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        Player.MyRigidBody.velocity = new Vector2(0, 0);
        Player.MyAnim.SetFloat("Speed", 0);
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "Wrong way.";
        Line[1] = "It's not for you — you're never wrong.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        MySceneManager.CutscenePlaying = false;
    }

    public void CommenceBeatting(){ StartCoroutine(CommenceBeatingEnum()); }

    public void TurtleNeck1()
    {
        CharAnim[2].Play("Turtleneck_Surprise");
        AudioMan.Play("Amount1");
        LookScrp[0].enabled = true;
    }

    public void Scarlett1()
    {
        CharAnim[3].Play("Scarlett_Scared");
        AudioMan.Play("Amount1");
        LookScrp[1].enabled = true;
    }

    public void NotAPimp1()
    {
        CharAnim[1].Play("NotAPimp_Scared");
        AudioMan.Play("Amount1");
        LookScrp[2].enabled = true;
    }

    IEnumerator CommenceBeatingEnum()
    {
        if (SceneMan = null) { SceneMan = FindObjectOfType<MySceneManager>(); }
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        Player.MyRigidBody.velocity = new Vector2(0, 0);
        while (Player.Grounded == false) { yield return null; }
        BLines.Show(220f, 3f);
        SceneMan = FindObjectOfType<MySceneManager>();
        yield return new WaitForSeconds(1f);
        Things[5].SetActive(false);


        //BOUNCER OPEN THE DOOR
        Done = false;
        AudioMan.Play("Dramatic Impact");
        yield return new WaitForSeconds(2f);
        Things[12].SetActive(false);
        Cutscene[2].Play();
        while (Done == false) { yield return null; }

        //BOUNCER OPENS HIS MOUTH
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            TalkAnim[i] = null;
        }
        #endregion
        Voice[0] = SFX[0];
        Voice[1] = SFX[1];
        Speed[0] = 0.03f;
        Speed[1] = 0.07f;
        Line[0] = "[Laughs nervously]";
        Line[1] = "- Oh. Oh, nuts. Usually, I'm supposed to let famous people in. But, eh.";
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


        //CAMERA SWITCH
        BLines.Hide(0f);
        Cams[0].SetActive(false);
        Cams[1].SetActive(true);
        SceneMan.MStop();
        SceneMan.MChange(16, 0.6f);
        SceneMan.MPlay();
        yield return new WaitForSeconds(4f);


        //BOUNCER SECOND LINES
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "— It's eh, an unusual day, isn't it?";
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

        Cams[1].SetActive(false);
        Cams[2].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Things[6].SetActive(false);
        Things[7].SetActive(true);

        //HERE ARE THE NEWS
        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[6];
        }
        #endregion
        Line[0] = "— Without a shred of doubt or proof.";
        Line[1] = "— Random Twitter posts our reporter skimmed over confirm star thief to be a far-right extremist.";

        if (MySceneManager.Cynthia != 3)
        {
            Line[2] = "— They encourage pushing women of the balconies. And they won't stop until we do something.";
        }
        else
        {
            Line[2] = "— They harass women into giving them Life Stars. And they won't stop until we do something.";
        }
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


        //AND HERE'S AN INSTRUCTION
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[6];
        }
        #endregion
        Line[0] = "— Now. In the event of seeing the star thief, Channel 22 does not advocate resorting to violence.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        Cams[2].SetActive(false);
        Cams[3].SetActive(true);
        yield return new WaitForSeconds(1f);

        //YEAH.
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[6];
        }
        #endregion
        Line[0] = "— But only because that will get us sued.";
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

        //CAMERA GOES BACK DOWN
        Cutscene[1].Play();
        yield return new WaitForSeconds(4f);


        //TALK YOUR WAY OUT
        Description[0].text = "You're just going to believe and do whatever the tv tells you? That is not what I've come to expect from the general public.";
        if (MySceneManager.Cynthia != 3)
        {
            Description[1].text = "Look, «Far - right,» «Extremist,» and «Pushing women off the balconies» are just buzzwords the media abuses, okay?";
        }
        else
        {
            Description[1].text = "Look, «Far - right,» «Extremist,» and «Harasses women into giving Life Stars» are just buzzwords the media abuses, okay?";
        }
        DLMan.LeftString = "[DISAPPROVE]";
        DLMan.RightString = "[DOWNPLAY]";
        DLMan.ChoiceStarter();
        while (BChose.ButtonChoice == 0) { yield return null; }
        AudioMan.Play("Dramatic Impact");
        SceneMan.MStop();


        //CROWD PULLS OUT THE WEAPONS
        yield return new WaitForSeconds(3f);
        Done = false;
        Cutscene[3].Play();


        while(Done == false) { yield return null; }
        CharAnim[4].Play("NotAPimp_Armed");


        //TRANSITION
        Description[2].text = "ONE DUE PROCESS LATER";
        Things[4].SetActive(true);
        Things[10].SetActive(true);
        SFX[3].Play();
        BLines.Show(220f, 0f);
        yield return new WaitForSeconds(5.7f);


        //CROWD IS DONE
        Player.MyAnim.Play("Invisible");
        AudioMan.Play("Dramatic Impact");
        Things[5].SetActive(true);
        Cams[3].SetActive(false);
        Cams[4].SetActive(true);
        Things[8].SetActive(true);
        Description[2].text = "";
        Things[4].SetActive(false);
        SFX[3].Stop();
        SFX[4].volume = 0.3f;


        yield return new WaitForSeconds(1f);
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[5];
        }
        #endregion
        Line[0] = "— Now remember - some people may\nlook down on mob justice.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        //JUSTIFY AWAY
        Cams[4].SetActive(false);
        Cams[5].SetActive(true);
        yield return new WaitForSeconds(1f);
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[5];
        }
        #endregion
        Line[0] = "— Then again, some people may\nlook down on being held accountable!";
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

        Things[9].SetActive(true);
        Cams[5].SetActive(false);
        Cams[6].SetActive(true);
        BLines.Hide(3f);
        yield return new WaitForSeconds(3f);
        MySceneManager.CutscenePlaying = false;
    }

    public void AngeriBoi()
    {
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
        if (Angeri == 0)
        {
            Line[0] = "[Silence]";
        }
        else
        {
            Line[0] = "The look in his eyes has so much hatred, it could burn through concrete.";
        }
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
    }

    public void ExitTheatre() { StartCoroutine(ExitTheatreScene()); }

    IEnumerator ExitTheatreScene()
    {
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        SceneMan.MStop();
        SceneMan.MChange(16, 0.5f);
        Things[9].SetActive(false);
        Cams[6].SetActive(false);
        SFX[4].volume = 0f;
        Cutscene[4].Play();
        Done = false;
        while (Done == false) { yield return null; }
        CharAnim[7].Play("Death_Start");
        yield return new WaitForSeconds(2f);
        Cams[10].SetActive(true);
        CharAnim[7].Play("Death_End");
        AudioMan.Play("News Loop");
        AudioMan.Play("Dramatic Impact");

        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "— The city is on a high alert, as every citizen is now on the lookout for the star thief.";
        Line[1] = "— The police are advising the star thief to turn themselves in because, quote:";
        Line[2] = "— «It's over. You have nowhere to go, perp. Quit wasting my time.»";
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
        Line[0] = "— Whenever the star thief will give up or not remains uncertain.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        AudioMan.StopIt("News Loop");
        AudioMan.Play("News End");
        yield return new WaitForSeconds(1.7f);
        CharAnim[7].Play("Death_Start");
        Cams[10].SetActive(false);
        yield return new WaitForSeconds(1f);
        MySceneManager.CutscenePlaying = false;
        Player.CanMove = true;
        Player.MyAnim.Play("Idle");
        MySceneManager.DeadState = 1;
        Cams[8].SetActive(false);
        Cams[9].SetActive(false);
        Things[11].SetActive(true);
        SceneMan.MPlay();
    }

    public void StrawboPatrol(){ StartCoroutine(StrawboPatrolIEnum()); }

    IEnumerator StrawboPatrolIEnum()
    {
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        Player.MyRigidBody.velocity = Vector3.zero;
        BLines.Show(220f, 1f);
        MySceneManager.Abyss_State = 9;
        yield return new WaitForSeconds(2f);
        StrawboState.State = 1;
        while (StrawboState.State != 3) { yield return null; }
        Player.CanMove = true;
        MySceneManager.CutscenePlaying = false;
        BLines.Hide(0.5f);
    }

    public void StopTheMusic()
    {
        SceneMan = FindObjectOfType<MySceneManager>();
        SceneMan.MFadeOut(0.001f, false);
    }

    public void SeanTalk()
    {
        if (SeanTalked == 0) { SeanTalked = 1; StartCoroutine(Sean()); } else
        {
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.03f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[8];
            }
            #endregion
            Line[0] = "— What? No, I don't pay other taxes either — I don't believe in them.";
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

    IEnumerator Sean()
    {
        MySceneManager.CutscenePlaying = true;
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[8];
        }
        #endregion
        Line[0] = "— Word of advice, «star thief»: avoid the sheeple.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        Things[13].SetActive(true);
        AudioMan.Play("Ting");
        yield return new WaitForSeconds(1f);

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[8];
        }
        #endregion
        Line[0] = "— If you see this icon, that means it's a sheeple, and they are looking for you.";
        Line[1] = "— They listen to that crap on TV. But not me. I know you're not some extremist.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        Things[13].SetActive(false);
        yield return new WaitForSeconds(1f);


        Line[0] = "— You're from the IRS.";
        Line[1] = "— Yes, scare me with «God of Death» fairy tale all you like — I'm not paying the «Prison Maintenance» tax.";
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
public class Balcony1 : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private Text[] Description;
    [SerializeField]
    private GameObject[] Cams;
    [SerializeField]
    private PlayableDirector[] Cutscene;
    [SerializeField]
    private GameObject[] Actors;
    [SerializeField]
    private AudioSource[] SFX;
    [SerializeField]
    private GameObject[] Things;
    private int Convince = 0;
    [SerializeField]
    private Cynthia FCyn;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private CHAIR_PLAYER Player;
    [SerializeField]
    private DialogueManager DLMan;
    [SerializeField]
    private Button_Choose BChose;
    [SerializeField]
    private Black_lines BLines;
    [SerializeField]
    private AUDIOMANAGER AudioMan;
    [SerializeField]
    private MySceneManager SceneMan;
    private bool Done = false;

    #region Private DL Stuff
    [HideInInspector]
    public string[] Line;
    private bool[] IsThereAface;
    private float[] Speed;
    private AudioSource[] Voice;
    private AudioSource[] Noise;
    private Animator[] TalkAnim;
    private int LineNum;
    #endregion

    public void Start()
    {
        MySceneManager.Abyss_State = 0;

        if (MySceneManager.Cynthia > 0)
        {
            Things[10].SetActive(true);
            if (MySceneManager.Cynthia == 2) { CharAnim[3].Play("Coffee Cup Full"); }
        }
    }

    public void YouWon() { Actors[0].SetActive(false); Things[0].SetActive(true); }

    public void HereAreTheNews()
    {
        SceneMan = FindObjectOfType<MySceneManager>();
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        StartCoroutine(News());
    }

    public void CutsceneDone() { Done = true; }

    public void End()
    {
        Done = false;
        Player.CanMove = false;
        Things[5].SetActive(true);
        SceneMan.MStop();
        AudioMan.Play("Dramatic Impact");
        Player.MyRigidBody.bodyType = RigidbodyType2D.Static;
        StartCoroutine(Ending());
    }

    private IEnumerator Ending()
    {
        SFX[5].volume = 1f;
        while (Done == false) { yield return null; }
        Done = false;
        yield return new WaitForSeconds(2f);
        AudioMan.Play("Dramatic Impact");
        Actors[4].SetActive(true);
        Cutscene[10].Stop();
        Actors[5].SetActive(false);
        yield return new WaitForSeconds(1.5f);
        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[5];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Oh my.";
        Line[1] = "- So that wasn't a joke?";
        Line[2] = "- What an odd person.";
        Line[3] = "- Very well, I'll call the police.";
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
        SceneMan.NormalSceneTransition(0, "Death_Start", "Death_End", 0f, "A_1", 0, 0);
    }

    private IEnumerator News()
    {
        Things[9].SetActive(false);
        Cutscene[10].Stop();
        Actors[5].SetActive(false);
        SceneMan.MFadeOut(0.01f, false);
        Cutscene[0].Play();
        yield return new WaitForSeconds(3.5f);
        Actors[6].SetActive(false);
        Done = false;
        CharAnim[2].Play("Death_End", 0, 0);
        AudioMan.Play("Dramatic Impact");
        Things[8].SetActive(true);
        while (Done == false) { yield return null; }

        Done = false;
        CharAnim[2].Play("Death_End", 0, 0);
        Things[8].SetActive(false);
        AudioMan.Play("Dramatic Impact");
        yield return new WaitForSeconds(2f);

        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "It's the Life Star.";
        Line[1] = "The very thing able to stop the God of Death.";
        Line[2] = "Completely unguarded.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        Actors[0].SetActive(true);
        yield return new WaitForSeconds(2f);

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Excuse me. Please step away \nfrom our only hope of survival.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        yield return new WaitForSeconds(2f);
        Cams[2].SetActive(true);
        Cams[1].SetActive(false);
        Actors[5].SetActive(true);

        CharAnim[0].Play("Angry");
        LineNum = 3;
        DeclareLines();
        #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.04f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
        Line[0] = "- Look, we have a plan, \nalright?";
        Line[1] = "— Make the coffee.";
        Line[2] = "— And then do absolutely nothing \nuntil Mr.Theodore arrives.";
        #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        while (DLMan.Playing) { yield return null; }

        CharAnim[0].Play("Idle");
        Actors[0].transform.localScale = new Vector3(1,1,1);


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Which will be any second now.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        SceneMan.MChange(0, 1f);
        SceneMan.MStop();
        SceneMan.MPlay();
        Cams[3].SetActive(true);
        yield return new WaitForSeconds(2f);

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Even though he's 6 hours late.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        yield return new WaitForSeconds(2f);

        CharAnim[0].Play("Scared");
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- And we can't contact him.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        SceneMan.MStop();
        Cams[3].SetActive(false);
        yield return new WaitForSeconds(2f);


        CharAnim[0].Play("Cyn_Sad_Idle");
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- And God of Death is almost free.";
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


        Cutscene[10].Stop();
        Actors[5].SetActive(false);
        BChose.ButtonChoice = 0;
        BLines.gameObject.SetActive(true);
        BLines.Show(180f, 0.2f);
        SceneMan.MChange(1, 0.5f);
        SceneMan.MStop();
        SceneMan.MPlay();
        DLMan.LeftString = "[DEMAND]";
        Description[0].text = "Give me the star, or I'll take it by force.";
        DLMan.RightString = "[REASON]";
        Description[1].text = "Give me the star, and I'll stop the God of Death myself.";
        DLMan.ChoiceStarter();
        while (BChose.ButtonChoice == 0) { yield return null; }
        BLines.Hide(0.3f);
        CharAnim[0].Play("Scared");
        SceneMan.Music.pitch = 1.1f;



        if (BChose.ButtonChoice == 1)
        {
            Convince = 1;
            yield return new WaitForSeconds(1.5f);
            Actors[0].transform.localScale = new Vector3(-1, 1, 1);
            Done = false;
            CharAnim[0].Play("Idle");
            Cutscene[2].Play();
            while (Done == false) { yield return null; }
            Done = false;
            AudioMan.Play("Ting");
            StartCoroutine(CallTheCops());
            yield break;
        }
        else if (BChose.ButtonChoice == 2)
        {
            yield return new WaitForSeconds(1f);
            CharAnim[0].Play("Idle");
           
            yield return new WaitForSeconds(0.4f);

            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.04f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Uh-huh.";
            Line[1] = "- And what exactly are your\nqualifications for this task?";
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



        SceneMan.Music.pitch = 1.2f;
        BChose.ButtonChoice = 0;
        BLines.Show(180f, 0.2f);
        DLMan.LeftString = "[LIE]";
        Description[0].text = "I'm from Theodore Glagolev. That was a surprise test, and you've passed.";
        DLMan.RightString = "[SHAME]";
        Description[1].text = "My qualifications? A willingness to do more than absolutely nothing.";
        DLMan.ChoiceStarter();
        while (BChose.ButtonChoice == 0) { yield return null; }
        BLines.Hide(0.3f);


        if (BChose.ButtonChoice == 1)
        {
            SceneMan.MStop();
            Actors[0].transform.localScale = new Vector3(-1, 1, 1);
            CharAnim[0].Play("Scared");
            AudioMan.Play("Dramatic Impact");
            yield return new WaitForSeconds(3.5f);
            Done = false;
            Cutscene[2].Play();
            while (Done == false) { yield return null; }
            Done = false;
            AudioMan.Play("Ting");
            Actors[0].transform.localScale = new Vector3(1, 1, 1);
            Convince = 3;
            StartCoroutine(CallTheCops());
            yield break;
        }
        else if (BChose.ButtonChoice == 2)
        {
            Actors[0].transform.localScale = new Vector3(-1, 1, 1);
            CharAnim[0].Play("Angry");
            SceneMan.Music.pitch = 1.4f;
            LineNum = 4;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.02f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Good luck with that!";
            Line[1] = "- I've been here for 40 years and I ain't never seen nobody but Theodore perform the ceremony.";
            Line[2] = "- What are you gonna do?\nWhat's the plan?";
            Line[3] = "- You just gonna waltz onstage?\nAnd then what? What will you do?";
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

        
        BChose.ButtonChoice = 0;
        BLines.Show(180f, 0.2f);
        DLMan.LeftString = "[MOCK]";
        Description[0].text = "More than you ever will.";
        DLMan.RightString = "[ASSERT]";
        Description[1].text = "I’ll figure something out!";
        DLMan.ChoiceStarter();
        while (BChose.ButtonChoice == 0) { yield return null; }
        BLines.Hide(0.3f);



        if (BChose.ButtonChoice == 1)
        {
            Actors[5].SetActive(true);
            Cutscene[10].Play();
            AudioMan.Play("Dramatic Impact");
            SceneMan.MStop();
            SceneMan.Music.pitch = 1f;
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.02f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Right, yeah, fine, fabulous. FABULOUS! Here.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }

            Done = false;
            Cutscene[6].Play();
            while (Done == false) { yield return null; }
            Done = false;
            AudioMan.Play("Victory");

            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.04f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Take it. I'm SURE no one will mind.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }

            Actors[2].SetActive(true);
            Cams[3].SetActive(true);
            Cams[2].SetActive(false);
            yield return new WaitForSeconds(2f);

            CharAnim[0].Play("Shocked");
            LineNum = 3;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.04f;
                Voice[i] = SFX[5];
                TalkAnim[i] = CharAnim[1];
            }
            #endregion
            Line[0] = "- Quite the contrary, Cynthia.";
            Line[1] = "- You're fired.";
            Line[2] = "- Come! My yellow-clad friend!\nWe have a job offer just for you.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            SceneMan.MChange(11, 0.6f);
            SceneMan.MPlay();

            MySceneManager.Cynthia = 3;
            Cutscene[9].Play();
            BLines.Show(220f, 0.3f);
            BChose.ButtonChoice = 0;
            DLMan.LeftString = "[SAVE THE WORLD]";
            Description[0].text = "No. I'm taking this star. And I'm stopping the God of Death.";
            DLMan.RightString = "[GET A JOB]";
            Description[1].text = "Sure. That's a sensible thing to do.";
            DLMan.ChoiceStarter();
            while (BChose.ButtonChoice == 0) { yield return null; }
           

            if (BChose.ButtonChoice == 1) { if (BChose.ButtonChoice == 1) { StartCoroutine(Refuse()); } }
            else if (BChose.ButtonChoice == 2) { BLines.Hide(0.2f); StartCoroutine(GetAJob()); yield break; }
        }
        else if (BChose.ButtonChoice == 2)
        {
            SceneMan.MStop();
            AudioMan.Play("Dramatic Impact");

            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.04f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- I've heard enough.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }

            Done = false;
            Cutscene[2].Play();
            while (Done == false) { yield return null; }
            Done = false;
            AudioMan.Play("Ting");

            Actors[0].transform.localScale = new Vector3(1, 1, 1);
            Convince = 6;
            StartCoroutine(CallTheCops());
            yield break;
        }
    }

    private IEnumerator CallTheCops()
    {
        Things[9].SetActive(false);
        //Take By force
        if (Convince == 1)
        {
            MySceneManager.Cynthia = 0;
            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.04f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Tell me. Where do you see yourself in ten minutes from now?";
            Line[1] = "- Because I see you getting whooped out of here.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }

            SceneMan.MStop();
            CharAnim[0].Play("Angry");
            Cams[4].SetActive(true);
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.04f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Behold! My stupendous technique!";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            SFX[3].Play();
            BLines.Show(280f, 8f);
            yield return new WaitForSeconds(8f);
            SFX[3].Stop();
            BLines.Hide(0f);
            //Things[2].SetActive(true);
            Cams[4].SetActive(false);
            Actors[0].transform.localScale = new Vector3(1, 1, 1);
            CharAnim[0].Play("Cyn_Phone_Idle");

            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.04f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Hello, security?";
            Line[1] = "- We need a delinquent escorted off the premises.";
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

        //Lie about yourself
        if (Convince == 3)
        {
            CharAnim[0].Play("Cyn_Phone_Idle");
            MySceneManager.Cynthia = 1;
            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Speed[0] = 0.04f;
            Speed[1] = 0.04f;
            Line[0] = "- Hello, security?";
            Line[1] = "- We’ll need you to help us escort\nan imposter off the premises.";
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

        //You're being too naive
        if (Convince == 6)
        {
            MySceneManager.Cynthia = 2;
            CharAnim[0].Play("Cyn_Phone_Idle");
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < IsThereAface.Length; i++)
            {
                IsThereAface[i] = false;
                Speed[i] = 0.06f;
                Voice[i] = SFX[1];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "I'm calling security.";
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

        Things[1].SetActive(true);
        Player.FIGHT = true;
        FCyn.enabled = true;
        AudioMan.Play("Amount1");
        SceneMan.Music.pitch = 1f;
        while (!Input.GetButtonDown("Skip")) { yield return null; }
        while (FCyn.Stage != -1) { yield return null; }


        Cutscene[3].Play();
        Player.FIGHT = false;
        Things[1].SetActive(false);
        Actors[2].SetActive(true);
        Cams[3].SetActive(true);
        Cams[2].SetActive(false);
        BLines.gameObject.SetActive(true);
        BLines.Show(180f, 1f);
        yield return new WaitForSeconds(1.5f);
        CharAnim[0].Play("Angry");


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[1];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Fabulous.";
        Line[1] = "- All the best in your employment search.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        Done = false;
        Cutscene[4].Play();
        while (Done == false) { yield return null; }
        Done = false;

        Actors[5].SetActive(true);
        Cutscene[10].Play();
        AudioMan.Play("Ting");
        BLines.Hide(0.2f);
        Cutscene[5].Play();
        Actors[1].SetActive(true);
        SceneMan.MStop();
        SceneMan.MChange(2, 0.8f);
        SceneMan.MPlay();
        MySceneManager.CutscenePlaying = false;
        Player.FIGHT = true;
        FCyn.Stage = 1;
        Things[3].SetActive(true);
        while (Actors[0].activeInHierarchy) { yield return null; }
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        Player.FIGHT = false;
        SceneMan.MStop();
        FCyn.Stage = 2;
        Things[3].SetActive(false);
        yield return new WaitForSeconds(4f);
        //AudioMan.Play("Victory");
        yield return new WaitForSeconds(3.5f);

        SceneMan.MChange(11, 0.6f);
        SceneMan.MPlay();
        Player.transform.localScale = new Vector3(1,1,1);
        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[5];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- That was a STUNNING performance, my friend!";
        Line[1] = "- Only ten minutes in, and you are already achieving things I could have only dreamt of at your age!";
        Line[2] = "- Come! You're just the rebellious type \nI need at my office space.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        BLines.Show(220f, 0.3f);
        BChose.ButtonChoice = 0;
        DLMan.LeftString = "[SAVE THE WORLD]";
        Description[0].text = "No. I'm taking this star. And I'm stopping the God of Death.";
        DLMan.RightString = "[GET A JOB]";
        Description[1].text = "Sure. That's a sensible thing to do.";
        DLMan.ChoiceStarter();
        while (BChose.ButtonChoice == 0) { yield return null; }
        Things[9].SetActive(true);

        if (BChose.ButtonChoice == 1) { StartCoroutine(Refuse()); }
        else if (BChose.ButtonChoice == 2) { BLines.Hide(0.2f); StartCoroutine(GetAJob()); }
    }

    private IEnumerator GetAJob()
    {
        SceneMan.MFadeOut(0.001f, false);
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[5];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Outstanding!";
        Line[1] = "- A bright future awaits us!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        CharAnim[2].Play("Death_Start");
        Description[2].text = "5 YEARS LATER";
        yield return new WaitForSeconds(1.5f);
        SceneMan.NormalSceneTransition(0, "Death_Start", "Death_End", 0f, "Office_2", 0, 0);
    }

    private IEnumerator Refuse()
    {
        //SceneMan.MStop();
        AudioMan.Play("Dramatic Impact");
        Cams[6].SetActive(true);
        yield return new WaitForSeconds(2f);


        CharAnim[1].Play("Boss_Laugh");
        SFX[6].Play();
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "[Laughs hysterically]";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        Done = false;
        CharAnim[1].Play("Boss_Open");
        SFX[6].Stop();
        Cutscene[8].Play();
        while (Done == false) { yield return null; }
        Done = false;
        //Secret thing
        Player.gameObject.transform.localScale = new Vector3(-1,1,1);
        AudioMan.Play("Ting");
        Actors[3].SetActive(true);
        Cams[7].SetActive(true);
        Cams[6].SetActive(false);
        Cutscene[7].Play();
        Things[4].SetActive(false);
        while (Done == false) { yield return null; }
        Done = false;

        //SceneMan.MStop();
        //SceneMan.MChange(5,1);
        //SceneMan.MPlay();
        //CharAnim[1].Play("Boss_Idle");

        CharAnim[1].Play("Boss_Laugh");
        SFX[5].volume = 0f;
        SFX[6].Play();
        LineNum = 3;
        Player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[5];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Stopping THE God of Death!";
        Line[1] = "- Magnificent!";
        Line[2] = "- Show us how it's done, soldier!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        Things[7].SetActive(true);
        CharAnim[1].Play("Boss_Idle");
        SFX[6].Stop();
        Player.CanMove = true;
        MySceneManager.CutscenePlaying = false;
        BLines.Hide(1f);
    }

    #region Lines
    void DeclareLines()
    {
        Line = new string[LineNum];
        IsThereAface = new bool[LineNum];
        Speed = new float[LineNum];
        TalkAnim = new Animator[LineNum];
        Voice = new AudioSource[LineNum];
        Noise = new AudioSource[LineNum];
    }
    #endregion
}
//CYNTHIA CONFLICT RESOLUTION
//0 = Tried to take by force.
//1 = Tried to convience you're from Theodore
//2 = Came close, but didn't have a plan
//3 = Convinced to give the star
//4 = Cynthia fell off the roof
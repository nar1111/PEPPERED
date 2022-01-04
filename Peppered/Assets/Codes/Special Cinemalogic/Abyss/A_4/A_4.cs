using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class A_4 : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private GameObject[] Cams;
    [SerializeField]
    private AudioSource[] SFX;
    [SerializeField]
    private GameObject[] Things;
    [SerializeField]
    private GameObject[] Actors;

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

    //ACTORS
    //Actor 0 = Cynthia
    //Actor 1 = Fish
    //Actor 2 = Merdeka

    //CYNTHIA CONFLICT RESOLUTION
    //0 = Tried to take by force.
    //1 = Tried to convience you're from Theodore
    //2 = Came close, but didn't have a plan
    //3 = Convinced to give the star
    //4 = Cynthia fell off the roof

    void Start()
    {
        if (MySceneManager.Abyss_State < 3) { MySceneManager.Abyss_State = 3; }
        else if (MySceneManager.Abyss_State > 3)
        {
            Things[0].SetActive(false);
            CharAnim[2].Play("Idle5");
        }

        if (MySceneManager.Cynthia != 3 && MySceneManager.Abyss_State != 5) { Actors[0].SetActive(true); }
        else if (MySceneManager.Cynthia == 3 && MySceneManager.Abyss_State != 5) { Actors[1].SetActive(true); }

        CharAnim[2].Play("Idle5");
    }

    public void EvidenceTrg() { StartCoroutine(Evidence()); }

    IEnumerator Evidence()
    {
        if (SceneMan == null) { SceneMan = FindObjectOfType<MySceneManager>(); }
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        Player.MyRigidBody.velocity = Vector2.zero;
        BLines.Show(220f, 1.5f);
        SceneMan.MFadeOut(0.01f, true);

        if (MySceneManager.Cynthia == 0)
        {
            CharAnim[2].Play("Idle5");
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Can you believe it?";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            Cams[0].SetActive(true);
            AudioMan.Play("Dramatic Impact");
            yield return new WaitForSeconds(1.5f);


            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Just like that, out of nowhere,\nthey threatened me!";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            CharAnim[0].Play("ACynthia_Side");
            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- But I stood my ground\nand didn't fall for it.";
            Line[1] = "- I did fall from the building though.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            CharAnim[0].Play("ACynthia_Scream");


            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- You know, once they\nTHREW ME OFF IT..";
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


            CharAnim[2].Play("Idle1");
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[2];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- This perpetrator. Did they look\nanything like, say...";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            Cams[1].SetActive(true);
            yield return new WaitForSeconds(1.5f);
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[2];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- That person below us?";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }

            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Yeah! That's the one!";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            StartCoroutine(GotYou());

        }
        else if (MySceneManager.Cynthia == 1)
        {
            CharAnim[2].Play("Idle5");
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- And then they pretended\nto be Mr. Theodore's associate.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            Cams[0].SetActive(true);
            AudioMan.Play("Dramatic Impact");
            yield return new WaitForSeconds(1.5f);


            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- When that failed, they straight up\nthrew me off the balcony.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }

            CharAnim[2].Play("Idle1");
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[2];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- Not illegal.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            CharAnim[0].Play("ACynthia_Shrug");
            yield return new WaitForSeconds(2f);
            CharAnim[0].Play("ACynthia_Side");


            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Fabulous. No wonder they decommissioned\nthe majority of you people.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[2];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "-  I'll ask again. That person over there, below us.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            Cams[1].SetActive(true);
            yield return new WaitForSeconds(1.5f);
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[2];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- Did they steal the star?";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Yep, that's them.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            StartCoroutine(GotYou());
        }
        else if (MySceneManager.Cynthia == 2)
        {
            CharAnim[2].Play("Idle5");
            CharAnim[0].Play("ACynthia_Side");
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- They tried convincing me\nto GIVE them the star.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }

            Cams[0].SetActive(true);
            AudioMan.Play("Dramatic Impact");
            yield return new WaitForSeconds(1.5f);


            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[2];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- What for? Did they specify?";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            CharAnim[0].Play("ACynthia_Shrug");
            yield return new WaitForSeconds(2f);
            CharAnim[0].Play("ACynthia_Side");
            CharAnim[2].Play("Idle5");

            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- To stop the God of Death or something?";
            Line[1] = "- Like. THEMSELVES.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            //Merdeka Pause
            CharAnim[2].speed = 0;
            yield return new WaitForSeconds(1.5f);
            CharAnim[2].speed = 1;


            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[2];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- Huh. Interesting.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            CharAnim[2].Play("Idle1");


            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[2];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "— In any case. This perpetrator.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            Cams[1].SetActive(true);
            yield return new WaitForSeconds(1.5f);
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[2];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- Is that them, right there?";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Oh yeah! That's the one!";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            StartCoroutine(GotYou());
        }
        else if (MySceneManager.Cynthia == 3)
        {
            CharAnim[2].Play("Idle5");
            LineNum = 1;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[2];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- Description? A yellow raincoat?\nRed swim shorts, maybe?";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            Cams[0].SetActive(true);
            AudioMan.Play("Dramatic Impact");
            yield return new WaitForSeconds(1.5f);


            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[2];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- Anything ring a bell?";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }


            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.05f;
                Voice[i] = SFX[3];
                TalkAnim[i] = CharAnim[1];
            }
            #endregion
            Line[0] = "[Glub-glub noises]";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            CharAnim[2].Play("Idle2");
            yield return new WaitForSeconds(2f);


            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[2];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- Was that a positive 'Glub-glub'? Or?";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }

            SFX[3].volume = 1f;
            SFX[3].pitch = 1.4f;
            CharAnim[1].Play("Fish_Idle 2");
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.05f;
                Voice[i] = SFX[3];
                TalkAnim[i] = CharAnim[1];
            }
            #endregion
            Line[0] = "[Glub-glub noises intensify]";
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
            CharAnim[2].Play("Idle5");


            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.06f;
                Voice[i] = SFX[2];
                TalkAnim[i] = CharAnim[2];
            }
            #endregion
            Line[0] = "- Witness described\nthe perpetrator as 'Glub-glub'";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            Cams[0].SetActive(false);
            BLines.Hide(1f);
            yield return new WaitForSeconds(1f);
            MySceneManager.CutscenePlaying = false;
            Player.CanMove = true;
            MySceneManager.Abyss_State = 4;
            SceneMan.MChange(12, 0.5f);
            SceneMan.MPlay();
        }
    }

    IEnumerator GotYou()
    {
        SceneMan.MStop();
        SceneMan.Music.pitch = 1.1f;
        SceneMan.MChange(8, 0.7f);
        SceneMan.MPlay();
        Cams[2].SetActive(true);
        Cams[0].SetActive(false);
        Cams[1].SetActive(false);
        BLines.Show(450f, 0.5f);
        CharAnim[2].Play("Idle3");
        yield return new WaitForSeconds(0.5f);
        BLines.Show(460f, 3f);
        yield return new WaitForSeconds(0.5f);

        LineNum = 2;
        DeclareLines();
        Speed[0] = 0.06f;
        Speed[1] = 0.09f;
        Voice[0] = SFX[1];
        Voice[1] = SFX[2];
        TalkAnim[0] = null;
        TalkAnim[1] = CharAnim[2];
        Line[0] = "[Inhales policewomanmanly]";
        Line[1] = "- You're mine, perp.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        BLines.Show(220f, 0f);
        Cams[2].SetActive(false);
        Cams[0].SetActive(true);
        CharAnim[0].Play("ACynthia_Side");
        SceneMan.MStop();
        SceneMan.Music.pitch = 1f;
        CharAnim[2].Play("TakeRadioIdle");

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.06f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[2];
        }
        #endregion
        Line[0] = "- ...once I'm done with the paperwork.\nRight Chief?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        CharAnim[0].Play("ACynthia_Scream");
        yield return new WaitForSeconds(0.8f);


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.02f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Are you serious?!\nThey'll run away!";
        Line[1] = "- Couldn't you catch them first?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        CharAnim[2].Play("Idle5");
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.06f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[2];
        }
        #endregion
        Line[0] = "- Plenty of things I could’ve\ndone in life. But.";
        Line[1] = "- Rules are rules.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        Cams[0].SetActive(false);
        BLines.Hide(1f);
        yield return new WaitForSeconds(1f);

        SceneMan.MChange(12, 0.5f);
        SceneMan.MPlay();
        MySceneManager.CutscenePlaying = false;
        Player.CanMove = true;
        MySceneManager.Abyss_State = 4;
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
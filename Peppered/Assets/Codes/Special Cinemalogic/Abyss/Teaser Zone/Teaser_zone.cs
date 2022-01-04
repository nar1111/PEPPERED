using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class Teaser_zone : MonoBehaviour
{
    #pragma warning disable 649
    public PlayableDirector[] Cutscene;
    public AudioSource[] SFX;
    public GameObject[] Stuff;
    private bool Done = false;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private PLAYER_CONTROLS Player;
    [SerializeField]
    private DialogueManager DLMan;
    [SerializeField]
    private MySceneManager SceneMan;
    [SerializeField]
    private AUDIOMANAGER AudioMan;
    [SerializeField]
    private Black_lines Blines;

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


    public void StartTheCommercial()
    {
        MySceneManager.CutscenePlaying = true;
        Player.CanMove = false;
        Player.MyRigidBody.velocity = Vector2.zero;
        StartCoroutine(CommercialActivate());
    }

    IEnumerator CommercialActivate()
    {
        SceneMan = FindObjectOfType<MySceneManager>();
        Stuff[0].SetActive(true);
        yield return new WaitForSeconds(6f);
        Cutscene[0].Play();
        Done = false;
        while (Done == false) { yield return null; }

        yield return new WaitForSeconds(3f);
        Stuff[1].SetActive(true);
        CharAnim[0].Play("Happy");
        AudioMan.Play("Dramatic Impact");
        SceneMan.MStop();
        SceneMan.MChange(15, 0.9f);
        SceneMan.MPlay();

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Ah, marvelous! What a show!";
        Line[1] = "- There's nothing like using a bit of violence to capture one’s attention.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        CharAnim[0].Play("Idle");


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- What? Oh, don't concern yourself over THAT man. He’s as fit as a fiddle.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        Stuff[2].SetActive(true);


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- We're in the teaser zone!";
        Line[1] = "- Nothing in here has any meaning\nor relevance to your story.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        Stuff[2].SetActive(false);


        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Speaking of your story, pumpkin, I’ve removed the safe files.";
        Line[1] = "- Oh, hush, my love. Not everyone's\nblessed with a chance to go back and try again.";
        Line[2] = "- Trust me, I know. If I’d had that chance.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        Stuff[3].SetActive(true);
        CharAnim[0].Play("UC Angry");
        SceneMan.MPause();

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.08f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- My absolute failure of an ex-husband\nwouldn’t be alive today.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        yield return new WaitForSeconds(1f);

        Stuff[3].SetActive(false);
        CharAnim[0].Play("Idle");
        SceneMan.MPlay();

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Oh goodness, look at me rambling on when there are far more important things to discuss.";
        Line[1] = "- If you happened to enjoy 'PEPPERED.'";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        CharAnim[0].Play("Happy");


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Then I’m sure you'll be delighted to know that we are planning to run a Kickstarter campaign!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        CharAnim[0].Play("Idle");


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- When?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        CharAnim[0].Play("Happy");
        AudioMan.Play("Battle Start");
        Stuff[4].SetActive(true);


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- No idea, honey!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        CharAnim[0].Play("Idle");


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Soon.";
        Line[1] = "- But you can stay updated\nby subscribing to our newsletter.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        Stuff[5].SetActive(false);
        Stuff[6].SetActive(true);
        Stuff[2].SetActive(true);
        AudioMan.Play("Dramatic Impact");
        CharAnim[0].Play("Happy");
        SceneMan.MFadeOut(0.05f, false);

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- On this website!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        AudioMan.Play("Dramatic Impact");
        Stuff[7].SetActive(true);


        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Or by following us HERE.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        Stuff[2].SetActive(false);
        Stuff[8].SetActive(true);
        Blines.Show(220f, 0f);
        SceneMan.MStop();
        SceneMan.MChange(5, 0.6f);
        SceneMan.MPlay();
        CharAnim[0].Play("Idle");


        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- This game means a lot to us.";
        Line[1] = "- It's a project made with\nunconditional love.";
        Line[2] = "- With your help, we would be able\nto work on it full time.";
        Line[3] = "- And bring the story we want to tell about the feelings we want to share to life.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        Stuff[8].SetActive(false);
        Blines.Hide(0f);
        yield return new WaitForSeconds(2f);
        Cutscene[1].Play();
        Done = false;
        while (Done == false) { yield return null; }
        SceneMan.MStop();
        yield return new WaitForSeconds(4f);


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < IsThereAface.Length; i++)
        {
            IsThereAface[i] = false;
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Stay tuned.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        if (DLMan.Playing == false) { DLMan.DIStarter(); }
        #endregion
        while (DLMan.Playing) { yield return null; }
        AudioMan.Play("Respawn");
        Stuff[9].SetActive(true);
        yield return new WaitForSeconds(4f);
        Application.Quit();
    }

    void DeclareLines()
    {
        Line = new string[LineNum];
        IsThereAface = new bool[LineNum];
        Speed = new float[LineNum];
        TalkAnim = new Animator[LineNum];
        Voice = new AudioSource[LineNum];
        Noise = new AudioSource[LineNum];
    }

    public void CutsceneDone() { Done = true; }
}
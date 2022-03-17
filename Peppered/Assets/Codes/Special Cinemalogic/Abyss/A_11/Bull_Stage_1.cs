using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class Bull_Stage_1 : State
{

    private int MyStage = 0;
    [SerializeField] private GameObject[] Stuff;
    [SerializeField] private ParticleSystem DustEf;
    [SerializeField] private PlayableDirector[] Cutscene;
    [SerializeField] private Text AdditionalText;
    [SerializeField] private Bull_Stage_2 Stage2;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField] private Animator[] CharAnim;
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

    public override State RunCurrentState()
    {
        if (MyStage == 0)
        {
            MyStage = 1;
            CharAnim[0].Play("Bull Bro Trick");
            CharAnim[1].Play("Bro 2 Chant");
        }

        else if (MyStage == 2)
        {
            MyStage = 3;
            return Stage2;
        }
        return this;
    }

    public void HereIAm()
    {
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        DustEf.Stop();
        Player.MyRigidBody.velocity = Vector2.zero;
        CharAnim[0].Play("Bull Bro Idle");
        CharAnim[1].Play("Bro 2 Chant Stop");
        CharAnim[0].gameObject.transform.localScale = new Vector3(-1,1,1);
        BLines.Show(200f, 0.5f);
        StartCoroutine(RockMeLikeAHUuuruaru());
    }

    IEnumerator RockMeLikeAHUuuruaru()
    {
        yield return new WaitForSeconds(1.5f);
        CharAnim[0].gameObject.transform.localScale = new Vector3(1, 1, 1);


        if (Player.transform.parent != null) { Player.transform.parent.gameObject.transform.localScale = new Vector3(-1, 1, 1); }
        else
        {
            Player.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        }


        yield return new WaitForSeconds(2f);
        CharAnim[0].Play("Bull Bro Talk 1");


        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "— STAR THIEF!";
        Line[1] = "— I'll show you how we treat thieves around these parts!";
        Line[2] = "— Come at me!";
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
        Cutscene[0].Play();
        if (Player.transform.parent == null) { Player.transform.position = new Vector3(0, Player.transform.position.y, Player.transform.position.z); }
        while (Done == false) { yield return null; }
        Stuff[0].SetActive(true);
        AdditionalText.text = "run.";
        CharAnim[0].Play("Bull Bro Idle");
        CharAnim[1].Play("Bro 2 Idle 1");
        Stuff[1].SetActive(false);
        Stuff[2].SetActive(true);


        if (Player.transform.parent != null) { Player.transform.parent.gameObject.transform.localScale = new Vector3(1, 1, 1); Player.CanMove = false; }
        else
        {
            Player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }


        yield return new WaitForSeconds(2f);
        MySceneManager.CutscenePlaying = false;
        Stuff[0].SetActive(false);
        AdditionalText.text = "";
        BLines.Hide(.5f);
        MyStage = 2;
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
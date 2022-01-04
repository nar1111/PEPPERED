using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.UI;
public class MERBOSS : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private Text[] Description;
    [SerializeField]private GameObject Merdeka;
    [SerializeField]private PLAYER_CONTROLS Player;
    [SerializeField]private GameObject[] MerPos;
    [SerializeField]private GameObject[] Bullets;
    [SerializeField]private GameObject FireEffect;
    [SerializeField]private AUDIOMANAGER Audiman;
    [SerializeField]private Animator[] Anims;
    [SerializeField]private GameObject[] Stuff;
    [SerializeField]private SpriteRenderer PepRen;
    [SerializeField]private TextMesh HitCount;
    [SerializeField]private Image HitFiller;
    [SerializeField]private MySceneManager SceneMan;
    [SerializeField]private Route_Follow[] BulletSpeedChange;
    [SerializeField]private PlayableDirector[] Cutscene;
    [SerializeField]private Button_Choose BChose;

    private bool CurrentlyShocked = false;
    private int HitPoint = 6;
    private int MoveSpot = 0;
    private Animator MyAnim;
    private bool Done;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private DialogueManager DLMan;
    [SerializeField]
    private Black_lines BLines;
    [SerializeField]
    private AudioSource[] SFX;

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
        MyAnim = Merdeka.GetComponent<Animator>();
        if (MySceneManager.Regret == 2 && MySceneManager.Cynthia == 3) { MySceneManager.Regret = 3; }
    }

    public void FightTrigger()
    {
        //If there are evidence
        if (MySceneManager.Regret != 3)
        {
            Player.CanMove = false;
            MySceneManager.CutscenePlaying = true;
            Player.MyRigidBody.velocity = Vector3.zero;
            StartCoroutine(LetTheFightBegin());
        }
        else
        {
            Player.CanMove = false;
            MySceneManager.CutscenePlaying = true;
            Player.MyRigidBody.velocity = Vector3.zero;
            StartCoroutine(Offer());
        }
    }

    IEnumerator LetTheFightBegin()
    {
        if (MySceneManager.Regret != 4)
        {
            SceneMan = FindObjectOfType<MySceneManager>();
            SceneMan.MFadeOut(0.01f, false);
            yield return new WaitForSeconds(2f);
        

            LineNum = 3;
            DeclareLines();
            #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
            Speed[0] = 0.04f;
            Speed[1] = 0.04f;
            Speed[2] = 0.1f;
            Voice[0] = SFX[0];
            Voice[1] = SFX[0];
            Voice[2] = SFX[1];
            Line[0] = "- Chief. Perpetrator located.";
            Line[1] = "- Permission for dramatic entrance?";
            Line[2] = "- Y E S";
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
        }

            Stuff[0].SetActive(true);
            Anims[1].Play("Strawberry Activate", 0, 0);
            Audiman.Play("Dramatic Impact");
            //Audiman.Play("Mer Gun Out");
            yield return new WaitForSeconds(2f);
            Cutscene[0].Play();
            Done = false;
            while (Done == false) { yield return null; }
            yield return new WaitForSeconds(1f);

        if (MySceneManager.Regret != 4)
        {
            LineNum = 4;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- If the God of Death breaks free.";
            Line[1] = "- Everybody will become mortal.";
            Line[2] = "- Chaos will ensue.";
            Line[3] = "- And worst of all.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            Stuff[6].SetActive(true);
            Audiman.Play("Dramatic Impact");


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
            Line[0] = "- I will lose my job.";
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
            Stuff[6].SetActive(false);

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
            Line[0] = "- Don't have a choice here, perp.";
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
            Line[0] = "- Don't worry.";
            Line[1] = "- With good behaviour,\nyou’ll get 12 years at most.";
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

            Cutscene[1].Play();
            Done = false;
            while (Done == false) { yield return null; }
            Bullets[0].SetActive(true);
            Anims[0].Play("Death_End", 0, 0);
            MyAnim.Play("Fight");
            Audiman.Play("Lazer");
            Stuff[1].SetActive(false);
            SceneMan.MStop();
            SceneMan.MChange(3, 0.3f);
            SceneMan.MPlay();

        if (MySceneManager.Regret != 4)
        {
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
            Line[0] = "- You have the right to stand there,\nwhere the bullets can hit you.";
            Line[1] = "- Dodging bullets is\na criminal offense.";
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
            MySceneManager.CutscenePlaying = false;
            Player.CanMove = true;

    }

    IEnumerator Offer()
    {
        SceneMan = FindObjectOfType<MySceneManager>();
        SceneMan.MFadeOut(0.01f, false);
        yield return new WaitForSeconds(2f);

        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Look, suspect. I know you're\nthe thief. It's obvious.";
        Line[1] = "- I just don't have evidence.";
        Line[2] = "- Could you...\nJust confess your crimes?";
        Line[3] = "- We'll do the mandatory\nfight and everything.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        BLines.Show(220f, 0.2f);
        Description[0].text = "";
        Description[1].text = "";
        DLMan.LeftString = "[CONFESS]";
        DLMan.RightString = "[REFUSE]";
        DLMan.ChoiceStarter();
        while (BChose.ButtonChoice == 0) { yield return null; }
        BLines.Hide(.1f);

        if (BChose.ButtonChoice == 1)
        {
            MySceneManager.Regret = 4;
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
            Line[0] = "- R...Really?";
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
            Line[0] = "- No one’s ever tried to make\nmy life easier before.";
            Line[1] = "- Thanks, perp.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
            while (DLMan.Playing) { yield return null; }
            yield return new WaitForSeconds(1.5f);
            StartCoroutine(LetTheFightBegin());
        }
        else
        {
            yield return new WaitForSeconds(2f);
            SFX[3].Stop();
            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Speed[0] = 0.04f;
            Speed[1] = 0.09f;
            Voice[0] = SFX[1];
            Voice[1] = SFX[0];
            Line[0] = "[tired sigh]";
            Line[1] = "- Copy that.";
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
            SceneMan.MChange(12, 0.4f);
            SceneMan.MPlay();
        }
    }

    #region Fight triggers
    public void NewLocation()
    {
        MoveSpot++;
        Stuff[0].SetActive(false);
        if (MoveSpot < MerPos.Length)   {   Merdeka.transform.position = MerPos[MoveSpot].transform.position;   }
    }

    public void SpeedChange1()
    {
            BulletSpeedChange[0].speedModifier = 0.03f;
            BulletSpeedChange[1].speedModifier = 0.03f;
            BulletSpeedChange[2].speedModifier = 0.03f;
    }
    #endregion

    #region Shoot
    public void Shoot(){ if (MySceneManager.Regret != 3) { StartCoroutine(ShootEnum()); } }

    IEnumerator ShootEnum()
    {
        MySceneManager.CutscenePlaying = true;
        Bullets[MoveSpot - 1].SetActive(false);
        Player.MyRigidBody.velocity = Vector2.zero;

        if (MySceneManager.Regret != 4)
        {
            MerLine();
            while (DLMan.Playing == false) { yield return null; }
            while (DLMan.Playing) { yield return null; }
        }

        Stuff[0].SetActive(true);
        Audiman.Play("Mer Gun Out");
        MyAnim.Play("FireGoUp");
        Anims[1].Play("Strawberry Activate", 0, 0);
        yield return new WaitForSeconds(1f);
        FireEffect.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        FireEffect.SetActive(false);
        Anims[0].Play("Death_End", 0, 0);
        Audiman.Play("Lazer");
        MyAnim.Play("Fire2");
        Bullets[MoveSpot].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        MyAnim.Play("Fight");

        MySceneManager.CutscenePlaying = false;
    }

    void MerLine()
    {
        if (MoveSpot == 1)
        {
            LineNum = 4;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "[silence]";
            Line[1] = "- Word of advice, perp:";
            Line[2] = "- Do things the right way.";
            Line[3] = "- Or do them in jail.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        }
        else if (MoveSpot == 2)
        {
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
            Line[0] = "- Lots of citizens are scared.\nNo one wants God of Death back.";
            Line[1] = "- But do you see anybody\npulling stunts like this?";
            Line[2] = "- Exactly.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        }
        else if (MoveSpot == 3)
        {
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
            Line[0] = "- So do the right thing.";
            Line[1] = "- And let these tasers hit you!";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        }
        else if (MoveSpot == 4)
        {
            LineNum = 4;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Speed[0] = 0.04f;
            Speed[1] = 0.1f;
            Speed[2] = 0.04f;
            Speed[3] = 0.08f;
            Voice[0] = SFX[0];
            Voice[1] = SFX[1];
            Voice[2] = SFX[1];
            Voice[3] = SFX[0];
            Line[0] = "- Chief. Permission to activate\ngun mode that’s actually useful?";
            Line[1] = "- N O";
            Line[2] = "[silence]";
            Line[3] = "- Copy that.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        }
        else if (MoveSpot == 5)
        {
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
            Line[0] = "- Know what you're achieving?";
            Line[1] = "- More paperwork. For me.\nThat's it.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        }
        else if (MoveSpot == 6)
        {
            LineNum = 4;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                Voice[i] = SFX[0];
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Line[0] = "- Look, perp.";
            Line[1] = "- If you give up.\nI promise.";
            Line[2] = "- The star will be in the right hands.\nI'll make sure.";
            Line[3] = "- Everything's going\nto be alright.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        }
        else if (MoveSpot == 7)
        {
            LineNum = 2;
            DeclareLines();
            #region Put things in
            for (int i = 0; i < LineNum; i++)
            {
                Speed[i] = 0.04f;
                TalkAnim[i] = CharAnim[0];
            }
            #endregion
            Voice[0] = SFX[1];
            Voice[1] = SFX[0];
            Line[0] = "[tired sigh]";
            Line[1] = "- I swear to the constitution.";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        }
        else if (MoveSpot == 8)
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
            Line[0] = "- Why always me?";
            #region Go
            DLMan.DILines = Line;
            DLMan.DITextSpeed = Speed;
            DLMan.DIVoice = Voice;
            DLMan.Noise = Noise;
            DLMan.WhoTalks = TalkAnim;
            DLMan.DIStarter();
            #endregion
        }
        else if (MoveSpot == 9)
        {
            Stuff[9].SetActive(true);
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
            Line[0] = "- Why. Is everything. Always.";
            Line[1] = "- So difficult.";
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
    #endregion

    #region Take damage
    //DAMAGE TAKEN
    public void Damage() { if ( !CurrentlyShocked ) { CurrentlyShocked = true; HitPoint--; StartCoroutine(TakeDamage()); } }

    IEnumerator TakeDamage()
    {
        if (HitPoint > 0)
        {
            //GameObject HP = Instantiate(HitCount, Player.gameObject.transform.position, Quaternion.identity) as GameObject;
            //HP.GetComponent<TextMesh>().text = HitPoint.ToString();
            //HP.GetComponent<Scaless_Follow_Target>().Target = Player.gameObject;
            HitCount.gameObject.SetActive(true);
            HitCount.text = HitPoint.ToString();
            Animator HPAnim = HitCount.GetComponent<Animator>();
            HPAnim.Play("TextPopup");
            HitFiller.fillAmount = HitPoint / 5f;

            Player.Dead = 1;
            Player.MyAnim.Play("Shocked");
            Audiman.Play("Shocked");
            while (!Player.MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Shocked")) { yield return null; }
            while (!Player.MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Crouch 2")) { yield return null; }
            Player.ResetAll();

            for (int i = 0; i < 10; i++)
            {
                PepRen.color = new Color(1, 1, 1, 0);
                yield return new WaitForSeconds(0.1f);
                PepRen.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.1f);
            }
            CurrentlyShocked = false;
            HitCount.gameObject.SetActive(false);
        } else
        {
            Anims[0].Play("Death_Start", 0, 0);
            Player.Dead = 1;
            yield return new WaitForSeconds(1f);
            SceneMan.JumpCut("[Main Menu]");
        }
    }
    #endregion

    public void Ending()
    {
        if (MySceneManager.Regret == 1 || MySceneManager.Regret == 2) { StartCoroutine(BossFightEnd()); }
        else if (MySceneManager.Regret == 4) { StartCoroutine(BossFightConfess()); }
        else if (MySceneManager.Regret == 3) { StartCoroutine(BossFightRefuse()); }
    }

    IEnumerator BossFightEnd()
    {
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        Player.MyRigidBody.velocity = Vector3.zero;
        BLines.Show(220f, 1f);


        yield return new WaitForSeconds(1f);

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Oh hello there, sweetie pie. You're officially leaving 'Life Star Storage Company' territory.";
        Line[1] = "- Is your letter of apology in order?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        CharAnim[1].Play("Old Lady Read");
        Audiman.Play("Ting");
        Merdeka.transform.position = MerPos[11].transform.position;
        Stuff[0].SetActive(true);
        Merdeka.transform.localScale = new Vector3(1, 1, 1);
        CharAnim[0].Play("Idle3");
        yield return new WaitForSeconds(1.5f);


        Stuff[2].SetActive(true);
        BLines.Hide(0.5f);
        Player.gameObject.transform.localScale = new Vector3(-1, 1, 1);
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
        Line[0] = "- End of the line, perp.";
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
        CharAnim[0].Play("FireGoUp");
        yield return new WaitForSeconds(0.5f);


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Don't rush me, honey. I'll let you out any moment now.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        Cutscene[3].Play();
        Done = false;
        while (Done == false) { yield return null; }
        Cutscene[3].Pause();


        yield return new WaitForSeconds(1f);
        Cutscene[3].Play();


        Done = false;
        while (Done == false) { yield return null; }
        Cutscene[3].Pause();

        CharAnim[1].Play("Old Lady Stop");
        CharAnim[2].Play("Turnstiles Closed");
        CharAnim[0].Play("Fight");
        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Dear Gods. Read the rules will you.";
        Line[1] = "- ONE at a time, please.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        CharAnim[2].Play("Turnstiles Closed");
        CharAnim[1].Play("Old Lady Read");
        Cutscene[3].Play();


        Done = false;
        while (Done == false) { yield return null; }
        Cutscene[3].Pause();
        yield return new WaitForSeconds(2f);

        BLines.Show(220f, 1f);
        Stuff[7].SetActive(true);
        CharAnim[0].Play("TakeRadio");
        yield return new WaitForSeconds(1.5f);


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
        Line[0] = "- Chief. Perpetrator is beyond reach.";
        Line[1] = "- Now what?";
        Line[2] = "- Let them get away?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        //Stuff[6].SetActive(true);
        //Stuff[7].SetActive(false);


        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            TalkAnim[i] = null;
            Speed[i] = 0.1f;
            Voice[i] = SFX[1];
        }
        #endregion
        Line[0] = "- Y E S";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        CharAnim[0].Play("Knockout");
        yield return new WaitForSeconds(3.5f);


        CharAnim[0].Play("Idle4");
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
        Line[0] = "- You win this round, perp.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        Stuff[7].SetActive(false);
        BLines.Hide(0.5f);


        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Strawb-o, activate protocol 22.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }


        Cutscene[4].Play();
        Done = false;


        while (Done == false) { yield return null; }
        Player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        Audiman.Play("News Flash");
        Stuff[8].SetActive(true);
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "- An important update just in! We’ve received an exclusive photo of the alleged star thief!";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        Audiman.Play("Dramatic Impact");
        Cutscene[5].Play();
        yield return new WaitForSeconds(1f);


        LineNum = 3;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "- The police urge the public to assist\nin tracking this criminal down.";
        Line[1] = "- But should we just assist\nor take the matter into our own hands?";
        Line[2] = "- We've assembled a panel of random hillbillies and disturbed moms to form public opinion about the situation in question.";
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


        Stuff[3].SetActive(true);
        SceneMan.MChange(13, 0.5f);
        SceneMan.MPlay();
        BLines.Show(220f, 0f);
        LineNum = 6;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- Don't know where you got the idea.";
        Line[1] = "- That you can just\ndo anything you want.";
        Line[2] = "- And risk everyone's lives\ntrying to be some kind of hero.";
        Line[3] = "- But. You're not going anywhere.";
        Line[4] = "- I will find you. No matter where you go.";
        Line[5] = "- Really hope that's clear to you, perp.";
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
        BLines.Hide(0);
        Player.gameObject.transform.localScale = new Vector3 (-1,1,1);
        yield return new WaitForSeconds(3.5f);

        SceneMan.MStop();

        CharAnim[1].Play("Old Lady Idle");
        SceneMan.MStop();
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Oh my, I’ve forgotten what I was reading.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        Audiman.Play("Door");
        Anims[2].Play("Gate Open");

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Alright, come right on through, sweetie pie.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        ES3.DeleteFile("SaveFile.es3");
        Stuff[4].SetActive(true);
        Audiman.Play("Dramatic Impact");
        yield return new WaitForSeconds(9f);
        SceneMan.JumpCut("Teaser_Zone");
    }

    IEnumerator BossFightConfess()
    {
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        Player.MyRigidBody.velocity = Vector3.zero;
        BLines.Show(220f, 1f);


        yield return new WaitForSeconds(1f);

        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Oh, hello there, sweetie pie. You're officially leaving 'Life Star Storage Company' territory.";
        Line[1] = "- Is your letter of apology in order?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        CharAnim[1].Play("Old Lady Read");
        Audiman.Play("Ting");
        Merdeka.transform.position = MerPos[11].transform.position;
        Merdeka.transform.localScale = new Vector3(1, 1, 1);
        Stuff[0].SetActive(true);
        CharAnim[0].Play("TakeRadioIdle2");
        yield return new WaitForSeconds(1.5f);


        Stuff[2].SetActive(true);
        BLines.Hide(0.5f);
        Player.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(3f);


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
        Line[0] = "- Look, perp. I like you.";
        Line[1] = "- But you’ve broken the law.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }



        Cutscene[4].Play();
        Done = false;
        while (Done == false) { yield return null; }
        Audiman.Play("News Flash");
        Player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        Stuff[8].SetActive(true);
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.03f;
            Voice[i] = SFX[1];
            TalkAnim[i] = null;
        }
        #endregion
        Line[0] = "- An important update just in!  We’ve just received an exclusive photo of the alleged star thief!";
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


        SceneMan.MChange(13, 0.5f);
        SceneMan.MPlay();
        Stuff[3].SetActive(true);
        BLines.Show(220f, 0f);
        LineNum = 5;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.04f;
            Voice[i] = SFX[0];
            TalkAnim[i] = CharAnim[0];
        }
        #endregion
        Line[0] = "- I get it.";
        Line[1] = "- It's brave. Taking the matter\ninto your own hands.";
        Line[2] = "- But it’s just too much responsibility for someone like you.";
        Line[3] = "- You'll mess everything up.\nFor all of us.";
        Line[4] = "- And I can't. Let you do that.";
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
        BLines.Hide(0);
        Player.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(3.5f);


        CharAnim[1].Play("Old Lady Idle");
        SceneMan.MStop();
        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Oh my, I’ve forgotten what I was reading.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        Audiman.Play("Door");
        Anims[2].Play("Gate Open");

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Alright, come right on through, sweetie pie.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }



        ES3.DeleteFile("SaveFile.es3");
        Stuff[4].SetActive(true);
        Audiman.Play("Dramatic Impact");
        yield return new WaitForSeconds(9f);
        SceneMan.JumpCut("Teaser_Zone");
    }

    IEnumerator BossFightRefuse()
    {
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        Player.MyRigidBody.velocity = Vector3.zero;
        BLines.Show(220f, 1f);
        yield return new WaitForSeconds(1f);


        LineNum = 2;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Oh, hello there, sweetie pie. You're officially leaving 'Life Star Storage Company' territory.";
        Line[1] = "- Is your letter of apology in order?";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        CharAnim[1].Play("Old Lady Read");
        Audiman.Play("Ting");
        yield return new WaitForSeconds(1.5f);
        Merdeka.transform.position = MerPos[11].transform.position;
        Merdeka.transform.localScale = new Vector3(1, 1, 1);
        CharAnim[0].Play("Idle1");
        Stuff[2].SetActive(true);
        BLines.Hide(0.5f);
        Player.gameObject.transform.localScale = new Vector3(-1, 1, 1);
        yield return new WaitForSeconds(3f);


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
        Line[0] = "- When you decide that you'd rather face jail time.";
        Line[1] = "- Than attempt to prolong the imprisonment of the God of Death by yourself, give me a signal.";
        Line[2] = "- And this all will be a competent person's problem.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        yield return new WaitForSeconds(1.5f);

        LineNum = 1;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- That handwriting...";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        CharAnim[1].Play("Old Lady Idle");

        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Oh dear Gods. Joey! Is that you?\nYou’ve lost so much weight!";
        Line[1] = "- And you’ve changed your signature!";
        Line[2] = "- Oh, sweetie, when will\nyou stop all this buffoonery?";
        Line[3] = "- I told you - you should pray. Pray for\nsalvation before it’s too late.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        Player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        yield return new WaitForSeconds(1.5f);


        SceneMan.MChange(13, 0.5f);
        SceneMan.MPlay();
        Stuff[5].SetActive(true);
        BLines.Show(220f, 2f);
        LineNum = 4;
        DeclareLines();
        #region Put things in
        for (int i = 0; i < LineNum; i++)
        {
            Speed[i] = 0.08f;
            Voice[i] = SFX[2];
            TalkAnim[i] = CharAnim[1];
        }
        #endregion
        Line[0] = "- Have you not heard the news, young man?";
        Line[1] = "- Our lord is returning, praise be.\nHe will make things right.";
        Line[2] = "- You must mend your ways.";
        Line[3] = "- Or he will make you.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }
        BLines.Hide(0f);
        Stuff[5].SetActive(false);
        Audiman.Play("Dramatic Impact");
        SceneMan.MStop();
        yield return new WaitForSeconds(3f);


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
        Line[0] = "- Yep.";
        Line[1] = "- Just give the signal, Joey.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
        while (DLMan.Playing) { yield return null; }

        
        ES3.DeleteFile("SaveFile.es3");
        Stuff[4].SetActive(true);
        Audiman.Play("Dramatic Impact");
        yield return new WaitForSeconds(9f);
        SceneMan.JumpCut("Teaser_Zone");
    }

    public void FadeMusic() { SceneMan.MFadeOut(0.001f, true); }

        #region Lines
        #region It's donskie
        public void CutsceneDone() { Done = true; }
    #endregion

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
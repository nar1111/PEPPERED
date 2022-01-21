using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State_Jeff_Lookout : State
{
    #pragma warning disable 649
    [Header("Everything")]
    public FOV FieldOfView;
    [SerializeField] private PLAYER_CONTROLS Pepper;
    [SerializeField] private GameObject UIThing;
    [SerializeField] private Animator JeffAnim;
    [SerializeField] private Animator JeffShaker;
    [SerializeField] private ParticleSystem[] RingEffect;
    [SerializeField] private float TimeToProcess;
    [SerializeField] private GameObject Cam;
    [SerializeField] private Black_lines BLines;
    [HideInInspector] private bool Solved = false;
    private float Modifier;
    private float Timer = 0.1f;
    private float AnnoyLvl = 0;
    private float ChillTimer = 0;
    private bool Safer = false;

    [Header("UI")]
    [SerializeField] private Image StealthUI;
    [SerializeField] private Image AngerUI;
    [SerializeField] private GameObject LoadingBar;
    [SerializeField] private State_Jeff_Egg EggState;
    [SerializeField] private State_Jeff_Detect Detect;
    [SerializeField] private The_Jeff_Egg Eggg;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField] private AudioSource DoorBellSource;
    [SerializeField] private AudioClip DoorBell;
    [SerializeField] private DialogueManager DLMan;
    [SerializeField] private AudioSource[] SFX;

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
        FOVFunction();
        AnnoyFunction();

        if (AnnoyLvl >= 1)
        {
            Pepper.CanMove = false;
            MySceneManager.CutscenePlaying = true;
            Pepper.MyRigidBody.velocity = Vector2.zero;
            Cam.SetActive(true);
            JeffAnim.Play("Jeff_Scream");
            BLines.Show(220f, 1f);
            FieldOfView.gameObject.SetActive(false);
            UIThing.SetActive(false);
            Solved = true;
            return EggState;
        }

        if (FieldOfView.PepperInSight == 600)
        {
            Pepper.CanMove = false;
            MySceneManager.CutscenePlaying = true;
            Pepper.MyRigidBody.velocity = Vector2.zero;
            Cam.SetActive(true);
            BLines.Show(220f, 1f);
            FieldOfView.gameObject.SetActive(false);
            UIThing.SetActive(false);
            JeffAnim.Play("Jeff_Annoyed_1");
            Solved = true;
            return Detect;
        }
        return this;
    }

    private void FOVFunction()
    {
        if (FieldOfView.PepperInSight == 1)
        {
            if (LoadingBar.activeInHierarchy == false) { LoadingBar.SetActive(true); }
            Modifier = 4f;
            TimerAdd();
        } else
        {
            if (Timer > 0f)
            {
                if (LoadingBar.activeInHierarchy == true) { LoadingBar.SetActive(false); }
                Timer -= Time.deltaTime;
                StealthUI.fillAmount = Timer / TimeToProcess;
            }
        }
    }

    private void TimerAdd()
    {
        if (Timer <= TimeToProcess)
        {
            Timer += Time.deltaTime * Modifier;
        }
        else if (Timer >= TimeToProcess)
        {
            Timer = TimeToProcess;
            LoadingBar.SetActive(false);
            FieldOfView.gameObject.SetActive(false);
            FieldOfView.PepperInSight = 600;
        }
        StealthUI.fillAmount = Timer / TimeToProcess;
    }

    public void DoorBellReact()
    {
        if (!Solved && !Safer)
        {
                Safer = true;
                RingEffect[0].Stop();
                RingEffect[0].Play();
                RingEffect[1].Stop();
                RingEffect[1].Play();
                if (AnnoyLvl == 0) { AnnoyLvl = AnnoyLvl + 0.2f; }
                else
                {
                    AnnoyLvl = AnnoyLvl + 0.018f;
                }
                ChillTimer = 0.4f;
                FieldOfView.gameObject.SetActive(false);
                DoorBellSource.PlayOneShot(DoorBell, 0.3f);
                JeffShaker.Play("Shaker_Shake");
                Invoke("ResetDoor", 0.1f);
        } else if (Solved)
        {
            if (Eggg.EggStage == 0 && Detect.CurrentState == 0) { CallDoorAgain(); }
            else if (Eggg.EggStage == 0 && Detect.CurrentState == 1) { NoMoreDoorbellsForYou(); }
            else { Oops(); }
        }
    }

    private void ResetDoor() { Safer = false; }

    private void AnnoyFunction()
    {
        if (AnnoyLvl > 0)
        {
            if (ChillTimer > 0) { ChillTimer -= Time.deltaTime; }
            else { AnnoyLvl -= Time.deltaTime; }

            if (AnnoyLvl > 0 && AnnoyLvl < 0.7f) { JeffAnim.Play("Jeff_Annoyed_1"); }
            else if (AnnoyLvl > 0.7f) { JeffAnim.Play("Jeff_Annoyed_2"); }
        }
        else
        {
            JeffAnim.Play("Jeff_Idle");
            JeffShaker.Play("Shaker_Idle");
            if (FieldOfView.gameObject.activeInHierarchy == false) { FieldOfView.gameObject.SetActive(true); AnnoyLvl = 0f; }
        }
        AngerUI.fillAmount = AnnoyLvl / TimeToProcess;
    }

    private void CallDoorAgain()
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
        Line[0] = "You probably should not ruin the moment for him.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
    }

    private void NoMoreDoorbellsForYou()
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
        Line[0] = "Sadly, this button has lost its potential forever.";
        #region Go
        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;
        DLMan.Noise = Noise;
        DLMan.WhoTalks = TalkAnim;
        DLMan.DIStarter();
        #endregion
    }

    private void Oops()
    {
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
        Line[0] = "Somebody has to break it to him.";
        Line[1] = "Somebody else, preferably.";
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
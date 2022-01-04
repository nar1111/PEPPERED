using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopLight : MonoBehaviour
{
    #pragma warning disable 649
    [Header("--STOP LIGHT --")]
    [SerializeField] private float Countdown;
    [SerializeField] private TextMesh CountdownText;
    [SerializeField] private Sprite[] SpriteStage;
    private SpriteRenderer MyRen;
    private float FuncTimer;

    [Header("-- SAW --")]
    [SerializeField]private string SFXName;
    [SerializeField]private Buzzsaw[] Buzzsaws;
    [SerializeField]private float SawSpeed;
    private int OnPoint = 0;

    [Header ("-- PLAYER --")]
    [SerializeField]private PLAYER_CONTROLS Player;
    [SerializeField]private AUDIOMANAGER AudiMan;
    private bool PlayerInRange = false;

    private void Start() {   MyRen = GetComponent<SpriteRenderer>();   }

    public void Update()
    {
        StartTheEngine();
        CountdownRun();
    }

    private void CountdownRun()
    {
        //Countdown start
        if (FuncTimer > 0)
        {
            FuncTimer -= 1 * Time.deltaTime;
            CountdownText.text = FuncTimer.ToString("0");
        }
        //Countdown ran out
        else if (FuncTimer < 0)
        {
            FuncTimer = 0;
            CountdownText.gameObject.SetActive(false);
            MyRen.sprite = SpriteStage[2];

            for (int i = 0; i < Buzzsaws.Length; i++)
            {
                Buzzsaws[i].StartTheEngine(SawSpeed);
            }
        }
    }

    private void StartTheEngine()
    {
        if (PlayerInRange && MySceneManager.Act && FuncTimer == 0 && OnPoint == 0)
        {
            AudiMan.Play(SFXName);
            FuncTimer = Countdown;
            CountdownText.gameObject.SetActive(true);
            MyRen.sprite = SpriteStage[1];

            for (int i = 0; i < Buzzsaws.Length; i++)
            {
                Buzzsaws[i].StartTheEngine(SawSpeed);
                OnPoint++;
            }
        }
    }

    public void StartPoint()
    {
        OnPoint--;
        if (OnPoint == 0) { MyRen.sprite = SpriteStage[0]; }
    }

    private void OnTriggerEnter2D(Collider2D other) {   if (other.CompareTag("Player")) {  PlayerInRange = true;  }  }

    private void OnTriggerExit2D(Collider2D other)  {   if (other.CompareTag("Player")) {  PlayerInRange = false; }  }
}
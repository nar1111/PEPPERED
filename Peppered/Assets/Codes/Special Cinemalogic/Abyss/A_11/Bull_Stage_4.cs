using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class Bull_Stage_4 : State
{
    [Header("STUFF")]
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private GameObject[] Bulls;
    [SerializeField] private Transform[] DropPoints;
    [SerializeField] private Animator[] CharAnim;
    [SerializeField] private Black_lines BLines;
    [SerializeField] private Bull_Stage_Win WinStage;
    private int MyStage = 0;

    [SerializeField] private PlayableDirector[] Cutscene;
    [SerializeField] private GameObject[] Stuff;
    [SerializeField] private Text AdditionalText;
    [SerializeField] private GameObject ShakeEffect;
    [SerializeField] private GameObject ShakeEffectBIG;

    [Header("ATTACK")]
    [SerializeField] private Transform[] ChargePoints;
    [SerializeField] private Transform FinalPoint;
    [SerializeField] private float Speed;
    private bool Done = false;

    private int ChargeNum = 0;
    private float Timer;
    private float StartTimer = 2f;
    private int CurrentChargePoint = 0;

    private float HalfTimer;
    private float MinimalTimer;

    public override State RunCurrentState()
    {
        if (MyStage == 0)
        {
            MyStage = 1;
            Bulls[0].layer = LayerMask.NameToLayer("Light");
            Bulls[1].layer = LayerMask.NameToLayer("Player And Me");
            HalfTimer = StartTimer / 2;
            MinimalTimer = StartTimer / 6;
            Bulls[0].SetActive(true);
            Bulls[1].SetActive(true);
            Bulls[0].transform.position = DropPoints[0].transform.position;
            Bulls[1].transform.position = DropPoints[1].transform.position;
            Bulls[1].transform.localScale = new Vector3(-1, 1, 1);
            CharAnim[0].Play("Bull Bro Trick");
            CharAnim[1].Play("Bro 2 Chant");
        }

        else if (MyStage == 2)
        {
            if (Timer > 0)
            {
                Charge();
            } else
            {
                Go();
            }
        }
        else if (MyStage == 3)
        {
            SlooowDown();
        }
        else if (MyStage == 5)
        {
            Timer -= Time.deltaTime;
            if (Timer < 1.2f && Timer > 0) { CharAnim[0].Play("Bull Bro Wind Up Max"); }
            else if (Timer <= 0)
            {
                MyStage = 6;
                CharAnim[0].Play("Bull Bro Push");
                Instantiate(ShakeEffect, new Vector3(Bulls[0].transform.position.x, Bulls[0].transform.position.y - 1), Quaternion.identity);
            }
        }
        else if (MyStage == 6)
        {
            if (Vector2.Distance(Bulls[0].transform.position, FinalPoint.position) > 0.2f)
            {
                Bulls[0].transform.position = Vector2.MoveTowards(Bulls[0].transform.position, new Vector2(FinalPoint.position.x, Bulls[0].transform.position.y), 18 * Time.deltaTime);
                if (Vector2.Distance(Bulls[0].transform.position, Bulls[1].transform.position) < 0.2f)
                {
                    Bulls[1].transform.position = Vector2.MoveTowards(Bulls[1].transform.position, new Vector2(FinalPoint.position.x, Bulls[1].transform.position.y), 18 * Time.deltaTime);
                }
            } else
            {
                MyStage = 7;
                Timer = 4f;
                Player.CanMove = false;
                Player.MyRigidBody.velocity = Vector2.zero;
                Player.transform.localScale = new Vector3(1,1,1);
                BLines.Show(220, 0.5f);
                Instantiate(ShakeEffectBIG, new Vector3(Bulls[0].transform.position.x, Bulls[0].transform.position.y - 1), Quaternion.identity);
                Bulls[0].SetActive(false);
                Bulls[1].SetActive(false);
                Stuff[2].SetActive(false);
                Stuff[3].SetActive(false);
            }
        }
        else if (MyStage == 7)
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;
            } else
            {
                MyStage = 8;
                Stuff[1].SetActive(true);
                Player.CanMove = true;
                BLines.Hide(3f);
            }

        }
        else if (MyStage == 9) { return WinStage; }
        return this;
    }

    private void Charge()
    {
        Timer -= Time.deltaTime;
        if (Timer > HalfTimer) { CharAnim[0].Play("Bull Bro Idle"); }
        else if (Timer < HalfTimer && Timer > MinimalTimer) { CharAnim[0].Play("Bull Bro Charge"); }
        else { CharAnim[0].Play("Bull Bro Wind Up"); }
    }

    private void Go()
    {
        if (Vector2.Distance(Bulls[0].transform.position, ChargePoints[CurrentChargePoint].position) > 0.2f)
        {
            Bulls[0].transform.position = Vector2.MoveTowards(Bulls[0].transform.position, new Vector2(ChargePoints[CurrentChargePoint].position.x, Bulls[0].transform.position.y), Speed * Time.deltaTime);
            if (!CharAnim[0].GetCurrentAnimatorStateInfo(0).IsName("Bull Bro Push"))
            {
                CharAnim[0].Play("Bull Bro Push");
                Instantiate(ShakeEffect, new Vector3(Bulls[0].transform.position.x, Bulls[0].transform.position.y - 1), Quaternion.identity);
            }
        }
        else
        {
            Timer = StartTimer;
            CurrentChargePoint++;
            int LocalScaleNum = 1;

            if (CurrentChargePoint > ChargePoints.Length - 1)
            {
                CurrentChargePoint = 0;
                LocalScaleNum = -1;
            }

            Bulls[0].transform.localScale = new Vector3(LocalScaleNum, 1, 1);
            Speed = Speed + 3f;
            MyStage = 3;
        }
    }

    private void SlooowDown()
    {
        if (Vector2.Distance (Bulls[0].transform.position, ChargePoints[0].transform.position) < 3.5f)
        {
            if (Vector2.Distance(Bulls[0].transform.position, new Vector3(ChargePoints[0].position.x - 3, ChargePoints[0].position.y)) > 0.2f)
            {
                CharAnim[0].Play("Bull Bro Idle");
                Bulls[0].transform.position = Vector2.MoveTowards(Bulls[0].transform.position, new Vector2(ChargePoints[0].position.x - 3, Bulls[0].transform.position.y), Speed / 3 * Time.deltaTime);
            }
            else
            {
                ChargeNum++;
                if (ChargeNum < 5)
                {
                    StartTimer -= 0.3f;
                    MyStage = 2;
                }
                else
                {
                    Timer = 3f;
                    MyStage = 5;
                }
            }
        } else
        {
            if (Vector2.Distance(Bulls[0].transform.position, new Vector3(ChargePoints[1].position.x + 2.5f, ChargePoints[1].position.y)) > 0.2f)
            {
                CharAnim[0].Play("Bull Bro Idle");
                Bulls[0].transform.position = Vector2.MoveTowards(Bulls[0].transform.position, new Vector2(ChargePoints[1].position.x + 2.5f, Bulls[0].transform.position.y), Speed / 3 * Time.deltaTime);
            }
            else
            {
                ChargeNum++;
                if (ChargeNum < 5)
                {
                    StartTimer -= 0.3f;
                    MyStage = 2;
                }
                else
                {
                    Timer = 3f;
                    MyStage = 5;
                }
            }
        }
    }

    public void NewChallenge()
    {
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        Player.MyRigidBody.velocity = Vector2.zero;

        if (Player.transform.parent != null)
        {
            State_Cart_Ride MyRide = Player.transform.parent.GetComponentInChildren<State_Cart_Ride>();
            MyRide.Chill(0.5f);
        }
        BLines.Show(200f, 0.5f);
        StartCoroutine(FinalStage());
    }

    IEnumerator FinalStage()
    {
        yield return new WaitForSeconds(1.5f);
        CharAnim[0].Play("Bull Bro Idle");
        CharAnim[1].Play("Bro 2 Chant Stop");
        CharAnim[0].gameObject.transform.localScale = new Vector3(-1, 1, 1);

        yield return new WaitForSeconds(1f);
        Done = false;
        BLines.Show(100f, 0f);
        BLines.Show(220f, 2.5f);
        Cutscene[0].Play();
        while (Done == false) { yield return null; }

        Stuff[2].SetActive(true);
        Stuff[3].SetActive(true);
        Stuff[0].SetActive(true);
        AdditionalText.text = "jump.";
        CharAnim[1].Play("Bro 2 Chant");
        yield return new WaitForSeconds(1.5f);
        Stuff[0].SetActive(false);
        BLines.Hide(0f);
        AdditionalText.text = "";
        MySceneManager.CutscenePlaying = false;

        if (Player.transform.parent == null) { Player.CanMove = true; }

        yield return new WaitForSeconds(1f);
        Timer = StartTimer;
        MyStage = 2; 
    }

    public void ChangeStageWin()
    {
        MyStage = 9;
    }


    #region It's donskie
    public void CutsceneDone() { Done = true; }
    #endregion
}
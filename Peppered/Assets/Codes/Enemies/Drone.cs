using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private LineRenderer AimLine;
    [SerializeField] private LineRenderer ShootLine;
    [SerializeField] private LineRenderer WhiteLine;
    [SerializeField] private Animator MyAnim;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private Transform MaxPos;
    [SerializeField] private Transform MinPos;
    [SerializeField] private GameObject Target;
    [SerializeField] private bool Horizontal;
    [SerializeField] private RaycastHit2D HitInfo;
    [SerializeField] private ParticleSystem Orders;
    [SerializeField] private AUDIOMANAGER AudioMan;
    [SerializeField] private float TimerStart;
    public float SafeDistance = 0;
    public float FleeDistance;
    private float Timer;
    private float Distance;
    private int Stage = 0;
    //0 = off
    //1 = turning on
    //2 = follow
    //3 = ready
    //4 = fire
    // Update is called once per frame

    void Update()
    {
        MOVING();

        WAKEUP();

        FIRE();
    }

    private void FIRE()
    {
        //DEAR GOD FIRE
        if (Stage == 4)
        {
            HitInfo = Physics2D.Raycast(FirePoint.position, transform.TransformDirection(-Vector2.up));
            ShootLine.gameObject.SetActive(true);
            ShootLine.SetPosition(0, new Vector3(FirePoint.position.x, FirePoint.position.y));
            ShootLine.SetPosition(1, new Vector3(HitInfo.point.x, HitInfo.point.y));
            WhiteLine.SetPosition(0, new Vector3(FirePoint.position.x, FirePoint.position.y));
            WhiteLine.SetPosition(1, new Vector3(HitInfo.point.x, HitInfo.point.y));
            if (HitInfo.transform.name == "PEPPER" && Player.Dead == 0) { KillPlayer(); }
        }
    }

    private void WAKEUP()
    {
        //Проснулся
        Distance = Vector3.Distance(Target.transform.position, transform.position);

        //Игрок подошел слишком близко
        if (Distance < SafeDistance && Stage == 0) { ChangeStage(); }
        else if (Distance > FleeDistance && Stage != 0) { Stage = 0; StopCoroutine(AimHor()); }
    }

    private void MOVING()
    {
        if (Stage == 2 && Player.Dead == 0)
        {
            if (Horizontal)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(Mathf.Clamp(Target.transform.position.x, MinPos.position.x, MaxPos.transform.position.x), transform.position.y), 7 * Time.deltaTime);
            }
            else if (!Horizontal)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, Mathf.Clamp(Target.transform.position.y, MinPos.position.y, MaxPos.transform.position.y)), 7 * Time.deltaTime);
            }

            if (Timer > 0) { Timer -= Time.deltaTime; }
            else { ChangeStage(); StartCoroutine(AimHor()); }
        }
    }

    public void ChangeStage()
    {
        if (Stage <= 4){ Stage++; }
        else { Stage = 0; }

        if (Stage == 1) { MyAnim.Play("Drone On"); Timer = TimerStart;  } 
    }

    void KillPlayer()
    {
        Player.Death(1, 1.5f);
        Stage = 0;
        StopCoroutine(AimHor());
    }

    #region Aim
    IEnumerator AimHor()
    {
        AimLine.gameObject.SetActive(true);
        MyAnim.Play("Drone Aim");
        AudioMan.Play("Amount1");
        Orders.Stop();
        Orders.Play();
        for (int i = 0; i < 10; i++)
        {
            HitInfo = Physics2D.Raycast(FirePoint.position, transform.TransformDirection(-Vector2.up));
            AimLine.SetPosition(0, new Vector3(FirePoint.position.x, FirePoint.position.y));
            AimLine.SetPosition(1, new Vector3(HitInfo.point.x, HitInfo.point.y));
            yield return new WaitForSeconds(0.1f);
        }
        AimLine.gameObject.SetActive(false);
        MyAnim.Play("Drone Fire");
        AudioMan.Play("Lazer");
        Stage = 4;
        yield return new WaitForSeconds(0.7f);
        ShootLine.gameObject.SetActive(false);
        MyAnim.Play("Drone On");
        Stage = 1;
        Timer = TimerStart;

    }
    #endregion
}
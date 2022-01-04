using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator_Trap : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private float ElevatorSpeed;
    [SerializeField] private Transform TopPoint;
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private float BounceForce;
    [SerializeField] private GameObject WarningZone;
    [SerializeField] private GameObject ShakeEffect;

    private Vector3 BottomPoint;
    private bool Activate = false;
    private float Timer;


    private void Start()
    {
        BottomPoint = transform.position;
    }

    void Update()
    {
        Restart();
        MoveElevator();
    }

    public void Activator()
    {
        if (Vector2.Distance(transform.position, TopPoint.position) > 0.02f)
        {
            Activate = true;
            if (WarningZone != null)
            {
                WarningZone.SetActive(true);
            }
        }
    }

    public void NPCActivator()
    {
        Activate = true;
    }

    void Restart()
    {
        if (Vector2.Distance(transform.position, TopPoint.position) < 0.02f && Timer > 0)
        {
            Timer -= Time.deltaTime;
        } else if (Timer < 0 && Activate == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, BottomPoint, ElevatorSpeed * Time.deltaTime);
        }
    }

    void MoveElevator()
    {
        if (Activate == true && Vector2.Distance(transform.position, TopPoint.position) > 0.02f)
        {
            transform.position = Vector2.MoveTowards(transform.position, TopPoint.position, ElevatorSpeed * Time.deltaTime);
        }
        else if (Activate == true && Vector2.Distance(transform.position, TopPoint.position) < 0.02f)
        {
            Activate = false;
            Timer = 4;
            if (WarningZone != null) {WarningZone.SetActive(false);}
            ShakeEffect.SetActive(true);
            Invoke("TurnOffShake", 0.2f);

            if (Player.transform.parent != null)
            {
                Player.Bounce(BounceForce, 1);
            }
        }
    }

    private void TurnOffShake() { ShakeEffect.SetActive(false); }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.gameObject.transform.parent = this.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player.gameObject.transform.parent = null;
        }
    }
}
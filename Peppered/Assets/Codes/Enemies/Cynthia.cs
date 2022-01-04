using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Cynthia : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]private Rigidbody2D MyRigid;
    [SerializeField]private float PushTimer;
    [SerializeField]private GameObject Target;
    [SerializeField][Range (1, 30)]private float PushPower;
    [SerializeField]private Animator MyAnim;
    [SerializeField]private GameObject Bonk;
    [SerializeField]private GameObject AttackCol;
    [SerializeField]private CHAIR_PLAYER Chair;
    [SerializeField]private AUDIOMANAGER Audiman;
    [SerializeField]private PlayableDirector Cutscene;
    private int AttackNum = 0;
    private float Timer = 0.5f;
    [HideInInspector]public int Stage = 0;


    // Update is called once per frame
    void Update()
    {
        if (Target.transform.position.x < transform.position.x && transform.localScale == new Vector3(1,1,1) && Stage != 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        } else if (Target.transform.position.x > transform.position.x && transform.localScale == new Vector3(-1, 1, 1) && Stage != 0)
        {
            transform.localScale = new Vector3(1,1,1);
        }


        //Move
        if (Timer == 0 && Stage == 1)
        {
            Timer = PushTimer;
            AttackNum++;

            if (transform.localScale == new Vector3(1, 1, 1))
            {
                if (AttackNum < 3)
                {
                    MyRigid.AddForce(transform.right * PushPower, ForceMode2D.Impulse);
                } else
                {
                    AttackNum = 4;
                    MyAnim.Play("Cyn_WindUp");
                }
            }
            else if (transform.localScale == new Vector3(-1, 1, 1))
            {
                if (AttackNum < 3)
                {
                    MyRigid.AddForce(transform.right * -PushPower, ForceMode2D.Impulse);
                } else
                {
                    AttackNum = 4;
                    MyAnim.Play("Cyn_WindUp");
                }
            }
        }

        //Attack
        if (AttackNum == 5 && Stage == 1)
        {
            AttackNum = 0;
            AttackCol.SetActive(true);
            MyAnim.Play("Cyn_Att");
            if (transform.localScale == new Vector3(1, 1, 1)) { MyRigid.AddForce(transform.right * (PushPower + 8), ForceMode2D.Impulse); }
            else if (transform.localScale == new Vector3(-1, 1, 1)) { MyRigid.AddForce(transform.right * (-PushPower - 8), ForceMode2D.Impulse); }
            Timer = 0.5f;
        }

        //Timer
        if (Timer > 0 && AttackNum < 3 && Stage == 1) { Timer -= Time.deltaTime; }
        else if (Timer < 0 && Stage == 1) { Timer = 0; MyAnim.Play("Angry"); AttackCol.SetActive(false); }
    }

    public void AttTrg() { AttackNum = 5; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            if (Stage == 0)
            {
                Stage = -1;
                MyRigid.AddForce(transform.right * 18f, ForceMode2D.Impulse);
            }
            Timer = 0.8f;
            AttackNum = 1;
            AttackCol.SetActive(false);
            MyAnim.Play("Cyn_Dam");
            Audiman.Play("Rock Impact");
            Chair.StopMove();
            Instantiate(Bonk, transform.position, Quaternion.identity);
            if (Stage == 1)
            {
                Cutscene.Stop();
                Cutscene.Play();
                if (transform.localScale == new Vector3(1, 1, 1)) { MyRigid.AddForce(transform.right * -18f, ForceMode2D.Impulse); }
                else if (transform.localScale == new Vector3(-1, 1, 1)) { MyRigid.AddForce(transform.right * 18f, ForceMode2D.Impulse); }
            }
        }
    }
}

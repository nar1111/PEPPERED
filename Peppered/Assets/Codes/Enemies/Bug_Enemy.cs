using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//CHANGE HERE

public class Bug_Enemy : MonoBehaviour
{

    [Header ("PUT IT IN")]
    public GameObject Pepper;
    public PLAYER_CONTROLS PlayerCntr;
    public AUDIOMANAGER Audi;
    public GameObject HidingPlace;

    [Header ("Cosmetics")]
    public Object DeathEffect;
    public Object RespawnEffect;
    public SlowMotion_Manager Slomo;

    [Header("MOVEMENT")]
    public float SafeDistance;
    public float FleeDistamce;
    public float RespawnTime;
    public float AngerTime;
    public Rigidbody2D MyRigidBody;

    [Header("Audio")]
    public AudioSource Buzz;

    [Header("Tech stuff")]
    public SpriteRenderer SprRen;
    public Animator MyAnim;
    public CircleCollider2D Col;
    public GameObject AttackCol;

    //Private stuff
    private float Distance;
    private float StartColRad;
    private Vector3 SpawnPoint;
    private int Alerted;
    private float timer = 5f;
    private float ATimer;

    private void Start()
    {
        StartColRad = Col.radius;
        SpawnPoint = transform.position;
        ATimer = AngerTime;
    }

    private void Update()
    {
        if (SprRen.isVisible)
        {
 
            if (Alerted >= 1)
            {
                if (Alerted == 1)
                {
                    //Проснулся
                    Distance = Vector3.Distance(Pepper.transform.position, transform.position);
                    
                    //Игрок подошел слишком близко
                    if (Distance < SafeDistance) { Alerted = 2; MyAnim.Play("Fly");Col.radius = 0.30f; Buzz.Play(); gameObject.layer = 14; } 
                }

                //Где Пеппер? Слева? Справа?
                if (Pepper.transform.position.x < transform.position.x && Alerted < 6) { transform.parent.localScale = new Vector3(1f, 1f, 1f); }
                else if (Pepper.transform.position.x > transform.position.x && Alerted < 6) { transform.parent.localScale = new Vector3(-1f, 1f, 1f); }

                if (Alerted == 2) { Distance = Vector3.Distance(Pepper.transform.position, transform.position); }

                //Игрок убежал слишком далеко
                if (Alerted == 2 && Distance > FleeDistamce) { Alerted = 3; MyRigidBody.velocity = new Vector2(0f, -1.5f); Col.radius = 0.19f; gameObject.layer = 0; StartCoroutine(Respawn()); }

                //Автоматическое убирание
                if (Alerted == 5 && timer > 0) { timer -= Time.deltaTime; }
                if (timer <= 0 && Alerted == 5)
                {
                    Alerted = 6;
                    Instantiate(DeathEffect, transform.position, Quaternion.identity);
                    StartCoroutine(Respawn());
                }

                //Злится
                if (Alerted == 2 && Distance < SafeDistance)
                {
                    ATimer -= Time.deltaTime;
                }

                //Атакует
                if (ATimer <= 0 && Alerted == 2)
                {
                    Alerted = 7;
                    if (HidingPlace.activeInHierarchy)
                    {
                        ATimer = AngerTime;
                        MyAnim.Play("Fly");
                        Col.radius = 0.30f;
                        Alerted = 2;
                        MyRigidBody.velocity = Vector3.zero;
                        AttackCol.SetActive(false);
                    }
                    else
                    {
                        StartCoroutine(Attack());
                    }
                }
            } 
        }
    }

    #region Коллайдеры
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            if (Alerted == 0)
            {MyAnim.Play("Alert"); Alerted = 1;}

            if (Alerted == 8)
            {
                MyAnim.Play("Bite");
                StartCoroutine(Bonk());
            }
        }

        if (other.tag == "Ground") 
        { 
            if (Alerted == 3) 
            {
                MyAnim.Play("Smarchook");
                Alerted = 0;
                Col.radius = StartColRad;
                MyRigidBody.velocity = Vector3.zero;
            }

            if (Alerted == 8) 
            {
                StopAllCoroutines();
                Alerted = 9;
               // MyRigidBody.velocity = Vector3.zero;
                StartCoroutine(Bonk());
            }
        }

        if (other.tag == "Wall") 
        {
            if (Alerted == 8)
            {
                StopAllCoroutines();
                Alerted = 9;
               // MyRigidBody.velocity = Vector3.zero;
                StartCoroutine(Bonk());
            }
        }

        //Получил пиздюлей
        if (other.tag == "Attack") 
        {
            if (Alerted == 2 || Alerted == 7 || Alerted == 9) 
        {
                StopAllCoroutines();
                MyAnim.Play("Away");
                //Slomo.DoSlowMotion();
                PlayerCntr.MyRigidBody.velocity = new Vector3(PlayerCntr.MyRigidBody.velocity.x / 2, PlayerCntr.MyRigidBody.velocity.y);
                MyRigidBody.velocity = Vector2.zero;
                Alerted = 5;
                Audi.Play("Crush");


                if (Pepper.transform.localScale.x == 1)
                {
                        transform.parent.localScale = new Vector3(1f, 1f, 1f);
                        MyRigidBody.velocity = new Vector2(15f, 0f);
                }

                else if (Pepper.transform.localScale.x == -1)
                {
                        transform.parent.localScale = new Vector3(-1f, 1f, 1f);
                        MyRigidBody.velocity = new Vector2(-15f, 0f);
                }
                
            }
        }

        // Умер
        if (Alerted == 5 && other.tag != "Player" && other.tag != "Attack")
        {
            Slomo.DoSlowMotion();
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
            StartCoroutine(Respawn());
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player") 
        {
            //Игрок отошел, жук заснул
            if (Alerted == 1)
            {MyAnim.Play("Smarchook"); Alerted = 0;}
        }
    }
    #endregion

    #region Инумераторы
    private IEnumerator Respawn() 
    {
        MyRigidBody.velocity = new Vector3(0f, 0f);
        SprRen.color = new Color(255, 255, 255, 0);
        AttackCol.SetActive(false);
        gameObject.layer = 0;
        Col.radius = 0f;
        Alerted = 6;
        MyAnim.Play("Smarchook");
        Buzz.Stop();
        yield return new WaitForSeconds(RespawnTime);
        transform.parent.position = SpawnPoint;
        transform.parent.localScale = new Vector3(1f, 1f, 1f);

        Instantiate(RespawnEffect, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(0.7f);
        SprRen.color = new Color(255, 255, 255, 255);
        timer = 5f;
        ATimer = AngerTime;
        Alerted = 0;
        Col.radius = StartColRad;
    }

    private IEnumerator Attack() 
    {
        if (HidingPlace.activeInHierarchy)
        {
            ATimer = AngerTime;
            MyAnim.Play("Fly");
            Col.radius = 0.30f;
            Alerted = 2;
            MyRigidBody.velocity = Vector3.zero;
            AttackCol.SetActive(false);
            StopCoroutine(Attack());
        }
        else
        {
            Alerted = 7;
            MyRigidBody.velocity = Vector3.zero;
            MyAnim.Play("Prepare");

            // if (transform.parent.localScale.x == 1f) { MyRigidBody.velocity = new Vector2( -1.3f, 0f); }
            //else if (transform.parent.localScale.x == -1f) { MyRigidBody.velocity = new Vector2(1.3f, 0f); }

            yield return new WaitForSeconds(1.2f);

            Alerted = 8;
            Col.radius = 0.30f;
            MyRigidBody.velocity = (Pepper.transform.position - transform.position).normalized * 6.5f;
            if (Pepper.transform.position.x < transform.position.x) { transform.parent.localScale = new Vector2(1f, 1f); }
            else if (Pepper.transform.position.x > transform.position.x) { transform.parent.localScale = new Vector2(-1f, 1f); }

            SprRen.color = new Color(255, 255, 255, 255);
            MyAnim.Play("Attack");
            AttackCol.SetActive(true);

            yield return new WaitForSeconds(2f);
            ATimer = AngerTime;
            MyAnim.Play("Fly");
            Col.radius = 0.30f;
            Alerted = 2;
            MyRigidBody.velocity = Vector3.zero;
            AttackCol.SetActive(false);
        }
    }

    private IEnumerator Bonk() 
    {
        if (Alerted != 8)
        {
            AttackCol.SetActive(false);
            MyRigidBody.velocity = Vector3.zero;
            MyAnim.Play("Bonk");

            yield return new WaitForSeconds(0.1f);
            if (transform.parent.localScale.x == 1f) { MyRigidBody.velocity = new Vector2(2.5f, 0f); }
            else if (transform.parent.localScale.x == -1f) { MyRigidBody.velocity = new Vector2(-2.5f, 0f); }
            yield return new WaitForSeconds(0.4f);

            MyRigidBody.velocity = Vector3.zero;

            ATimer = AngerTime;
            MyAnim.Play("Fly");
            Col.radius = 0.30f;
            Alerted = 2;
            MyRigidBody.velocity = Vector3.zero;
            AttackCol.SetActive(false);
        } else
        {
            //УКУСИЛ
            Alerted = 42;
            timer = 5f;
            ATimer = AngerTime;
            Alerted = 0;
            Col.radius = StartColRad;
            MyRigidBody.velocity = Vector3.zero;
            yield return new WaitForSeconds(1.5f);
            MyRigidBody.velocity = new Vector3 ((MyRigidBody.velocity.x / 2), (MyRigidBody.velocity.y / 2));
            AttackCol.SetActive(false);
            Alerted = 3;
            MyRigidBody.velocity = new Vector2(0f, -1.5f);
            Col.radius = 0.19f;

        }

    }
    #endregion




}

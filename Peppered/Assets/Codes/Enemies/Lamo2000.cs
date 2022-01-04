using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamo2000 : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private float velocity;
    [SerializeField] private Animator MyAnim;
    [SerializeField] private Rigidbody2D MyRigid;
    [SerializeField] private Transform FirePoint;
    [SerializeField] private SpriteRenderer MyRen;
    [SerializeField] private GameObject Explosion;
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private GameObject UmYeah;
    [SerializeField] private AUDIOMANAGER Audioman;
    [SerializeField] private GameObject Oil;
    [SerializeField] private AudioSource ActivatedSounds;
    private RaycastHit2D HitInfo;
    private int Activated = 0;
    private float Timer = 5;

    // Update is called once per frame
    void Update()
    {
        WorkRobot();
    }

    private void WorkRobot()
    {
        if (MyRen.isVisible)
        {
            if (transform.localScale.x == -1 && Activated == 0)
            {
                HitInfo = Physics2D.Raycast(FirePoint.position, Vector3.right);
            }

            else if (transform.localScale.x == 1 && Activated == 0)
            {
                HitInfo = Physics2D.Raycast(FirePoint.position, -Vector3.right);
            }

            if (HitInfo.transform.name == "PEPPER" && Activated == 0)
            {
                Activated = 1;
                UmYeah.SetActive(true);
                MyAnim.Play("Lamo 2000");
                ActivatedSounds.Play();
            }

            if (Activated == 1)
            {
                if (transform.localScale.x == -1) { MyRigid.velocity = new Vector2(velocity, MyRigid.velocity.y); }
                else { MyRigid.velocity = new Vector2(-velocity, MyRigid.velocity.y); }
                if (Timer != 5) { Timer = 5; }
            }
        }
        else if (MyRen.isVisible == false && Activated == 1)
        {
            Timer -= Time.deltaTime;
            if (Timer < 0)
            {
                if (Oil != null) { Instantiate(Oil, transform.position, Quaternion.identity); }
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (Activated == 1)
        {
            if (other.gameObject.tag == "Wall" || other.gameObject.tag == "Player" || other.gameObject.name == "Lamo 2000")
            {
                Instantiate(Explosion, new Vector3(transform.position.x, transform.position.y + 0.2f), Quaternion.identity);
                if (other.gameObject.tag == "Player") { Player.Death(0, 1.5f) ; }
                ActivatedSounds.Stop();
                Audioman.Play("Explode");
                if (Oil != null) { Instantiate(Oil, transform.position, Quaternion.identity); }
                Destroy(gameObject);
            }
        }
    }
}

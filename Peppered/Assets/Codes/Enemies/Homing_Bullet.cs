using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Homing_Bullet : MonoBehaviour
{

    public Animator MyAnim;
    public SpriteRenderer Renderer;
    public float Speed;
    public float RotateSpeed;
    public float FollowTime;
    public GameObject Explosion;
    public Collider2D MyCol;
    private Transform Target;
    public Rigidbody2D MyRigidbody;
    private bool Follow = true;
    private MERBOSS BOSS;
    private float CoolDown;

	// Use this for initialization
	void Start () 
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        CoolDown = 0.3f;
        RotateSpeed = 200f;
        MyRigidbody.AddForce(transform.up * 6f, ForceMode2D.Impulse);
    }

	void Update()
    {
        if (Renderer.isVisible)
        {
            if (FollowTime > 0) { FollowTime -= Time.deltaTime; }
            if (FollowTime < 0 && Follow)
            {
                Follow = false;
                MyAnim.SetTrigger("end");
                MyRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }

            if (MyCol.enabled != true && CoolDown > 0) { CoolDown -= Time.deltaTime; }
            else if (MyCol.enabled != true && CoolDown < 0) { MyCol.enabled = true; }

        }
        else { Destroy(gameObject); }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Renderer.isVisible)
        {
            if (Follow && MyCol.enabled == true)
            {
                Vector2 direction = (Vector2)Target.position - MyRigidbody.position;
                direction.Normalize();
                float rotateamount = Vector3.Cross(direction, transform.up).z;

                MyRigidbody.angularVelocity = -rotateamount * RotateSpeed;
                MyRigidbody.velocity = transform.up * Speed;
            }

            else if (!Follow && MyCol.enabled == true) { MyRigidbody.velocity = transform.up * Speed; }
        }
	}

	void OnTriggerEnter2D(Collider2D other)
	{
        if (other.tag != "KillPlane" && other.tag != "Attack")
        {
            if (other.tag == "Ground" && !Follow)
            {
              //  Instantiate(Explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            } else if (other.tag == "Player"){
           // Instantiate(Explosion, transform.position, transform.rotation);
                Destroy(gameObject);}
        }
	}


}

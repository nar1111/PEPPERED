using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Straight_Bullet : MonoBehaviour 
{
    private float Speed = 3f;

    public GameObject Explosion;
    public Rigidbody2D MyRigidbody;

	void Start()
	{
        MyRigidbody.AddForce(transform.up * Speed, ForceMode2D.Impulse);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "KillPlane" && other.tag != "Attack")
        {
            if (other.tag == "Wall")
            {
                if (Explosion != null)
                {
                    Instantiate(Explosion, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }

            else if (other.tag == "Player")
            {
                if (Explosion != null)
                {
                    Instantiate(Explosion, transform.position, transform.rotation);
                }
                Destroy(gameObject);
            }
        }
    }

}

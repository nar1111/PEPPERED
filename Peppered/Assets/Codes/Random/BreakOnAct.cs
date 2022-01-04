using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BreakOnAct : MonoBehaviour
{

    public GameObject BrokenBits;
    public float force;


    // Start is called before the first frame update
    void Start()
    {
        GameObject Broke = (GameObject)Instantiate(BrokenBits, transform.position, Quaternion.identity);
        foreach (Transform child in Broke.transform)
        {
            child.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-force, force), Random.Range(-force, force));
            child.GetComponent<Rigidbody2D>().AddTorque(20, ForceMode2D.Impulse);
        }

        Destroy(gameObject);
    }
}
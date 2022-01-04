using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect_Chip : MonoBehaviour
{
    public string CoinName;
    public AUDIOMANAGER Audioman;
    public Object Nice;

    void Start()
    {
        if (WHAT_HAVE_I_DONE.Collectibles.ContainsKey(CoinName)) { Destroy(gameObject); }
    }


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Audioman.Play("Confirm");
            if (Nice != null)
            {
                WHAT_HAVE_I_DONE.Collectibles.Add(CoinName, 1);
                WHAT_HAVE_I_DONE.Coins++;
                Instantiate(Nice, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}

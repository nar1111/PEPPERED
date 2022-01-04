using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildOfPlatofrm : MonoBehaviour
{
    public GameObject Player;

    private void OnCollisionEnter2D(Collision2D collision)  { if (collision.gameObject.CompareTag("Player")) { Player.transform.parent = this.transform; } }

    private void OnCollisionExit2D(Collision2D collision)   { if (collision.gameObject.CompareTag("Player")) { Player.transform.parent = null; } }
}

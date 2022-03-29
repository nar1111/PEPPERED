using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart_Crash : MonoBehaviour
{
    [SerializeField] private AUDIOMANAGER Audiman;
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private GameObject BrokenCart;
    [SerializeField] private State_Manager StateMan;
    [SerializeField] private State_Cart_Idle IdleState;
    [SerializeField] private GameObject ShakeEffect;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "KillPlane")
        {
            Shit();
        }
        else if (other.tag == "Boss")
        {
            Poop();
        }
    }

    public void Shit()
    {
        if (StateMan.currentState != IdleState)
        {
            Instantiate(BrokenCart, transform.position, Quaternion.identity);
            Instantiate(ShakeEffect, new Vector3(transform.position.x, transform.position.y - 1), Quaternion.identity);
            Player.gameObject.transform.localScale = transform.localScale;
            Player.Bounce(17f, 2);
            Player.gameObject.transform.parent = null;
            Player.MyAnim.Play("Take Off");
            Player.CanMove = true;
            Player.Wind = true;
            Audiman.Play("Cart Crash");
            MySceneManager.DontKillMe = 0;
            Destroy(gameObject);
        }
        else
        {
            Audiman.Play("Cart Crash");
            Instantiate(BrokenCart, transform.position, Quaternion.identity);
            Instantiate(ShakeEffect, new Vector3(transform.position.x, transform.position.y - 1), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Poop()
    {
        if (StateMan.currentState == IdleState)
        {
            Audiman.Play("Cart Crash");
            Instantiate(BrokenCart, transform.position, Quaternion.identity);
            Instantiate(ShakeEffect, new Vector3(transform.position.x, transform.position.y - 1), Quaternion.identity);
            Destroy(gameObject);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncy_Thing : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private Animator MyAnim;
    [SerializeField]
    private PLAYER_CONTROLS Player;
    [SerializeField]
    private float SmallJump;
    [SerializeField]
    private float BigJump;
    private bool Charged = true;
    [SerializeField]
    private AUDIOMANAGER Audioman;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && Charged && Player.DiveKick > 0)
        {
            Charged = false;
            Audioman.Play("Jump Platform Big");
            Player.Bounce(BigJump, 2);
            MyAnim.Play("Jump Platform Max");
        } else if (other.tag == "Player" && Charged && Player.DiveKick == 0)
        {
            Charged = false;
            Player.Bounce(SmallJump, 1);
            Audioman.Play("Jump Platform Small");
            MyAnim.Play("Jump Platform Min");
        }
    }

    void Ready() { Charged = true; }
}

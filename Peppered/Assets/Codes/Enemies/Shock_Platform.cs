using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shock_Platform : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private AUDIOMANAGER Audiman;
    [SerializeField] private Animator MyAnim;
    [SerializeField] private PLAYER_CONTROLS Player;
    private int TurnedOn;
    //0 = Turned off
    //1 = On a trigger now
    //2 = Deadly

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TurnedOn == 0) { TurnedOn = 1; MyAnim.Play("Shock Platform Trigger"); }
        else if(TurnedOn == 2)
        {
            TurnedOn = 3;
            MyAnim.Play("Shock Platform Turning Off");
            if (other.tag == "Player") { Player.Death(1, 1.5f); }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (TurnedOn == 1) { TurnedOn = 2; MyAnim.Play("Shock Platform On"); if (Audiman != null) { Audiman.Play("Shock Platform"); } }
        if (TurnedOn == 3) { TurnedOn = 0; }
    }

}

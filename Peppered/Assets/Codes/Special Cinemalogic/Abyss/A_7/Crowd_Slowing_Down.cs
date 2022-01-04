using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crowd_Slowing_Down : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private PLAYER_CONTROLS Player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player")) { Player.MyRigidBody.drag = 18f; Player.MyAnim.speed = 0.5f; }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) { Player.MyRigidBody.drag = 1f; Player.MyAnim.speed = 1f; }
    }
}

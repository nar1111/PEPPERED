using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Player : MonoBehaviour
{
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private Transform OutPoint;
    [SerializeField] private Transform MovePoint;
    [SerializeField] private bool FaceLeft = false;
    private int MoveGo = 0;


    private void Update()
    {
        if (Vector2.Distance(Player.gameObject.transform.position, MovePoint.position) > 0.2f && MoveGo == 1)
        {
            Player.MyAnim.SetBool("Cutscene", true);
            Player.MyAnim.SetFloat("Speed", 2);
            Player.MyAnim.Play("Run");
            if (FaceLeft == false)
             {
                 Player.MyRigidBody.velocity = new Vector2(3f, Player.MyRigidBody.velocity.y);
             } else
             {
                 Player.MyRigidBody.velocity = new Vector2(-3f, Player.MyRigidBody.velocity.y);
             }
        } else if (Vector2.Distance(Player.gameObject.transform.position, MovePoint.position) < 0.2f && MoveGo == 1)
        {
            MoveGo = 0;
            Player.MyAnim.SetBool("Cutscene", false);
            Player.MyAnim.SetFloat("Speed", 0);
            Player.MyRigidBody.velocity = Vector3.zero;
            Invoke("Unfreeze", 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player.ResetAll();
            Player.MyRigidBody.velocity = Vector3.zero;
            Player.CanMove = false;
            Player.gameObject.transform.position = OutPoint.position;
            if (FaceLeft == false) { Player.gameObject.transform.localScale = new Vector3(1, 1, 1); }
            else { Player.gameObject.transform.localScale = new Vector3(-1,1,1); }
            MoveGo = 1;
        }
    }

    private void Unfreeze()
    {
        Player.CanMove = true;
    }
}
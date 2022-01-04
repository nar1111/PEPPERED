using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Door : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private int RequiredNumber = 1;
    [SerializeField] private GameObject WallCollider;
    [SerializeField] private Animator MyAnim;
    [SerializeField] private AUDIOMANAGER AudiMan;
    [SerializeField] private MERBOSS Boss;

    [Header("PLAYER EFFECT")]
    [SerializeField] private PLAYER_CONTROLS Pepper;
    [SerializeField] private GameObject TakenEffect;
    private int KeyNumber;

    public void AddKey()
    {
        KeyNumber++;
        AudiMan.Play("Ting");
        Instantiate(TakenEffect, Pepper.transform.position, Quaternion.identity);
        if (KeyNumber == RequiredNumber)
        {
            MyAnim.SetTrigger("trg");
            WallCollider.SetActive(false);
            AudiMan.Play("Door");
            if (Boss != null){   Boss.NewLocation();  }
        }
    }

    public void Closed()
    {
        //AudiMan.Play("Door");
        WallCollider.SetActive(true);
        MyAnim.SetTrigger("trg");
    }

    public void Open(bool Quiet)
    {
        if (!Quiet) { AudiMan.Play("Door"); }
        else
        {
            WallCollider.SetActive(false);
            MyAnim.SetTrigger("trg");
        }
    }
}

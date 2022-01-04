using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dumpster_Moveset : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private AudioSource MoveSound;
    [SerializeField] private Animator Myanim;
    private bool Moved;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0f && Moved == false)
        {
            Moved = true;
            Myanim.Play("Dumpester_Shake");
            MoveSound.Play();
        }

        if (Input.GetAxisRaw("Horizontal") == 0) { Moved = false; }
    }
}

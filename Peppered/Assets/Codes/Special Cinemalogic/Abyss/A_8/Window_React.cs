using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window_React : MonoBehaviour
{
    [SerializeField] private Animator[] Myanim;
    [SerializeField] private AUDIOMANAGER Audiman;
    private int CurrentState = 0;

    public void React()
    {
        if (CurrentState < Myanim.Length)
        {
            Myanim[CurrentState].Play("Window_Filler_Close");
            CurrentState++;
            Audiman.Play("Door");
            Invoke("ReactSecondFloor", 0.15f);
        }
    }

    void ReactSecondFloor()
    {
        if (CurrentState < Myanim.Length)
        {
            Myanim[CurrentState].Play("Window_Filler_Close");
            Audiman.Play("Door");
            CurrentState++;
        }
    }

}

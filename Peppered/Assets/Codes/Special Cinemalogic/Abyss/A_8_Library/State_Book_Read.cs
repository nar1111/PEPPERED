using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Book_Read : State
{
    [SerializeField] private Dialogue_Holder[] BookContent;
    [SerializeField] private DialogueManager DialMan;
    [SerializeField] private State_Book_Look LookStage;
    private int EndStage = 0;

    public override State RunCurrentState()
    {
        if (EndStage == 1 && !DialMan.Playing)
        {
            EndStage = 0;
            return LookStage;
        }
        return this;
    }

    public void ReadIt(int BookNumber)
    {
        BookContent[BookNumber].StartDialogue();
        EndStage = 1;
    }
}

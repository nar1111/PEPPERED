using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_Choose : MonoBehaviour
{

    [HideInInspector]
    public int ButtonChoice;

    public void FirstOption()
    {
        ButtonChoice = 1;
    }

    public void SecondOption()
    {
        ButtonChoice = 2;
    }

}

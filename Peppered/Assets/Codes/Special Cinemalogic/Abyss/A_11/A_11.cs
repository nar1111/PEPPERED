using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_11 : MonoBehaviour
{
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private GameObject[] Stuff;
    [SerializeField] private Animator[] Charanim;
    [SerializeField] private AUDIOMANAGER Audiman;
    [SerializeField] private State_Manager BullManager;

    private float HandicapTimer;
    [HideInInspector]public bool Handicapped;
    private float Timer = 0;
    private float StartTimer = 2f;
    private bool EndingTriggered = false;


    private void Update()
    {
        if (Timer > 0) { Timer -= Time.deltaTime; }

        if (Handicapped == true && HandicapTimer > 0) { HandicapTimer -= Time.deltaTime; }
        else
        {
            HandicapTimer = 0;
            Handicapped = false;
            if (Vector2.Distance (Stuff[0].transform.position, Player.transform.position) < 0.9f  && Stuff[0].activeInHierarchy || Vector2.Distance (Stuff[1].transform.position, Player.transform.position) < 0.9f && Stuff[1].activeInHierarchy)
            {
                GetCaught();
            }
        }
    }


    public void ScareLeft()
    {
        Charanim[0].Play("Random_Dude_Scared_Left");
    }

    public void ScareRight()
    {
        Charanim[0].Play("Random_Dude_Scared_Right");
    }

    public void GetCaught()
    {
        if (Timer <= 0)
        {
            if (Player.transform.parent != null)
            {
                Timer = StartTimer;
                HandicapTimer = 1.5f;
                Handicapped = true;
                Cart_Crash Craash = Player.transform.parent.GetComponentInChildren<Cart_Crash>();
                Craash.Shit();
            }
            else
            {
                if (!EndingTriggered)
                {
                    EndingTriggered = true;
                    Player.CanMove = false;
                    MySceneManager.CutscenePlaying = true;
                    Stuff[1].SetActive(false);
                    Stuff[2].SetActive(false);
                    Stuff[0].SetActive(true);
                    BullManager.currentState = null;
                    Audiman.Play("Busted");
                    Audiman.Play("Dramatic Impact");
                }
            }
        }
    }

    public void AlternativeCaught()
    {
        if (!EndingTriggered)
        {
            EndingTriggered = true;
            Player.CanMove = false;
            MySceneManager.CutscenePlaying = true;
            Stuff[1].SetActive(false);
            Stuff[2].SetActive(false);
            Stuff[0].SetActive(true);
            BullManager.currentState = null;
            Audiman.Play("Busted");
            Audiman.Play("Dramatic Impact");
        }
    }
}

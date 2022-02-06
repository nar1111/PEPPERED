using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Experimental.Rendering.Universal;

public class State_Book_Read : State
{
    [SerializeField] private PLAYER_CONTROLS Player;
    //    [SerializeField] private Dialogue_Holder[] BookContent;
    [SerializeField] private GameObject[] BookContent;
    [SerializeField] private DialogueManager DialMan;
    [SerializeField] private State_Book_Look LookStage;
    [SerializeField] private PlayableDirector[] Cutscene;
    [SerializeField] private GameObject Cam;
    [SerializeField] private Light2D LibraryLight;

    private MySceneManager SceneMan;
    private int EndStage = 0;
    private int BookPook;
    private bool Done = false;

    public override State RunCurrentState()
    {
        if (EndStage == 2 && Input.GetButtonDown("Jump"))
        {
            EndStage = 0;
            Done = false;
            LibraryLight.intensity = 0.85f;
            Cam.SetActive(false);
            SceneMan.MPlay();
            Player.CanMove = true;
            MySceneManager.CutscenePlaying = false;
            BookContent[BookPook].SetActive(false);
            return LookStage;
        } else if (EndStage == 1)
        {
            if (Done == true)
            {
                EndStage = 2;
            }
        }
        return this;
    }

    public void ReadIt(int BookNumber)
    {
        if (SceneMan == null) { SceneMan = FindObjectOfType<MySceneManager>(); }
        SceneMan.MPause();
        BookPook = BookNumber;
        BookContent[BookPook].SetActive(true);
        MySceneManager.CutscenePlaying = true;
        Done = false;
        Cutscene[0].Play();
        EndStage = 1;
    }

    #region It's donskie
    public void CutsceneDone() { Done = true; }
    #endregion


}

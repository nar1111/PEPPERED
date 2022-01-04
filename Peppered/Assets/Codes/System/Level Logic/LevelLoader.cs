using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{

    [Header ("——PLACEMENT——")]
    public bool Door = false;
    public int EntranceNumber;
    public string ExitAnimation;
    public string EnterAnimation;
    public float Time;
    public string LevelName;
    [Header("——MUSIC——")]
    public int Track;
    public float Vol;
    private MySceneManager SceneMan;
    private bool InRange;


    private void Update()
    {
        if (InRange && MySceneManager.Act && MySceneManager.CutscenePlaying == false)
        {
            SceneMan = FindObjectOfType<MySceneManager>();
            SceneMan.NormalSceneTransition(EntranceNumber, ExitAnimation, EnterAnimation, Time, LevelName, Track, Vol);
            InRange = false;
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!Door)
            {
                SceneMan = FindObjectOfType<MySceneManager>();
                SceneMan.NormalSceneTransition(EntranceNumber, ExitAnimation, EnterAnimation, Time, LevelName, Track, Vol);
            }

            else if (Door)
            {
                InRange = true;
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Door) { InRange = false; }
        }
    }


}

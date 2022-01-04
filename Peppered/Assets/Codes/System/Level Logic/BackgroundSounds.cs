using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSounds : MonoBehaviour
{

    [SerializeField] private AudioSource[] SFX;
    private bool PlayerDead = false;



    // Update is called once per frame
    void Update()
    {
        if (MySceneManager.DeadState > 0 && PlayerDead == false)
        {
            PlayerDead = true;
            for (int i = 0; i < SFX.Length; i++)
            {
                SFX[i].mute = true;
            }

        }
        else if (MySceneManager.DeadState == 0 && PlayerDead == true)
        {
            PlayerDead = false;
            for (int i = 0; i < SFX.Length; i++)
            {
                SFX[i].mute = false;
            }
        }
    }
}

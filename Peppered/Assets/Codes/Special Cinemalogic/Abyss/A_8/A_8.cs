using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_8 : MonoBehaviour
{
    [SerializeField] private GameObject[] TurnThingsOff;
    [SerializeField] private GameObject Nosy;
    [SerializeField] private GameObject Jeff;
    [SerializeField] private Animator[] WindowAnims;

    // Start is called before the first frame update
    void Start()
    {
        if (WHAT_HAVE_I_DONE.Collectibles.ContainsKey("Nosy")) { Destroy(Nosy); TurnThingsOff[0].SetActive(true); }
        if (WHAT_HAVE_I_DONE.Collectibles.ContainsKey("Jeff")) { Destroy(Jeff); }

        if (MySceneManager.Abyss_State < 9) { MySceneManager.Abyss_State = 9; }
        else if (MySceneManager.Abyss_State > 9)
        {
            for (int i = 0; i < WindowAnims.Length; i++)
            {
                WindowAnims[i].Play("Window_Closed");
            }

            for (int i = 1; i < 7; i++)
            {
                Destroy(TurnThingsOff[i]);
            }
        }
    }
}

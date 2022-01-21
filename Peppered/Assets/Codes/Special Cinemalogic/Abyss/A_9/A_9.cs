using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_9 : MonoBehaviour
{
    [SerializeField] private GameObject CoffeeChum;
    [SerializeField] private Transform CoffeeNewPos;
    [SerializeField] private GameObject[] TurnThingsOff;
    [SerializeField] private Animator[] Anims;

    // Start is called before the first frame update
    void Start()
    {
        if (MySceneManager.Abyss_State < 10) { MySceneManager.Abyss_State = 10; }
        if (WHAT_HAVE_I_DONE.Collectibles.ContainsKey("Barista")) { Destroy(TurnThingsOff[0]); Destroy(TurnThingsOff[1]); Destroy(TurnThingsOff[2]); Anims[0].Play("Window_Closed"); TurnThingsOff[3].SetActive(true); }

        if (WHAT_HAVE_I_DONE.Collectibles.ContainsKey("CoffeeChum"))
        {
            WHAT_HAVE_I_DONE.Collectibles.Remove("CoffeeChum");
            CoffeeChum.transform.position = CoffeeNewPos.position;
        } else if (WHAT_HAVE_I_DONE.Collectibles.ContainsKey("CoffeeChum."))
        {
            CoffeeChum.SetActive(false);
            TurnThingsOff[4].SetActive(true);
        }
    }

}

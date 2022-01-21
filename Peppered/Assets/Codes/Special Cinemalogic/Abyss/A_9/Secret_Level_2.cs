using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret_Level_2 : MonoBehaviour
{
    [SerializeField] private GameObject CoffeeChum;
    [SerializeField] private SpriteRenderer CupItself;

    // Start is called before the first frame update
    void Start()
    {
        if (WHAT_HAVE_I_DONE.Collectibles.ContainsKey("CoffeeChum"))
        {
            WHAT_HAVE_I_DONE.Collectibles.Remove("CoffeeChum");
            CoffeeChum.SetActive(true);
        }
    }



    public void AbondonCoffee()
    {
        if (CoffeeChum.activeInHierarchy == true)
        {
            CupItself.flipX = true;
            WHAT_HAVE_I_DONE.Collectibles.Add("CoffeeChum.", 1);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merdeka_Follows : MonoBehaviour
{
    #pragma warning disable 649
    public Transform[] MoveSpots;
    [SerializeField] private PLAYER_CONTROLS Target;
    [SerializeField] private GameObject Merdeka;
    [SerializeField] private Animator MerAnim;
    [SerializeField] private Renderer MerRen;
    private int CurrentPos = 0;

    // Update is called once per frame
    void Update()
    {
        if (MerRen.isVisible)
        {
            if (Merdeka.transform.position.x < Target.transform.position.x && Merdeka.transform.localScale.x == -1)
            {
                Merdeka.transform.localScale = new Vector3(1,1,1);
            } else if (Merdeka.transform.position.x > Target.transform.position.x && Merdeka.transform.localScale.x == 1)
            {
                Merdeka.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

    }

    public void NextPos()
    {
        CurrentPos++;
        Merdeka.transform.position = MoveSpots[CurrentPos - 1].position;
        MySceneManager.Merdeka = 1;
        MerAnim.Play("Mer_Drink");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Secret_Level_1 : MonoBehaviour
{
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private DialogueManager DLMan;
    [SerializeField] private SpriteRenderer WhiteRen;
    [SerializeField] private Animator TransitionAnim;

    private void Start()
    {
        TransitionAnim.Play("Death_End");
    }

    public void ExitThisPlace()
    {
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        StartCoroutine(ExitSequence());
    }

    IEnumerator ExitSequence()
    {
        while (DLMan.Playing == true) { yield return null; }
        yield return new WaitForSeconds(1f);

        while (WhiteRen.color.a < 1)
        {
            WhiteRen.color = new Color(1 ,1 ,1 , WhiteRen.color.a + 0.01f);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(1f);
        MySceneManager.CutscenePlaying = false;
        Player.Death(0, 1);
    }
}

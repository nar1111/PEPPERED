using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Nosy_Explode : State 
{
    #pragma warning disable 649
    //Collectibles Nosy 1
    private int MyStage = 0;
    [Header("RANDOM")]
    [SerializeField] private GameObject Explosion;
    [SerializeField] private Black_lines Blines;
    [SerializeField] private Animator MyAnim;
    [SerializeField] private AudioSource Scream;
    [SerializeField] private AUDIOMANAGER AudiMan;
    [Header("RTURN OFF")]
    [SerializeField] private GameObject[] Off;
    [SerializeField] private PLAYER_CONTROLS Player;

    public override State RunCurrentState()
    {
        if (MyStage == 0)
        {
            MyStage = 1;
            WHAT_HAVE_I_DONE.Collectibles.Add("Nosy", 1);
            StartCoroutine(OhNo());
        }
        return this;
    }

    IEnumerator OhNo()
    {
        Off[4].SetActive(true);
        Blines.Show(220f, 1f);
        yield return new WaitForSeconds(2f);
        Off[0].SetActive(false);
        Off[1].SetActive(false);

        MyAnim.Play("Nose Man Scared");
        if (Player.gameObject.transform.position.x > Off[2].gameObject.transform.position.x)
        {
            Off[2].gameObject.transform.localScale = new Vector3(1, 1, 1);
            Player.gameObject.transform.localScale = new Vector3(-1,1,1);
        }
        else
        {
            Off[2].gameObject.transform.localScale = new Vector3(-1, 1, 1);
            Player.gameObject.transform.localScale = new Vector3(1, 1, 1);
        }

        Scream.Play();
        while (Scream.pitch < 2)
        {
            Scream.pitch = Scream.pitch + 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        Scream.Stop();
        Instantiate(Explosion, new Vector3 (MyAnim.gameObject.transform.position.x, MyAnim.gameObject.transform.position.y - 0.1f), Quaternion.identity);
        AudiMan.Play("Explode");
        MyAnim.gameObject.SetActive(false);
        Off[3].SetActive(true);
        yield return new WaitForSeconds(2f);
        Off[4].SetActive(false);
        Player.CanMove = true;
        MySceneManager.CutscenePlaying = false;
        Blines.Hide(0.5f);
        Off[2].SetActive(false);
        AudiMan.Play("Busted");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CheckPoint : MonoBehaviour {

    #pragma warning disable 649
    [Header("-- IMPORTAN STUFF --")]
    [SerializeField] private AUDIOMANAGER AudiMan;
    //public SaveSystem SS;
    public GameObject Pepper;
    public float TriggerDistance;
    public ParticleSystem CheckEffect;
    private bool CheckFollow;
    public SpriteRenderer MyRen;
    public Transform CheckPointPlace;
    public Animator Myanim;
    public Animator VeryWell;
    private float timer;
    public bool TESTLEVEL = false;


    [Header("-- DIALOGUE --")]
    [TextArea]
    public string[] Line;
    public float[] Speed;
    public AudioSource[] Voice;
    public AudioSource[] Noise;
    public Animator[] TalkAnim;
    private bool InReach;

    [SerializeField]
    private DialogueManager DialogueScript;

    private void Update()
    {
        CHECKPOINT();

        DIALOGUE();
    }

    private void DIALOGUE()
    {
        if (MySceneManager.Act && DialogueScript.Playing == false && InReach)
        {
            DialogueScript.DILines = Line;
            DialogueScript.DITextSpeed = Speed;
            DialogueScript.DIVoice = Voice;
            DialogueScript.Noise = Noise;
            DialogueScript.WhoTalks = TalkAnim;
            DialogueScript.DIStarter();
        }
    }

    private void CHECKPOINT()
    {
        if (MyRen.isVisible)
        {
            if (Vector2.Distance(gameObject.transform.position, Pepper.transform.position) < TriggerDistance && MySceneManager.CheckPointPos != new Vector3(CheckPointPlace.position.x - 0.3f, CheckPointPlace.position.y, CheckPointPlace.position.z))
            {
                Myanim.SetTrigger("Check");
                MySceneManager.CheckPointPos = new Vector3(CheckPointPlace.position.x - 0.3f, CheckPointPlace.position.y, CheckPointPlace.position.z);
                CheckEffect.transform.position = Pepper.transform.position;
                CheckFollow = true;
                CheckEffect.Stop();
                CheckEffect.gameObject.SetActive(true);
                CheckEffect.Play();
                MySceneManager.DeadState = 5;

                //SS.Save();
                VeryWell.Play("Saved");
                AudiMan.Play("Save Game");
                AudiMan.Play("Checkpoint");
            }
        }

        if (CheckFollow)
        {
            CheckEffect.transform.position = new Vector2(Pepper.transform.position.x, Pepper.transform.position.y - 0.2f);
            timer += Time.deltaTime;
            if (timer > 3f) { CheckFollow = false; timer = 0f; }
        }
    }

    #region Triggers
    void OnTriggerEnter2D (Collider2D other)
	{
        if (other.tag == "Player")
        {
            InReach = true;

            if (MySceneManager.CheckPointPos != new Vector3(CheckPointPlace.position.x - 0.3f, CheckPointPlace.position.y, CheckPointPlace.position.z))
            {
                Myanim.SetTrigger("Check");
                MySceneManager.CheckPointPos = new Vector3(CheckPointPlace.position.x - 0.3f, CheckPointPlace.position.y, CheckPointPlace.position.z);
                MySceneManager.DeadState = 5;
                AudiMan.Play("Save Game");
                AudiMan.Play("Checkpoint");
            }
            
        }
        else if (other.tag == "Player" && MySceneManager.DeadState == 3 || MySceneManager.DeadState == 2) {Myanim.SetTrigger("Check"); InReach = true; }
	}

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) { InReach = false; }
    }
    #endregion

}
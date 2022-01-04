using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue_Holder : MonoBehaviour
{
    #pragma warning disable 649
    [TextArea]
	public string[]  Line;
	public float[] Speed;
	public AudioSource[] Voice;
    public AudioSource[] Noise;
    public Animator[] TalkAnim;
    private bool InReach;

    [SerializeField]
	private DialogueManager DialogueScript;


    private void Update()
    {
        if (MySceneManager.Act && DialogueScript.Playing == false && InReach && MySceneManager.CutscenePlaying == false)
        {
            DialogueScript.DILines = Line;
            DialogueScript.DITextSpeed = Speed;
            DialogueScript.DIVoice = Voice;
            DialogueScript.Noise = Noise;
            DialogueScript.WhoTalks = TalkAnim;
            DialogueScript.DIStarter();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) { InReach = true; }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) { InReach = false; }
    }

}

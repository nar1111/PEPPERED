using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Double_Dialogue_Holder : MonoBehaviour
{
	private int TimesTalked = 0;

	[Header ("FIRST LINES")]
    [TextArea]
	public string[]  Line;
	public float[] Speed;
	public AudioSource[] Voice;
    public AudioSource[] Noise;
    public Animator[] TalkAnim;

    [Header ("SECOND LINES")]
    [TextArea]
	public string[]  Line2;
	public float[] Speed2;
	public AudioSource[] Voice2;
    public AudioSource[] Noise2;
    public Animator[] TalkAnim2;
    private bool InReach; 
    public DialogueManager DialogueScript;


    private void Update()
    {
        if (MySceneManager.Act && DialogueScript.Playing == false && InReach)
        {
            if (TimesTalked == 0)
            {
                DialogueScript.DILines = Line;
                DialogueScript.DITextSpeed = Speed;
                DialogueScript.DIVoice = Voice;
                DialogueScript.Noise = Noise;
                DialogueScript.WhoTalks = TalkAnim;
                DialogueScript.DIStarter();
                TimesTalked++;
            }
            else if (TimesTalked >= 1)
            {
                DialogueScript.DILines = Line2;
                DialogueScript.DITextSpeed = Speed2;
                DialogueScript.DIVoice = Voice2;
                DialogueScript.Noise = Noise2;
                DialogueScript.WhoTalks = TalkAnim2;
                DialogueScript.DIStarter();
            }
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

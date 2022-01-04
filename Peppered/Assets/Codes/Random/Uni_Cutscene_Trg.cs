using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Uni_Cutscene_Trg : MonoBehaviour
{
	public PlayableDirector playableDirector;

    public void Play()
    {
		playableDirector.Stop();
		playableDirector.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStar : MonoBehaviour {

	public string StarName;
	public GameObject Star;
	public ParticleSystem StarEffect;

	void Start()
	{
	        if (WHAT_HAVE_I_DONE.Collectibles.ContainsKey (StarName)) {Destroy(gameObject);}
	}

	void OnTriggerStay2D (Collider2D other)
	{
	if (other.CompareTag ("Player") && MySceneManager.Act && !WHAT_HAVE_I_DONE.Collectibles.ContainsKey (StarName))
	{
            WHAT_HAVE_I_DONE.Collectibles.Add (StarName, 1);
            WHAT_HAVE_I_DONE.Collectibles["Stars"]++;
            WHAT_HAVE_I_DONE.StarNum++;
	        Destroy(Star);
	        StarEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
	}
	}
}

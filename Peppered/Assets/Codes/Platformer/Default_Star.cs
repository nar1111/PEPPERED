using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Default_Star : MonoBehaviour {

	private Animator MyAnim;


	void OnTriggerStay2D (Collider2D other)
	{
		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			if (other.tag == "Player")
			{
			WHAT_HAVE_I_DONE.Collectibles ["Stars"]++;
			Destroy(gameObject);
			}
		}
	}
}

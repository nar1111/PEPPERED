using UnityEngine;
using System.Collections;

public class DestroyOverTime : MonoBehaviour {

	public float LifeTime;
	
	// Update is called once per frame
	void Update ()
	{
		LifeTime = LifeTime - Time.deltaTime;
		if (LifeTime <= 0f) {Destroy(gameObject);}
	}
}

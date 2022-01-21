using UnityEngine.Audio;
using System;
using UnityEngine;

public class AUDIOMANAGER : MonoBehaviour 
{

	public Sound[] Sounds;

    // Use this for initialization
    void Awake ()
    {

    	foreach (Sound s in Sounds)
    	{
		    s.Source = gameObject.AddComponent<AudioSource>();
	    	s.Source.clip = s.Clip;

	    	s.Source.volume = s.Volume;
	    	s.Source.pitch = s.Pitch;
	    	s.Source.loop = s.Loop;
    	}
		
	}
	
	public void Play (string name)
	{
    	Sound S = Array.Find (Sounds, Sound => Sound.Name == name);
    	if (S == null){return;}
    	S.Source.Play();
	}

	public void StopIt(string name)
	{
		Sound S = Array.Find(Sounds, Sound => Sound.Name == name);
		if (S == null) { return; }
		S.Source.Stop();
	}

	public void ChangePitch(string name, float pitchlvl)
	{
		Sound S = Array.Find(Sounds, Sound => Sound.Name == name);
		if (S == null) { return; }
		S.Source.pitch = pitchlvl;
	}
}
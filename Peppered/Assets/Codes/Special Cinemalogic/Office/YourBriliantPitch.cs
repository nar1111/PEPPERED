using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourBriliantPitch : MonoBehaviour
{
    [HideInInspector]
    public string Pitch;


    // Start is called before the first frame update
    void Start() {DontDestroyOnLoad(gameObject);}

    void WriteItDown(string Write){ Pitch = Write; }

}

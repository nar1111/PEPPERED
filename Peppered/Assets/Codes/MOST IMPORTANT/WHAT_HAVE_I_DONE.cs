using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WHAT_HAVE_I_DONE : MonoBehaviour {



    public static WHAT_HAVE_I_DONE instance = null; 
	private static bool CreateDictionary;

    public static int Immortality = 1;
    public static int Lives = 3;
    public static int StarNum = 0;
    public static int MaxStars = 100;
    public static int Coins = 0;

	static public Dictionary <string, int> Collectibles = new Dictionary <string, int>();
    static public Dictionary<string, int> GreenGem = new Dictionary<string, int>();

    public void Awake ()
	{
        if (instance == null) { DontDestroyOnLoad(gameObject); instance = this; if (!CreateDictionary)
            {
                Collectibles.Add("Stars", 0);
                CreateDictionary = true;
            }
        }

        else if (instance != null)
        {

            if (!CreateDictionary)
            {
                Collectibles.Add("Stars", 0);
                CreateDictionary = true;
            }

            Destroy(gameObject);
        }
	}
}

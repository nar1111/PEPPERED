using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSystem : MonoBehaviour
{
    public MySceneManager Sceneman;
    public int OldSave = 0;
    //IF KIERK = -999, THEN DELETE SAVE IN FULL GAME


    public void Load()
    {

        if (ES3.FileExists("SaveFile.es3"))
        {
            //Checkpoint 
            if (ES3.KeyExists("Pos")) { MySceneManager.CheckPointPos = ES3.Load<Vector3>("Pos"); }
            else { OldSave = 1; }
            if (ES3.KeyExists("LvlName")) { MySceneManager.CheckPointLvlName = ES3.Load<string>("LvlName"); }
            else { OldSave = 1; }

            //Decision
            if (ES3.KeyExists("Regret")) { MySceneManager.Regret = ES3.Load<int>("Regret"); }
            else { OldSave = 1; }
            if (ES3.KeyExists("Abyss_State")) { MySceneManager.Abyss_State = ES3.Load<int>("Abyss_State"); }
            else { OldSave = 1; }
            if (ES3.KeyExists("Cynthia")) { MySceneManager.Cynthia = ES3.Load<int>("Cynthia"); }
            else { OldSave = 1; }
            if (ES3.KeyExists("Merdeka")) { MySceneManager.Merdeka = ES3.Load<int>("Merdeka"); }
            else { OldSave = 1; }

            //Items
            if (ES3.KeyExists("Stars")) { WHAT_HAVE_I_DONE.StarNum = ES3.Load<int>("Stars"); }
            else { OldSave = 1; }
            if (ES3.KeyExists("Lives")) { WHAT_HAVE_I_DONE.Lives = ES3.Load<int>("Lives"); }
            else { OldSave = 1; }
            if (ES3.KeyExists("Coins")) { WHAT_HAVE_I_DONE.Coins = ES3.Load<int>("Coins"); }
            else { OldSave = 1; }
            if (ES3.KeyExists("TheStuff")) { WHAT_HAVE_I_DONE.Collectibles = ES3.Load<Dictionary<string, int>>("TheStuff"); }
            else { OldSave = 1; }
            if (ES3.KeyExists("Green Gem")) { WHAT_HAVE_I_DONE.GreenGem = ES3.Load<Dictionary<string, int>>("Green Gem"); }
            else { OldSave = 1; }

            //Music
            if (ES3.KeyExists("Track")) { Sceneman.CurrentSongNum = ES3.Load<int>("Track"); }
            else { OldSave = 1; }
            if (ES3.KeyExists("Volume")) { Sceneman.BGVol = ES3.Load<float>("Volume"); }
            else { OldSave = 1; }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    private MySceneManager Sceneman;

    public void Save()
    {
        Sceneman = FindObjectOfType<MySceneManager>();

        //Level and position
        ES3.Save("LvlName", MySceneManager.CheckPointLvlName);
        ES3.Save("Pos", MySceneManager.CheckPointPos);

        //Choices
        ES3.Save("Regret", MySceneManager.Regret);
        ES3.Save("Abyss_State", MySceneManager.Abyss_State);
        ES3.Save("Cynthia", MySceneManager.Cynthia);
        ES3.Save("Merdeka", MySceneManager.Merdeka);

        //Items
        ES3.Save("TheStuff", WHAT_HAVE_I_DONE.Collectibles);
        ES3.Save("Stars", WHAT_HAVE_I_DONE.StarNum);
        ES3.Save("Lives", WHAT_HAVE_I_DONE.Lives);
        ES3.Save("Coins", WHAT_HAVE_I_DONE.Coins);
        ES3.Save("Green Gem", WHAT_HAVE_I_DONE.GreenGem);

        //Music
        ES3.Save("Track", Sceneman.CurrentSongNum);
        ES3.Save("Volume", Sceneman.BGVol);

    }
}

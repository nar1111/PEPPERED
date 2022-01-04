using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnim_Manager : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]private PLAYER_CONTROLS Player;
    [SerializeField]private GameObject[] DeathAnim;
    private float TimeBeforeVoid;
    private bool Play = false;

    private void Update()
    {
        if(TimeBeforeVoid <= 0 && MySceneManager.DeadState != 1 && Play) { TimeBeforeVoid = -100; MySceneManager.DeadState = 1; Play = false; }
        else { TimeBeforeVoid -= Time.deltaTime; }
    }

    public void CreateDeathAnim(int AnimNum, float timer)
    {
        GameObject NewDeathAnim = Instantiate(DeathAnim[AnimNum], gameObject.transform.position, Quaternion.identity);
        NewDeathAnim.transform.localScale = Player.transform.localScale;
        TimeBeforeVoid = timer;
        Play = true;
    }


}

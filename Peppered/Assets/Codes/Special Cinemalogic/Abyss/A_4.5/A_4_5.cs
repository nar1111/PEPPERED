using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_4_5 : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private GameObject[] Actors;
    [SerializeField]
    private Fall_Desk_Special FallDesk;

    [Header("--DIALOGUE STUFF--")]
    [SerializeField]
    private Animator[] CharAnim;
    [SerializeField]
    private PLAYER_CONTROLS Player;
    [SerializeField]
    private DialogueManager DLMan;
    [SerializeField]
    private AUDIOMANAGER AudioMan;

    private MySceneManager SceneMan;

    #region Private DL Stuff
    [HideInInspector]
    public string[] Line;
    private float[] Speed;
    private AudioSource[] Voice;
    private AudioSource[] Noise;
    private Animator[] TalkAnim;
    private int LineNum;
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        if (MySceneManager.Abyss_State < 5)
        {
            Player.MyRigidBody.isKinematic = true;
            Player.CanMove = false;
            Invoke("WaitForIt", 4f);
            MySceneManager.Abyss_State = 5;
        }
        else if (MySceneManager.Abyss_State == 5)
        {
            Player.MyRigidBody.isKinematic = false;
            FallDesk.HeDed();
        }
        else if (MySceneManager.Abyss_State > 5)
        {
            FallDesk.HeDed();
            for (int i = 0; i < 6; i++)
            {
                Actors[i].SetActive(false);
            }
        }
    }

    void WaitForIt()
    {
        Player.MyRigidBody.isKinematic = false;
        Player.CanMove = true;
    }

}

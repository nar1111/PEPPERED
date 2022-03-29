using UnityEngine;

public class KillTrigger : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private int AnimNum;
    [SerializeField] private float AnimationTime;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && MySceneManager.DeadState == 0 && MySceneManager.DontKillMe == 0)
        {
            Player.Death(AnimNum, AnimationTime);
        }
    }
}

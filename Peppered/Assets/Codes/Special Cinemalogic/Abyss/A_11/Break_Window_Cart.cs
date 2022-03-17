using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break_Window_Cart : MonoBehaviour
{
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private GameObject ShatterEffect;
    [SerializeField] private GameObject GlasssTile;
    [SerializeField] private GameObject ShakeEffect;
    [SerializeField] private AUDIOMANAGER Audiman;
    [SerializeField] private Collider2D MyCol;

    private int MyState = 0;
    private State_Cart_Ride RideState;

    private void Update()
    {
        if (Vector2.Distance(Player.transform.position, GlasssTile.transform.position) < 0.2f && Player.transform.parent != null)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PunchAt" && MyState == 0)
        {
            RideState = Player.transform.parent.GetComponentInChildren<State_Cart_Ride>();
            if (RideState.Boosting == true)
            {
                MyCol.enabled = false;
                Audiman.Play("Glass Break");
                MyState = 1;
                GlasssTile.SetActive(false);
                ShatterEffect.SetActive(true);
                Instantiate(ShakeEffect, new Vector3(GlasssTile.transform.position.x, GlasssTile.transform.position.y - 1f, 0f), Quaternion.identity);
            }
        }
    }
}

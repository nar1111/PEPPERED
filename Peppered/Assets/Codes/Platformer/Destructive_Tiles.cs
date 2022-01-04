using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Destructive_Tiles : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]private Tilemap tilemap;
    [SerializeField]private AUDIOMANAGER AudioMan;
    [SerializeField]private string BreakSound = "Rock Impact";
    [SerializeField]private GameObject BreakEffect;
    private Vector3 hitposs;


    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Attack"))
        {

            Vector3 hitPos = Vector3.zero;

            foreach (ContactPoint2D hit in other.contacts)
            {
                hitPos.x = hit.point.x - 0.01f * hit.normal.x;
                hitPos.y = hit.point.y - 0.01f * hit.normal.y;
                tilemap.SetTile(tilemap.WorldToCell(hitPos), null);
            }

            Instantiate(BreakEffect, hitPos, Quaternion.identity);
            hitposs = hitPos;
            AudioMan.Play(BreakSound);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(hitposs, 0.1f);
    }
}

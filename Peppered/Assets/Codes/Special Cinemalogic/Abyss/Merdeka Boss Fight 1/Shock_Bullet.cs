using UnityEngine;

public class Shock_Bullet : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private MERBOSS Merdeka;

    private void OnTriggerEnter2D(Collider2D other) {   if (other.CompareTag ("Player")){Merdeka.Damage();}   }
}

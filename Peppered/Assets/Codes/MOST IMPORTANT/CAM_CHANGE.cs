using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM_CHANGE : MonoBehaviour
{

    public GameObject MyCam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MyCam.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MyCam.SetActive(false);
        }
    }
}

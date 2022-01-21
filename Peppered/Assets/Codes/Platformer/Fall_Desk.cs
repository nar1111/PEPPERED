using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Desk : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private Rigidbody2D MyRigidbody;
    [SerializeField] private float WaitTime;
    [SerializeField] private float RespawnTime;
    [SerializeField] private string LayerName;
    [SerializeField] private Animator FireAnim;
    [SerializeField] private Transform FellPoint;
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private AudioSource ExplodeSFX;

    private Vector3 StartPos;
    [HideInInspector]public int Stage = 0;

    private void Start()
    {
        StartPos = transform.position;
    }

    private void Update()
    {
        //3 FELL FAR ENOUGH
        if (Stage == 2)
        {
            if (transform.position.y < FellPoint.position.y)
            {
                ExplodeSFX.Play();
                Stage = 3;
                MyRigidbody.isKinematic = true;
                FireAnim.Play("HeatUp");
                Invoke("Respawn", RespawnTime);
            } //5 GOT TO THE START POINT, RESTART THE PROCESS
        } else if (Stage == 4)
        {
            //if (Vector2.Distance(transform.position, StartPos) < 0.1f)
            if (transform.position.y < StartPos.y)
            {
                MyRigidbody.velocity = Vector3.zero;
                transform.position = StartPos;
                Stage = 0;
                MyRigidbody.isKinematic = true;
            }
        }
    }

    //1 YOU TRIGGERED THE DESK
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(LayerName) && Stage == 0)
        {
            Stage = 1;
            StartCoroutine(ShakeIt());
            Invoke("DropDown", WaitTime);
        }
    }

    //2 START FALLING
    public void DropDown()
    {
        Stage = 2;
        MyRigidbody.isKinematic = false;
    }

    //4 Respawn the desk
    void Respawn()
    {
        Stage = 4;
        transform.position = SpawnPoint.position;
        MyRigidbody.isKinematic = false;
        MyRigidbody.velocity = Vector3.zero;
    }

    //SHAKE IT
    IEnumerator ShakeIt()
    {
        while (Stage == 1)
        {
            yield return new WaitForSeconds(0.05f);
            transform.position += new Vector3(0.03f, 0, 0);
            yield return new WaitForSeconds(0.05f);
            transform.position -= new Vector3(0.03f, 0, 0);
        }
    }
}
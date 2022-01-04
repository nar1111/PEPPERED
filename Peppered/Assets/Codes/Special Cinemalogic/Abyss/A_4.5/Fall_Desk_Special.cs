using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall_Desk_Special : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private Rigidbody2D MyRigidbody;
    [SerializeField] private float WaitTime;
    [SerializeField] private float RespawnTime;
    [SerializeField] private string LayerName;
    [SerializeField] private Animator FireAnim;
    [SerializeField] private Transform FellPoint;
    [SerializeField] private Transform SpawnPoint;

    [Header("VINCENT")]
    [SerializeField] private GameObject Player;
    [SerializeField] private Animator MyAnim;
    [SerializeField] private Transform SafePos;
    [SerializeField] private GameObject RespawnEffect;
    [SerializeField] private GameObject DiedEffect;
    private bool Died = false;

    [Header("Dialogue")]
    [SerializeField] private GameObject NoNo;
    [SerializeField] private GameObject AAAA;
    [SerializeField] private GameObject DialLines;

    private Vector3 StartPos;
    [HideInInspector] public int Stage = 0;

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
                Stage = 3;
                MyRigidbody.isKinematic = true;
                FireAnim.Play("HeatUp");
                if (Died == false)
                {
                    Died = true;
                    AAAA.SetActive(false);
                    Instantiate(DiedEffect, MyAnim.gameObject.transform.position, Quaternion.Euler (-90, 0, 0));
                    Instantiate(RespawnEffect, SafePos.transform.position, Quaternion.identity);
                    Invoke("HeDed", 0.8f);
                }
                Invoke("Respawn", RespawnTime);
            }
        }
        else if (Stage == 4)
        {
            //5 GOT TO THE START POINT, RESTART THE PROCESS
            if (Vector2.Distance(transform.position, StartPos) < 0.1f)
            {
                MyRigidbody.velocity = Vector3.zero;
                transform.position = StartPos;
                Stage = 0;
                MyRigidbody.isKinematic = true;
            }
        }
        //FALL EAIRLIER
        else if (Stage == 1 && Vector2.Distance(transform.position, Player.transform.position) > 2f)
        {
            CancelInvoke("DropDown");
            DropDown();
        }
    }

    //1 YOU TRIGGERED THE DESK
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(LayerName) && Stage == 0)
        {
            Stage = 1;
            if (Died == false) { MyAnim.Play("Vincent Jerome Panic"); NoNo.SetActive(true); } 
            StartCoroutine(ShakeIt());
            Invoke("DropDown", WaitTime);
        }
    }

    //2 START FALLING
    public void DropDown()
    {
        Stage = 2;
        if (Died == false)
        {
            NoNo.SetActive(false);
            AAAA.SetActive(true);
        }
        MyAnim.Play("Vincent Jernoooo");
        MyRigidbody.isKinematic = false;
    }

    //4 RESPAWN THE DESK
    void Respawn()
    {
        Stage = 4;
        transform.position = SpawnPoint.position;
        MyRigidbody.isKinematic = false;
        MyRigidbody.velocity = Vector3.zero;
    }

    public void HeDed()
    {
        MyAnim.Play("Vincent Jernoooo");
        Died = true;
        MyAnim.gameObject.transform.parent = null;
        MyAnim.gameObject.transform.position = SafePos.transform.position;
        DialLines.SetActive(true);
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
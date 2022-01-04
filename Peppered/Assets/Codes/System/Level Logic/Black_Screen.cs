using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Black_Screen : MonoBehaviour
{
    #pragma warning disable 649

    [SerializeField]
    private SpriteRenderer MyRen;
    [SerializeField]
    private float Speed;
    private Color MyColor;
    [SerializeField]
    private bool Starter;
    private int State;


    void Start()
    {
        if (Starter) { MyColor = new Color(1, 1, 1, 0); MyRen.color = MyColor; State = 1; }
        else { MyColor = new Color(1, 1, 1, 1); MyRen.color = MyColor; State = 0; }
    }

    private void Update()
    {
        if (State == 3 && MyRen.color.a > 0) { MyColor = new Color(1,1,1, (MyRen.color.a - Speed)); MyRen.color = MyColor;}
        else if (State == 4 && MyRen.color.a < 1) { MyColor = new Color(1, 1, 1, (MyRen.color.a + Speed)); MyRen.color = MyColor;}
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         if (other.CompareTag ("Player") && State == 0 || other.CompareTag("Player") && State == 4) { State = 3; } 
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag ("Player") && State == 1 || other.CompareTag("Player") && State == 3) { State = 4; }
    }

}

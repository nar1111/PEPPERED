using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Benji_State_Hide : State
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Hugh;
    [SerializeField] private Animator HughAnim;
    [SerializeField] private GameObject Steam;
    [SerializeField] private Animator MyAnim;
    [SerializeField] private Transform LeftPoint;
    [SerializeField] private Transform RightPoint;
    [SerializeField] private float Speed;
    [SerializeField] private SpriteRenderer MyRen;
    [SerializeField] private A_7 Theatre;

    private int Hide = 0;
    private bool SwitchSide = false;


    public override State RunCurrentState()
    {
        RunAndHide();
        return this;
    }

    private void RunAndHide ()
    {
        if (MyRen.isVisible)
        {
            if (Player.transform.position.x > Hugh.transform.position.x)
            {
                Hugh.transform.localScale = new Vector3(1, 1, 1);
                if (Vector2.Distance(MyAnim.transform.position, LeftPoint.transform.position) > 0.05f)
                {
                    if (SwitchSide == false) { SwitchSide = true; Hide++; }
                    MyAnim.transform.localScale = new Vector3(-1, 1, 1);
                    MyAnim.gameObject.transform.position = Vector2.MoveTowards(MyAnim.transform.position, new Vector2(LeftPoint.position.x, MyAnim.transform.position.y), Speed * Time.deltaTime);
                    MyAnim.Play("Benjamin_Run");
                }
                else
                {
                    MyAnim.transform.localScale = new Vector3(1, 1, 1);
                    MyAnim.Play("Benjamin_Uwu");
                }
            }


            else if (Player.transform.position.x < Hugh.transform.position.x)
            {
                Hugh.transform.localScale = new Vector3(-1, 1, 1);
                if (Vector2.Distance(MyAnim.transform.position, RightPoint.transform.position) > 0.05f)
                {
                    if (SwitchSide == true) { SwitchSide = false; Hide++; }
                    MyAnim.transform.localScale = new Vector3(1, 1, 1);
                    MyAnim.gameObject.transform.position = Vector2.MoveTowards(MyAnim.transform.position, new Vector2(RightPoint.position.x, MyAnim.transform.position.y), Speed * Time.deltaTime);
                    MyAnim.Play("Benjamin_Run");
                }
                else
                {
                    MyAnim.transform.localScale = new Vector3(-1, 1, 1);
                    MyAnim.Play("Benjamin_Uwu");
                }
            }

            if (Hide == 10)
            {
                Hide = 11;
                Theatre.Angeri = 1;
                HughAnim.Play("Hugh_Angeri");
                Steam.SetActive(true);
            }
        }
    }
}
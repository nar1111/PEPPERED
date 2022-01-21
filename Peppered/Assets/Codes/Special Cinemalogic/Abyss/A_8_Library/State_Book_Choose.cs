using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Book_Choose : State
{
    [Header ("VISUALS")]
    [SerializeField] private GameObject Book;
    [SerializeField] private SpriteRenderer BookRen;
    [SerializeField] private Sprite[] Cover;
    [SerializeField] private GameObject Cam;

    [Header("ACTIONS")]
    [SerializeField] private State_Book_Read ReadState;
    [SerializeField] private Transform[] MovePoints;
    [SerializeField] private AUDIOMANAGER AudiMan;

    //TECH STUFF
    private bool Press = false;
    private int BookSide;
    private int CurrentBook = 0;
    private float MoveSpeed = 0.1f;

    public override State RunCurrentState()
    {
        Control();

        if (Input.GetButtonDown("Jump") && Press == false)
        {
            ReadState.ReadIt(CurrentBook);
            BookRen.color = new Color(1, 1, 1, 0);
            BookSide = 0;
            CurrentBook = 0;
            Cam.SetActive(false);
            return ReadState;

        }
        return this;
    }

    private void Control()
    {
        //Flip Right
        if (Input.GetAxisRaw("Horizontal") > 0f && Press == false)
        {
            Press = true;
            BookSide = 0;
            if (CurrentBook < Cover.Length - 1)
            {
                CurrentBook++;
                BookRen.sprite = Cover[CurrentBook];
            }
            else
            {
                CurrentBook = 0;
                BookRen.sprite = Cover[CurrentBook];
            }

            StartCoroutine(VisualChange());
        }
        //Flip Flip
        else if (Input.GetAxisRaw("Horizontal") < 0f && Press == false)
        {
            Press = true;
            BookSide = 2;
            if (CurrentBook > 0)
            {
                CurrentBook--;
                BookRen.sprite = Cover[CurrentBook];
            }
            else
            {
                CurrentBook = Cover.Length - 1;
                BookRen.sprite = Cover[CurrentBook];
            }

            StartCoroutine(VisualChange());
        }
    }

    IEnumerator VisualChange()
    {
        AudiMan.Play("Paper");
        BookRen.color = new Color(1,1,1,0);
        Book.transform.position = MovePoints[BookSide].position;
        MoveSpeed = 0.1f;

        while (BookRen.color.a < 1 || Vector2.Distance(Book.transform.position, MovePoints[1].position) > 0f)
        {
            BookRen.color = new Color(1, 1, 1, BookRen.color.a + 0.01f);
            Book.transform.position = Vector2.MoveTowards(Book.transform.position, MovePoints[1].position, MoveSpeed * Time.deltaTime);
            MoveSpeed = MoveSpeed + 0.08f;
            yield return new WaitForSeconds(0.001f);
        }
        Press = false;
    }

    public void StartLooking()
    {
        Press = true;
        BookSide = 0;
        CurrentBook = 0;
        BookRen.sprite = Cover[CurrentBook];
        StartCoroutine(VisualChange());
    }
}
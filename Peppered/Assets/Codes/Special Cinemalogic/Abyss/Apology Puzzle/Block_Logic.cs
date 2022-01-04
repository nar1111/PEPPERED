using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_Logic : MonoBehaviour
{
    private Vector3 StartPos;
    public Vector3 PaperPos;
    public Vector3[] CorrectPos;
    public GameObject MyNumber;
    public SpriteRenderer[] MyRen;
    public bool Used = false;
    public Puzzle_Logic Puzzle;


    private void Start()
    {
        StartPos = transform.localPosition;
    }

    public void ColorMeUnused(Color NewColor)
    {
        transform.localPosition = StartPos;
        if (MyNumber.activeInHierarchy == false) { MyNumber.SetActive(true); Used = false; }
        for (int i = 0; i < MyRen.Length; i++)
        {
            MyRen[i].color = NewColor;
            MyRen[i].sortingLayerName = "Effects in front";
        }
    }

    public void ColorMeInUse(Color NewColor)
    {
        transform.localPosition = PaperPos;
        for (int i = 0; i < MyRen.Length; i++)
        {
            MyRen[i].color = NewColor;
            MyRen[i].sortingLayerName = "Special";
        }
    }

    public void ColorMeUsed(Color NewColor)
    {
        for (int i = 0; i < CorrectPos.Length; i++)
        {
            if (Vector2.Distance(transform.localPosition, CorrectPos[i]) < 0.2f) { Puzzle.Correct(); }
            else { Puzzle.Incorrect(); }
        }

        MyNumber.SetActive(false);
        Used = true;
        for (int i = 0; i < MyRen.Length; i++)
        {
            MyRen[i].color = NewColor;
            MyRen[i].sortingLayerName = "Effects in front";
        }
    }

    public void AutoComplete(Color NewColor)
    {
        transform.localPosition = CorrectPos[0];
        MyNumber.SetActive(false);
        Used = true;
        for (int i = 0; i < MyRen.Length; i++)
        {
            MyRen[i].color = NewColor;
            MyRen[i].sortingLayerName = "Effects in front";
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boss") { transform.localPosition = PaperPos; }
    }
}

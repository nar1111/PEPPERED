using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Black_lines : MonoBehaviour
{
    public GameObject Quitsignal;
    private float Timer;
    private float StartTimer = 2.5f;
    private RectTransform TopBar, BottomBar;
    private float TargetSize;
    private float ChangeSizeAmount;
    private bool IsActive;


    private void Awake()
    {
        Timer = StartTimer;
        GameObject gameObject = new GameObject("TopBar", typeof(Image));
        gameObject.transform.SetParent(transform, false);
        gameObject.GetComponent<Image>().color = Color.black;
        TopBar = gameObject.GetComponent<RectTransform>();
        TopBar.anchorMin = new Vector2(0, 1);
        TopBar.anchorMax = new Vector2(1, 1);
        TopBar.sizeDelta = new Vector2(0, 000);

        GameObject gameObject2 = new GameObject("BottomBar", typeof(Image));
        gameObject2.transform.SetParent(transform, false);
        gameObject2.GetComponent<Image>().color = Color.black;
        BottomBar = gameObject2.GetComponent<RectTransform>();
        BottomBar.anchorMin = new Vector2(0, 0);
        BottomBar.anchorMax = new Vector2(1, 0);
        BottomBar.sizeDelta = new Vector2(0, 0);
    }

    public void Update()
    {
        if (IsActive == true)
        {
            Vector2 sizeDelta = TopBar.sizeDelta;
            sizeDelta.y += ChangeSizeAmount * Time.deltaTime;

            if (ChangeSizeAmount > 0)
            {
                if (sizeDelta.y >= TargetSize) { sizeDelta.y = TargetSize; IsActive = false; }
            }

            else 
            { 
                if (sizeDelta.y <= TargetSize) { sizeDelta.y = TargetSize; IsActive = false; }
            }

            TopBar.sizeDelta = sizeDelta;
            BottomBar.sizeDelta = sizeDelta;
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            if (Quitsignal.activeInHierarchy == false)
            {
                Quitsignal.SetActive(true);
            }

            Timer -= Time.deltaTime;
            if(Timer <= 0) { Timer = StartTimer; Application.Quit(); }
        }
        else if (Input.GetKeyUp(KeyCode.Escape))
        {
            Quitsignal.SetActive(false);
            Timer = StartTimer;
        }
    }

    public void Show(float TargetSize, float Time) 
    {
        this.TargetSize = TargetSize;
        ChangeSizeAmount = (TargetSize - TopBar.sizeDelta.y) / Time;
        IsActive = true; 

    }

    public void Hide (float Time)
    {
        this.TargetSize = 0f;
        ChangeSizeAmount = (TargetSize - TopBar.sizeDelta.y) / Time;
        IsActive = true;
    }
}

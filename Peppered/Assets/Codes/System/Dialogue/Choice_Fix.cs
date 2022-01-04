using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Choice_Fix : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private GameObject SelectBox;
    [SerializeField]
    private GameObject FakeButton;
    [SerializeField]
    private GameObject LeftButton;
    [SerializeField]
    public GameObject RightButton;
    [SerializeField]
    private GameObject LeftDescript;
    [SerializeField]
    private GameObject RightDescript;
    [SerializeField]
    private Text[] ClearText;
    private int SafetyNum = 0;



    private void OnEnable() { EventSystem.current.SetSelectedGameObject(FakeButton); SafetyNum = 0; }

    private void OnDisable() { LeftDescript.SetActive(false); RightDescript.SetActive(false); ClearText[0].text = ""; ClearText[1].text = ""; }

    void Update()
    {
        if (SelectBox.activeInHierarchy == true)
        {
            if (Input.GetAxisRaw("Horizontal") < 0f && SafetyNum != 1) { SafetyNum = 1; EventSystem.current.SetSelectedGameObject(LeftButton); LeftDescript.SetActive(true);  RightDescript.SetActive(false); }
            else if (Input.GetAxisRaw("Horizontal") > 0f && SafetyNum != 2) { SafetyNum = 2; EventSystem.current.SetSelectedGameObject(RightButton); LeftDescript.SetActive(false); RightDescript.SetActive(true); }
        }
    }
}

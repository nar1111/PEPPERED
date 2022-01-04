using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer_Follow : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField] private SpriteRenderer StopLight;
    [SerializeField] private GameObject Pepper;
    [SerializeField] private float YOffset;
    [SerializeField] private float MaxYPoint;
    [SerializeField] private GameObject Icon;
    private Vector3 StartPoint;
    private float Speed = 5;

    private void Start()
    {
        StartPoint = new Vector3 (transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (StopLight.isVisible == false)
        {
                //transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, Pepper.transform.position.y - YOffset), Speed * Time.deltaTime);
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, Mathf.Clamp(Pepper.transform.position.y - YOffset, StartPoint.y, MaxYPoint)), Speed * Time.deltaTime);
                Icon.SetActive(true);
        } else
        {
            transform.position = StartPoint;
            Icon.SetActive(false);
        }
    }
}

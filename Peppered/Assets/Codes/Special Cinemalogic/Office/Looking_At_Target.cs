using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Looking_At_Target : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private GameObject Target;
    [SerializeField]
    private SpriteRenderer SprRen;
    private float Distance;

    // Update is called once per frame
    void Update()
    {
        if (SprRen.isVisible)
        {
            Distance = Vector3.Distance(Target.transform.position, transform.position);

            if (gameObject.transform.position.x < Target.transform.position.x && Distance < 3f && gameObject.transform.localScale.x != 1)
            { gameObject.transform.localScale = new Vector3(1, 1, 1); }

            else if (gameObject.transform.position.x > Target.transform.position.x && Distance < 3f && gameObject.transform.localScale.x != -1)
            { gameObject.transform.localScale = new Vector3(-1, 1, 1); }
        }
    }
}

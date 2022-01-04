using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaless_Follow_Target : MonoBehaviour
{
    public GameObject Target;
    public Vector3 Offset;


    // Update is called once per frame
    void Update()   {  transform.position = Target.transform.position + Offset;  }
}

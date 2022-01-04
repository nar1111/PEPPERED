using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverRespawn : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (MySceneManager.DeadState == 2) { Destroy(gameObject); }
    }
}

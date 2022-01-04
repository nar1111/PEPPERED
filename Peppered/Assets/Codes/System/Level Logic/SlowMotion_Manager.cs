using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowMotion_Manager : MonoBehaviour
{
    public float SlowDownAmount;
    public float SlowDownTime;

    public void DoSlowMotion() { StopCoroutine(SlowMotion()); StartCoroutine(SlowMotion()); }

    IEnumerator SlowMotion()
    {
        Time.timeScale = SlowDownAmount;
        Time.fixedDeltaTime = Time.timeScale * .02f;
        yield return new WaitForSeconds(SlowDownTime);
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }

}

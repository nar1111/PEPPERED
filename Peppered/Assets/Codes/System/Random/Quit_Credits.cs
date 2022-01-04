using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Quit_Credits : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private PlayableDirector Cutscene;

    // Start is called before the first frame update
    void Start() { StartCoroutine(Quit());}

    IEnumerator Quit()
    {
        yield return new  WaitForSeconds(1f);
        while (Cutscene.state == PlayState.Playing) { yield return null; }
        yield return new WaitForSeconds(2f);
        Application.Quit();
    }

}

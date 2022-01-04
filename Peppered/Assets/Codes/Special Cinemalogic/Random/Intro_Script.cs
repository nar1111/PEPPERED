using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Intro_Script : MonoBehaviour
{
    public DialogueManager_SPECIAL DLMan;
    public CanvasGroup UIElement;
    public CanvasGroup Black;

    [HeaderAttribute("Slidesf")]
    public Image CurrentSlide;
    public Sprite[] Slides;

    public Text HundredYears;
    public AudioSource Ost;
    public MySceneManager SceneMan;
    public PLAYER_CONTROLS Player;

    [HeaderAttribute("Dialgue Stuff")]
    public AudioSource GenericVoice;
    public string[] Line;
    private float[] Speed;
    private AudioSource[] Voice;


    private void Start()
    {
        Player.CanMove = false;
        StartCoroutine(Cutscene());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(EndCustcene());
        }
    }


    public IEnumerator Cutscene() 
    {


        DefaulDialogue(1, 0.08f, GenericVoice);
        yield return new WaitForSeconds(1f);
        FadeIn();
        Ost.Play();
        #region Начинай рассказ

        #region Запульни фразу
        string[] Line = new string[1];
        Line[0] = "Long ago, we were mortals.";
        DLMan.DILines = Line;
        DLMan.DIStarter();
        #endregion

        while (DLMan.Texting) {yield return null;}
        yield return new WaitForSeconds(2f);

        #region Запульни фразу
        Line[0] = "We lived under the oppression of our ex-ruler:";
        DLMan.DILines = Line;
        DLMan.DIStarter();
        #endregion

        while (DLMan.Texting) { yield return null; }
        yield return new WaitForSeconds(2f);

        FadeOut();
        yield return new WaitForSeconds(0.5f);
        CurrentSlide.sprite = Slides[2];
        FadeIn();

        #region Запульни фразу
        Line[0] = "The God of DEATH";
        DLMan.DILines = Line;
        DLMan.DIStarter();
        #endregion

        while (DLMan.Texting) { yield return null; }
        yield return new WaitForSeconds(3f);

        FadeOut();
        yield return new WaitForSeconds(0.5f);
        CurrentSlide.sprite = Slides[3];
        FadeIn();

        #region Запульни фразу
        Line[0] = "One day, a human appeared and challenged the deity.";
        DLMan.DILines = Line;
        DLMan.DIStarter();
        #endregion

        while (DLMan.Texting) { yield return null; }
        yield return new WaitForSeconds(2f);

        FadeOut();
        yield return new WaitForSeconds(0.5f);
        CurrentSlide.sprite = Slides[4];
        FadeIn();

        #region Запульни фразу
        Line[0] = "Using magical stars, he defeated the God of death";
        DLMan.DILines = Line;
        DLMan.DIStarter();
        #endregion

        while (DLMan.Texting) { yield return null; }
        yield return new WaitForSeconds(1f);
        Line[0] = "";
        DLMan.DILines = Line;
        DLMan.DIStarter();

        FadeOut();
        yield return new WaitForSeconds(0.5f);
        CurrentSlide.sprite = Slides[5];
        FadeIn();

        #region Запульни фразу
        Line[0] = "And sealed him in a prison.";
        DLMan.DILines = Line;
        DLMan.DIStarter();
        #endregion

#endregion

        while (DLMan.Texting) { yield return null; }
        yield return new WaitForSeconds(2f);
        StartCoroutine(TurnOffMusic());
        while (Ost.volume > 0) { yield return null; }
    }

    public IEnumerator EndCustcene()
    {
        FadeInBlack();
        while (Black.alpha != 1)
        { yield return null; }
        while (Ost.volume > 0) { Ost.volume -= Time.deltaTime; }
        yield return new WaitForSeconds(1f);
    }


    private IEnumerator TurnOffMusic()
    {
        while (Ost.volume > 0) { Ost.volume -= 0.001f; yield return new WaitForSeconds(0.01f); }
    }

    #region Настрой диалоговое окно
    private void DefaulDialogue(int NumberOfLines, float DialogueSpeed, AudioSource VoiceClip) 
    {
        string[] Line = new string[NumberOfLines];
        float[] Speed = new float[NumberOfLines];
        AudioSource[] Voice = new AudioSource[NumberOfLines];

        for (int i = 0; i < Speed.Length; i++) { Speed[i] = DialogueSpeed; }
        for (int i = 0; i < Voice.Length; i++) { Voice[i] = VoiceClip; }

        DLMan.DILines = Line;
        DLMan.DITextSpeed = Speed;
        DLMan.DIVoice = Voice;

    }
    #endregion

    #region FADE, BRAH
    public void FadeIn() 
    {
        StartCoroutine(Fade(UIElement, UIElement.alpha, 1));
    }

    public void FadeInBlack() 
    {
        StartCoroutine(Fade(Black, Black.alpha, 1));
    }

    public void FadeOut()
    {
        StartCoroutine(Fade(UIElement, UIElement.alpha, 0));
    }

    public IEnumerator Fade(CanvasGroup cg, float start, float end, float lerpTime = 0.5f)
    {
        //I have no idea how any of this works. I stole these here codes from a youtube tutorial. Fuck me.
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float PercentageComplete = timeSinceStarted / lerpTime;


        while (true) 
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            PercentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, PercentageComplete);

            cg.alpha = currentValue;

            if (PercentageComplete >= 1) break;

            yield return new WaitForEndOfFrame(); 
        }
    }
    #endregion
}

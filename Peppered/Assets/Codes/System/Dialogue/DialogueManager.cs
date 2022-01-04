using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    #pragma warning disable 649
    #region UI Dialogue box
    // Ссылка на UI окно
    public GameObject DialogueBox;
	public Text TextBox;
    [SerializeField]private GameObject Pointer;
    public GameObject ChoiceBox;
    public Text FirstText;
    public Text SecondText;
    public Button_Choose BChoose;
    [SerializeField]private Animator ChoiceAnim;
	#endregion

	#region Dialogue info
	//Тут, короче, он просит холдеру закинуть array фраз, лиц, скорости текста, голосов и проверок на наличие картинки
	public string[] DILines;
	public float[] DITextSpeed;
	public AudioSource[] DIVoice;
    public AudioSource[] Noise;
    public string LeftString;
    public string RightString;
    public Animator[] WhoTalks;
	#endregion

	#region Функционал
	//Вырубает движение игроку, проверяет на какой фразе находится и прочая техническая ахтунг-поеботина
    [SerializeField]
	private PLAYER_CONTROLS Player;
    private CHAIR_PLAYER Chplayer;
	private int Skip = 0;
	private int DILength;
	public int Currentline = 0;
	public int DialogueLength;
	public bool Playing = false;
	private Text TextUI;
	public bool Texting = false;
    private AUDIOMANAGER AudioMan;
    private bool BitchOneAtTheTime;
	#endregion


    void Awake ()
    {
        Player = FindObjectOfType <PLAYER_CONTROLS>();
        AudioMan = FindObjectOfType<AUDIOMANAGER>();
        if (Player == null) { Chplayer = FindObjectOfType<CHAIR_PLAYER>(); }
    }

    void Update()
    {
        if (Playing == true)
        {
            if (Input.GetButtonDown("Skip")) { Skip = 1; }

            if (Player != null)
            {
                if (Player.CanMove == true) { Player.CanMove = false; }
            }    
        }

    }

    public void DIStarter ()
	{      
        if (BitchOneAtTheTime == false)
        {
            if (Player == null) { if (Chplayer != null) { Chplayer.CanMove = false; } }
            else if (Player != null) { Player.CanMove = false; }
            DialogueBox.SetActive(true);
            TextUI =  TextBox;
            DialogueLength = DILines.Length;
            BitchOneAtTheTime = true;
            StartCoroutine(StartDialogue());
        }
	}

    private void DINextLine()
    {

        if (Player == null) { if (Chplayer != null) { Chplayer.CanMove = false; } }
        else if (Player != null) { Player.CanMove = false; }
        DialogueBox.SetActive(true);
        TextUI = TextBox;
        DialogueLength = DILines.Length;
        StartCoroutine(StartDialogue());

    }

    public void ChoiceStarter() 
    {

        if (Player == null) { if (Chplayer != null) { Chplayer.CanMove = false; } }
        else if (Player != null) { Player.CanMove = false; }
        ChoiceBox.SetActive(true);
        StartCoroutine(StartChoice());

    }

    #region Выбор
    IEnumerator StartChoice() 
    {
       // Player.CanMove = false;
        Playing = true;
        FirstText.text = "";
        SecondText.text = "";
        ChoiceAnim.Play("Start_Choice");
        if (Player != null)
        {
            Player.MyRigidBody.velocity = new Vector3(0f, Player.MyRigidBody.velocity.y);
        }

        FirstText.text = LeftString;
        SecondText.text = RightString;
        Texting = false;
        BChoose.ButtonChoice = 0;
        

        while (BChoose.ButtonChoice == 0)
        {
            yield return null;
        }

        AudioMan.Play("Confirm");
        ChoiceAnim.Play("End_Choice");
        ChoiceBox.SetActive(false);
        FirstText.text = "";
        SecondText.text = "";
        Currentline = 0;
        Playing = false;
        Skip = 0;
        if (Player == null) { if (Chplayer != null) { Chplayer.CanMove = true; } }
        else if (Player != null) { Player.CanMove = true; }
    }
    #endregion

    #region Диалог
    IEnumerator StartDialogue ()
	{
        //string builder

        #region Refresh text, stop player and stuff
            if (Player == null) { if (Chplayer != null) { Chplayer.CanMove = false; } }
            else if (Player != null) { Player.CanMove = false; }
            Playing = true;
			TextUI.text = "";
            if (Player != null){Player.MyRigidBody.velocity = new Vector3(0f, Player.MyRigidBody.velocity.y);}
            if (Noise[Currentline] != null) { Noise[Currentline].Play(); }
        #endregion

        #region Typewriter
        for (int i = 0; i <= DILines[Currentline].Length; i++)
		{
			    Texting = true;
			    if (Skip == 1){TextUI.text = DILines[Currentline]; if (WhoTalks[Currentline] != null) { if (WhoTalks[Currentline].GetBool("Talk") == true) { WhoTalks[Currentline].SetBool("Talk", false); } } break;}
			    TextUI.text = DILines[Currentline].Substring(0,i);
                if (WhoTalks[Currentline] != null) { if (WhoTalks[Currentline].GetBool("Talk") == false) { WhoTalks[Currentline].SetBool("Talk", true); } }

			    if (i > 0 && i != DILines[Currentline].Length)
		    	{
                    DIVoice[Currentline].pitch = Random.Range(0.9f, 1.2f);

		    	    if (DILines[Currentline].Substring(i - 1,1) == (".") && DILines[Currentline].Substring(i, 1) != (".")|| DILines[Currentline].Substring(i - 1,1) == ("?") && DILines[Currentline].Substring(i, 1) != ("?") || DILines[Currentline].Substring(i - 1,1) == ("!") && DILines[Currentline].Substring(i, 1) != ("!"))
			        {yield return new WaitForSeconds(DITextSpeed[Currentline] + 0.4f); DIVoice[Currentline].Play();}

			        else if (DILines[Currentline].Substring(i - 1,1) == (","))
			        {yield return new WaitForSeconds(DITextSpeed[Currentline] + 0.2f); DIVoice[Currentline].Play();}
    
			        else if (DILines[Currentline].Substring(i - 1,1) == (" "))
			        {yield return new WaitForSeconds(0.0f);}

                    else if (DILines[Currentline].Substring(i - 1, 1) == (".") && DILines[Currentline].Substring(i, 1) == (".") || DILines[Currentline].Substring(i - 1, 1) == ("?") && DILines[Currentline].Substring(i, 1) == ("?") || DILines[Currentline].Substring(i - 1, 1) == ("!") && DILines[Currentline].Substring(i, 1) == ("!"))
                    { yield return new WaitForSeconds(DITextSpeed[Currentline]); DIVoice[Currentline].Play(); }

			        else {yield return new WaitForSeconds(DITextSpeed[Currentline]); DIVoice[Currentline].Play();}
			    }
		}
		#endregion

			Texting = false;
            if (WhoTalks[Currentline] != null) { if (WhoTalks[Currentline].GetBool("Talk") == true) { WhoTalks[Currentline].SetBool("Talk", false); } }
            Skip = 0;
            Pointer.SetActive(true);

			//Ждем пока не нажмут "Дальше"
			while (!Input.GetButtonDown("Jump"))//(!Input.GetButtonDown ("Jump")) 
			{	
				yield return null;
			}

			Skip = 0;
			Currentline++;

			//Проверка все ли линии прочитали
			if (Currentline >= DialogueLength) 
			{
			//Вырубаем нах
			TextBox.text = "";
            Pointer.SetActive(false);
            DialogueBox.SetActive (false);
            if (Player == null) { if (Chplayer != null) { Chplayer.CanMove = true; } }
            else if (Player != null) { Player.CanMove = true; }
            Currentline = 0;
			Playing = false;
            BitchOneAtTheTime = false;
            }  

            else 
			{
            //Запускаем следующую фразу
            Pointer.SetActive(false);
            TextUI.text = "";
            DINextLine();
			}
    }
    #endregion

}
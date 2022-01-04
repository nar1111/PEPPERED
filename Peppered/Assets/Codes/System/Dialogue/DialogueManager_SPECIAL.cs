using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager_SPECIAL : MonoBehaviour {

    #region UI Dialogue box
    // Ссылка на UI окно
    public bool ControlOverDialogue;
	public GameObject DialogueBox;
	public Text TextBox;
	public Image FaceBox;
	#endregion

	#region Dialogue info
	//Тут, короче, он просит холдеру закинуть array фраз, лиц, скорости текста, голосов и проверок на наличие картинки
	public string[] DILines;
	public float[] DITextSpeed;
	public AudioSource[] DIVoice;
	#endregion

	#region Функционал
	//Вырубает движение игроку, проверяет на какой фразе находится и прочая техническая ахтунг-поеботина
	//private PLAYER_CONTROLS Player;
	private int Skip = 0;
	private int DILength;
	public int Currentline = 0;
	public int DialogueLength;
	public bool Playing = false;
	private Text TextUI;
	public bool Texting = false;
    #endregion


    void Update()
    {
        if (Input.GetButtonDown("Skip") && Playing == true && ControlOverDialogue == true) { Skip = 1; }
    }

    public void DIStarter ()
	{
		MySceneManager.CutscenePlaying = true;
		DialogueBox.SetActive (true);
        TextUI = TextBox;
		DialogueLength = DILines.Length;
		StartCoroutine(StartDialogue());
	}


IEnumerator StartDialogue ()
	{
			#region Refresh text, stop player and stuff
			MySceneManager.CutscenePlaying = true;
			Playing = true;
			TextUI.text = "";
        #endregion


        #region Typewriter
        for (int i = 0; i <= DILines[Currentline].Length; i++)
        {
            Texting = true;
            if (Skip == 1) { TextUI.text = DILines[Currentline]; break; }
            TextUI.text = DILines[Currentline].Substring(0, i);

            if (i > 0 && i != DILines[Currentline].Length)
            {
				DIVoice[Currentline].pitch = Random.Range(0.9f, 1.2f);

				if (DILines[Currentline].Substring(i - 1, 1) == (".") && DILines[Currentline].Substring(i, 1) != (".") || DILines[Currentline].Substring(i - 1, 1) == ("?") && DILines[Currentline].Substring(i, 1) != ("?") || DILines[Currentline].Substring(i - 1, 1) == ("!") && DILines[Currentline].Substring(i, 1) != ("!"))
                { yield return new WaitForSeconds(DITextSpeed[Currentline] + 0.4f); DIVoice[Currentline].Play(); }

                else if (DILines[Currentline].Substring(i - 1, 1) == (","))
                { yield return new WaitForSeconds(DITextSpeed[Currentline] + 0.2f); DIVoice[Currentline].Play(); }

                else if (DILines[Currentline].Substring(i - 1, 1) == (" "))
                { yield return new WaitForSeconds(0.0f); }

                else if (DILines[Currentline].Substring(i - 1, 1) == (".") && DILines[Currentline].Substring(i, 1) == (".") || DILines[Currentline].Substring(i - 1, 1) == ("?") && DILines[Currentline].Substring(i, 1) == ("?") || DILines[Currentline].Substring(i - 1, 1) == ("!") && DILines[Currentline].Substring(i, 1) == ("!"))
                { yield return new WaitForSeconds(DITextSpeed[Currentline]); DIVoice[Currentline].Play(); }

                else { yield return new WaitForSeconds(DITextSpeed[Currentline]); DIVoice[Currentline].Play(); }
            }
        }
        #endregion

        Texting = false;
			
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
			DialogueBox.SetActive (false);
			MySceneManager.CutscenePlaying = false;
			Currentline = 0;
			Playing = false;
			}  else 
			{
			//Запускаем следующую фразу
			TextUI.text = "";
			DIStarter();
			}}





}

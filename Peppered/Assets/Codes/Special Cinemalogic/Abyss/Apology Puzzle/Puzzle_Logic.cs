using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


public class Puzzle_Logic : MonoBehaviour
{
    #pragma warning disable 649
    private GameObject ControlledBlock;
    [SerializeField] private GameObject Crumbled;
    [SerializeField] private AUDIOMANAGER Audiman;
    [SerializeField] private Color UseColor;
    [SerializeField] private Color UsedColor;
    [SerializeField] private Color UnusedColor;
    [SerializeField] private GameObject[] Blocks;
    [SerializeField] private GameObject[] Rules;
    [SerializeField] private float MoveAmount;
    [SerializeField] private PlayableDirector Cutscene;
    [SerializeField] private GameObject StackOfPaper;

    private int NumberOfTries = 10;
    private Block_Logic BlockScript;
    private bool Moved;
    private bool CanRestart;
    private int Points = 0;
    private bool Done = false;
    [SerializeField] private PLAYER_CONTROLS Player;

    private KeyCode[] keyCodes =
    {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
    };

    // Update is called once per frame
    private void Start()
    {
        Player.CanMove = false;
        Cutscene.Play();
    }

    void Update()
    {
        
        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]) && Done == true)
            {
                if (ControlledBlock != null && BlockScript.Used == false) { BlockScript.ColorMeUnused(UnusedColor); }
                int numberPressed = i + 1;
                if (numberPressed - 1 != 8)
                {
                    if (Rules[0].activeInHierarchy == false && numberPressed != 0 && NumberOfTries != 0) { Rules[0].SetActive(true); }
                    ControlledBlock = Blocks[numberPressed - 1];
                    BlockScript = ControlledBlock.GetComponent<Block_Logic>();
                    if (BlockScript.Used == false) { BlockScript.ColorMeInUse(UseColor); Audiman.Play("Amount1"); }
                }
            }
        }

        #region Control
        if (Input.GetAxisRaw("Horizontal") < 0f && !Moved && BlockScript.Used == false && BlockScript != null)
        {
            Moved = true;
            Audiman.Play("Put Down");
            ControlledBlock.transform.position += new Vector3(-MoveAmount, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0.01f && !Moved && BlockScript.Used == false && BlockScript != null)
        {
            Moved = true;
            Audiman.Play("Put Down");
            ControlledBlock.transform.position += new Vector3(MoveAmount, 0, 0);
        }
        else if (Input.GetAxisRaw("Vertical") > 0f && !Moved && BlockScript.Used == false && BlockScript != null)
        {
            Moved = true;
            Audiman.Play("Put Down");
            ControlledBlock.transform.position += new Vector3(0, MoveAmount, 0);
        }
        else if (Input.GetAxisRaw("Vertical") < 0f && !Moved && BlockScript.Used == false && BlockScript != null)
        {
            Moved = true;
            Audiman.Play("Put Down");
            ControlledBlock.transform.position += new Vector3(0, -MoveAmount, 0);
        }
        else if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0 && Moved){    Moved = false;  }

        if (Input.GetButtonDown("Jump") && ControlledBlock != null)
       {
            //PLACE THAT BLOCK, BABE
            Audiman.Play("Confirm");
            if (NumberOfTries == 10 && Rules[2].activeInHierarchy == false && Rules[1].activeInHierarchy && NumberOfTries != 0) { Rules[2].SetActive(true); }
            if (Rules[1].activeInHierarchy == false) { Rules[1].SetActive(true); }
            BlockScript.ColorMeUsed(UsedColor);
            BlockScript = null;
            ControlledBlock = null;
            if (CanRestart == false && NumberOfTries != 0) { CanRestart = true; if (NumberOfTries < 10) { Rules[2].SetActive(true); } }
        }

        //RESTART
        if (Input.GetButtonDown("Skip") && CanRestart && NumberOfTries != 0)
        {
            StackOfPaper.transform.position = new Vector3(StackOfPaper.transform.position.x, StackOfPaper.transform.position.y - 0.0275f, 0);

            if (NumberOfTries > 0)
            {
                Points = 0;
                GameObject Broke = (GameObject)Instantiate(Crumbled, new Vector3(-5.7f, 0.5f, 0f), Quaternion.identity);
                Broke.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(1f, 4f), Random.Range(0, 5));
                Broke.GetComponent<Rigidbody2D>().AddTorque(20, ForceMode2D.Impulse);

                Done = false;
                CanRestart = false;
                Audiman.Play("Paper");
                Rules[2].SetActive(false);
                Cutscene.Play();
                for (int i = 0; i < Blocks.Length; i++)
                {
                    ControlledBlock = Blocks[i];
                    BlockScript = ControlledBlock.GetComponent<Block_Logic>();
                    BlockScript.ColorMeUnused(UnusedColor);
                }
                ControlledBlock = null;
                BlockScript = null;
                NumberOfTries--;
            }

            //YOU RAN OUT OF TRIES
            if (NumberOfTries == 0)
            {
                Rules[0].SetActive(false);
                Rules[1].SetActive(false);
                Rules[2].SetActive(false);
                Rules[3].SetActive(false);
                Rules[4].SetActive(true);
                Points = 7;
                MySceneManager.Regret = 2;
                for (int i = 0; i < 7; i++)
                {
                    ControlledBlock = Blocks[i];
                    BlockScript = ControlledBlock.GetComponent<Block_Logic>();
                    BlockScript.AutoComplete(UsedColor);
                }
                Done = true;
                BlockScript = null;
                ControlledBlock = null;
                gameObject.SetActive(false);
            }
        }
        #endregion
    }

    public void Correct()
    {
        Points++;
        //YOU WIN
        if (Points == 8 && NumberOfTries > 0)
        {
            MySceneManager.Regret = 1;
            Destroy(gameObject);
        } else if (Points == 8 && NumberOfTries == 0)
        {
            MySceneManager.Regret = 4;
            gameObject.SetActive(false);
        }
    }

    public void Incorrect()
    {
        if (NumberOfTries == 0) { MySceneManager.Regret = 3; gameObject.SetActive(false); }
    }

    public void Doneskie (){ Done = true; }
}
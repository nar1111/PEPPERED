using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Simon_Says : MonoBehaviour
{
    #pragma warning disable 649
    [SerializeField]
    private Transform[] SEPoints;
    [SerializeField]
    private GameObject ActArea;
    private SpriteRenderer SRActArea;
    [SerializeField]
    private Collider2D ActCol;
    [SerializeField]
    private GameObject[] Comands;
    [SerializeField]
    private GameObject ALLComands;
    [SerializeField]
    private GameObject GameTarget;
    [SerializeField]
    private Rigidbody2D[] MyrigidBody;
    [SerializeField]
    private bool InArea;
    private int Go;
    private float ShakeAmount;
    private string KeyPressed;
    [HideInInspector]
    public int StartShake = 0;
    [HideInInspector]
    public string PerfectPitchPresantation = "";
    [SerializeField]
    private AUDIOMANAGER Audioman;
    [SerializeField]
    private Sprite NewZiggy;
    [SerializeField]
    private SpriteRenderer ZiggyRen;


    // Start is called before the first frame update
    void Start()
    {
        Audioman.Play("Battle Start");
        gameObject.transform.position = GameTarget.gameObject.transform.position;

        ALLComands.transform.position = new Vector2(GameTarget.gameObject.transform.position.x - 8f, ALLComands.transform.position.y);

        if (MySceneManager.Regret == -2)
        {
            Comands[21].SetActive(true);
            Comands[21].transform.position = new Vector2(GameTarget.gameObject.transform.position.x - 6f, Comands[21].transform.position.y);
        } else
        {
            Comands[21].SetActive(true);
            Comands[21].transform.position = new Vector2(GameTarget.gameObject.transform.position.x - 6f, Comands[21].transform.position.y);
        }


        ALLComands.SetActive(true);
        StartCoroutine(Getready());   
    }

    private void Update()
    {
        if (Go > 0)
        {
            if (MySceneManager.Regret == -1)
            {
                if (InArea == false)
                {
                    Comands[21].transform.position = new Vector2(Comands[21].transform.position.x + 2f * Time.deltaTime, Comands[21].transform.position.y);
                }

                else if (InArea == true)
                {
                    Comands[21].transform.position = new Vector2(Comands[21].transform.position.x + 0.09f * Time.deltaTime, Comands[21].transform.position.y);
                    if (Input.GetAxisRaw("Vertical") > 0 && Input.GetAxisRaw("Horizontal") == 0f)
                    {
                        StartCoroutine(Gotit());
                        Comands[21].transform.position = new Vector2(-1000f, -1000f);
                        Go = 0;
                        MySceneManager.Regret = -3;
                    }
                }

            }

            else if (MySceneManager.Regret == -2)
            {
                if (Input.anyKeyDown)
                {
                    if (KeyPressed != Input.inputString)
                    {
                        
                        for (int i = 0; i < Comands.Length; i++)
                        {
                            if (Comands[i].transform.position.x > SEPoints[0].transform.position.x && Comands[i].transform.position.x < SEPoints[1].transform.position.x)
                            {
                                StopCoroutine(Gotit());
                                ShakeAmount += 0.0010f;
                                StartCoroutine(Gotit());
                                Comands[i].transform.position = new Vector2(GameTarget.gameObject.transform.position.x - 8f, Comands[i].transform.position.y);
                                if (Comands[i].gameObject == Comands[16].gameObject && ZiggyRen.sprite != NewZiggy) { ZiggyRen.sprite = NewZiggy; }
                                break;
                            }
                        }

                        KeyPressed = Input.inputString;
                        if (PerfectPitchPresantation.Length < 208)
                        {
                            PerfectPitchPresantation = PerfectPitchPresantation + Input.inputString;
                        }
                    }
                }

                if (Go >= 2)
                {
                    Comands[0].transform.position  = new Vector2(Comands[0].transform.position.x + 3f * Time.deltaTime, Comands[0].transform.position.y);
                    Comands[1].transform.position  = new Vector2(Comands[1].transform.position.x + 3f * Time.deltaTime, Comands[1].transform.position.y);
                    Comands[2].transform.position  = new Vector2(Comands[2].transform.position.x + 3f * Time.deltaTime, Comands[2].transform.position.y);
                    Comands[3].transform.position  = new Vector2(Comands[3].transform.position.x + 3f * Time.deltaTime, Comands[3].transform.position.y);
                    Comands[4].transform.position  = new Vector2(Comands[4].transform.position.x + 3f * Time.deltaTime, Comands[4].transform.position.y);
                    Comands[5].transform.position  = new Vector2(Comands[5].transform.position.x + 3f * Time.deltaTime, Comands[5].transform.position.y);
                    Comands[6].transform.position  = new Vector2(Comands[6].transform.position.x + 3f * Time.deltaTime, Comands[6].transform.position.y);
                    Comands[7].transform.position  = new Vector2(Comands[7].transform.position.x + 3f * Time.deltaTime, Comands[7].transform.position.y);
                    Comands[8].transform.position  = new Vector2(Comands[8].transform.position.x + 3f * Time.deltaTime, Comands[8].transform.position.y);
                    Comands[9].transform.position  = new Vector2(Comands[9].transform.position.x + 3f * Time.deltaTime, Comands[9].transform.position.y);
                    Comands[10].transform.position = new Vector2(Comands[10].transform.position.x + 3f * Time.deltaTime, Comands[10].transform.position.y);
                    Comands[11].transform.position = new Vector2(Comands[11].transform.position.x + 3f * Time.deltaTime, Comands[11].transform.position.y);
                    Comands[12].transform.position = new Vector2(Comands[12].transform.position.x + 3f * Time.deltaTime, Comands[12].transform.position.y);
                    Comands[13].transform.position = new Vector2(Comands[13].transform.position.x + 3f * Time.deltaTime, Comands[13].transform.position.y);
                    Comands[14].transform.position = new Vector2(Comands[14].transform.position.x + 3f * Time.deltaTime, Comands[14].transform.position.y);
                    Comands[15].transform.position = new Vector2(Comands[15].transform.position.x + 3f * Time.deltaTime, Comands[15].transform.position.y);
                    Comands[16].transform.position = new Vector2(Comands[16].transform.position.x + 3f * Time.deltaTime, Comands[16].transform.position.y);
                    Comands[17].transform.position = new Vector2(Comands[17].transform.position.x + 3f * Time.deltaTime, Comands[17].transform.position.y);
                    Comands[18].transform.position = new Vector2(Comands[18].transform.position.x + 3f * Time.deltaTime, Comands[18].transform.position.y);
                    Comands[19].transform.position = new Vector2(Comands[19].transform.position.x + 3f * Time.deltaTime, Comands[19].transform.position.y);
                    Comands[20].transform.position = new Vector2(Comands[20].transform.position.x + 3f * Time.deltaTime, Comands[20].transform.position.y);
                }

                if (Go >= 1)
                {
                    Comands[21].transform.position = new Vector2(Comands[21].transform.position.x + 0.3f * Time.deltaTime, Comands[21].transform.position.y);
                }
            }

            //if (Count > 30 && )
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            InArea = true;
            SRActArea.color = new Color(0.8f, 1, 1, 0.8f);
            ActArea.transform.localScale = new Vector2(1f, ActArea.transform.localScale.y);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Bullet")
        {
            InArea = false;
            if (other.gameObject.transform.position.x < gameObject.transform.position.x)
            {
                SRActArea.color = new Color(0, 1, 0.3f, 0.4f);
                ActArea.transform.localScale = new Vector2(1f, ActArea.transform.localScale.y);
            }

            else if (other.gameObject.transform.position.x > gameObject.transform.position.x)
            {
                StopCoroutine(Gotit());
                StopCoroutine(Lostit());
                ShakeAmount += 0.0010f;
                if (MySceneManager.Regret == -2)
                {
                    other.gameObject.transform.position = new Vector2(GameTarget.gameObject.transform.position.x - 8f, other.gameObject.transform.position.y);
                    if (other.gameObject == Comands[16].gameObject && ZiggyRen.sprite != NewZiggy) { ZiggyRen.sprite = NewZiggy; }
                }

                else { MySceneManager.Regret = -4;}
                StartCoroutine(Lostit());
            }
        }
    }

    IEnumerator Gotit()
    {
        if (StartShake == 0) { StartShake = 1; }
        SRActArea.color = new Color(0, 1, 0.3f, 1f);
        ActArea.transform.localScale = new Vector2(2.1f, ActArea.transform.localScale.y);
        yield return new WaitForSeconds(0.2f);
        SRActArea.color = new Color(1, 1, 1, 0.4f);
        ActArea.transform.localScale = new Vector2(1f, ActArea.transform.localScale.y);
        if (MySceneManager.Regret == -3) { Destroy(gameObject); }
        Audioman.Play("Ting");
    }

    IEnumerator Lostit()
    {
        if (StartShake == 0) { StartShake = 1; }
        SRActArea.color = new Color(1, 0, 0, 0.6f);
        ActArea.transform.localScale = new Vector2(2.1f, ActArea.transform.localScale.y);
        yield return new WaitForSeconds(0.2f);
        SRActArea.color = new Color(1, 1, 1, 0.4f);
        ActArea.transform.localScale = new Vector2(1f, ActArea.transform.localScale.y);
        if (MySceneManager.Regret == -4) { Comands[21].SetActive(false);  Destroy(gameObject); }
        Audioman.Play("Error");
    }

    IEnumerator Getready()
    {
        if (MySceneManager.Regret == -1)
        {
            MySceneManager.CutscenePlaying = true;
            ActCol.enabled = true;

                while (ActArea.transform.localScale.x < 1)
                {
                    ActArea.transform.localScale = new Vector2(ActArea.transform.localScale.x + 0.015f, ActArea.transform.localScale.y);
                    yield return new WaitForSeconds(0.01f);
                }
            
            ActArea.transform.localScale = new Vector2(1f, ActArea.transform.localScale.y);
            SRActArea = ActArea.GetComponent<SpriteRenderer>();
            SRActArea.color = new Color(0, 1, 0.3f, 0.7f);
            yield return new WaitForSeconds(0.05f);
            SRActArea.color = new Color(1, 1, 1, 0.4f);
            Go = 1;
        }

        else if (MySceneManager.Regret == -2)
        {
            MySceneManager.CutscenePlaying = true;
            ActCol.enabled = true;
            for (int i = 0; i < 24; i++)
            {
                ActArea.transform.localScale = new Vector2(ActArea.transform.localScale.x + 0.05f, ActArea.transform.localScale.y);
                yield return new WaitForSeconds(0.01f);
            }
            ActArea.transform.localScale = new Vector2(1.51f, ActArea.transform.localScale.y);
            SRActArea = ActArea.GetComponent<SpriteRenderer>();
            SRActArea.color = new Color(0, 1, 0.3f, 0.7f);
            yield return new WaitForSeconds(0.05f);
            SRActArea.color = new Color(1, 1, 1, 0.4f);
            Go = 1;
            yield return new WaitForSeconds(6f);
            Go = 2;

        }
       
    }
}
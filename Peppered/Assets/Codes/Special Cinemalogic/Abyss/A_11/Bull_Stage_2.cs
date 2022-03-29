using UnityEngine;

public class Bull_Stage_2 : State
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject[] Bulls;
    [SerializeField] private GameObject[] EndPoints;
    [SerializeField] private GameObject[] DoorStuff;
    [SerializeField] private Animator[] Myanim;
    [SerializeField] private Bull_Stage_3 Stage3;
    [SerializeField] private A_11 A11;
    [SerializeField] private AUDIOMANAGER Audiman;
    [SerializeField] private AudioHighPassFilter AudiFilter;
    private MySceneManager sceneman;

    //Tech Stuff
    private int MyStage = 0;
    private GameObject Target;

    //Moving.
    private float Speed;
    private float MaxSpeed = 12f;
    private float MinSpeed = 9.5f;
    private float OurDistance = 5f;

    //Handicap
    private float StartTimer = 4.5f;

    public override State RunCurrentState()
    {
        if (Target == EndPoints[0] && MyStage > 0)
        {
            FlipBulls();
        }

        EndSequence();

        if (MyStage == 0)
        {
            Target = Player;
            Handicap();
        } else if (MyStage > 0)
        {
            AdjustSpeed();

            Move();

            Handicap2();
        }

        //if (MyStage == 2) { GainOnMe(); }

        if (MyStage == 3) { SecondBull(); }

        if (MyStage == 4) { return Stage3; }
        return this;
    }

    private void Move()
    {
        if (Bulls[0].activeInHierarchy && Target != null && A11.Handicapped == false)
        {
            Bulls[0].transform.position = Vector2.MoveTowards(Bulls[0].transform.position, new Vector2(Target.transform.position.x, Bulls[0].transform.position.y), Speed * Time.deltaTime);
            Myanim[0].Play("Bull Bro Ride");
        }
        else
        {
            if (Bulls[0].activeInHierarchy)
            {
                Myanim[0].Play("Bull Bro Idle");
            }
        }

        if (Bulls[1].activeInHierarchy && Target != null && A11.Handicapped == false)
        {
            Bulls[1].transform.position = Vector2.MoveTowards(Bulls[1].transform.position, new Vector2(Target.transform.position.x, Bulls[1].transform.position.y), Speed * Time.deltaTime);
            Myanim[1].Play("Bro 2 Ride 1");
        } else
        {
            if (Bulls[1].activeInHierarchy)
            {
                Myanim[1].Play("Bro 2 Idle 1");
            }
        }
    }

    private void FlipBulls()
    {
        if (Player.transform.position.x > Bulls[0].transform.position.x)
        {
            Bulls[0].transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            Bulls[0].transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Player.transform.position.x > Bulls[1].transform.position.x)
        {
            Bulls[1].transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            Bulls[1].transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    private void AdjustSpeed()
    {
        if (Vector2.Distance(Bulls[0].transform.position, Player.transform.position) < OurDistance)
        {
            Speed = MinSpeed;
        }
        else
        {
            Speed = MaxSpeed;
        }
    }

    private void Handicap()
    {
        StartTimer -= Time.deltaTime;

        if (StartTimer < 0)
        {
            StartTimer = 5f;
            Target = Player;
            MyStage = 1;
        }
        else if (StartTimer >= 0 && Vector2.Distance(Bulls[0].transform.position, Player.transform.position) > 8)
        {
            Animator Charanim1 = Bulls[0].GetComponent<Animator>();
            Animator Charanim2 = Bulls[1].GetComponent<Animator>();

            Charanim1.Play("Bull Bro Ride");
            Charanim2.Play("Bro 2 Ride 1");
            StartTimer = 5f;
            Target = Player;
            MyStage = 1;
        }
    }

    private void Handicap2()
    {
        StartTimer -= Time.deltaTime;
        if (StartTimer < 0 && MyStage < 2) { MyStage = 2; }
    }

    private void EndSequence()
    {
        if (Target != EndPoints[1] && Vector2.Distance(Bulls[0].transform.position, EndPoints[0].transform.position) < 0.5f)
        {
            Target = EndPoints[1];
        }

        if (Target == EndPoints[1] && Vector2.Distance(Bulls[0].transform.position, EndPoints[1].transform.position) < 0.5f && MyStage < 3)
        {
            DoorStuff[0].SetActive(true);
            DoorStuff[1].SetActive(false);
            DoorStuff[2].SetActive(true);
            DoorStuff[3].SetActive(true);
            Bulls[0].SetActive(false);
            Audiman.Play("Rock Impact");
            Audiman.Play("Battle Start");

            sceneman = FindObjectOfType<MySceneManager>();
            AudiFilter = sceneman.GetComponent<AudioHighPassFilter>();
            AudiFilter.enabled = true;
            if (Player.transform.parent != null)
            {
                State_Cart_Ride MyRide = Player.transform.parent.GetComponentInChildren<State_Cart_Ride>();
                MyRide.Chill(1f);
            }
            MyStage = 3;
        }
    }

    private void SecondBull()
    {
        if (Target == EndPoints[1] && Vector2.Distance(Bulls[1].transform.position, EndPoints[1].transform.position) < 0.5f)
        {
            Target = null;
            Bulls[1].SetActive(false);
            MyStage = 4;
        }
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpfullBoss : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private AUDIOMANAGER Audiman;
    [SerializeField]private Animator BossAnim;
    [SerializeField]private GameObject Advice;
    [SerializeField]private float Timer;
    [SerializeField]private Cynthia Cyn;
    private float TimesUp;
    private int Stage = 0;
    private int AdviceLimit = 0;
    private GameObject AdviceClone;

    private void Start(){    TimesUp = 8f;    }

    // Update is called once per frame
    void Update()
    {
        //Wait for the first advice
        if (Stage == 0 && Cyn.Stage == 1)
        {
            if (TimesUp > 0) { TimesUp -= Time.deltaTime; }
            else if (TimesUp <= 0)
            {
                TimesUp = Timer;
                Stage = 1;
            }          
        }

        if (Stage == 1 && Cyn.Stage == 1)
        {
            //Give an advice
            if (AdviceLimit == 0)
            {
                AdviceLimit = 1;
                AdviceClone = Instantiate(Advice, gameObject.transform.position + new Vector3(-0.3f, 1, 0), Quaternion.identity);
                BossAnim.Play("Boss_Advice");
                Audiman.Play("Amount1");
                TimesUp = 4f;
            }

            //Move this advice
            if (AdviceClone != null)
            {
                AdviceClone.transform.position = new Vector3(AdviceClone.transform.position.x, AdviceClone.transform.position.y + 0.0009f);
            }

            if (TimesUp > 0) { TimesUp -= Time.deltaTime; }
            else if (TimesUp <= 0)
            {    
                if (AdviceLimit == 1) { Destroy(AdviceClone); AdviceLimit = 2; TimesUp = Timer; BossAnim.Play("Boss_Idle"); }
                else if (AdviceLimit == 2) { AdviceLimit = 0; }
            }
        }

        if (Cyn.Stage == 2 && AdviceLimit != 66)
        {
            AdviceLimit = 66;
            if (AdviceClone.activeInHierarchy)
            {
                AdviceClone.SetActive(false);
                Destroy(AdviceClone);
            }
            BossAnim.Play("Boss_Idle");
            AdviceLimit = 2;
        }
    }
}

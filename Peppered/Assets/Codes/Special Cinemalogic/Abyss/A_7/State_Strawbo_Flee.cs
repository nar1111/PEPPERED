using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Strawbo_Flee : State
{
    #pragma warning disable 649
    [SerializeField] private Transform EscapePoint;
    [SerializeField] private Animator FlashAnim;
    [SerializeField] private Animator StrawAnim;
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private GameObject Strawbo;
    [SerializeField] private GameObject PoliceSiren;
    [SerializeField] private Black_lines Blines;
    [SerializeField] private AUDIOMANAGER AudiMan;
    private int MyState;

    public override State RunCurrentState()
    {
        //ROTATE THIS BITCH
        if (MyState == 0) { StartCoroutine(Cutscene()); MyState = 1; }

        if (MyState == 1)
        {
            Vector3 dir = Player.transform.position - Strawbo.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
            Strawbo.transform.rotation = Quaternion.Slerp(Strawbo.transform.rotation, rotation, 10f * Time.deltaTime);
        } else if (MyState == 2)
        {
            Vector3 dir = EscapePoint.transform.position - Strawbo.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle + 180));
            Strawbo.transform.rotation = Quaternion.Slerp(Strawbo.transform.rotation, rotation, 10f * Time.deltaTime);
            Strawbo.transform.position = Vector2.MoveTowards(Strawbo.transform.position, EscapePoint.position, 3f * Time.deltaTime);
            if (Vector2.Distance(Strawbo.transform.position, EscapePoint.position) < 0.05f) { Strawbo.SetActive(false); }
        }

        return this;
    }

    IEnumerator Cutscene()
    {
        Player.CanMove = false;
        MySceneManager.CutscenePlaying = true;
        Player.MyRigidBody.velocity = Vector2.zero;
        if (Player.transform.position.x < Strawbo.transform.position.x) { Player.transform.localScale = new Vector3(1,1,1); }
        Blines.Show(220f, 1f);
        yield return new WaitForSeconds(2f);
        FlashAnim.Play("White_End", 0, 0.3f);
        AudiMan.Play("Put Down");
        yield return new WaitForSeconds(2f);
        MyState = 2;
        Player.CanMove = true;
        MySceneManager.CutscenePlaying = false;
        Blines.Hide(0.3f);
        //AudiMan.Play("Mer Gun Out");
        StrawAnim.Play("Strawberry Off");
        PoliceSiren.SetActive(false);
        AudiMan.Play("Busted");
    }
}

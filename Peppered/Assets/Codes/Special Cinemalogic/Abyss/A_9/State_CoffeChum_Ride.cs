using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_CoffeChum_Ride : State
{
    #pragma warning disable 649
    [Header("MOVEMENT")]
    [SerializeField] private Animator MyAnim;
    [SerializeField] private Rigidbody2D MyRigidBody;
    [SerializeField] private State_CoffeeChum_Idle IdleState;
    private float fHorizontalDampingWhenStopping = 0.01f;
    private float fHorizontalDampingWhenTurning = 0.01f;
    private float fHorizontalVelocity;
    [SerializeField] private string AnimName;
    [SerializeField] private bool FlipRen;

    [Header("PLAYER")]
    [SerializeField] private PLAYER_CONTROLS Player;
    [SerializeField] private SpriteRenderer MyRen;
    [SerializeField] private GameObject AdditionalThing;


    public override State RunCurrentState()
    {
        MoveThisGuy();

        if (FlipRen)
        {
            TurnSprite();
        }

        if (Input.GetButtonDown("Jump") && MySceneManager.CutscenePlaying == false)
        {
            Player.Wind = true;
            MyAnim.Play(AnimName);
            Player.gameObject.transform.parent = null;
            Player.MyAnim.Play("Take Off");
            Player.CanMove = true;
            Player.Bounce(10.5f, 2);
            WHAT_HAVE_I_DONE.Collectibles.Remove("CoffeeChum");
            if (AdditionalThing != null) { AdditionalThing.SetActive(false); }
            return IdleState;
        } else if (!Player.MyAnim.GetCurrentAnimatorStateInfo(0).IsName("Invisible") && !Input.GetButtonDown("Jump"))
        {
            Player.Wind = true;
            MyAnim.Play(AnimName);
            Player.gameObject.transform.parent = null;
            Player.MyAnim.Play("Take Off");
            WHAT_HAVE_I_DONE.Collectibles.Remove("CoffeeChum");
            Player.CanMove = true;
            Player.Bounce(22f, 2);
            MyRigidBody.velocity = new Vector2(25f, 0f);
            if (AdditionalThing != null) { AdditionalThing.SetActive(false); }
            return IdleState;
        }

        return this;
    }

    private void MoveThisGuy()
    {
        Player.gameObject.transform.position = new Vector2(transform.parent.position.x, transform.parent.position.y + 0.3f);
        fHorizontalVelocity = MyRigidBody.velocity.x;
        fHorizontalVelocity += Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) < 0.01f && MySceneManager.CutscenePlaying == true)
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenStopping, Time.fixedDeltaTime * 3f);
        }

        else if (Mathf.Sign(Input.GetAxisRaw("Horizontal")) != Mathf.Sign(fHorizontalVelocity) || MySceneManager.CutscenePlaying == true)
        {
            fHorizontalVelocity *= Mathf.Pow(1f - fHorizontalDampingWhenTurning, Time.fixedDeltaTime * 3f);
        }

        if (MySceneManager.CutscenePlaying == false)
        {
            MyRigidBody.velocity = new Vector2(fHorizontalVelocity, MyRigidBody.velocity.y);
        }
    }

    private void TurnSprite()
    {
        //Turn Sprite Around
            if (Input.GetAxisRaw("Horizontal") > 0 && MySceneManager.CutscenePlaying == false) { MyRen.flipX = false; Player.gameObject.transform.localScale = new Vector3(1, 1, 1); }
            else if (Input.GetAxisRaw("Horizontal") < 0 && MySceneManager.CutscenePlaying == false) { MyRen.flipX = true; Player.gameObject.transform.localScale = new Vector3(-1, 1, 1); }
    }
}

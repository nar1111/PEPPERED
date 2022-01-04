using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly_At_Player : MonoBehaviour
{
    public float Speed;

    private Transform Player;
    private Vector2 target;

    // Start is called before the first frame update
    void Start()
    {
        if (MySceneManager.DeadState == 0)
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else { Destroy(gameObject); }
        target = new Vector2(Player.position.x, Player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, Speed * Time.deltaTime);

        if (transform.position.x == target.x && transform.position.y == target.y) { DestroyMe(); }
        if (Player == null) { Destroy(gameObject); }
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }

}

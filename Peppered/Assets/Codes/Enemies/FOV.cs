using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    //raio de visao
    public float viewRadius = 5;
    //angulo de visao
    public float viewAngle = 115;
    public int PepperInSight = 0;

    //variaveis para identificarmos o que eh obtaculo e o que deve ser detectado respectivamente
    public LayerMask obstacleMask, detectionMask;

    //vetor de possiveis alvos em nosso raio de visao
    public Collider2D[] targetsInRadius;

    //lista de posicoes de possiveis alvos visiveis
    public List<Transform> visibleTargets = new List<Transform>();

    private void Update()
    {
        FindVisibleTargets();

        if (visibleTargets.Count == 0)
        {
            PepperInSight = 0;

        } else if (visibleTargets.Count > 0)
        {
            PepperInSight = 1;
        }
    }

    void FindVisibleTargets()
    {

        targetsInRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, detectionMask, -Mathf.Infinity, Mathf.Infinity);
        visibleTargets.Clear();

        for (int i = 0; i < targetsInRadius.Length; i++)
        {

            Transform target = targetsInRadius[i].transform;

            Vector2 dirTarget = new Vector2(target.position.x - transform.position.x, target.position.y - transform.position.y);

            Vector2 dir = new Vector2();
            dir = transform.right;

            if (Vector2.Angle(dirTarget, dir) < viewAngle / 2)
            {
                float distanceTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, dirTarget, distanceTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    public Vector2 DirFromAngle(float angleDeg, bool global)
    {
        if (!global)
        {
            angleDeg += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Cos(angleDeg * Mathf.Deg2Rad), Mathf.Sin(angleDeg * Mathf.Deg2Rad));
    }
}
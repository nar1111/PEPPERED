using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Vector2 parallaxEffectMultiplier;
    private Transform CameraTransform;
    private Vector3 lastCameraPosition;

    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = Camera.main.transform;
        lastCameraPosition = CameraTransform.position;
    }

    private void Update()
    {
        Vector3 deltaMovement = CameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPosition = CameraTransform.position;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScroll : MonoBehaviour
{
    public Vector3 farLeftPos;
    public Vector3 farRightPos;

    public float scrollingSpeed;
    private float distance;
    public GameController gameController;

    private void Update()
    {
        distance = Vector3.Distance(transform.position, farLeftPos);

        if(gameController.IsPlaying)
            transform.Translate(Vector3.left * scrollingSpeed);
        if (distance < 0.1)
        {
            transform.position = farRightPos;
        }
    }
}

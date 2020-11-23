using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Obstacle : MonoBehaviour
{
    public float xPosFarLeft;
    public float yScaleMin;
    public float yScaleMax;

    public float scrollingSpeed;
    public GameController gameController;

    private void OnEnable()
    {
        transform.localScale = new Vector3(1, Random.Range(yScaleMin, yScaleMax));
    }

    private void Update()
    {
        if(gameController.IsPlaying)
            transform.Translate(Vector3.left * scrollingSpeed);

        if (transform.position.x < xPosFarLeft)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameController.KillPlayer();
        }
    }
}

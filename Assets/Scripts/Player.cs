using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float fallMultiplierFloat;
    public float lowJumpMultiplierFloat;
    public float jumpVelocitiy;
    public ScoreController scoreController;

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = Vector2.up * jumpVelocitiy;
        }

        //faster falling
        if (rigidbody.velocity.y < 0)
        {
            rigidbody.velocity += Vector2.up * Physics.gravity.y * (fallMultiplierFloat - 1) * Time.deltaTime;
        }

        //control jump height by length of time jump button held
        if (rigidbody.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rigidbody.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplierFloat - 1) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PointTrigger"))
        {
            scoreController.AddScore();
        }
    }
}

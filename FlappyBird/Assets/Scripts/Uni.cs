using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Uni : MonoBehaviour
{
    private const float JUMP_AMOUNT = 80f;

    
    private static Uni instance;
    public static Uni GetInstance()
    {
        return instance;

    }

    public event EventHandler OnDied;

    private Rigidbody2D Unirigidbody2D;

    private void Awake()
    {
        instance = this;
        Unirigidbody2D = GetComponent<Rigidbody2D>();
    }
  
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    private void Jump()
    {
        Unirigidbody2D.velocity = Vector2.up * JUMP_AMOUNT;

    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unirigidbody2D.bodyType = RigidbodyType2D.Static;
        if (OnDied != null) OnDied(this, EventArgs.Empty);
    }
}

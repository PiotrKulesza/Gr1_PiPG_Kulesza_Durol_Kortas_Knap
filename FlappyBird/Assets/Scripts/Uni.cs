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
    public event EventHandler OnStartedPlaying;

    private Rigidbody2D Unirigidbody2D;
    private State state;


    private enum State
    {
        WaitingToStart,
        Playing,
        Dead
    }


    private void Awake()
    {
        instance = this;
        Unirigidbody2D = GetComponent<Rigidbody2D>();
        Unirigidbody2D.bodyType = RigidbodyType2D.Static;
        state = State.WaitingToStart;
    }
  
    private void Update()
    {
        switch (state)
        {
            default:
            case State.WaitingToStart:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    state = State.Playing;
                    Unirigidbody2D.bodyType = RigidbodyType2D.Dynamic;
                    Jump();
                    if (OnStartedPlaying != null) OnStartedPlaying(this, EventArgs.Empty);
                }
                break;
            case State.Playing:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Jump();
                }
                break;
            case State.Dead:
                break;

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

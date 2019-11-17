using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uni : MonoBehaviour
{
    private const float JUMP_AMOUNT = 80f;

    private Rigidbody2D Unirigidbody2D;
    private void Awake()
    {
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

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rd;

    [SerializeField] float speed;

    private void Start()
    {
        rd = GetComponent<Rigidbody2D>();    
    }

    private void FixedUpdate()
    {
        Vector2 moveData = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
        {
            moveData += Vector2.up;
        }
        if(Input.GetKey(KeyCode.S))
        {
            moveData += Vector2.down;
        }
        if(Input.GetKey(KeyCode.D))
        {
            moveData += Vector2.right;
        }
        if(Input.GetKey(KeyCode.A)) 
        { 
            moveData+= Vector2.left;
        }
        moveData = moveData.normalized;

        rd.velocity = moveData * speed;
    }
}

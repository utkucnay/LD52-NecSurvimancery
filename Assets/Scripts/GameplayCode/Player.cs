using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Singleton<Player>
{
    CharachterRotater cr;
    Rigidbody2D rb;

    private void Start()
    {
        cr = GetComponent<CharachterRotater>();
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        cr.Dir = rb.velocity;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    private AnimationScript anim;

    public float speed = 10;
    public float jumpForce = 10;

    public bool canMove=true;
    public bool isPressingKey;

    private bool onGround;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
    }

    // Update is called once per frame
    void Update()
    {
        isPressingKey = false;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        if (dir != Vector2.zero)
        {
            isPressingKey = true;
            Walk(dir);
            anim.SetHorizontalMovement(x,y);
        }

        if (Input.GetButtonDown("Jump"))
        {
            Jump(Vector2.up);
        }
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }

    private void Jump(Vector2 dir)
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
    }
}

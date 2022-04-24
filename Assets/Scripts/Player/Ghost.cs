using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    private GAnimationScript anim;
    private Collision coll;

    public float speed = 10;

    public bool canMove = true;
    public bool isPressingKey;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<GAnimationScript>();
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
        }

        Fly(dir);
        anim.SetHorizontalMovement(x, y);
    }

    private void Fly(Vector2 dir)
    {
        if (!canMove)
            return;

        rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
    }
}

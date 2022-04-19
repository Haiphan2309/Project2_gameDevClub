using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    bool isGround;
    Rigidbody2D rigi;
    SpriteRenderer sprRen;
    public bool isMoveLeft = true;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rigi = gameObject.GetComponent<Rigidbody2D>();
        sprRen = gameObject.GetComponent<SpriteRenderer>();

        speed *= 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGround) Jump();
    }

    void Jump()
    {
        Debug.Log("Jump");
        isGround = false;
        if (isMoveLeft)
            rigi.velocity = new Vector3(-speed * Time.fixedDeltaTime, speed * Time.fixedDeltaTime, 0); 
        else
            rigi.velocity = new Vector3(speed * Time.fixedDeltaTime, speed * Time.fixedDeltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts[0].normal.x != 0)
        {
            isMoveLeft = !isMoveLeft;
            if (isMoveLeft) sprRen.flipX = false;
            else sprRen.flipX = true;
        }
        isGround = true;
        
    }
}

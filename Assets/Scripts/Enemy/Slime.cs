using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public LayerMask groundLayer;
    public LayerMask interactives;

    public bool isGround;
    public bool isMoveLeft = true;
    public float speed;
    public GameObject jumpDust;
    // Start is called before the first frame update
    void Start()
    {
        speed *= 100;
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //isGround = Physics2D.BoxCast(coli.bounds.center, coli.bounds.size, 0f, Vector2.down, .1f, groundLayer)
        //    || Physics2D.BoxCast(coli.bounds.center, coli.bounds.size, 0f, Vector2.down, .1f, interactives);   
    }

    void Jump()
    {
        //Debug.Log("Jump");
        isGround = false;
        if (isMoveLeft)
            rigi.velocity = new Vector3(-speed * Time.fixedDeltaTime, speed * Time.fixedDeltaTime, 0); 
        else
            rigi.velocity = new Vector3(speed * Time.fixedDeltaTime, speed * Time.fixedDeltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.contacts[0].normal.y);
        isGround = Physics2D.BoxCast(coli.bounds.center, coli.bounds.size, 0f, Vector2.down, .1f, groundLayer)
            || Physics2D.BoxCast(coli.bounds.center, coli.bounds.size, 0f, Vector2.down, .1f, interactives);

        if (other.contacts[0].normal.x >= 1-0.03f || other.contacts[0].normal.x <= -1+0.03f)
        {
            isGround = false;

            rigi.velocity = new Vector2(other.contacts[0].normal.x, other.contacts[0].normal.y) * 3;
            isMoveLeft = !isMoveLeft;
            if (isMoveLeft) sprRen.flipX = false;
            else sprRen.flipX = true;
        }
        //if (other.contacts[0].normal.y >= 1-0.03f)
        //{
        //    GameObject jumpDustObj = Instantiate(jumpDust, transform.position - new Vector3(0,0.4f,0), Quaternion.identity);
        //    Destroy(jumpDustObj, 1);
        //    anim.Play("Move");
        //    isGround = true;
        //}
        isGround = Physics2D.BoxCast(coli.bounds.center, coli.bounds.size, 0f, Vector2.down, .1f, groundLayer)
            || Physics2D.BoxCast(coli.bounds.center, coli.bounds.size, 0f, Vector2.down, .1f, interactives);

        if (isGround)
        {
            GameObject jumpDustObj = Instantiate(jumpDust, transform.position - new Vector3(0, 0.4f, 0), Quaternion.identity);
            Destroy(jumpDustObj, 1);
            anim.Play("Move");
            if (isDie == false)
            {
                Invoke("Jump", 0.3f);
            }
        }
    }

    void OnBecameVisible()
    {
        enabled = true;
    }
    void OnBecameInvisible()
    {
        enabled = false;
    }
}

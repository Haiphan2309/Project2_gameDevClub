using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    private AnimationScript anim;
    private Collision coll;

    public float speed = 10;
    public float jumpForce = 10;

    public bool canMove=true;
    public bool onGround;
    public bool isPressingKey;

    public GameObject dieObj, jumpDust;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
        coll = GetComponent<Collision>();
    }


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
        Walk(dir);
        anim.SetHorizontalMovement(x,y,rb.velocity.y);

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("jump");
            Jump(Vector2.up);
        }

        if (coll.onGround)
        {
            Debug.Log("on Ground");
        }
        if (coll.onRightWall)
        {
            Debug.Log("on Right Wall");
        }
        if (coll.onLeftWall)
        {
            Debug.Log("on Left Wall");
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
        if (!canMove || !onGround) return;

        onGround = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += dir * jumpForce;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.contacts[0].normal.y);
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "UndefeatEnemy")
        {
            canMove = false;
            rb.velocity = new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.y) * 6;
            if (collision.contacts[0].normal.y >= 1-0.03f && collision.gameObject.tag == "Enemy")
            {
                collision.gameObject.GetComponent<Enemy>().GetHit();
                Invoke("CanMove", 0.3f);
            }
            else
            {
                //CameraController.Shake();
                //Invoke("Die", 0.2f);
                Die();
            }
        }

        if (collision.contacts[0].normal.y >= 1-0.03f)
        {
            onGround = true;
            if (collision.gameObject.tag != "Plate")
            {
                GameObject jumpDustObj = Instantiate(jumpDust, transform.position - new Vector3(0, 0.25f, 0), Quaternion.identity);
                Destroy(jumpDustObj, 1);
            }
        }
    }

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    //if (collision.contacts[0].normal.x >= 1 - 0.03f && rb.velocity == Vector2.zero) //Fix bug khong the di chuyen khi ket vao goc
    //    //{
    //    //    rb.velocity = new Vector2(1f, 3f);
    //    //}
    //    //if (collision.contacts[0].normal.x <= -1 + 0.03f && rb.velocity == Vector2.zero)
    //    //{
    //    //    rb.velocity = new Vector2(-1f, 3f);
    //    //}
    //}

    void CanMove()
    {
        canMove = true;
    }

    void Die()
    {
        CameraController.Shake();
        GameObject dieObject;
        dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
        Destroy(dieObject, 1);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        Debug.Log("reload level");
    }
}

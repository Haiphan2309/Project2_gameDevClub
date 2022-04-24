using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    private AnimationScript anim;
    private Collision coll;
    private GameObject dieObj, jumpDust, gameController;

    public float speed = 10;
    public float jumpForce = 10;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 7;
    public float dashTime = 0.5f;
    private Vector2 dashdir;

    public bool wallJumped;
    public bool canMove=true;
    public bool onGround;
    public bool isPressingKey;
    public bool wallSlide;
    public bool isDashing;

    private bool groundTouch;
    private bool hasDashed=false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
        coll = GetComponent<Collision>();

        gameController = GameObject.FindGameObjectWithTag("GameController");
    }


    void Update()
    {
        isPressingKey = false;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);
        
        if (dir != Vector2.zero)
        {
            isPressingKey = true;
        }
        
        Walk(dir);
        anim.SetHorizontalMovement(x,y,rb.velocity.y);
        
        if (coll.onWall && !coll.onGround)
        {
            wallSlide = true;
        }
        else
        {
            wallSlide = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            anim.SetTrigger("jump");

            if(coll.onGround)
            {
                Jump(Vector2.up,false);
            }

            if(wallSlide)
            {
                WallJump();
            }
        }


        if (wallSlide)
        {
            if (canMove)
            {
                bool pushingWall = false;
                if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
                {
                    pushingWall = true;
                }
                float push = pushingWall ? 0 : rb.velocity.x;

                rb.velocity = new Vector2(push, Mathf.Clamp(rb.velocity.y, -slideSpeed, float.MaxValue));
            }  
        }

        DashInit(xRaw, yRaw);
        Dash();

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
            groundTouch = true;
        }

        if (!coll.onGround && groundTouch)
        {
            groundTouch = false;
        }

    }

    private void GroundTouch()
    {
        hasDashed = false;
    }

    private void Dash()
    {
        if (isDashing)
        {
            rb.velocity = dashdir.normalized * dashSpeed;
        }
    }

    private void DashInit(float x, float y)
    {
        //Can 1 cai shake o day

        if(Input.GetButtonDown("Dash") && !hasDashed)
        {
            isDashing = true;
            hasDashed = true;
            rb.velocity = Vector2.zero;
            dashdir = new Vector2(x, y);
            if(dashdir == Vector2.zero)
            {
                dashdir = new Vector2(transform.localScale.x, 0);
            }
        
            StartCoroutine(StopDashing());
        }
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void Jump(Vector2 dir, bool push)
    { 

        onGround = false;
        if (push)
        {
            rb.velocity = new Vector2(-rb.velocity.x, 0);
            rb.velocity += dir * jumpForce;
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += dir * jumpForce;
        }
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

    IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashTime);
        if (coll.onGround)
            hasDashed = false;
        isDashing = false;
        rb.velocity = Vector2.zero;
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void Die()
    {
        CameraController.Shake();
        GameObject dieObject;
        dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
        Destroy(dieObject, 1);

        Destroy(gameObject,0.5f);
    }

    private void OnDestroy()
    {
        Debug.Log("reload level");
        gameController.GetComponent<GameController>().ReStartLevel();
    }

    private void WallJump()
    {
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump(Vector2.up/1.5f + wallDir/1.5f,false);

        wallJumped = true;
    }
}

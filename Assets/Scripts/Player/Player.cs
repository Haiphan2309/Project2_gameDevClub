using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    private AnimationScript anim;
    [HideInInspector]
    private Collision coll;
    private GameObject gameController;

    public GameObject dieObj, jumpDust, afterImage; //Chet, bui, du anh
    public GameObject ghost, player;
    private GameObject ghostclone;

    public float speed = 10;
    public float jumpForce = 10;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 7;
    public float dashTime = 0.5f;
    public float GhostBack = 6f;
    private float GB;
    public float GhostCD = 1f, ghostTimeRemain = 0;
    private float GCD;
    private Vector2 dashdir;

    public bool Gcount;
    public bool wallJumped;
    public bool canMove=true;
    public bool onGround;
    public bool isPressingKey;
    public bool wallSlide;
    public bool isDashing;
    public bool isGhost=false;


    private bool groundTouch;
    private bool hasDashed=false;

    public ParticleSystem obtainEffect;

    AudioSource music;
    public AudioClip jumpClip, dashClip, dieClip, landClip, hitEnemyClip;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
        coll = GetComponent<Collision>();
        music = GetComponent<AudioSource>();

        Gcount = false;
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

        if (isGhost) ghostTimeRemain = GB - Time.time;

        if ((Input.GetKeyDown("z") || (GB <= Time.time && isGhost)) && (GCD <= Time.time))
        {
            music.clip = hitEnemyClip;
            music.Play();
            if (isGhost)
            {
                isGhost = false;
                canMove = true;
                GCD = Time.time + GhostCD;
                Destroy(ghostclone);
                CameraController.ZoomIn();
            }
            else
            {
                GB = Time.time + GhostBack;
                isGhost = true;
                canMove = false;
                rb.velocity = Vector2.zero;
                ghostclone = Instantiate(ghost, player.transform);
                CameraController.ZoomOut();
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
        music.clip = landClip;
        music.Play();
        hasDashed = false;
        wallJumped = false;
    }

    private void Dash()
    {
        if (isDashing && canMove)
        {
            rb.velocity = dashdir.normalized * dashSpeed;
        }
    }

    void DoAfterImage() //Tao ra cac du anh
    {
        Instantiate(afterImage, transform.position, Quaternion.identity);
    }

    private void DashInit(float x, float y)
    {
        if (Input.GetButtonDown("Dash") && !hasDashed && !isGhost)
        {
            if (x == 0 && y == 0) x = 1; //dash ve phia ben phai neu khong nhan gi

            //Hieu ung Shake + bui luc nhay
            StartCoroutine(GroundDash());
            CameraController.LightShake();
            GameObject jumpDustObj = Instantiate(jumpDust, transform.position - new Vector3(0, 0.25f, 0), Quaternion.identity);
            Destroy(jumpDustObj, 1);

            music.clip = dashClip;
            music.Play();

            Invoke("DoAfterImage",0);
            Invoke("DoAfterImage", 0.05f);
            Invoke("DoAfterImage", 0.1f); 
            Invoke("DoAfterImage", 0.15f);

            isDashing = true;
            hasDashed = true;
            rb.velocity = Vector2.zero;
            dashdir = new Vector2(x, y);
        
            StartCoroutine(StopDashing());
        }
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
        {
            GetComponent<Collision>().onWalk = false;
            return;
        }

        if (dir.x >= -0.1f && dir.x <= 0.1f)
        {
            GetComponent<Collision>().onWalk = false;
        }
        else
        {
            GetComponent<Collision>().onWalk = true;
        }

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
        if (isGhost)
            return;

        music.clip = jumpClip;
        music.Play();

        onGround = false;
        if (push)
        {
            rb.velocity = new Vector2(0, 0);
            rb.velocity += dir * jumpForce;
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.velocity += dir * jumpForce;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bat")
        {
            if (transform.position.y - collision.transform.position.y > 0.4f)
            {
                canMove = false;
                rb.velocity = new Vector2(rb.velocity.x, 6);
                collision.gameObject.GetComponent<Enemy>().GetHit();
                Invoke("CanMove", 0.3f);

                music.clip = hitEnemyClip;
                music.Play();
            }
            else Die();
        }
        if (collision.gameObject.tag == "UndefeatEnemy") Die();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.contacts[0].normal.y);
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "UndefeatEnemy")
        {
            //canMove = false;
            rb.velocity = new Vector2(collision.contacts[0].normal.x, collision.contacts[0].normal.y) * 6;
            if (collision.contacts[0].normal.y >= 1 - 0.03f && collision.gameObject.tag == "Enemy")
            {
                music.clip = hitEnemyClip;
                music.Play();
                collision.gameObject.GetComponent<Enemy>().GetHit();

                //Giong GroundTouch nhung ko co am thanh landClip
                hasDashed = false;
                wallJumped = false;
                //GroundTouch();
                //Invoke("CanMove", 0.3f);
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

    void CanMove()
    {
        canMove = true;
    }

    IEnumerator StopDashing()
    {
        if (coll.onGround)
            hasDashed = false;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        rb.velocity = Vector2.zero;
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    public void Die()
    {
        CameraController.Shake();
        GameObject dieObject;
        dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
        Destroy(dieObject, 1);
        Instantiate(obtainEffect, transform.position, Quaternion.identity);

        music.clip = dieClip;
        music.Play();

        transform.localScale = new Vector3(0,0,0); //xoa hinh anh Player luc chet
        Invoke("Restart",1);
    }

    void Restart()
    {
        Debug.Log("reload level");
        gameController.GetComponent<GameController>().ReStartLevel();
    }

    private void WallJump()
    {
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.2f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump(Vector2.up/1f + wallDir/1.5f, true);
        wallJumped = true;

        //Hieu ung bui:
        if (coll.onRightWall)
        {
            GameObject jumpDustObj = Instantiate(jumpDust, transform.position + new Vector3(0.2f, 0, 0), Quaternion.Euler(0, 0, 90));
            Destroy(jumpDustObj, 1);
        }
        else
        {
            GameObject jumpDustObj = Instantiate(jumpDust, transform.position - new Vector3(0.2f, 0, 0), Quaternion.Euler(0, 0, -90));
            Destroy(jumpDustObj, 1);
        }
        
    }
}

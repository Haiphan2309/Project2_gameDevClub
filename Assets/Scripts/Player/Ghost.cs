using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody2D rb;
    private GAnimationScript anim;
    private Collision coll;
    public GameObject visual;

    public float speed = 10;

    public bool canMove = true;
    public bool isPressingKey;

    public GameObject dieObj;
    public ParticleSystem obtainEffect;

    bool isBlinking = false;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<GAnimationScript>();
        player = GameObject.FindGameObjectWithTag("Player");
        
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

        if (player.GetComponent<Player>().ghostTimeRemain <= 3 && isBlinking == false)
        {
            isBlinking = true;
            anim.Blinking();
        }
    }

    private void Fly(Vector2 dir)
    {
        if (!canMove)
            return;

        rb.velocity = new Vector2(dir.x * speed, dir.y * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bat")
        {
            Die();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.contacts[0].normal.y);
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "UndefeatEnemy")
        {
            Die();
        }
    }

    void Die()
    {
        player.GetComponent<Player>().Die();
        Destroy(gameObject);
              
    }

    private void OnDestroy()
    {
        GameObject dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
        Destroy(dieObject, 1);
        Instantiate(obtainEffect, transform.position, Quaternion.identity);
    }
}

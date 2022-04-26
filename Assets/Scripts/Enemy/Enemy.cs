using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP;
    protected Rigidbody2D rigi;
    protected SpriteRenderer sprRen;
    protected Animator anim;
    protected Collider2D coli;

    protected GameObject originPlayer, player;
    public GameObject dieObj;
    protected bool isDie = false;
    void Awake()
    {
        rigi = gameObject.GetComponent<Rigidbody2D>();
        sprRen = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        coli = gameObject.GetComponent<Collider2D>();

        originPlayer = GameObject.FindGameObjectWithTag("Player");
        player = originPlayer;
    }

    private void LateUpdate()
    {
        if (originPlayer.GetComponent<Player>().isGhost == true)
        {
            targetGhost();
        }
        else
        {
            targetPlayer();
        }
    }
    public void GetHit()
    {
        HP--;
        if (HP>=1) anim.Play("GetHit");
    }
    public void Die()
    {
        //Debug.Log(gameObject.name + " die");
        isDie = true;
        GameObject dieObject;
        dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
        Destroy(dieObject, 1);

        anim.speed = 0;
        rigi.gravityScale = 1;
        sprRen.flipY = true;
        coli.isTrigger = true;
        Destroy(gameObject, 5);
    }

    public void targetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void targetGhost()
    {
        player = GameObject.FindGameObjectWithTag("Ghost");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.contacts[0].normal.x >= 1 - 0.03f && rigi.velocity == Vector2.zero) //Fix bug khong the di chuyen khi ket vao goc
        {
            rigi.velocity = new Vector2(1f,3f);
        }
        if (collision.contacts[0].normal.x <= -1 + 0.03f && rigi.velocity == Vector2.zero)
        {
            rigi.velocity = new Vector2(-1f, 3f);
        }
    }
}

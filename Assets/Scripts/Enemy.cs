using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP;
    protected Rigidbody2D rigi;
    protected SpriteRenderer sprRen;
    protected Animator anim;
    Collider2D coli;

    protected GameObject player;
    public GameObject dieObj;
    protected bool isDie = false;
    void Awake()
    {
        rigi = gameObject.GetComponent<Rigidbody2D>();
        sprRen = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        coli = gameObject.GetComponent<Collider2D>();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Die()
    {
        //Debug.Log(gameObject.name + " die");
        isDie = true;
        GameObject dieObject;
        dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
        Destroy(dieObject, 1);

        anim.speed = 0;
        sprRen.flipY = true;
        coli.isTrigger = true;
        Destroy(gameObject, 5);
    }
}

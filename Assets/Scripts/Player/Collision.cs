using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask interactives;

    public bool onGround;
    public bool onWall;
    public bool onRightWall;
    public bool onLeftWall;
    public bool onWalk;

    public ParticleSystem dustEffect;
    ParticleSystem par;

    public BoxCollider2D coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        par = Instantiate(dustEffect, transform.position - new Vector3(0, 0.4f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

        if (onWalk)
        {
            if (par.loop == false) par.Play();
            par.loop = true;
        }
        else par.loop = false;
        
        par.transform.position = transform.position - new Vector3(0,0.4f,0);

        onGround = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, groundLayer)
            || Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, interactives);
        onRightWall = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, .1f, groundLayer);
        onLeftWall = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, .1f, groundLayer);
        onWall = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.right, .1f, groundLayer)
            || Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.left, .1f, groundLayer);
    }
}

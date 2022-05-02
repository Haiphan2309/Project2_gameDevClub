using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask interactives;

    public Vector3 bottom = new Vector3(0,-0.5f,0);
    public Vector3 right = new Vector3(0.4f, 0, 0);
    public Vector3 left = new Vector3(-0.4f, 0, 0);

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

        if (onWalk && onGround)
        {
            par.transform.position = transform.position - new Vector3(0, 0.4f, 0);
            if (par.loop == false) par.Play();
            par.loop = true;
        }
        else if (onRightWall)
        {
            par.transform.position = transform.position + new Vector3(0.4f, 0, 0);
            if (par.loop == false) par.Play();
            par.loop = true;
        }
        else if (onLeftWall)
        {
            par.transform.position = transform.position - new Vector3(0.4f, 0, 0);
            if (par.loop == false) par.Play();
            par.loop = true;
        }
        else par.loop = false;

        onGround = Physics2D.OverlapCircle(transform.position + bottom,0.1f,groundLayer) ||
            Physics2D.OverlapCircle(transform.position + bottom, 0.1f, interactives);
        onRightWall = Physics2D.OverlapCircle(transform.position + right, 0.1f, groundLayer);
        onLeftWall = Physics2D.OverlapCircle(transform.position + left, 0.1f, groundLayer);
        onWall = onRightWall || onLeftWall;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + bottom, 0.1f);
        Gizmos.DrawWireSphere(transform.position + right, 0.1f);
        Gizmos.DrawWireSphere(transform.position + left, 0.1f);
    }
}

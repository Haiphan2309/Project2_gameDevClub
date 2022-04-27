using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinMon : Enemy
{
    Vector3 moveVec;
    public float speed;
    public GameObject bullet;
    bool isCanMove = true;
    // Start is called before the first frame update
    void Start()
    {
        if (player!=null)
            moveVec = (player.transform.position - transform.position).normalized;
        //Invoke("Shot", 5);
        InvokeRepeating("Shot", 5, 5);
    }

    private void Update()
    {
        //if (isDie == false)
        //{
        //    if (HP <= 0) Die();
        //}
        if (moveVec.x > 0) sprRen.flipX = true;
        else sprRen.flipX = false;
    }
    void FixedUpdate()
    {
        if (isDie == false && isCanMove == true)
        {
            if (player!=null)
                moveVec = (player.transform.position - transform.position).normalized;
            rigi.velocity = moveVec * speed * 100 * Time.fixedDeltaTime;
        }
    }

    void Shot()
    {
        if (isDie == false && player!=null)
        {
            rigi.velocity = Vector2.zero;
            isCanMove = false;
            anim.Play("Shot");
            Instantiate(bullet, transform.position, Quaternion.identity);
            Invoke("CanMove", 0.5f);
            //Invoke("Shot", 5);
        }
    }
    void CanMove()
    {
        isCanMove = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinMon : Enemy
{
    Vector3 moveVec;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        moveVec = (player.transform.position - transform.position).normalized;
    }

    private void Update()
    {
        if (isDie == false)
        {
            if (HP <= 0) Die();
        }
        if (moveVec.x > 0) sprRen.flipX = true;
        else sprRen.flipX = false;
    }
    void FixedUpdate()
    {
        if (isDie == false)
        {
            moveVec = (player.transform.position - transform.position).normalized;
            rigi.velocity = moveVec * speed * 100 * Time.fixedDeltaTime;
        }
    }
}

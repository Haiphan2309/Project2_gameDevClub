using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    bool isGround;
    public bool isMoveLeft = true;
    public float speed;
    public GameObject jumpDust;
    // Start is called before the first frame update
    void Start()
    {
        speed *= 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie == false)
        {
            if (HP <= 0) Die();
            if (isGround) Invoke("Jump", 0.3f);
        }
    }

    void Jump()
    {
        Debug.Log("Jump");
        isGround = false;
        if (isMoveLeft)
            rigi.velocity = new Vector3(-speed * Time.fixedDeltaTime, speed * Time.fixedDeltaTime, 0); 
        else
            rigi.velocity = new Vector3(speed * Time.fixedDeltaTime, speed * Time.fixedDeltaTime, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.contacts[0].normal.y);

        if (other.contacts[0].normal.x == 1 || other.contacts[0].normal.x == -1)
        {
            rigi.velocity = new Vector2(other.contacts[0].normal.x, other.contacts[0].normal.y) * 3;
            isMoveLeft = !isMoveLeft;
            if (isMoveLeft) sprRen.flipX = false;
            else sprRen.flipX = true;
        }
        if (other.contacts[0].normal.y == 1)
        {
            GameObject jumpDustObj = Instantiate(jumpDust, transform.position - new Vector3(0,0.4f,0), Quaternion.identity);
            Destroy(jumpDustObj, 1);
            anim.Play("Move");
            isGround = true;
        }
    }
}

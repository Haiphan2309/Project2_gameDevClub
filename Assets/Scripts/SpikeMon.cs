using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMon : Enemy
{
    [SerializeField]
    private int direct; //cac huong di theo chieu kim dong ho tu 0 den 7
    public float speed;
    float distance = 25;

    public Sprite look0, look1, look2, look3, look4, look5, look6, look7;
    Sprite defaultSprite;
    // Start is called before the first frame update
    void Start()
    {
        defaultSprite = sprRen.sprite;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Vector3.SqrMagnitude(player.transform.position - transform.position) <= distance) Look();
        else sprRen.sprite = defaultSprite;
    }
    void FixedUpdate()
    {
        if (direct == 0) rigi.velocity = new Vector3(0, speed * Time.fixedDeltaTime, 0);
        if (direct == 1) rigi.velocity = new Vector3(speed*Time.fixedDeltaTime, speed * Time.fixedDeltaTime, 0);
        if (direct == 2) rigi.velocity = new Vector3(speed * Time.fixedDeltaTime, 0, 0);
        if (direct == 3) rigi.velocity = new Vector3(speed * Time.fixedDeltaTime, -speed * Time.fixedDeltaTime, 0);
        if (direct == 4) rigi.velocity = new Vector3(0, -speed * Time.fixedDeltaTime, 0);
        if (direct == 5) rigi.velocity = new Vector3(-speed * Time.fixedDeltaTime, -speed * Time.fixedDeltaTime, 0);
        if (direct == 6) rigi.velocity = new Vector3(-speed * Time.fixedDeltaTime, 0, 0);
        if (direct == 7) rigi.velocity = new Vector3(-speed * Time.fixedDeltaTime, speed * Time.fixedDeltaTime, 0);
    }

    void Look()
    {
        float angle = Vector2.Angle(player.transform.position - transform.position,Vector2.up);
        if ((player.transform.position - transform.position).x >= 0)
        {
            if (0 < angle && angle <= 22.5f) sprRen.sprite = look0;
            else if (angle <= 67.5f) sprRen.sprite = look1;
            else if (angle <= 112.5f) sprRen.sprite = look2;
            else if (angle <= 157.5f) sprRen.sprite = look3;
            else sprRen.sprite = look4;
        }
        else
        {
            if (0 < angle && angle <= 22.5f) sprRen.sprite = look0;
            else if (angle <= 67.5f) sprRen.sprite = look7;
            else if (angle <= 112.5f) sprRen.sprite = look6;
            else if (angle <= 157.5f) sprRen.sprite = look5;
            else sprRen.sprite = look4;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        direct = (direct + 4) % 8;
    }
}

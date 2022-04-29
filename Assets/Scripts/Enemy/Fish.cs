using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    GameObject obj;
    Animator anim;
    Rigidbody2D rigi;
    SpriteRenderer sprRen;

    public ParticleSystem parSys;

    public float speed;
    public int HP;

    public float timeDestroy;
    private float timeStartDestroy = -100;
    private const float timeStopMove = 5;
    //private float timeStartDelay = -100;
    //private float timeDelay = 50; // khoang thoi gian dung chuyen dong sau khi dung trung player

    // Start is called before the first frame update

    private void Awake()
    {
        obj = gameObject;
        anim = obj.GetComponent<Animator>();
        rigi = obj.GetComponent<Rigidbody2D>();
        sprRen = obj.GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        enabled = false;
    }

    bool firstTimeparSys = true; //dung de parSys chi hien thi dung 1 lan
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (HP <= 0)
        {
            if (Time.fixedTime - timeStartDestroy > timeStopMove*Time.fixedDeltaTime)
            {
                rigi.velocity = new Vector3(0,0);
                if (firstTimeparSys) Instantiate(parSys, obj.transform.position, Quaternion.identity);
                firstTimeparSys = false;
            }
            Destroy(obj, timeDestroy * Time.fixedDeltaTime);
        }
        else //if (Time.fixedTime - timeStartDelay > timeDelay * Time.fixedDeltaTime)
        {
            obj.transform.Translate(new Vector3(-speed * Time.fixedDeltaTime, 0, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet") { HP--; anim.Play("GetHit"); }
        if (other.gameObject.tag == "BigBullet") { HP -= 3; anim.Play("GetHit"); }
        if (HP==0)
        {
            timeStartDestroy = Time.fixedTime;
            obj.tag = "Untagged";
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //timeStartDelay = Time.fixedTime;
        }
        else
        {
            sprRen.flipX = !sprRen.flipX;
            speed = -speed;
        }
    }
    void OnBecameVisible()
    {
        //Debug.Log("in");
        enabled = true;
    }
    void OnBecameInvisible()
    {
        //Debug.Log("out");
        enabled = false;
    }
    private void OnDestroy()
    {
        //Instantiate(parSys, obj.transform.position, Quaternion.identity);
    }
}

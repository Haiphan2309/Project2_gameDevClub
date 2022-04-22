using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject obj;
    static Animator anim;
    private Transform player;
    public float maxX, minX, maxY, minY;
    public float moveSpeed;
    //bool isScoll = false;

    Rigidbody2D rigi;
    // Start is called before the first frame update
    void Awake()
    {
        obj = gameObject;
        anim = gameObject.GetComponent<Animator>();
        rigi = obj.GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null /*&& isScoll == false*/)
        { 
            // Tao camera di chuyen theo player
            Vector3 target = obj.transform.position;
            target.x = player.position.x;
            target.y = player.position.y;

            if (target.x < minX) target.x = minX;
            if (target.x > maxX) target.x = maxX;
            if (target.y < minY) target.y = minY;
            if (target.y > maxY) target.y = maxY;

            obj.transform.position = Vector3.Lerp(obj.transform.position, target, moveSpeed * Time.deltaTime);
        }
        
    }

    private void FixedUpdate()
    {
        //if (isScoll)
        //{
        //    if (obj.transform.position.x <= maxX)
        //        rigi.velocity = new Vector3(100 * Time.fixedDeltaTime, 0, 0);
        //    else
        //    {
        //        isScoll = false;
        //    }
        //}
    }

    public static void Shake()
    {
        anim.Play("Shake");
    }

    //public void Scoll()
    //{
    //    isScoll = true;
    //}
}

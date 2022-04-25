using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject obj;
    static Animator anim;
    private Transform player;
    public static float maxX = 0, minX = 0, maxY = 0, minY = 0;
    public float moveSpeed;

    const float slideDistance = 20;
    static bool isFollowPlayer = true;
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
        isFollowPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && isFollowPlayer/*&& isScoll == false*/)
        {
            // Tao camera di chuyen theo player
            Vector3 target = obj.transform.position;
            if (player.GetComponent<Player>().isGhost == true)
            {
                GameObject ghost = GameObject.FindGameObjectWithTag("Ghost");
                if (ghost != null)
                {
                    target.x = (player.position.x + ghost.transform.position.x) / 2;
                    target.y = (player.position.y + ghost.transform.position.y) / 2;
                }
            }
            else
            {
                target.x = player.position.x;
                target.y = player.position.y;
            }

            if (target.x < minX) target.x = minX;
            if (target.x > maxX) target.x = maxX;
            if (target.y < minY) target.y = minY;
            if (target.y > maxY) target.y = maxY;

            obj.transform.position = Vector3.Lerp(obj.transform.position, target, moveSpeed * Time.deltaTime);
        }

        if (isFollowPlayer == false) Invoke("FollowPlayer", 1);
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

    public static void Shake() //rung
    {
        anim.Play("Shake");
    }

    public static void LightShake() //rung nhe
    {
        anim.Play("LightShake");
    }

    public static void SlideRight() //cuon phai
    {        
        anim.Play("SlideRight");
        isFollowPlayer = false;
    }

    public static void SlideLeft()
    {
        anim.Play("SlideLeft");
        isFollowPlayer = false;
    }

    public static void ZoomIn()
    {
        anim.Play("ZoomIn");
    }

    public static void ZoomOut()
    {
        anim.Play("ZoomOut");
    }

    void FollowPlayer()
    {
        isFollowPlayer = true;
    }

    //public void Scoll()
    //{
    //    isScoll = true;
    //}
}

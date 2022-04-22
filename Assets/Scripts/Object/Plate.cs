using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    float x0, y0;
    public int direct; // 1: trai -> phai; 2: phai sang trai; 3: duoi len tren; 4: tren xuong duoi 
    public float dx, dy, speed;
    // Start is called before the first frame update
    void Start()
    {
        //toa do tam:
        x0 = transform.position.x;
        y0 = transform.position.y;


    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < x0 - dx) direct = 1;
        if (transform.position.x > x0 + dx) direct = 2;
        if (transform.position.y < y0 - dy) direct = 3;
        if (transform.position.y > y0 + dy) direct = 4;
    }
    private void FixedUpdate()
    {
        if (direct == 1) transform.Translate(speed*Time.fixedDeltaTime, 0, 0);
        if (direct == 2) transform.Translate(-speed*Time.fixedDeltaTime, 0, 0);
        if (direct == 3) transform.Translate(0, speed * Time.fixedDeltaTime, 0);
        if (direct == 4) transform.Translate(0, -speed * Time.fixedDeltaTime, 0);
    }
}

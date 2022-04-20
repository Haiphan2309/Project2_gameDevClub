using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundObject : Enemy
{
    // Start is called before the first frame update
    //public GameObject Center;
    public float R;

    float x0, y0;
    Vector3 centerPos;

    public float speed;

    
    void Start()
    {
        x0 = transform.position.x;
        y0 = transform.position.y;
        centerPos = new Vector3(x0, y0, 0);

        transform.position = new Vector3(R, 0, 0);

        //transform.position = (transform.position - centerPos).normalized*R + centerPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.RotateAround(centerPos,new Vector3(0,0,1),Time.fixedDeltaTime*speed);
        
    }
}

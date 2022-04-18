using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundObject : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Center;
    public float R; 

    public float speed;

    
    void Start()
    {
        transform.position = (transform.position - Center.transform.position).normalized*R + Center.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(Center.transform.position,new Vector3(0,0,1),Time.deltaTime*speed);
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    // Start is called before the first frame update
    public float distance;
    public float speed;
    
    bool isFoundPlayer = false;
    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector3.Distance(transform.position,player.transform.position) <= distance){
            isFoundPlayer = true;
        }
        if(isFoundPlayer){
            transform.position = Vector3.MoveTowards(transform.position,player.transform.position,Time.fixedDeltaTime*speed);
        }
    }
}

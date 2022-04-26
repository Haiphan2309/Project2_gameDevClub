using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBullet : MonoBehaviour
{   
    [SerializeField] float speed;
    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        Canon canon = FindObjectOfType<Canon>();  
        dir = canon.GetDirection();
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(dir*speed*Time.deltaTime);
    }

    
}

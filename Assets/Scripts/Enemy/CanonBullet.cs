using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBullet : Enemy
{   
    [SerializeField] float speed;

    Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        Canon canon = FindObjectOfType<Canon>();  
        dir = canon.GetDirection();
        GameObject dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
        Destroy(dieObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
       transform.Translate(dir*speed*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            GameObject dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
            Destroy(dieObject, 1);
            Destroy(gameObject);
        }
    }

}

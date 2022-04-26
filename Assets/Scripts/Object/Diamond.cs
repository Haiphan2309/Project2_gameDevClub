using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    GameObject player;
    [SerializeField] ParticleSystem obtainEffect;

    //bool isObtain = false;
    // Start is called before the first frame update
    void Start()
    {
        //if (isObtain) Destroy(gameObject);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //isObtain = true;
            Instantiate(obtainEffect, transform.position, Quaternion.identity);
            GameController.point++;
            Destroy(gameObject);
        }
    }
}

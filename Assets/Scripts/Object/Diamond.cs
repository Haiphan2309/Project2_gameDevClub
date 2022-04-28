using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    GameObject player;
    [SerializeField] ParticleSystem obtainEffect;

    public DiamondInfo info = new DiamondInfo();

    //public DiamondInfo 
    // Start is called before the first frame update
    void Start()
    {
        info.m_pos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            info.m_isCollecting = true;
            Instantiate(obtainEffect, transform.position, Quaternion.identity);
            GameController.point++;
            Destroy(gameObject);
        }
    }
}

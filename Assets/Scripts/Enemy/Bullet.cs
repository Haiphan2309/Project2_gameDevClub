using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rigi;
    Collider2D coli;
    public float speed;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<Player>().isGhost) player = GameObject.FindGameObjectWithTag("Ghost");
        rigi = gameObject.GetComponent<Rigidbody2D>();
        coli = gameObject.GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        rigi.velocity = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "UndefeatEnemy") coli.isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}

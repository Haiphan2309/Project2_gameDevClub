using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float HP;
    protected Rigidbody2D rigi;
    protected SpriteRenderer sprRen;
    void Awake()
    {
        rigi = gameObject.GetComponent<Rigidbody2D>();
        sprRen = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0) Die();
    }

    void Die()
    {
        Debug.Log(gameObject + " die");
    }
}

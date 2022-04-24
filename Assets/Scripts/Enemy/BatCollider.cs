using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatCollider : Enemy
{
    public GameObject bat;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0)
        {
            Die();
            Destroy(bat);
        }
        transform.position = bat.transform.position;
    }
}

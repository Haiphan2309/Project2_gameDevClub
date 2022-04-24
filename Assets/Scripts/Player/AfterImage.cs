using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfterImage : MonoBehaviour
{
    public GameObject targetObj;
    // Start is called before the first frame update
    void Start()
    {
        targetObj = GameObject.FindGameObjectWithTag("AnimPlayer");
        GetComponent<SpriteRenderer>().sprite = targetObj.GetComponent<SpriteRenderer>().sprite;
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

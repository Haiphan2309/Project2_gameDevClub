using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignCollider : MonoBehaviour
{
    Animator anim;
    public GameObject signObj;
    // Start is called before the first frame update
    void Start()
    {
        anim = signObj.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("SignAppear");
        if (collision.tag == "Player") anim.Play("Appear");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") anim.Play("Disappear");
    }
}

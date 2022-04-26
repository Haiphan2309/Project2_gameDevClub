using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAnimationScript : MonoBehaviour
{
    private Animator anim;
    private Ghost move;
    private Collision coll;
    [HideInInspector]
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        move = GetComponentInParent<Ghost>();
        coll = GetComponentInParent<Collision>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("canMove", move.canMove);
        anim.SetBool("isPressingKey", move.isPressingKey);
    }

    public void SetHorizontalMovement(float x, float y)
    {
        anim.SetFloat("Xinput", x);
        anim.SetFloat("Yinput", y);
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }

    public void Blinking()
    {
        Color c = sr.material.color;
        c.a = 0.5f;
        sr.material.color = c;
        Invoke("NoBlinking", 0.2f);
    }

    public void NoBlinking()
    {
        Color c = sr.material.color;
        c.a = 1;
        sr.material.color = c;
        Invoke("Blinking", 0.5f);
    }
}

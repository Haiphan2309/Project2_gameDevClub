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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Animator anim;
    private Player move;
    private Collision coll;
    [HideInInspector]
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        move = GetComponentInParent<Player>();
        coll = GetComponentInParent<Collision>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("canMove", move.canMove);
        anim.SetBool("isPressingKey", move.isPressingKey);
        anim.SetBool("onGround", coll.onGround);
        
    }

    public void SetHorizontalMovement(float x, float y, float yVer)
    {
        anim.SetFloat("Xinput", x);
        anim.SetFloat("Yinput", y);
        anim.SetFloat("VerticalY", yVer);
    }

    public void SetTrigger(string trigger)
    {
        anim.SetTrigger(trigger);
    }
}

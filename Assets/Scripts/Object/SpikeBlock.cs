using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBlock : MonoBehaviour
{
    public int direct = 0; // 0: ko quay; 1:nguoc chieu kim dong ho ; 2:cung chieu
    Animator anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (direct == 1) anim.Play("SpikeBlock");
        if (direct == 2) anim.Play("SpikeBlockReverse");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

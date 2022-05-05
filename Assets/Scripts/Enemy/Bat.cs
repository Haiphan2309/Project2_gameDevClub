using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    // Start is called before the first frame update
    [SerializeField] float m_Distance;
    [SerializeField] float m_Speed;

    [SerializeField] float m_FollowTime;
    
    float FollowTime;
    bool m_isFoundPlayer = false;
    
    bool m_isGoingBack = false;
    
    Vector3 m_StartPosition;

    Animator m_animator;
    
    void Start()
    {   m_animator = GetComponent<Animator>();
        m_StartPosition = transform.position;
        FollowTime = m_FollowTime;
        m_isFoundPlayer = false;
        m_isGoingBack = false;
        m_animator.SetBool("isIdle",true);
        m_animator.SetBool("isFlying",false);
    }
    void Update()
    {
        if (isDie == false)
        {
            //if (HP <= 0) Die();

            if (m_isGoingBack)
            {
                BackToStart();
                if (transform.position == m_StartPosition)
                {
                    transform.localScale = new Vector3(1, -1, 1);

                    transform.up = Vector3.up;
                    FollowTime = m_FollowTime;
                    m_isGoingBack = false;
                    m_animator.SetBool("isIdle", true);                
                    m_animator.SetBool("isFlying", false);
                }
            }
            else CheckFoundPlayer();

            if (m_isFoundPlayer)
            {
                MoveToPlayer();
                transform.localScale = new Vector3(1, 1, 1);

                m_animator.SetBool("isIdle", false);
                m_animator.SetBool("isFlying", true);
                FollowTime -= Time.deltaTime;
                if (FollowTime <= 0)
                {
                    m_isFoundPlayer = false;
                    m_isGoingBack = true;
                }
            }
        }

    }

    void MoveToPlayer(){
        if(!m_isFoundPlayer) return;
        transform.position = Vector2.MoveTowards(transform.position,player.transform.position,m_Speed*Time.deltaTime);
        transform.up = player.transform.position - transform.position;
    }

    void BackToStart(){
        transform.position = Vector2.MoveTowards(transform.position,m_StartPosition,m_Speed*Time.deltaTime);
        transform.up = (m_StartPosition - transform.position);
    }

    void CheckFoundPlayer(){
        if(Vector3.Distance(transform.position,player.transform.position) <= m_Distance){
            m_isFoundPlayer = true;
        }   
    }    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_bullet;

    [SerializeField] float m_AttackSpeed;

    [SerializeField] int m_direction;

    [SerializeField] float m_headDistacne;

    // 0 = right , 1 = down , 2 = left , 3 = up

    Animator m_animator;

    GameObject head;

    float m_FireTime;
    float FireTime;

    List<Vector3> dirList = new List<Vector3>(){Vector3.right, Vector3.down , Vector3.left , Vector3.up};
    void Start()
    {
        FireTime = 1.0f / m_FireTime;
        m_animator = GetComponent<Animator>();
        head = GameObject.Find("Head");
        
    }

    // Update is called once per frame
    void Update()
    {
        FireTime -= Time.deltaTime;
        if(FireTime <= 0){
            m_animator.SetBool("isFire",true);
            FireTime = 1.0f / m_FireTime;
        }
    }

    void Fire(){
        float angle = Vector2.SignedAngle(Vector2.right,(head.transform.position - transform.position));
        Instantiate(m_bullet,head.transform.position,Quaternion.Euler(0,0,angle));
        m_animator.SetBool("isFire",false);    
    }

    public Vector3 GetDirection(){
        return dirList[m_direction];
    }

}

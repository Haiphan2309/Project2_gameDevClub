using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject m_bullet;

    [SerializeField] float m_DeltaTimeAttack;

    //[SerializeField] float m_AttackSpeed;

    [SerializeField] int m_direction;
    // 0 = right , 1 = down , 2 = left , 3 = up

    Animator m_animator;

    GameObject head;

    //float m_FireTime;
    //float FireTime;

    List<Vector3> dirList = new List<Vector3>(){Vector3.right, Vector3.down , Vector3.left , Vector3.up};
    void Start()
    {
        m_animator = GetComponent<Animator>();
        head = FindChild("Head");
        InvokeRepeating("SetFire", Time.deltaTime, m_DeltaTimeAttack);
        Debug.Log(gameObject.name + "<- " + head.transform.GetInstanceID());
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(FireTime);
        //FireTime -= Time.deltaTime;
        //if(FireTime <= 0){
        //    m_animator.SetBool("isFire",true);
        //    FireTime = 1.0f / m_FireTime;
        //}
    }

    void SetFire()
    {
        m_animator.Play("fire");
    }
    void Fire(){
        float angle = Vector2.SignedAngle(Vector2.right,(head.transform.position - transform.position));
        //GameObject bullet = Instantiate(m_bullet,head.transform.position,Quaternion.Euler(0,0,angle));
        GameObject bullet = Instantiate(m_bullet,head.transform.position,Quaternion.identity);
        bullet.GetComponent<CanonBullet>().GetCanon(gameObject);
        
    }

    public Vector3 GetDirection(){
        return dirList[m_direction];
    }



    GameObject FindChild(string name){
        return transform.Find(name).transform.gameObject;
    }

}

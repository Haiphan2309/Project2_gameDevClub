using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBullet : Enemy
{   
    [SerializeField] float speed;

    //[SerializeField] GameObject dieObj;

    Vector3 dir;
    Canon canon;
    // Start is called before the first frame update
    void Start()
    {   
        transform.localScale = canon.transform.localScale;
        Debug.Log("bullet of " + canon.name);
        dir = canon.GetDirection();
        GameObject dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
        Destroy(dieObject, 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie == false)
            transform.Translate(dir*speed*Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            GameObject dieObject = Instantiate(dieObj, transform.position, Quaternion.identity);
            Destroy(dieObject, 1);
            Destroy(gameObject);
        }
    }

    public void GetCanon(GameObject canon){
        this.canon = canon.GetComponent<Canon>();
    }

}

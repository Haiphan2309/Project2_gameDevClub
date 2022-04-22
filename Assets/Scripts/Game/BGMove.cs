using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    public float moveSpeed;
    Animator anim;
    GameObject obj;
    private GameObject cameraObj;
    
    public Vector3 oldPos;
    
    private void Awake()
    {
        obj = gameObject;
        anim = obj.GetComponent<Animator>();
        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Start is called before the first frame update
    void Start()
    {
        oldPos = obj.transform.position; //gan vi tri ban dau cho BG
    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log(vxCamera);
        Vector3 target;
        target.z = 0;
        target.x = cameraObj.transform.position.x;
        target.y = cameraObj.transform.position.y;

        Vector3 addPos; //do doi cua BG so voi oldPos
        addPos.z = 0;
        addPos.x = ((target.x - oldPos.x) * moveSpeed);
        addPos.y = (target.y - oldPos.y) * moveSpeed; 

        obj.transform.position = oldPos + addPos;
        
    }
}

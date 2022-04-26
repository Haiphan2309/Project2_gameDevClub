using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeOfRoom : MonoBehaviour
{
    GameObject player;
    public int id;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {

            if (player.transform.position.x > transform.position.x && id == GameController.room + 1) 
            {
                CameraController.SlideRight();
                GameController.room++;
            }
            if (player.transform.position.x < transform.position.x && id == GameController.room) 
            {
                CameraController.SlideLeft();
                GameController.room--;
            }
        }
    }
}

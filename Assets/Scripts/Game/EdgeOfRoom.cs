using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeOfRoom : MonoBehaviour
{
    GameObject player, gameController;
    GameController gameCtrlScr;
    public int leftRoom, rightRoom;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("GameController");
        gameCtrlScr = gameController.GetComponent<GameController>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Player")
        {
            if (player.transform.position.x > transform.position.x) 
            {
                if (leftRoom < rightRoom)
                {
                    if (GameController.room + 1 > rightRoom) return;
                    GameController.room++;
                }
                if (rightRoom < leftRoom)
                {
                    if (GameController.room - 1 < rightRoom) return;
                    GameController.room--;
                }

                gameCtrlScr.UpdateRoom();
                CameraController.SlideRight();

            }
            if (player.transform.position.x < transform.position.x/* && id == GameController.room*/) 
            {               
                if (leftRoom < rightRoom)
                {
                    if (GameController.room - 1 < leftRoom) return;
                    GameController.room--;
                }
                if (rightRoom < leftRoom)
                {
                    if (GameController.room + 1 > leftRoom) return;
                    GameController.room++;
                }

                CameraController.SlideLeft();
                gameCtrlScr.UpdateRoom();
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    Animator anim;
    GameObject gameController;

    [SerializeField] Text diamondPoint;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        gameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Time.timeScale = 0.05f;
            anim.Play("goal");

            CameraController.maxX = float.MaxValue;
            CameraController.maxY = float.MaxValue;
            CameraController.minX = float.MinValue;
            CameraController.minY = float.MinValue;

            CameraController.SuperZoomIn();

            diamondPoint.text = "Your Diamonds: " + GameController.point.ToString();
        }

    }

    public void NextLevel()
    {
        Debug.Log("NextLevel");
        gameController.GetComponent<GameController>().NextLevel();
    }
}

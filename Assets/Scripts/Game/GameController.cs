using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //public GameObject obj;
    GameObject player;
    static public int room = 1, level = 1;

    [SerializeField] Text diamondPoint;
    static public int point;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        if (level == 1)
        {
            if (room == 1)
            {
                CameraController.minX = -20;
                CameraController.maxX = 0;
                player.transform.position = new Vector3(0, 0, 0);
            }
            if (room == 2)
            {
                player.transform.position = new Vector3(11, 1, 0);
                CameraController.minX = 20;
                CameraController.maxX = 20;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (level == 1)
        {
            if (room == 1)
            {
                CameraController.minX = -20;
                CameraController.maxX = 0;
            }
            if (room == 2)
            {
                CameraController.minX = 20;
                CameraController.maxX = 20;
            }
        }

        diamondPoint.text = "Diamond: " + point.ToString();
    }

    public void ReStartLevel()
    {
        SceneManager.LoadScene(0);
    }
}

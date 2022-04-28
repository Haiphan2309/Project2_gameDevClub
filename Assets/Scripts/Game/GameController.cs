using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //public GameObject obj;
    GameObject player;
    static public int room = 0;

    [SerializeField] Text diamondPoint;
    static public int point;

    public float[] minX = new float[5];
    public float[] maxX = new float[5];
    public float[] minY = new float[5];
    public float[] maxY = new float[5];
    public Vector2[] playerPos = new Vector2[5];

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        CameraController.minX = minX[room];
        CameraController.maxX = maxX[room];
        player.transform.position = new Vector3(playerPos[room].x, playerPos[room].y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CameraController.minX = minX[room];
        CameraController.maxX = maxX[room];

        diamondPoint.text = "Diamond: " + point.ToString();
    }

    public void ReStartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

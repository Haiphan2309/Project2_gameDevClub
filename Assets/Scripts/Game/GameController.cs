using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //public GameObject obj;
    GameObject player, cameraObj;
    static public int room = 0, level = 0;

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
        cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateRoom();
        player.transform.position = new Vector3(playerPos[room].x, playerPos[room].y, 0);
        cameraObj.GetComponent<CameraController>().CameraPos = new Vector2(playerPos[room].x-3, playerPos[room].y);

        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        diamondPoint.text = "Diamond: " + point.ToString();

        //Chi dung de debug (nhan phim K de sang room ke tiep ngay lap tuc)
        if (Input.GetKeyDown(KeyCode.K))
        {
            room++;
            ReStartLevel();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("Room: " + room + " level: " + level);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            NextLevel();
        }
    }

    public void UpdateRoom()
    {
        CameraController.minX = minX[room];
        CameraController.maxX = maxX[room];
        CameraController.minY = minY[room];
        CameraController.maxY = maxY[room];
    }

    public void ReStartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        if (level < 3)
        {
            Data.diamonds.Clear();
            room = 0;
            level++;
            Debug.Log(level + 1);
            SceneManager.LoadScene(level+1);           
        }
    }
}

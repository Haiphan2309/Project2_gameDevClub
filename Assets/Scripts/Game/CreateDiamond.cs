using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDiamond : MonoBehaviour
{
    public Vector2[] diamondPos;
    public GameObject diamond;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Count: " + Data.diamonds.Count);
        if (Data.diamonds.Count == 0)
        {
            foreach (Vector2 pos in diamondPos)
            {
                GameObject diamondObj = Instantiate(diamond, pos, Quaternion.identity);
                Data.diamonds.Add(diamondObj.GetComponent<Diamond>().info);
            }
        }
        else
        {
            for (int i=0; i<Data.diamonds.Count; i++)
            {
               // Debug.Log(i + ":" + Data.diamonds[i].m_isCollecting);
                
                if (Data.diamonds[i].m_isCollecting == false)
                {
                    GameObject diamondObj = Instantiate(diamond, Data.diamonds[i].m_pos, Quaternion.identity);
                    diamondObj.GetComponent<Diamond>().info = Data.diamonds[i];
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

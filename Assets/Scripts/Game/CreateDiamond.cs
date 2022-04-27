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
        Debug.Log(Data.diamonds.Count);
        if (Data.diamonds.Count == 0)
        {
            foreach (Vector2 pos in diamondPos)
            {
                GameObject diamondObj = Instantiate(diamond, pos, Quaternion.identity);
                Data.diamonds.Add(diamondObj.GetComponent<Diamond>());
            }
        }
        else
        {
            for (int i=0; i<Data.diamonds.Count; i++)
            {
                Debug.Log(i + ":" + Data.diamonds[i]);
                //Instantiate(Data.diamonds[i], Data.diamonds[i].transform.position, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    public GameObject cubePrefeb0;
    public GameObject cubePrefeb1;
    public GameObject cubePrefeb2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateDeathBox(int num)
    {
        int type = Random.Range(1, 4);
        switch (type)
        {
            case 1:
                Instantiate(cubePrefeb1);
                break;
            case 2:
                Instantiate(cubePrefeb2);
                break;
            default:
                Instantiate(cubePrefeb0);
                break;
        }
        
    }
}

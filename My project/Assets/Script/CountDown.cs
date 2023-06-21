using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text sourceText;


    float time;
    public float timeAll;

    public GameObject menu;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeAll -= Time.fixedDeltaTime;
        // Debug.Log(timeAll);
        sourceText.text = System.TimeSpan.FromSeconds(value: timeAll).ToString(format: @"mm\:ss\:ff");
        if (timeAll <= 0)
        {
            menu.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public float GetTime()
    {
        return timeAll;
    }
}

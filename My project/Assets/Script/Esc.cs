using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class Esc : MonoBehaviour
{
    public GameObject menu;
    public GameObject timeText;

    private bool active = false;
    private CountDown time;

    // Start is called before the first frame update
    void Start()
    {
        menu.SetActive(false);
        time = timeText.GetComponent<CountDown>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time.GetTime() > 0)
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Stop();
            }
        }
    }

    public void Stop()
    {
        active = !active;
        menu.SetActive(active);
        if (active)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}

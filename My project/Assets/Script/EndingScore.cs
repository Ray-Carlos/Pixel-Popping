using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EndingScore : MonoBehaviour
{
    public Text fillText;
    public Text deathText;
    public Text scoreText;

    private float final;
    private float now;
    private bool flag = false;
    public float time;

    public int mul = 2;


    // Start is called before the first frame update
    void Start()
    {
        now = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        final = float.Parse(fillText.text)-mul*float.Parse(deathText.text);
        // Debug.Log(final);
        if (Time.timeScale == 0 && !flag)
        {
            Debug.Log("hi");
            StartCoroutine(WaitAndChangeVariable());
        }
        Debug.Log(flag);
        if (flag)
        {
            now = Mathf.Lerp(now, final, 0.05f);
        }
        
        int roundedFillDegree = Mathf.RoundToInt(now);
        scoreText.text = roundedFillDegree.ToString("D3");
    }

    IEnumerator WaitAndChangeVariable()
    {
        yield return new WaitForSecondsRealtime(time); // 等待一秒
        flag = true;
    }
}

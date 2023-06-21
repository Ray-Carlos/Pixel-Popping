using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    float fillDegree;
    public Text sourceText;
    public GameObject obj;
    private Detection detection;

    // Start is called before the first frame update
    void Start()
    {
        fillDegree = 0.0f;
        detection = obj.GetComponent<Detection>();
        if (detection == null)
        {
            Debug.Log("as"); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        fillDegree = detection.searchCoverage()*100;
        // Debug.Log(fillDegree);
        // if (fillDegree < 10)
        // {
        //     sourceText.text = "00" + Mathf.Round(fillDegree).ToString();
        // }
        // else if (fillDegree < 100)
        // {
        //     sourceText.text = '0' + Mathf.Round(fillDegree).ToString();
        // }
        // else
        // {
        //     sourceText.text = Mathf.Round(fillDegree).ToString();
        // }
        int roundedFillDegree = Mathf.RoundToInt(fillDegree);
        sourceText.text = roundedFillDegree.ToString("D3");
    }
}

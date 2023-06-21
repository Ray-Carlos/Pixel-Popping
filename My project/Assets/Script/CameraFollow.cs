using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target1;
    public Transform target2;

    public Camera camera1;

    public float smoothing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (target1 != null && target2 != null)
        {
            if (transform.position != (target1.position + target2.position)/2)
            {
                Vector3 targetPos = (target1.position + target2.position)/2;
                transform.position = Vector3.Lerp(transform.position, targetPos, smoothing); 
            }

            float distance = Vector3.Distance(target1.position, target2.position);
            if (distance > 7)
            {
                camera1.orthographicSize = Mathf.Lerp(camera1.orthographicSize, Mathf.Pow(distance, 0.7f), smoothing);
            }
            else if (camera1.orthographicSize > 5)
            {
                camera1.orthographicSize = Mathf.Lerp(camera1.orthographicSize, 5, smoothing);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

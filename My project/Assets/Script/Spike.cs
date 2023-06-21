using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public int damage;
    private PlayerHealth playerHealth1;
    private PlayerHealth1 playerHealth2;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerHealth>();
        playerHealth2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerHealth1>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            playerHealth1.DamagePlayer(damage);
        }
        if (other.CompareTag("Player2") && other.GetType().ToString() == "UnityEngine.PolygonCollider2D")
        {
            playerHealth2.DamagePlayer(damage);
        }
    }
}

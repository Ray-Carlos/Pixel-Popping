using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public int healthMax;
    public int health;
    public int death;
    public Text sourceText;

    private Renderer myRender;
    private PolygonCollider2D polygonCollider2D;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private string playerTag;

    private Transform playerTransform;

    private CreateObject createObject;
    private GrabObject grabObject;



    // Start is called before the first frame update
    void Start()
    {
        death =0;
        health = healthMax;
        myRender = GetComponent<Renderer>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        playerTransform = player.GetComponent<Transform>();
        createObject = GameObject.FindGameObjectWithTag(playerTag).GetComponent<CreateObject>();
        grabObject = GameObject.FindGameObjectWithTag(playerTag).GetComponent<GrabObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            grabObject.DamageJoint();
            death += 1;
            sourceText.text = death.ToString("D3");
            
            playerTransform.position = Vector3.zero;
            health = healthMax;
            createObject.CreateDeathBox(1);
            
        }
        BlinkPlayer(2, 0.075f);
        polygonCollider2D.enabled = false;
        StartCoroutine(ShowPlayerHitBox());
    }

    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(1.5f);
        polygonCollider2D.enabled = true;
    }

    void BlinkPlayer(int numBlinks, float timeBlinks)
    {
        StartCoroutine(DoBlinks(numBlinks, timeBlinks));
    }

    IEnumerator DoBlinks(int numBlinks, float timeBlinks)
    {
        for(int i = 0; i < numBlinks * 2; i++)
        {
            myRender.enabled = !myRender.enabled;
            yield return new WaitForSeconds(timeBlinks);
        }
        myRender.enabled = true;
    }
}

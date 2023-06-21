using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demonstrate : MonoBehaviour
{
    // 走路和跳跃
    public int type;
    public float runSpeed;
    public float jumpSpeed;

    public float circle;
    private float time = 0;
    private Rigidbody2D myRigidbody;
    private BoxCollider2D myFeet;

    // 抓取
    public LayerMask grabbable;

    [SerializeField]
    private Transform grabPoint;
    [SerializeField]
    private Transform touchDetect;
    [SerializeField]
    private float touchDistance;
    [SerializeField]
    private PhysicsMaterial2D zeroFriction;
    [SerializeField]
    private PhysicsMaterial2D boxFriction;

    private string targetLayerName1 = "Box";
    private string targetLayerName2 = "GrabbedBox";

    private GameObject grabbedObject;
    private FixedJoint2D fixedJoint; // 添加一个FixedJoint2D变量
    private bool grabFlag = true;

    // 受伤
    private Renderer myRender;
    private int health = 2;
    private Transform playerTransform;
    [SerializeField]
    private GameObject player;
    public GameObject cubePrefeb0;



    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();

        myRigidbody.velocity = new Vector2(0.0f, 0.0f);

        grabbedObject = null;

        myRender = GetComponent<Renderer>();
        playerTransform = player.GetComponent<Transform>();
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.fixedDeltaTime;
        if (time >= circle)
        {
            time -= circle;
        }
        Flip();

        if (type == 0)
        {
            Run(time);
        }
        else if (type == 1)
        {
            Jump();
        }
        else if (type == 2)
        {
            if (time < 0.1f && grabFlag)
            {
                Debug.Log(time);
                Grab(); 
                grabFlag = false;
            }
            if (time > circle - 0.1f)
            {
                grabFlag = true;
            }
        }
        else if (type == 3)
        {
            if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            {
                DamagePlayer();
            }
        }
        else if (type == 4)
        {
            if (time < 0.1f && grabFlag)
            {
                Debug.Log(time);
                Grab(); 
                grabFlag = false;
            }
            if (time > circle - 0.1f)
            {
                grabFlag = true;
            }
        }
    }

    void Run(float time)
    {
        Vector2 playerVel;
        if (time < circle/2)
        {
            playerVel = new Vector2(1.0f * runSpeed, myRigidbody.velocity.y);
        }
        else
        {
            playerVel = new Vector2(-1.0f * runSpeed, myRigidbody.velocity.y);
        }
        myRigidbody.velocity = playerVel;
    }

    void Flip()
    {
        bool playerHasXAxisSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasXAxisSpeed)
        {
            if (myRigidbody.velocity.x > 0.2f)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            if (myRigidbody.velocity.x < -0.2f)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }

    void Jump()
    {
        if (myFeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
            myRigidbody.velocity = Vector2.up * jumpVel;
        }

    }

    void Grab()
    {
        if (!grabbedObject)
        {
            Collider2D hit = Physics2D.OverlapCircle(touchDetect.position, touchDistance, grabbable);

            if (hit)
            {
                grabbedObject = hit.GetComponent<Collider2D>().gameObject;

                grabbedObject.layer = LayerMask.NameToLayer(targetLayerName2);

                grabbedObject.transform.position = grabPoint.position;
                grabbedObject.transform.SetParent(transform);

                // 创建一个新的FixedJoint2D并连接到抓取的对象上
                fixedJoint = gameObject.AddComponent<FixedJoint2D>();
                fixedJoint.connectedBody = grabbedObject.GetComponent<Rigidbody2D>();
                fixedJoint.anchor = transform.InverseTransformPoint(grabPoint.position);

                grabbedObject.GetComponent<Rigidbody2D>().sharedMaterial = zeroFriction;
                
                if (grabbedObject.CompareTag("floating"))
                {
                    grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                }
            }
        }
        else // release currently grabbed object
        {
            if (grabbedObject.CompareTag("floating"))
            {
                grabbedObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }

            grabbedObject.GetComponent<Rigidbody2D>().sharedMaterial = boxFriction;

            grabbedObject.transform.SetParent(null);
            
            Destroy(fixedJoint); // 销毁FixedJoint2D组件以释放物体
            fixedJoint = null;
            
            grabbedObject.layer = LayerMask.NameToLayer(targetLayerName1);

            grabbedObject = null;
        }
    }

    public void DamagePlayer()
    {
        health -= 1;
        if (health <= 0)
        {
            // grabObject.DamageJoint();
            // death += 1;
            // sourceText.text = death.ToString("D3");
            
            playerTransform.position = new Vector3 (2, 1, 0);
            myRigidbody.velocity = new Vector2(0.0f, 0.0f);
            health = 2;
            Instantiate(cubePrefeb0);
            // createObject.CreateDeathBox(1);
            
        }
        else
        {
            BlinkPlayer(2, 0.075f);
            // polygonCollider2D.enabled = false;
            StartCoroutine(ShowPlayerHitBox());
        }

    }

    IEnumerator ShowPlayerHitBox()
    {
        yield return new WaitForSeconds(1.5f);
        // polygonCollider2D.enabled = true;
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

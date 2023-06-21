using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController1 : MonoBehaviour
{
    public float runSpeed;
    public float jumpSpeed;

    float moveDir = 0.0f;

    private Rigidbody2D myRigidbody;
    private BoxCollider2D myFeet;
    
    private bool isGround;
    private bool isTouchingBox;
    private bool isTouchingGrid;

    private int jumpRestTimes;
    private const int jumpMaxTimes = 2;

    // public KeyControl aKey;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myFeet = GetComponent<BoxCollider2D>();
        jumpRestTimes = jumpMaxTimes;
    }

    // Update is called once per frame
    void Update()
    {
        movePress();
        Jump();
    }

    // This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    void FixedUpdate()
    {
        Run();
        
        Flip();
    }

    void movePress()
    {
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)//判断这个按键是否按下。 wasPressedThisFrame 按下
        {
            moveDir = -1.0f;
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)//判断这个按键是否按下。 wasPressedThisFrame 按下
        {
            moveDir = 1.0f;
        }

        if (!Keyboard.current.leftArrowKey.isPressed && !Keyboard.current.rightArrowKey.isPressed)//判断这个按键是否按下。 wasPressedThisFrame 按下
        {
            moveDir = Mathf.Lerp(moveDir, 0.0f, 0.05f);
        }
    }

    void Run()
    {
        Vector2 playerVel = new Vector2(moveDir * runSpeed, myRigidbody.velocity.y);
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

    void CheckGrounded()
    {
        isGround = myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"));
        // Debug.Log(isGround);
    }

    void CheckTouchingBox()
    {
        isTouchingBox = myFeet.IsTouchingLayers(LayerMask.GetMask("Box")) || myFeet.IsTouchingLayers(LayerMask.GetMask("GrabbedBox")) || myFeet.IsTouchingLayers(LayerMask.GetMask("Player"));
        // Debug.Log(isTouchingBox);
    }

    void CheckTouchingGrid()
    {
        isTouchingGrid = myFeet.IsTouchingLayers(LayerMask.GetMask("Grid"));
        // Debug.Log(isTouchingBox);
    }

    void Jump()
    {
        if (Keyboard.current.numpad0Key.wasPressedThisFrame && jumpRestTimes >= 1) 
        {
            jumpRestTimes--;
            Vector2 jumpVel = new Vector2(0.0f, jumpSpeed);
            myRigidbody.velocity = Vector2.up * jumpVel;
            Debug.Log(jumpRestTimes);
        }
        

        CheckGrounded();
        CheckTouchingBox();
        CheckTouchingGrid();
        
        if (isGround || isTouchingBox || isTouchingGrid)
        {
            jumpRestTimes = jumpMaxTimes;
        }
        
    }
    
}

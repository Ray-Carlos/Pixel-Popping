using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GrabObject1 : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        grabbedObject = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.numpadPeriodKey.wasPressedThisFrame)
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

        
    }

    public void DamageJoint()
    {
        if (grabbedObject != null)
        {
            grabbedObject.GetComponent<Rigidbody2D>().sharedMaterial = boxFriction;
            grabbedObject.transform.SetParent(null);
            
            Destroy(fixedJoint);
            fixedJoint = null;

            grabbedObject.layer = LayerMask.NameToLayer(targetLayerName1);
            
            grabbedObject = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.grey;
        Gizmos.DrawWireSphere(touchDetect.position, touchDistance);
    }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.InputSystem;

// public class GrabObject : MonoBehaviour
// {

//     public LayerMask grabbable;

//     [SerializeField]
//     private Transform grabPoint;
//     [SerializeField]
//     private Transform touchDetect;
//     [SerializeField]
//     private float touchDistance;
    
//     private GameObject grabbedObject;
//     private int layerIndexs;
//     // private Transform currentlyGrabbedObject;

//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         if (Keyboard.current.kKey.wasPressedThisFrame)
//         {
//             if (!grabbedObject)
//             {      
//                 Collider2D hit = Physics2D.OverlapCircle(touchDetect.position, touchDistance, grabbable);

//                 if (hit)
//                 {
//                     grabbedObject = hit.GetComponent<Collider2D>().gameObject;
//                     grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
//                     grabbedObject.transform.position = grabPoint.position;
//                     grabbedObject.transform.SetParent(transform);
//                 }     
//             }
//             else // release currently grabbed object
//             {
//                 grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
//                 grabbedObject.transform.SetParent(null);
//                 grabbedObject = null;
//             }               
//         }
    
//     }
// }
        // Collider2D hitInfo = Physics2D.OverlapCircle(rayPoint.position, rayDistance, layerIndexs);

        // if (hitInfo.GetComponent<Collider>() != null || grabbedObject != null)
        // {
        //     if (Keyboard.current.kKey.wasPressedThisFrame && grabbedObject == null)
        //     {
        //         grabbedObject = hitInfo.GetComponent<Collider>().gameObject;
        //         grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
        //         grabbedObject.transform.position = grabPoint.position;
        //         grabbedObject.transform.SetParent(transform);

        //     }
        //     else if (Keyboard.current.kKey.wasPressedThisFrame)
        //     {
        //         grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
        //         grabbedObject.transform.SetParent(null);
        //         grabbedObject = null;

        //     }
        // }
        // Debug.Log(hitInfo == null);
        // Debug.DrawWireSphere(rayPoint.position, rayDistance);


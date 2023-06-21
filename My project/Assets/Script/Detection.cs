using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Detection : MonoBehaviour
{
    public Vector2 boxSize = new Vector2(6.0f, 6.0f); // 搜索矩形的大小
    public LayerMask layerMask; // 特定的layer
    // public Text sourceText;

    private Collider2D[] hitColliders;
    private int objectCount;
    private float averageCoverage;

    // Start is called before the first frame update
    void Start()
    {
        hitColliders = Physics2D.OverlapBoxAll(transform.position, boxSize, 0, layerMask); // 获取范围内的所有碰撞体
        objectCount = hitColliders.Length;
    }

    void Update()
    {
        averageCoverage = 0.0f;
        foreach (Collider2D hitCollider in hitColliders) // 遍历碰撞体
        {
            GameObject hitObject = hitCollider.gameObject; // 获取碰撞体所附着的游戏对象
            FillDegree scriptComponent = hitObject.GetComponent<FillDegree>(); // 获取游戏对象上的脚本组件

            if (scriptComponent != null)
            {
                float returnValue = scriptComponent.SearchCoverage(); // 调用脚本组件中的带返回值的函数
                // Debug.Log("Function return value: " + returnValue);
                averageCoverage += returnValue;
                
            }
        }
        averageCoverage = averageCoverage / objectCount;
        // sourceText.text = Mathf.Round(averageCoverage / 9 * 10).ToString();
        // Debug.Log("Function return value: " + averageCoverage);
    }

    public float searchCoverage()
    {
        return averageCoverage;
    }

    // 在Scene视图中显示搜索矩形
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}

        

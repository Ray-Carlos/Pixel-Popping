using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillDegree : MonoBehaviour
{
    public Vector2 boxSize = new Vector2(1, 1);
    public int gridCount = 4;
    public LayerMask layerMask;
    private int allScrap;
    private int scrapNum;
    private float coverage;


    void Start()
    {
        
        scrapNum = 0;
        coverage = 0;
    }

    private void Update()
    {
        allScrap = gridCount * gridCount;
        CheckForOverlap();
    }

    private void CheckForOverlap()
    {
        int num = 0;
        float cellSize = boxSize.x / gridCount;
        Vector2 halfBoxSize = boxSize / 2;

        for (int i = 0; i < gridCount; i++)
        {
            for (int j = 0; j < gridCount; j++)
            {
                Vector2 cellPosition = new Vector2(transform.position.x - halfBoxSize.x + cellSize / 2 + i * cellSize,
                                                   transform.position.y - halfBoxSize.y + cellSize / 2 + j * cellSize);

                Collider2D collider = Physics2D.OverlapBox(cellPosition, new Vector2(cellSize, cellSize), 0, layerMask);

                if (collider != null)
                {
                    // Debug.Log("物体覆盖在方块(" + i + "," + j + ")上");
                    num++;
                }
            }
        }
        scrapNum = num;
        coverage = scrapNum * 1.0f / allScrap;
        // Debug.Log(coverage);
    }

    public float SearchCoverage()
    {
        return coverage;
    }

    private void OnDrawGizmos()
    {
        float cellSize = boxSize.x / gridCount;
        Vector2 halfBoxSize = boxSize / 2;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < gridCount; i++)
        {
            for (int j = 0; j < gridCount; j++)
            {
                Vector2 cellPosition = new Vector2(transform.position.x - halfBoxSize.x + cellSize / 2 + i * cellSize,
                                                   transform.position.y - halfBoxSize.y + cellSize / 2 + j * cellSize);

                Gizmos.DrawWireCube(cellPosition, new Vector3(cellSize, cellSize, 0));
            }
        }
    }
}

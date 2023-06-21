using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDamage : MonoBehaviour
{
    public float damageThreshold = -50f; // 设置阈值，当y坐标小于此值时应用伤害

    private void Update()
    {
        // 获取游戏对象的位置
        Vector3 position = transform.position;

        // 检查y坐标是否小于阈值
        if (position.y < damageThreshold)
        {
            ApplyDamage();
        }
    }

    private void ApplyDamage()
    {
        Destroy(gameObject);
    }
}
